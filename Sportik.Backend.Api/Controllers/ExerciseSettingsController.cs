using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Sportik.Backend.Api.DTOs.Settings;
using Sportik.Backend.Api.Mappers;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Models.Settings;

namespace Sportik.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ExerciseSettingsController : ControllerBase
{
    private readonly IExerciseSettingsService _exerciseSettingsService;

    public ExerciseSettingsController(IExerciseSettingsService exerciseSettingsService)
    {
        _exerciseSettingsService = exerciseSettingsService;
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ExerciseSettingsUpdateDto exerciseSettingsUpdateDto, CancellationToken cancellationToken)
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

        Guid exerciseId = exerciseSettingsUpdateDto.ExerciseId;
        ExerciseSettingsDelta delta = ExerciseSettingsDeltaMapper.ToDomain(exerciseSettingsUpdateDto.Delta);

        OperationResult<ExerciseSettings> result = await _exerciseSettingsService.UpdateAsync(userId, delta, exerciseId, cancellationToken);

        if (!result.Succeeded)
        {
            return BadRequest("Failed to update exercise settings.");
        }

        return Ok(ExerciseSettingsMapper.ToDto(result.Value!));
    }
}