using LasmartTestTask.ViewModels.Request;
using LasmartTestTask.ViewModels.Response;

namespace LasmartTestTask.Abstractions
{
    public interface IPointsService
    {
        public Task<PointDto> Get(int id);
        public Task<PointDto[]> Get();
        public Task<PointDto> Create(CreatePointDto pointDto);
        public Task<PointDto> Update(int id, UpdatePointDto pointDto);
        public Task Delete(int pointId);

        public Task<PointDto> UpdateComments(int pointId, CreateCommentDto[] commentDto);
    }
}
