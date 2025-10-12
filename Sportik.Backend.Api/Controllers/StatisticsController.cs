using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sportik.Backend.Api.DTOs.Statistics;
using Sportik.Backend.Api.Mappers.Statistics;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Entities.Statistics;

namespace Sportik.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class StatisticsController : ControllerBase
{
    private readonly IExerciseStatisticsService _exerciseStatisticsService;

    public StatisticsController(IExerciseStatisticsService exerciseStatisticsService)
    {
        _exerciseStatisticsService = exerciseStatisticsService;
    }

    [HttpGet("weekly")]
    public async Task<IActionResult> GetWeekly(CancellationToken cancellationToken)
    {
        if (!User.Identity?.IsAuthenticated ?? true)
        {
            return Unauthorized("User is not authenticated.");
        }

        string? userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userIdValue) || !Guid.TryParse(userIdValue, out Guid userId))
        {
            return Unauthorized("Invalid or missing user ID claim.");
        }

        IEnumerable<WeekStatistics> statistics =
            await _exerciseStatisticsService.GetAllAsync(userId, WeekStatisticsOrder.Descending, cancellationToken);

        return Ok(statistics.Select(WeekStatisticsMapper.ToDto));
    }

    [HttpPost("sets")]
    public async Task<IActionResult> Add(AddSetDto addSetDto, CancellationToken cancellationToken)
    {
        if (!User.Identity?.IsAuthenticated ?? true)
        {
            return Unauthorized("User is not authenticated.");
        }

        string? userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userIdValue) || !Guid.TryParse(userIdValue, out Guid userId))
        {
            return Unauthorized("Invalid or missing user ID claim.");
        }

        Set set = SetMapper.ToDomain(addSetDto);
        Set? addedSet = await _exerciseStatisticsService.AddSetAsync(userId, set, cancellationToken);

        if (addedSet is null)
        {
            return BadRequest("Failed to add the set.");
        }

        return Ok(SetMapper.ToDto(addedSet));
    }
}