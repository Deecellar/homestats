using LiteDB;
using SqlKata;
namespace backend.Data;

public interface IUnitOfWork
{
    
    ILiteDatabase LiteDbDatabase { get; }
    
    Task<bool> InitializeTransaction();
    Task<bool> CommitTransaction();
    Task<bool> RollbackTransaction();

    #region Repositories

    IHouseRepository HouseRepository { get; }
    ISensorRepository SensorRepository { get; }

    #endregion
}