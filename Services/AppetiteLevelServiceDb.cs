using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class AppetiteLevelServiceDb : IAppetiteLevelService
{

    private readonly AppetiteLevelDbRepos _appetitelevelRepo;
    private readonly ILogger<AppetiteLevelServiceDb> _logger;

    public AppetiteLevelServiceDb(AppetiteLevelDbRepos appetitelevelRepo, ILogger<AppetiteLevelServiceDb> logger)
    {
        _appetitelevelRepo = appetitelevelRepo;
        _logger = logger;
    }

    public Task<ResponsePageDto<IAppetiteLevel>> ReadAppetiteLevelsAsync(bool flat, string filter, int pageNumber, int pageSize) => _appetitelevelRepo.ReadItemsAsync(flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IAppetiteLevel>> ReadAppetiteLevelAsync(Guid id, bool flat) => _appetitelevelRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IAppetiteLevel>> DeleteAppetiteLevelAsync(Guid id) => _appetitelevelRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IAppetiteLevel>> UpdateAppetiteLevelAsync(AppetiteLevelCuDto item) => _appetitelevelRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IAppetiteLevel>> CreateAppetiteLevelAsync(AppetiteLevelCuDto item) => _appetitelevelRepo.CreateItemAsync(item);


}