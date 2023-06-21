using System.Runtime.Serialization;

namespace Axess.Common.Application.Exceptions;
[Serializable]
public class ForbiddenAccessException : Exception
{
	public ForbiddenAccessException()
	   : base()
	{
	}

	public ForbiddenAccessException(string message)
		: base(message)
	{
	}

	public ForbiddenAccessException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
	// Without this constructor, deserialization will fail
	protected ForbiddenAccessException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
}
