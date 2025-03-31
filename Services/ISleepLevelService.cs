using Models;
using Models.DTO;

namespace Services;

public interface ISleepLevelService {

    public Task<ResponsePageDto<ISleepLevel>> ReadSleepLevelsAsync(bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<ISleepLevel>> ReadSleepLevelAsync(Guid id, bool flat);
    public Task<ResponseItemDto<ISleepLevel>> DeleteSleepLevelAsync(Guid id);
     public Task<ResponseItemDto<ISleepLevel>> UpdateSleepLevelAsync(SleepLevelCuDto item);
    public Task<ResponseItemDto<ISleepLevel>> CreateSleepLevelAsync(SleepLevelCuDto item);


}