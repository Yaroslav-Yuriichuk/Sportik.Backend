using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sportik.Backend.Api.DTOs.Exercises;
using Sportik.Backend.Api.Mappers;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Entities;

namespace Sportik.Backend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class ExercisesController : ControllerBase
{
    private readonly IExercisesService _exercisesService;

    public ExercisesController(IExercisesService exercisesService)
    {
        _exercisesService = exercisesService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
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

        IEnumerable<Exercise> exercises = await _exercisesService.GetUserExercisesAsync(userId, cancellationToken);

        return Ok(exercises.Select(ExerciseMapper.ToDto));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddExerciseDto addExerciseDto, CancellationToken cancellationToken)
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

        Exercise exercise = new Exercise { Name = addExerciseDto.Name, };
        exercise = await _exercisesService.AddUserExerciseAsync(userId, exercise, cancellationToken);

        return Ok(ExerciseMapper.ToDto(exercise));
    }

    [HttpDelete("{exerciseId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid exerciseId, CancellationToken cancellationToken)
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

        Exercise? deletedExercise = await _exercisesService.DeleteUserExerciseAsync(userId, exerciseId, cancellationToken);

        if (deletedExercise is null)
        {
            return NotFound("Exercise not found.");
        }

        return Ok(ExerciseMapper.ToDto(deletedExercise));
    }
}