using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class GraphServiceDb : IGraphService {

    private readonly GraphDbRepos _graphRepo;
    private readonly MoodDbRepos _moodRepo;
    private readonly ActivityDbRepos _activityRepo;
    private readonly PatientDbRepos _patientRepo;

    private readonly AppetiteDbRepos _appetiteRepo;
    private readonly SleepDbRepos _sleepRepo;
    private readonly ILogger<GraphServiceDb> _logger;    
    
    public GraphServiceDb(GraphDbRepos graphRepo, MoodDbRepos moodRepo, ActivityDbRepos activityRepo, AppetiteDbRepos appetiteRepo, SleepDbRepos sleepRepo,ILogger<GraphServiceDb> logger)
    {
        _graphRepo = graphRepo;
        _moodRepo = moodRepo;
        _activityRepo = activityRepo;
        _appetiteRepo = appetiteRepo;
        _sleepRepo = sleepRepo;
        _logger = logger;
    }

    public Task<ResponsePageDto<IGraph>> ReadGraphsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _graphRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IGraph>> ReadGraphAsync(Guid id, bool flat) => _graphRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IGraph>> DeleteGraphAsync(Guid id) => _graphRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IGraph>> UpdateGraphAsync(GraphCuDto item) => _graphRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IGraph>> CreateGraphAsync(GraphCuDto item) => _graphRepo.CreateItemAsync(item);

    public Task<ResponsePageDto<IMood>> ReadMoodsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _moodRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IMood>> ReadMoodAsync(Guid id, bool flat) => _moodRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IMood>> DeleteMoodAsync(Guid id) => _moodRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IMood>> UpdateMoodAsync(MoodCuDto item) => _moodRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IMood>> CreateMoodAsync(MoodCuDto item) => _moodRepo.CreateItemAsync(item);


    public Task<ResponsePageDto<IAppetite>> ReadAppetitesAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _appetiteRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IAppetite>> ReadAppetiteAsync(Guid id, bool flat) => _appetiteRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IAppetite>> DeleteAppetiteAsync(Guid id) => _appetiteRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IAppetite>> UpdateAppetiteAsync(AppetiteCuDto item) => _appetiteRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IAppetite>> CreateAppetiteAsync(AppetiteCuDto item) => _appetiteRepo.CreateItemAsync(item);

        // Activity Methods
    public Task<ResponsePageDto<IActivity>> ReadActivitiesAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _activityRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IActivity>> ReadActivityAsync(Guid id, bool flat) => _activityRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IActivity>> DeleteActivityAsync(Guid id) => _activityRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IActivity>> UpdateActivityAsync(ActivityCuDto item) => _activityRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IActivity>> CreateActivityAsync(ActivityCuDto item) => _activityRepo.CreateItemAsync(item);

    // Sleep Methods
    public Task<ResponsePageDto<ISleep>> ReadSleepsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _sleepRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<ISleep>> ReadSleepAsync(Guid id, bool flat) => _sleepRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<ISleep>> DeleteSleepAsync(Guid id) => _sleepRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<ISleep>> UpdateSleepAsync(SleepCuDto item) => _sleepRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<ISleep>> CreateSleepAsync(SleepCuDto item) => _sleepRepo.CreateItemAsync(item);

    }