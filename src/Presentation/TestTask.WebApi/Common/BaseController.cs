using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestTask.WebApi.Common;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BaseController : ControllerBase
{
}
