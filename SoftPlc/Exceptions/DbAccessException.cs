namespace SoftPlc.Exceptions;

public abstract class DbAccessException : Exception
{
    protected DbAccessException(string message, Exception? innerException = null) : base(message, innerException)
    {
    }

    public abstract int StatusCode { get; }
    public abstract string Title { get; }
}

public class InvalidDbSizeException : DbAccessException
{
    public InvalidDbSizeException(int size) : base($"DB size must be > 0 but was {size}.")
    {
        Size = size;
    }

    public int Size { get; }

    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string Title => "Invalid DB size";

    public static void ThrowIfInvalid(int size)
    {
        if (size < 1)
            throw new InvalidDbSizeException(size);
    }
}

public class DbOutOfRangeException : DbAccessException
{
    public DbOutOfRangeException(string message, int dbNo, Exception? innerException = null) : base(message, innerException)
    {
        DbNo = dbNo;
    }

    public DbOutOfRangeException(int dbNo) : base($"DB number must be positive, but is {dbNo}.")
    {
        DbNo = dbNo;
    }

    public int DbNo { get; }

    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string Title => "DB out of range";

    public static void ThrowIfInvalid(int dbNo)
    {
        if (dbNo < 1)
            throw new DbOutOfRangeException(dbNo);
    }
}

public class DbNotFoundException : DbAccessException
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

    public override int StatusCode => StatusCodes.Status404NotFound;
    public override string Title => "DB not found";
}

public class DbExistsException : DbAccessException
{
    public DbExistsException(int dbNo) : base($"DB {dbNo} already exists.")
    {
        DbNo = dbNo;
    }

    public int DbNo { get; }

    public override int StatusCode => StatusCodes.Status409Conflict;
    public override string Title => "DB already exists";
}

public class DateExceedsDbLengthException : DbAccessException
{
    public DateExceedsDbLengthException(int dbNo, int dbLength, int dataLength) : base($"Data with {dataLength} bytes exceeds length of DB {dbNo} of {dbLength} bytes.")
    {
        DbNo = dbNo;
        DBLength = dbLength;
        DataLength = dataLength;
    }

    public int DataLength { get; }
    public int DBLength { get; }
    public int DbNo { get; }

    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string Title => "Data exceeds DB length";
}