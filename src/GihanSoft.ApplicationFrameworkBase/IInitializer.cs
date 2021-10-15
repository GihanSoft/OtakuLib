namespace GihanSoft.ApplicationFrameworkBase
{
    public interface IInitializer
    {
        void Initialize();
        void FirstRunInitialize();
        void UpdateInitialize();
    }
}
