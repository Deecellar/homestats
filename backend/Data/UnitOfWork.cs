using LiteDB;

namespace backend.Data;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(ILiteDatabase db)
    {
        LiteDbDatabase = db;
    }

    public IHouseRepository HouseRepository => new HouseRepository(this, null);
    public ISensorRepository SensorRepository => new SensorRepository(this, null);

    public ILiteDatabase LiteDbDatabase { get; }


    public Task<bool> CommitTransaction()
    {
        return Task.FromResult(LiteDbDatabase.Commit());
    }

    public Task<bool> InitializeTransaction()
    {
        return Task.FromResult(LiteDbDatabase.BeginTrans());
    }

    public Task<bool> RollbackTransaction()
    {
        return Task.FromResult(LiteDbDatabase.Rollback());
    }
}