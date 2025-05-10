using Microsoft.AspNetCore.Mvc;

namespace LasmartTestTask.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
