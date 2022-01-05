#if DEBUG

using System.Runtime.Serialization;

namespace GihanSoft.AppBase.Exceptions;

[Serializable]
public class UnExpectedException : SystemException
{
    public UnExpectedException()
    { }

    public UnExpectedException(string message) : base(message)
    {
    }

    public UnExpectedException(string message, Exception inner) : base(message, inner)
    {
    }

    protected UnExpectedException(
      SerializationInfo info,
      StreamingContext context) : base(info, context) { }
}

#endif