using backend.Models.Entity;
namespace backend.Services
{
    public interface ISensorService
    {
        public Task<IEnumerable<Sensor>> GetAllSensors(int page, int pageSize);
        public Task<Sensor> GetSensorById(Guid id);
        public Task<Sensor> CreateSensor(Sensor sensor);
        public Task<Sensor> UpdateSensor(Guid id, Sensor sensor);
        public Task DeleteSensor(Guid id);
    }
}
