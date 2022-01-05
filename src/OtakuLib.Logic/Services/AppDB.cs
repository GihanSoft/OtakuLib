using LiteDB;

using OtakuLib.Logic.Models;

namespace OtakuLib.Logic.Services;

[CLSCompliant(false)]
public class AppDB : IDisposable
{
    private bool disposedValue;

    public AppDB(ILiteDatabase liteDatabase)
    {
        Database = liteDatabase;
        Init();
    }

    public ILiteDatabase Database { get; }

    public ILiteCollection<Data> Datas => Database.GetCollection<Data>();

    private void Init()
    {
        // ToDo: set indexes
        _ = Datas.EnsureIndex(s => s.Id);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposedValue)
        {
            return;
        }

        if (disposing)
        {
            Database.Checkpoint();
        }

        disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}