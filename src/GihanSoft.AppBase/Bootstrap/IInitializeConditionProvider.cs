namespace GihanSoft.AppBase.Bootstrap;

public interface IInitializeConditionProvider
{
    public bool IsFirstRun();

    public bool IsUpdate();
}