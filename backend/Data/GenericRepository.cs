using System.Collections.ObjectModel;
using System.Reflection;
using backend.Models.Common;
using LiteDB;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace backend.Data;

public abstract class GenericRepository<T> : IRepository<T> where T : EntityBase
{
    public bool _isLiteDb = false;
    public string _tableName = "";

    public QueryFactory? _compiler = null;

    public UnitOfWorkSqlKata? _unitOfWorkSqlKata = null;

    protected GenericRepository(IUnitOfWork unitOfWork, string tableName = null, QueryFactory compiler = null)
    {
        UnitOfWork = unitOfWork;
        if (tableName != null)
        {
            _tableName = tableName;
        }
        else
        {
            _tableName = typeof(T).Name;
        }
        if (unitOfWork is UnitOfWorkSqlKata unitOfWorkSqlKata)
        {
            _isLiteDb = false;
            _compiler = compiler;
            _unitOfWorkSqlKata = unitOfWorkSqlKata;
        }
        else
        {
            Collection = unitOfWork.LiteDbDatabase.GetCollection<T>();
            _isLiteDb = true;
        }
    }

    public IUnitOfWork UnitOfWork { get; }
    public ILiteCollection<T> Collection { get; }



    public Task<T> AddAsync(T entity)
    {
        if (_isLiteDb)
        {
            var e = Collection.Insert(entity);
            Collection.EnsureIndex(x => x.Id);
            Collection.EnsureIndex(x => x.CreatedAt);
            return Task.FromResult(Collection.FindById(e));
        }
        else
        {
                        IDictionary<string,object>  entityNoID =  new Dictionary<string,object>();
            foreach(var e in entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)){
                if(e.Name != "Id")
                entityNoID[e.Name] = e.GetValue(entity);
            }
            _compiler.Query(_tableName).Insert(new ReadOnlyDictionary<string,object>(entityNoID), _unitOfWorkSqlKata?._transaction);
            return Task.FromResult(entity);
        }
    }

    public Task DeleteAsync(Guid entity)
    {
        if (_isLiteDb)
        {
            Collection.Delete(entity);
            return Task.CompletedTask;
        }
        else
        {
            _compiler.Query(_tableName).Where("Id", entity).Delete(_unitOfWorkSqlKata?._transaction);
            return Task.CompletedTask;
        }
    }

    public Task<IReadOnlyCollection<T>> GetAllAsync()
    {
        if (_isLiteDb)
        {
            return Task.FromResult<IReadOnlyCollection<T>>(new ReadOnlyCollection<T>(Collection.FindAll().ToList()));
        }
        else
        {
            return Task.FromResult<IReadOnlyCollection<T>>(new ReadOnlyCollection<T>(_compiler.Query(_tableName).Get<T>(_unitOfWorkSqlKata?._transaction).ToList()));
        }
    }

    public Task<T> GetByIdAsync(Guid id)
    {
        if (_isLiteDb)
        {
            return Task.FromResult(Collection.FindById(id));
        }
        else
        {
            return Task.FromResult(_compiler.Query(_tableName).Where("Id", id).FirstOrDefault<T>(_unitOfWorkSqlKata?._transaction));
        }
    }

    public Task<IReadOnlyCollection<T>> GetPagedAsync(int page, int pageSize)
    {
        if (_isLiteDb)
        {
            return Task.FromResult<IReadOnlyCollection<T>>(
    new ReadOnlyCollection<T>(Collection.Find(Query.All(), page, pageSize).ToList()));
        }
        else
        {
            return Task.FromResult<IReadOnlyCollection<T>>(
            new ReadOnlyCollection<T>(_compiler.Query(_tableName).Paginate<T>(page, pageSize, _unitOfWorkSqlKata?._transaction).List.ToList()));
        }
    }

    public Task UpdateAsync(T entity)
    {
        if (_isLiteDb)
        {
            Collection.Update(entity);
            return Task.CompletedTask;
        }
        else
        {
                                    IDictionary<string,object>  entityNoID =  new Dictionary<string,object>();
            foreach(var e in entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)){
                if(e.Name != "Id")
                entityNoID[e.Name] = e.GetValue(entity);
            }
            _compiler.Query(_tableName).Where("Id", entity.Id).Update(entityNoID, _unitOfWorkSqlKata?._transaction);
            return Task.CompletedTask;
        }

    }
}