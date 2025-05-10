using AutoMapper;
using LasmartTestTask.Data.Entites;
using LasmartTestTask.ViewModels.Request;
using LasmartTestTask.ViewModels.Response;

namespace LasmartTestTask.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreatePointDto, Point>();
            CreateMap<Point, PointDto>();
        }
    }
}
