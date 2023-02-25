using System;
using backend.Models.Entity;

namespace backend.Services.Implementations
{
    public class SensorService : ISensorService
    {
        public Task<Sensor> CreateSensor(Sensor sensor)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSensor(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Sensor>> GetAllSensors(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Sensor> GetSensorById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Sensor> UpdateSensor(Guid id, Sensor sensor)
        {
            throw new NotImplementedException();
        }
    }
}
