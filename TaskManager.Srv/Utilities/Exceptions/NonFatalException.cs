namespace WhExportShared.Exceptions;

public class NonFatalException : Exception
{
	public NonFatalException(string message) : base(message)
	{

	}

	public NonFatalException(string message, Exception inner) : base(message, inner)
	{

	}
}

