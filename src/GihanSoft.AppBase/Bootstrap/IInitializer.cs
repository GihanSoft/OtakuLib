namespace GihanSoft.AppBase.Bootstrap;

public interface IInitializer
{
    void FirstRunInitialize();

    void UpdateInitialize();

    void Initialize();

    void LateInitialize();
}
