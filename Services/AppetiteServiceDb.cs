using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class AppetiteServiceDb : IAppetiteService
{
    private readonly AppetiteDbRepos _appetiteRepo;
    private readonly ILogger<AppetiteServiceDb> _logger;

 public AppetiteServiceDb(AppetiteDbRepos appetiteRepo, ILogger<AppetiteServiceDb> logger)

    {
        _appetiteRepo = appetiteRepo;
        _logger = logger;
    }


    public Task<ResponsePageDto<IAppetite>> ReadAppetitesAsync(bool flat, string filter, int pageNumber, int pageSize) => _appetiteRepo.ReadItemsAsync(flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IAppetite>> ReadAppetiteAsync(Guid id, bool flat) => _appetiteRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IAppetite>> DeleteAppetiteAsync(Guid id) => _appetiteRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IAppetite>> UpdateAppetiteAsync(AppetiteCuDto item) => _appetiteRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IAppetite>> CreateAppetiteAsync(AppetiteCuDto item) => _appetiteRepo.CreateItemAsync(item);

}