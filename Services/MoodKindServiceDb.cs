using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class MoodKindServiceDb : IMoodKindService {

    private readonly MoodKindDbRepos _moodkindRepo;
    private readonly ILogger<MoodKindServiceDb> _logger;    
    
    public MoodKindServiceDb(MoodKindDbRepos moodkindRepo,ILogger<MoodKindServiceDb> logger)
    {
        _moodkindRepo = moodkindRepo;
        _logger = logger;
    }

    public Task<ResponsePageDto<IMoodKind>> ReadMoodKindsAsync(bool flat, string filter, int pageNumber, int pageSize) => _moodkindRepo.ReadItemsAsync( flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IMoodKind>> ReadMoodKindAsync(Guid id, bool flat) => _moodkindRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IMoodKind>> DeleteMoodKindAsync(Guid id) => _moodkindRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IMoodKind>> UpdateMoodKindAsync(MoodKindCuDto item) => _moodkindRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IMoodKind>> CreateMoodKindAsync(MoodKindCuDto item) => _moodkindRepo.CreateItemAsync(item);


}