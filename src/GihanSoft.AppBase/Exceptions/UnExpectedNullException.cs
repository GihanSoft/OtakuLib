namespace GihanSoft.AppBase.Exceptions;

[Serializable]
public class UnExpectedNullException : NullReferenceException
{
    public UnExpectedNullException() { }
    public UnExpectedNullException(string message) : base(message) { }
    public UnExpectedNullException(string message, Exception inner) : base(message, inner) { }
    protected UnExpectedNullException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
