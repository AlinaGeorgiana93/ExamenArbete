using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class ActivityServiceDb : IActivityService {

    private readonly ActivityDbRepos _activityRepo;
    private readonly ILogger<ActivityServiceDb> _logger;    
    
    public ActivityServiceDb(ActivityDbRepos activityRepo,ILogger<ActivityServiceDb> logger)
    {
        _activityRepo = activityRepo;
        _logger = logger;
    }

    public Task<ResponsePageDto<IActivity>> ReadActivityAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _moodRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IActivity>> ReadActivityAsync(Guid id, bool flat) => _activityRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IActivity>> DeleteActivityAsync(Guid id) => _activityRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IActivity>> UpdateActivityAsync(ActivityCuDto item) => _activityRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IActivity>> CreateActivityAsync(ActivityCuDto item) => _activityRepo.CreateItemAsync(item);


}