namespace GihanSoft.AppBase.Exceptions;

[System.Serializable]
public class MsgException : System.Exception
{
    public MsgException() { }
    public MsgException(string message) : base(message) { }
    public MsgException(string message, System.Exception inner) : base(message, inner) { }
    protected MsgException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
