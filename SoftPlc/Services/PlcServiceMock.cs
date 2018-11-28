using System.Collections.Generic;
using SoftPlc.Interfaces;
using SoftPlc.Models;

namespace SoftPlc.Services
{
	public class PlcServiceMock : IPlcService
	{
		public IEnumerable<DatablockDescription> GetDatablocksInfo()
		{
			throw new System.NotImplementedException();
		}

		public DatablockDescription GetDatablock(int id)
		{
			throw new System.NotImplementedException();
		}

		public void AddDatablock(int id, int size, byte[] data)
		{
			throw new System.NotImplementedException();
		}

		public void AddDatablock(int id, int size)
		{
			throw new System.NotImplementedException();
		}

		public void UpdateDatablockData(int id, byte[] data)
		{
			throw new System.NotImplementedException();
		}

		public void RemoveDatablock(int id)
		{
			throw new System.NotImplementedException();
		}

        public void AddDatablock(int id, DatablockDescription datablock)
        {
            throw new System.NotImplementedException();
        }
    }
}