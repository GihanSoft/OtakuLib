using System;

using LiteDB;

using OtakuLib.Logic.Models;

namespace OtakuLib.Logic.Services
{
    public class AppDB : IDisposable
    {
        private bool disposedValue;

        public AppDB(ILiteDatabase liteDatabase)
        {
            Database = liteDatabase;
            Init();
        }

        public ILiteDatabase Database { get; }

        public ILiteCollection<Setting> Settings => Database.GetCollection<Setting>();

        private void Init()
        {

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Database.Checkpoint();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
