namespace GihanSoft.AppBase.Exceptions;

[Serializable]
public class UnExpectedException : Exception
{
    public UnExpectedException() { }
    public UnExpectedException(string message) : base(message) { }
    public UnExpectedException(string message, Exception inner) : base(message, inner) { }
    protected UnExpectedException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
