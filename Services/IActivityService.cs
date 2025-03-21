using Models;
using Models.DTO;
namespace Services;

public interface IActivityService
{

    public Task<ResponsePageDto<IActivity>> ReadActivitiesAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IActivity>> ReadActivityAsync( Guid id, bool flat);
    public Task<ResponseItemDto<IActivity>> DeleteActivityAsync(Guid id);
    public Task<ResponseItemDto<IActivity>> UpdateActivityAsync(ActivityCuDto item);
    public Task<ResponseItemDto<IActivity>> CreateActivityAsync(ActivityCuDto item);

}
