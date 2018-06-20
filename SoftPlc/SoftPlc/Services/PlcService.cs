using System;
using SoftPlc.Interfaces;

namespace SoftPlc.Services
{
	public class PlcService : IPlcService, IDisposable
	{
		private readonly S7Server server;
		private readonly bool serverRunning;

		public PlcService()
		{
			Console.WriteLine("Initializing plc service...");

			server = new S7Server();
			var error = server.Start();
			serverRunning = error == 0;
			if (serverRunning) Console.WriteLine("plc server started!");
			else Console.WriteLine($"plc server error {error}");
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
	}
}