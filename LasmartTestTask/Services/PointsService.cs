using AutoMapper;
using LasmartTestTask.Abstractions;
using LasmartTestTask.Data;
using LasmartTestTask.Data.Entites;
using LasmartTestTask.Exceptions;
using LasmartTestTask.ViewModels.Request;
using LasmartTestTask.ViewModels.Response;
using Microsoft.EntityFrameworkCore;

namespace LasmartTestTask.Services
{
    public class PointsService : IPointsService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _dbContext;

        public PointsService(
            IMapper mapper,
            ApplicationContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<PointDto> Get(int id)
        {
            var point = await _dbContext.Points.AsNoTracking().Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == id);
            if (point == null)
                throw new ServiceException("Point Not Found", $"Point with id {id} not found.", StatusCodes.Status404NotFound);

            return _mapper.Map<PointDto>(point);
        }

        public async Task<PointDto[]> Get()
        {
            var points = await _dbContext.Points.AsNoTracking().Include(p => p.Comments).ToArrayAsync();
            return _mapper.Map<PointDto[]>(points);
        }

        public async Task<PointDto> Create(CreatePointDto pointDto)
        {
            var point = _mapper.Map<Point>(pointDto);
            await _dbContext.Points.AddAsync(point);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PointDto>(point);
        }

        public async Task<PointDto> Update(int id, UpdatePointDto pointDto)
        {
            var point = await _dbContext.Points.Include(p => p.Comments).FirstOrDefaultAsync(m => m.Id == id);
            if (point == null)
                throw new ServiceException("Point Not Found", $"Point with id {id} not found.", StatusCodes.Status404NotFound);

            point.X = pointDto.X;
            point.Y = pointDto.Y;
            point.ColorHEX = pointDto.ColorHEX;
            point.Radius = pointDto.Radius;
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PointDto>(point);
        }

        public async Task Delete(int id)
        {
            var point = await _dbContext.Points.FirstOrDefaultAsync(m => m.Id == id);
            if (point == null)
                throw new ServiceException("Point Not Found", $"Point with id {id} not found.", StatusCodes.Status404NotFound);

            _dbContext.Points.Remove(point);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PointDto> UpdateComments(int pointId, CreateCommentDto[] commentDto)
        {
            var point = await _dbContext.Points.Include(p => p.Comments).FirstOrDefaultAsync(m => m.Id == pointId);
            if (point == null)
                throw new ServiceException("Point Not Found", $"Point with id {pointId} not found.", StatusCodes.Status404NotFound);

            point.Comments = _mapper.Map<List<Comment>>(commentDto);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PointDto>(point);
        }
    }
}
