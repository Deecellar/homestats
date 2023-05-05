using backend.Models.Entity;
using SqlKata.Execution;

namespace backend.Data;

public class HouseRepository : GenericRepository<House>, IHouseRepository
{
    public HouseRepository(IUnitOfWork unitOfWork, QueryFactory queryCompiler = null) : base(unitOfWork, "House", queryCompiler )
    {
    }
}