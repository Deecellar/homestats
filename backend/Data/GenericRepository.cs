using LiteDB;
using backend.Models.Common;
using System.Collections.ObjectModel;

namespace backend.Data
{
    public abstract class GenericRepository<T> : IRepository<T> where T : EntityBase
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILiteCollection<T> Collection { get; }

        protected GenericRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Collection = unitOfWork.Db.GetCollection<T>();
        }

        public Task<T> AddAsync(T entity)
        {
            var e = Collection.Insert(entity);
            Collection.EnsureIndex(x => x.Id);
            Collection.EnsureIndex(x => x.CreatedAt);
            return Task.FromResult(Collection.FindById(e));
        }

        public Task DeleteAsync(Guid entity)
        {
            Collection.Delete(entity);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return Task.FromResult<IReadOnlyCollection<T>>(new ReadOnlyCollection<T>(Collection.FindAll().ToList()));
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Collection.FindById(id));
        }

        public Task<IReadOnlyCollection<T>> GetPagedAsync(int page, int pageSize)
        {
            return Task.FromResult<IReadOnlyCollection<T>>(new ReadOnlyCollection<T>(Collection.Find(Query.All(), page, pageSize).ToList()));
        }

        public Task UpdateAsync(T entity)
        {
            Collection.Update(entity);
            return Task.CompletedTask;
        }
    }
}
