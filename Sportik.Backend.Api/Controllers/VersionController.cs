using Microsoft.AspNetCore.Mvc;
using Sportik.Backend.Api.DTOs.Version;
using Sportik.Backend.Api.Helpers;

namespace Sportik.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class VersionController : Controller
{
    [HttpGet]
    public ActionResult<VersionDto> Get()
    {
        string version = VersionHelper.GetVersion();
        return Ok(new VersionDto(version));
    }
}