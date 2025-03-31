using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class SleepLevelServiceDb : ISleepLevelService {

    private readonly SleepLevelDbRepos _sleeplevelRepo;
    private readonly ILogger<SleepLevelServiceDb> _logger;    
    
    public SleepLevelServiceDb(SleepLevelDbRepos sleeplevelRepo,ILogger<SleepLevelServiceDb> logger)
    {
        _sleeplevelRepo = sleeplevelRepo;
        _logger = logger;
    }

    public Task<ResponsePageDto<ISleepLevel>> ReadSleepLevelsAsync(bool flat, string filter, int pageNumber, int pageSize) => _sleeplevelRepo.ReadItemsAsync(flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<ISleepLevel>> ReadSleepLevelAsync(Guid id, bool flat) => _sleeplevelRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<ISleepLevel>> DeleteSleepLevelAsync(Guid id) => _sleeplevelRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<ISleepLevel>> UpdateSleepLevelAsync(SleepLevelCuDto item) => _sleeplevelRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<ISleepLevel>> CreateSleepLevelAsync(SleepLevelCuDto item) => _sleeplevelRepo.CreateItemAsync(item);


}