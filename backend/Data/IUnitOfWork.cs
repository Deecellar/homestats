using LiteDB;

namespace backend.Data
{
    public interface IUnitOfWork
    {
        #region Repositories
        IHouseRepository HouseRepository { get; }
        ISensorRepository SensorRepository { get; }
        #endregion
        ILiteDatabase Db { get; }
         Task<bool> InitializeTransaction();
        Task<bool> CommitTransaction();
        Task<bool> RollbackTransaction();
    }
}
