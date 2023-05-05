using LiteDB;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace backend.Data;

public class UnitOfWorkSqlKata : IUnitOfWork
{
    public ILiteDatabase LiteDbDatabase => throw new NotImplementedException();

    public QueryFactory QueryFactory { get;} 
    public UnitOfWorkSqlKata(QueryFactory queryFactory)
    {
        QueryFactory = queryFactory;
    }

    public IHouseRepository HouseRepository => new HouseRepository(this, QueryFactory);

    public ISensorRepository SensorRepository => new SensorRepository(this, QueryFactory);

    public System.Data.IDbTransaction? _transaction = null;
    public Task<bool> CommitTransaction()
    {
        if(_transaction == null)
            return Task.FromResult(false);
        _transaction.Commit();
        return Task.FromResult(true);
    }

    public Task<bool> InitializeTransaction()
    {
        if(_transaction != null)
            return Task.FromResult(false);
        _transaction = QueryFactory.Connection.BeginTransaction();
        return Task.FromResult(true);
    }

    public Task<bool> RollbackTransaction()
    {
        if(_transaction == null)
            return Task.FromResult(false);
        _transaction.Rollback();
        return Task.FromResult(true);
    }
}