using System;
using System.Collections.Generic;
using System.Linq;
using SoftPlc.Interfaces;
using SoftPlc.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json;

namespace SoftPlc.Services
{
	public class PlcService : IPlcService, IDisposable
	{
		private readonly S7Server server;
		private readonly bool serverRunning;
		private readonly Dictionary<int, DatablockDescription> datablocks = new Dictionary<int, DatablockDescription>();
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

		private void SaveDataBlocks()
		{
			var settingsFile = Path.Combine(GetSaveLocation(), "datablocks.json");
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

		private void ReleaseUnmanagedResources()
		{
			Console.WriteLine("Stopping plc server...");
			server.Stop();
            SaveDataBlocks();
		}

		public void Dispose()
		{
			ReleaseUnmanagedResources();
			GC.SuppressFinalize(this);
		}

		~PlcService()
		{
			ReleaseUnmanagedResources();
		}

		public IEnumerable<DatablockDescription> GetDatablocksInfo()
		{
			CheckServerRunning();
			return datablocks.Select(pair => pair.Value);
		}

		public DatablockDescription GetDatablock(int id)
		{
			CheckServerRunning();
			DatablockDescription db;
			var found = datablocks.TryGetValue(id, out db);
			if (found) return db;
			throw new InvalidOperationException("Datablock not found");
		}

		public void AddDatablock(int id, DatablockDescription datablock)
		{
			AddDatablock(id, datablock.Size);
			UpdateDatablockData(id, datablock.Data);
		}

		public void AddDatablock(int id, int size)
		{
			CheckServerRunning();
			if (id < 1) throw new ArgumentException("Invalid id for datablock - id must be > 1", nameof(id));
			if (size < 1) throw new ArgumentException("Invalid size for datablock - size must be > 1", nameof(size));
			if (datablocks.ContainsKey(id)) throw new InvalidOperationException($"A Datablock with id = {id} already exists");
			var db = new DatablockDescription(id, size);
			datablocks[id] = db;
			server.RegisterArea(S7Server.srvAreaDB, id, ref datablocks[id].Data, datablocks[id].Data.Length);
		}

		public void UpdateDatablockData(int id, byte[] data)
		{
			if (data != null && data.Length > datablocks[id].Data.Length) throw new ArgumentException("Too much data as expected", nameof(data));
			if(data != null)
				Array.Copy(data, datablocks[id].Data, data.Length);
		}

		public void RemoveDatablock(int id)
		{
			server.UnregisterArea(S7Server.srvAreaDB, id);
			datablocks.Remove(id);
		}
	}
}