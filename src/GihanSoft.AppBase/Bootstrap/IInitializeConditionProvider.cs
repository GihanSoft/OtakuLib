namespace GihanSoft.AppBase;

public interface IInitializeConditionProvider
{
    public bool IsFirstRun();

    public bool IsUpdate();
}
