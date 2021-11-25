namespace GihanSoft.ApplicationFrameworkBase;

public interface IInitializer
{
    void FirstRunInitialize();

    void UpdateInitialize();

    void Initialize();

    void LateInitialize();
}
