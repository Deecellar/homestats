using LiteDB;

namespace backend.Data;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(ILiteDatabase db)
    {
        Db = db;
    }

    public IHouseRepository HouseRepository => new HouseRepository(this);
    public ISensorRepository SensorRepository => new SensorRepository(this);

    public ILiteDatabase Db { get; }


    public Task<bool> CommitTransaction()
    {
        return Task.FromResult(Db.Commit());
    }

    public Task<bool> InitializeTransaction()
    {
        return Task.FromResult(Db.BeginTrans());
    }

    public Task<bool> RollbackTransaction()
    {
        return Task.FromResult(Db.Rollback());
    }
}