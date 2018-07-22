using System;
using System.Collections.Generic;
using System.Linq;
using SoftPlc.Interfaces;
using SoftPlc.Models;

namespace SoftPlc.Services
{
	public class PlcService : IPlcService, IDisposable
	{
		private readonly S7Server server;
		private readonly bool serverRunning;
		private readonly Dictionary<int, DatablockDescription> datablocks = new Dictionary<int, DatablockDescription>();
		public PlcService()
		{
			Console.WriteLine("Initializing plc service...");

			server = new S7Server();
			var error = server.Start();
			serverRunning = error == 0;
			if (serverRunning) Console.WriteLine("plc server started!");
			else Console.WriteLine($"plc server error {error}");
		}

		private void CheckServerRunning()
		{
			if(!serverRunning) throw new Exception("Plc server is not running");
		}

		private void ReleaseUnmanagedResources()
		{
			Console.WriteLine("Stopping plc server...");
			server.Stop();
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

		public void AddDatablock(int id, int size, byte[] data)
		{
			AddDatablock(id, size);
			UpdateDatablockData(id, data);
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