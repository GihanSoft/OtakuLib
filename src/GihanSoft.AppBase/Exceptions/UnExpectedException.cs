namespace GihanSoft.ApplicationFrameworkBase.Exceptions;

[System.Serializable]
public class UnExpectedException : System.Exception
{
    public UnExpectedException() { }
    public UnExpectedException(string message) : base(message) { }
    public UnExpectedException(string message, System.Exception inner) : base(message, inner) { }
    protected UnExpectedException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
