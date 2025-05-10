using Microsoft.AspNetCore.Mvc;
using LasmartTestTask.Filters;

namespace LasmartTestTask.Controllers
{
    [TypeFilter<ApiExceptionFilter>]
    public class BaseController : ControllerBase
    {

    }
}
