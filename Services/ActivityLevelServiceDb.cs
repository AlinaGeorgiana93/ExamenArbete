using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class ActivityLevelServiceDb : IActivityLevelService
{

    private readonly ActivityLevelDbRepos _activityLevelRepo;
    private readonly ILogger<ActivityLevelServiceDb> _logger;

    public ActivityLevelServiceDb(ActivityDbRepos activityRepo, ILogger<ActivityLevelServiceDb> logger)
    {
        _activityLevelRepo = activityRepo;
        _logger = logger;
    }

    public Task<ResponsePageDto<IActivityLevel>> ReadActivitiesLevelAsync(bool flat, string filter, int pageNumber, int pageSize) => _activityLevelRepo.ReadItemsAsync(flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IActivityLevel>> ReadActivityLevelAsync(Guid id, bool flat) => _activityLevelRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IActivityLevel>> DeleteActivityLevelAsync(Guid id) => _activityLevelRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IActivityLevel>> UpdateActivityLevelAsync(ActivityLevelCuDto item) => _activityLevelRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IActivityLevel>> CreateActivityLevelAsync(ActivityLevelCuDto item) => _activityLevelRepo.CreateItemAsync(item);


}