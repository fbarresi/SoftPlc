namespace SoftPlc.Exceptions;

public abstract class DbAccessExceptions : Exception
{
    protected DbAccessExceptions(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}

public class DbOutOfRangeException : DbAccessExceptions
{
    public DbOutOfRangeException(string message, int dbNo, Exception? innerException = null) : base(message, innerException)
    {
        DbNo = dbNo;
    }

    public DbOutOfRangeException(int dbNo) : base($"DB number \"{dbNo}\" is out of range.")
    {
        DbNo = dbNo;
    }

    public int DbNo { get; }

    public static void ThrowIfInvalid(int dbNo)
    {
        if (dbNo < 1)
            throw new DbOutOfRangeException(dbNo);
    }
}

public class DbNotFoundException : DbAccessExceptions
{
    public DbNotFoundException(int dbNo) : base($"DB {dbNo} not found.")
    {
        DbNo = dbNo;
    }

    public DbNotFoundException(string message, int dbNo, Exception? innerException = null) : base(message, innerException)
    {
        DbNo = dbNo;
    }

    public int DbNo { get; }
}