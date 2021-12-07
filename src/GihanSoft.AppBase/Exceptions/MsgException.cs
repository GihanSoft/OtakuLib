using System.Runtime.Serialization;

namespace GihanSoft.AppBase.Exceptions;

/// <summary>
/// For showing error messages to user.
/// </summary>
[Serializable]
public class MsgException : Exception
{
    public MsgException() { }
    public MsgException(string message) : base(message) { }
    public MsgException(string message, Exception inner) : base(message, inner) { }
    protected MsgException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
