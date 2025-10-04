using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Sportik.Backend.Application.DTOs.Version;

namespace Sportik.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class VersionController : Controller
{
    [HttpGet]
    public ActionResult<VersionDto> Get()
    {
        string version = Assembly
            .GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion ?? "0.0.0";

        return Ok(new VersionDto(version));
    }
}