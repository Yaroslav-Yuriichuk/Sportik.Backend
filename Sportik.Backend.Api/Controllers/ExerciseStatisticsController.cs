using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sportik.Backend.Api.DTOs.Statistics;
using Sportik.Backend.Api.Mappers.Statistics;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Models.Statistics;

namespace Sportik.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class ExerciseStatisticsController : ControllerBase
{
    private readonly IExerciseStatisticsService _exerciseStatisticsService;

    public ExerciseStatisticsController(IExerciseStatisticsService exerciseStatisticsService)
    {
        _exerciseStatisticsService = exerciseStatisticsService;
    }

    [HttpGet("weekly")]
    public async Task<IActionResult> GetWeekly([FromQuery] WeekStatisticsOrder order, [FromQuery] TimeSpan offset,
        CancellationToken cancellationToken)
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
            await _exerciseStatisticsService.GetAllAsync(userId, order, offset, cancellationToken);

        return Ok(statistics.Select(WeekStatisticsMapper.ToDto));
    }

    [HttpGet("sets")]
    public async Task<IActionResult> GetSets(CancellationToken cancellationToken)
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

        IEnumerable<Set> sets = await _exerciseStatisticsService.GetAllAsync(userId, cancellationToken);

        return Ok(sets.Select(SetMapper.ToDto));
    }

    [HttpPost("sets")]
    public async Task<IActionResult> Add([FromBody] AddSetDto addSetDto, CancellationToken cancellationToken)
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
        Set? addedSet = await _exerciseStatisticsService.AddAsync(userId, set, cancellationToken);

        if (addedSet is null)
        {
            return BadRequest("Failed to add the set.");
        }

        return Ok(SetMapper.ToDto(addedSet));
    }

    [HttpPost("sets/batch")]
    public async Task<IActionResult> AddBatch([FromBody] IEnumerable<AddSetDto> addSetDtos, CancellationToken cancellationToken)
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

        IEnumerable<Set> sets = addSetDtos.Select(SetMapper.ToDomain);
        IEnumerable<Set> addedSets = await _exerciseStatisticsService.AddRangeAsync(userId, sets, cancellationToken);

        return Ok(addedSets.Select(SetMapper.ToDto));
    }
}