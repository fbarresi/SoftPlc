using System;
using Newtonsoft.Json;

namespace SoftPlc.Models
{
	public class DatablockDescription
	{
		public byte[] Data;
		public int Id { get; }
		public int Size { get; }

		public DatablockDescription(int id, int size)
		{
			Id = id;
			Size = size;
			Data = new byte[Size];
		}

        [JsonConstructor]public DatablockDescription(int id, int size, byte[] data)
        {
            Id = id;
            Size = size;
            Data = new byte[Size];
			Array.Copy(data, Data, data.Length);
        }
	}
}