using System;
using LiteDB.Engine;
using LiteDB;

namespace backend.Data
{


    public class UnitOfWork : IUnitOfWork
    {
        private ILiteDatabase _db;

        public IHouseRepository HouseRepository => new HouseRepository(this);
        public ISensorRepository SensorRepository => new SensorRepository(this);

        public ILiteDatabase Db { get => _db;}

        public UnitOfWork(ILiteDatabase db)
        {
            _db = db;
        }


        public Task<bool> CommitTransaction()
        {
            return Task.FromResult<bool>(Db.Commit());
        }

        public Task<bool> InitializeTransaction()
        {
            return Task.FromResult<bool>(Db.BeginTrans());
        }

        public Task<bool> RollbackTransaction()
        {
            return Task.FromResult<bool>(Db.Rollback());
        }
    }
}
