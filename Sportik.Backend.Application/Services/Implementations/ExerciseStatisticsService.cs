using Sportik.Backend.Application.Helpers;
using Sportik.Backend.Application.Repositories.Interfaces;
using Sportik.Backend.Application.Services.Interfaces;
using Sportik.Backend.Domain.Common;
using Sportik.Backend.Domain.Models.Statistics;

namespace Sportik.Backend.Application.Services.Implementations;

internal sealed class ExerciseStatisticsService : IExerciseStatisticsService
{
    private readonly IExerciseSetsRepository _exerciseSetsRepository;
    private readonly IExerciseStatisticsRepository _exerciseStatisticsRepository;

    public ExerciseStatisticsService(IExerciseSetsRepository exerciseSetsRepository, IExerciseStatisticsRepository exerciseStatisticsRepository)
    {
        _exerciseSetsRepository = exerciseSetsRepository;
        _exerciseStatisticsRepository = exerciseStatisticsRepository;
    }

    public async Task<IEnumerable<WeekStatistics>> GetAllAsync(Guid userId, WeekStatisticsOrder order, TimeSpan offset,
        CancellationToken cancellationToken = default)
    {
        IEnumerable<IGrouping<DateTime, ExerciseStatistics>> groupedStatistics = await _exerciseStatisticsRepository
            .GetAllAsync(userId, set => set.LoggedAt.ToOffset(offset).Date, cancellationToken);

        IEnumerable<DayStatistics> dayStatistics = groupedStatistics
            .Select(group => new DayStatistics
            {
                Date = group.Key,
                ExerciseStatistics = group.ToList(),
            });

        dayStatistics = order switch
        {
            WeekStatisticsOrder.Ascending => dayStatistics.OrderBy(statistics => statistics.Date),
            WeekStatisticsOrder.Descending => dayStatistics.OrderByDescending(statistics => statistics.Date),
            _ => throw new ArgumentOutOfRangeException(nameof(order), order, null)
        };

        IEnumerable<WeekStatistics> weekStatistics = dayStatistics
            .GroupBy(statistics => CalendarHelper.GetFirstDayOfWeek(statistics.Date))
            .Select(group => new WeekStatistics
            {
                DayStatistics = group.ToList(),
            });

        return order switch
        {
            WeekStatisticsOrder.Ascending => weekStatistics.OrderBy(StatisticsHelper.GetFirstDayDate),
            WeekStatisticsOrder.Descending => weekStatistics.OrderByDescending(StatisticsHelper.GetFirstDayDate),
            _ => throw new ArgumentOutOfRangeException(nameof(order), order, null)
        };
    }

    public async Task<Set?> AddSetAsync(Guid userId, Set set, CancellationToken cancellationToken = default)
    {
        return await _exerciseSetsRepository.AddAsync(userId, set, cancellationToken);
    }
}