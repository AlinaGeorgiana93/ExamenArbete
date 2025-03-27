using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class MoodServiceDb : IMoodService
{

    private readonly MoodDbRepos _moodRepo;
    private readonly ILogger<MoodServiceDb> _logger;

    public MoodServiceDb(MoodDbRepos moodRepo, ILogger<MoodServiceDb> logger)
    {
        _moodRepo = moodRepo;
        _logger = logger;
    }

    public Task<ResponsePageDto<IMood>> ReadMoodsAsync(bool flat, string filter, int pageNumber, int pageSize) => _moodRepo.ReadItemsAsync(flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IMood>> ReadMoodAsync(Guid id, bool flat) => _moodRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IMood>> DeleteMoodAsync(Guid id) => _moodRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IMood>> UpdateMoodAsync(MoodCuDto item) => _moodRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IMood>> CreateMoodAsync(MoodCuDto item) => _moodRepo.CreateItemAsync(item);


}