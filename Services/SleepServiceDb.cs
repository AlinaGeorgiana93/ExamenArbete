using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class SleepServiceDb : ISleepService
{
    private readonly SleepDbRepos _sleepRepo;
    private readonly ILogger<SleepServiceDb> _logger;



    public SleepServiceDb(SleepDbRepos sleepRepo, ILogger<SleepServiceDb> logger)
    {
        _sleepRepo = sleepRepo;
        _logger = logger;
    }


    public Task<ResponsePageDto<ISleep>> ReadSleepsAsync(bool flat, string filter, int pageNumber, int pageSize) => _sleepRepo.ReadItemsAsync(flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<ISleep>> ReadSleepAsync(Guid id, bool flat) => _sleepRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<ISleep>> DeleteSleepAsync(Guid id) => _sleepRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<ISleep>> UpdateSleepAsync(SleepCuDto item) => _sleepRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<ISleep>> CreateSleepAsync(SleepCuDto item) => _sleepRepo.CreateItemAsync(item);

}