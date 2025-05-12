using LasmartTestTask.Abstractions;
using LasmartTestTask.ViewModels.Request;
using LasmartTestTask.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;

namespace LasmartTestTask.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PointsController : BaseController
    {
        private readonly IPointsService _pointsService;

        public PointsController(IPointsService pointsService)
        {
            _pointsService = pointsService;
        }

        [HttpGet("{pointId}")]
        public async Task<ActionResult<PointDto>> Get(int pointId)
        {
            var pointDto = await _pointsService.Get(pointId);
            return Ok(pointDto);
        }

        [HttpGet]
        public async Task<ActionResult<PointDto[]>> Get()
        {
            var pointsDto = await _pointsService.Get();
            return Ok(pointsDto);
        }

        [HttpPost]
        public async Task<ActionResult<PointDto[]>> Create(CreatePointDto model)
        {
            var pointDto = await _pointsService.Create(model);
            return Ok(pointDto);
        }

        [HttpPatch("{pointId}/comments")]
        public async Task<ActionResult<PointDto[]>> UpdateComments(int pointId, CreateCommentDto[] model)
        {
            var pointDto = await _pointsService.UpdateComments(pointId, model);
            return Ok(pointDto);
        }


        [HttpPut("{pointId}")]
        public async Task<ActionResult<PointDto[]>> Update(int pointId, UpdatePointDto model)
        {
            Console.WriteLine("I am here");
            var pointDto = await _pointsService.Update(pointId, model);
            return Ok(pointDto);
        }

        [HttpDelete("{pointId}")]
        public async Task<ActionResult> Delete(int pointId)
        {
            await _pointsService.Delete(pointId);
            return Ok();
        }
    }
}
