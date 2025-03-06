using Models;
using Models.DTO;

namespace Services;

public interface IGraphService {

    public Task<ResponsePageDto<IGraph>> ReadGraphsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IGraph>> ReadGraphAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IGraph>> DeleteGraphAsync(Guid id);
    public Task<ResponseItemDto<IGraph>> UpdateGraphAsync(GraphCuDto item);
    public Task<ResponseItemDto<IGraph>> CreateGraphAsync(GraphCuDto item);

    public Task<ResponsePageDto<IMood>> ReadMoodsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IMood>> ReadMoodAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IMood>> DeleteMoodAsync(Guid id);
    public Task<ResponseItemDto<IMood>> UpdateAnimalAsync(MoodCuDto item);
    public Task<ResponseItemDto<IMood>> CreateAnimalAsync(MoodCuDto item);

    public Task<ResponsePageDto<IAppetite>> ReadAppetitesAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IAppetite>> ReadAppetiteAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IAppetite>> DeleteAppetiteAsync(Guid id);
    public Task<ResponseItemDto<IAppetite>> UpdateAppetiteAsync(AppetiteCuDto item);
    public Task<ResponseItemDto<IAppetite>> CreateAppetiteAsync(AppetiteCuDto item);

    // Activity Methods
    public Task<ResponsePageDto<IActivity>> ReadActivitiesAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IActivity>> ReadActivityAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IActivity>> DeleteActivityAsync(Guid id);
    public Task<ResponseItemDto<IActivity>> UpdateActivityAsync(ActivityCuDto item);
    public Task<ResponseItemDto<IActivity>> CreateActivityAsync(ActivityCuDto item);

    // Sleep Methods
    public Task<ResponsePageDto<ISleep>> ReadSleepsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<ISleep>> ReadSleepAsync(Guid id, bool flat);
    public Task<ResponseItemDto<ISleep>> DeleteSleepAsync(Guid id);
    public Task<ResponseItemDto<ISleep>> UpdateSleepAsync(SleepCuDto item);
    public Task<ResponseItemDto<ISleep>> CreateSleepAsync(SleepCuDto item);

}