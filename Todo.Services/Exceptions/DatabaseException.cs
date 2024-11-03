namespace Todo.Busines.Exceptions;

internal class DatabaseException : Exception
{
    public DatabaseException()
    {
    }

    public DatabaseException(string message) : base(message)
    {
    }

    public DatabaseException(string message, Exception inner) : base(message, inner)
    {
    }
}
