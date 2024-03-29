﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using SoftPlc.Interfaces;
using SoftPlc.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json;
using SoftPlc.Exceptions;

namespace SoftPlc.Services
{
	public class PlcService : IPlcService, IDisposable
	{
		private readonly S7Server server;
		private readonly bool serverRunning;
		private readonly string datablockFilename = "datablocks.json";
		private readonly ConcurrentDictionary<int, DatablockDescription> datablocks = new ConcurrentDictionary<int, DatablockDescription>();
		public PlcService(IConfiguration configuration)
		{
			Console.WriteLine("Initializing plc service...");

			server = new S7Server();

			var usedPlcPort = 102;

			if(configuration.GetChildren().Any(item => item.Key.Equals("plcPort")))
			{	
				UInt16 plcPort;
				var parsed = UInt16.TryParse(configuration["plcPort"], out plcPort);
				if(parsed)
					server.SetParam(S7Consts.p_u16_LocalPort, ref plcPort);
				usedPlcPort = plcPort;
			}

			var error = server.Start();
			serverRunning = error == 0;
			if (serverRunning) Console.WriteLine($"plc server started on port {usedPlcPort}!");
			else Console.WriteLine($"plc server error {error}");
            ReadDataBlocks();
		}

		private void CheckServerRunning()
		{
			if(!serverRunning) throw new Exception("Plc server is not running");
		}

		public void SaveDatablocks()
		{
			var settingsFile = Path.Combine(GetSaveLocation(), datablockFilename);
            var json = JsonConvert.SerializeObject(datablocks, Formatting.Indented);
			File.WriteAllText(settingsFile, json);
		}

        private void ReadDataBlocks()
        {
            var settingsFile = Path.Combine(GetSaveLocation(), "datablocks.json");

			try
			{
				if(File.Exists(settingsFile))
				{
                    var json = File.ReadAllText(Path.Combine(GetSaveLocation(),settingsFile));
                	var retrievedDatablock = JsonConvert.DeserializeObject<Dictionary<int, DatablockDescription>>(json);
					foreach(var item in retrievedDatablock)
						AddDatablock(item.Key, item.Value);
				}
			}
			catch(Exception e)
			{
                Console.WriteLine($"Error while deserializing data blocks {e.Message}");
			}

        }

		private string GetSaveLocation()
		{
            try
            {
	            var dataPath = Environment.GetEnvironmentVariable(Constants.EnvironmentVariables.DataPath);
				if(!string.IsNullOrEmpty(dataPath))
					return dataPath;
            }
			catch(Exception e)
			{
                Console.WriteLine($"Error during retrieving env variable DATA_PATH {e.Message}");
			}
			var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
			return Path.GetDirectoryName(location);
		}

		private void ReleaseUnmanagedResourcesAndSaveDatablocks()
		{
			Console.WriteLine("Stopping plc server...");
			server.Stop();
            SaveDatablocks();
		}

		public void Dispose()
		{
			ReleaseUnmanagedResourcesAndSaveDatablocks();
			GC.SuppressFinalize(this);
		}

		~PlcService()
		{
			ReleaseUnmanagedResourcesAndSaveDatablocks();
		}

		public IEnumerable<DatablockDescription> GetDatablocksInfo()
		{
			CheckServerRunning();
			return datablocks.Select(pair => pair.Value);
		}

		public DatablockDescription GetDatablock(int id)
		{
			CheckServerRunning();
            DbOutOfRangeException.ThrowIfInvalid(id);

            if (datablocks.TryGetValue(id, out var db)) 
                return db;
			else
			    throw new DbNotFoundException(id);
		}

		private void AddDatablock(int id, DatablockDescription datablock)
		{
			AddDatablock(id, datablock.Size);
			UpdateDatablockData(id, datablock.Data);
		}

		public void AddDatablock(int id, int size)
		{
			CheckServerRunning();
            DbOutOfRangeException.ThrowIfInvalid(id);
            InvalidDbSizeException.ThrowIfInvalid(size);

			var db = new DatablockDescription(id, size);
			if (!datablocks.TryAdd(id, db))
                throw new DbExistsException(id);

			server.RegisterArea(S7Server.srvAreaDB, id, ref datablocks[id].Data, datablocks[id].Data.Length);
		}

		public void UpdateDatablockData(int id, byte[] data)
		{
            DbOutOfRangeException.ThrowIfInvalid(id);
            
            if (!datablocks.TryGetValue(id, out var db))
                throw new DbNotFoundException(id);

			if (data.Length > db.Data.Length) 
                throw new DateExceedsDbLengthException(id, db.Data.Length, data.Length);

			Array.Copy(data, datablocks[id].Data, data.Length);
		}

		public void RemoveDatablock(int id)
		{
            DbOutOfRangeException.ThrowIfInvalid(id);

            if (datablocks.TryRemove(id, out _))
                server.UnregisterArea(S7Server.srvAreaDB, id);
            else
                throw new DbNotFoundException(id);
        }
	}
}