namespace SoftPlc.Exceptions;

public abstract class DbAccessExceptions : Exception
{
    protected DbAccessExceptions(string? message, Exception? innerException = null) : base(message, innerException)
    {
    }
}

public class DbNotFoundException : DbAccessExceptions
{
    public DbNotFoundException(int dbNo) : base($"DB {dbNo} not found.")
    {
        DbNo = dbNo;
    }

    public DbNotFoundException(string? message, int dbNo, Exception? innerException = null) : base(message, innerException)
    {
        DbNo = dbNo;
    }

    public int DbNo { get; }
}