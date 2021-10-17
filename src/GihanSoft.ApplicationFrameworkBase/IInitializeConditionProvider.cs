namespace GihanSoft.ApplicationFrameworkBase
{
    public interface IInitializeConditionProvider
    {
        public bool IsFirstRun();
        public bool IsUpdate();
    }
}
