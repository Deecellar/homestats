using LiteDB;
using SqlKata;
using SqlKata.Compilers;

namespace backend.Data;

public class UnitOfWorkSqlKata : IUnitOfWork
{
    public ILiteDatabase LiteDbDatabase => throw new NotImplementedException();

    public IHouseRepository HouseRepository => throw new NotImplementedException();

    public ISensorRepository SensorRepository => throw new NotImplementedException();

    public Task<bool> CommitTransaction()
    {
        throw new NotImplementedException();
    }

    public Task<bool> InitializeTransaction()
    {
        throw new NotImplementedException();
    }

    public Task<bool> RollbackTransaction()
    {
        throw new NotImplementedException();
    }
}