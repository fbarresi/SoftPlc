namespace SoftPlc.Exceptions;

public abstract class DbAccessExceptions:Exception
{
    protected DbAccessExceptions(string? message, Exception? innerException = null) : base(message, innerException)
    {
    }
}

public class DbNotFoundException : DbAccessExceptions
{
    public int DbNo { get; }
    public DbNotFoundException(string? message, int dbNo, Exception? innerException = null) : base(message, innerException)
    {
        DbNo = dbNo;
    }
}