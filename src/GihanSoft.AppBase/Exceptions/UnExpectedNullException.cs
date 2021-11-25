namespace GihanSoft.AppBase.Exceptions;

[System.Serializable]
public class UnExpectedNullException : System.NullReferenceException
{
    public UnExpectedNullException() { }
    public UnExpectedNullException(string message) : base(message) { }
    public UnExpectedNullException(string message, System.Exception inner) : base(message, inner) { }
    protected UnExpectedNullException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
