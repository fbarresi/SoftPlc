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
	}
}