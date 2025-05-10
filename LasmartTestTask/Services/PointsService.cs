using LasmartTestTask.Abstractions;
using LasmartTestTask.ViewModels.Request;
using LasmartTestTask.ViewModels.Response;

namespace LasmartTestTask.Services
{
    public class PointsService : IPointsService
    {
        public Task<PointDto> Create(CreatePointDto pointDto)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PointDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PointDto[]> Get()
        {
            throw new NotImplementedException();
        }

        public Task<PointDto> Update(UpdatePointDto pointDto)
        {
            throw new NotImplementedException();
        }

        Task<PointDto> IPointsService.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<PointDto[]> IPointsService.Get()
        {
            throw new NotImplementedException();
        }
    }
}
