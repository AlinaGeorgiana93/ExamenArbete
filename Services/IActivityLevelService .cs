using Models;
using Models.DTO;
namespace Services;

public interface IActivityLevelService
{

    public Task<ResponsePageDto<IActivityLevel>> ReadActivitiesLevelAsync(bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IActivityLevel>> ReadActivityLevelAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IActivityLevel>> DeleteActivityLevelAsync(Guid id);
    public Task<ResponseItemDto<IActivityLevel>> UpdateActivityLevelAsync(ActivityLevelCuDto item);
    public Task<ResponseItemDto<IActivityLevel>> CreateActivityLevelAsync(ActivityLevelCuDto item);

}
