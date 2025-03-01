using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v/{version:ApiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
