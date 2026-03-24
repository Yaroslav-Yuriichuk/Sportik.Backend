using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sportik.Backend.Api.DTOs.Exercises;
using Sportik.Backend.Api.Mappers;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Models;

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

        IEnumerable<Exercise> exercises = await _exercisesService.GetAllAsync(userId, cancellationToken);

        return Ok(exercises.Select(ExerciseMapper.ToDto));
    }

    [HttpGet("{exerciseId:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid exerciseId, CancellationToken cancellationToken)
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

        Exercise? exercise = await _exercisesService.GetByIdAsync(userId, exerciseId, cancellationToken);

        if (exercise is null)
        {
            return NotFound("Exercise not found.");
        }

        return Ok(ExerciseMapper.ToDto(exercise));
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

        Exercise exercise = ExerciseMapper.ToDomain(addExerciseDto);
        exercise = await _exercisesService.AddAsync(userId, exercise, cancellationToken);

        return Ok(ExerciseMapper.ToDto(exercise));
    }

    [HttpPost("batch")]
    public async Task<IActionResult> PostBatch([FromBody] IEnumerable<AddExerciseDto> addExerciseDtos, CancellationToken cancellationToken)
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

        IEnumerable<Exercise> exercises = addExerciseDtos.Select(ExerciseMapper.ToDomain);
        exercises = await _exercisesService.AddRangeAsync(userId, exercises, cancellationToken);

        return Ok(exercises.Select(ExerciseMapper.ToDto));
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

        Exercise? deletedExercise = await _exercisesService.DeleteAsync(userId, exerciseId, cancellationToken);

        if (deletedExercise is null)
        {
            return NotFound("Exercise not found.");
        }

        return Ok(ExerciseMapper.ToDto(deletedExercise));
    }
}