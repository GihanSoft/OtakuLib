namespace GihanSoft.AppBase.Services;

public interface IDataManager<TData>
    where TData : class
{
    TData Fetch();

    bool TryFetch(out TData? setting);

    void Save(TData setting);

    void Delete();
}