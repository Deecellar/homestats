using backend.Models.Entity;

namespace backend.Data
{
    public class HouseRepository : GenericRepository<House>, IHouseRepository
    {
        public HouseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
