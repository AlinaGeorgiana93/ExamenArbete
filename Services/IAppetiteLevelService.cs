using Models;
using Models.DTO;

namespace Services;

public interface IAppetiteLevelService
{

    public Task<ResponsePageDto<IAppetiteLevel>> ReadAppetiteLevelsAsync(bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IAppetiteLevel>> ReadAppetiteLevelAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IAppetiteLevel>> DeleteAppetiteLevelAsync(Guid id);
    public Task<ResponseItemDto<IAppetiteLevel>> UpdateAppetiteLevelAsync(AppetiteLevelCuDto item);
    public Task<ResponseItemDto<IAppetiteLevel>> CreateAppetiteLevelAsync(AppetiteLevelCuDto item);


}