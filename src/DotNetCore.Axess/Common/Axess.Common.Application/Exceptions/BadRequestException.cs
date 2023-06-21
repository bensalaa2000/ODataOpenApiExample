using System.Runtime.Serialization;

namespace Axess.Common.Application.Exceptions;
[Serializable]
public class BadRequestException : Exception
{
	public BadRequestException()
		: base()
	{
	}

	public BadRequestException(string message)
		: base(message)
	{
	}

	public BadRequestException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
	// Without this constructor, deserialization will fail
	protected BadRequestException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
}
