using Models;
using Models.DTO;

namespace Services;

public interface IAppetiteService {

    public Task<ResponsePageDto<IAppetite>> ReadAppetitesAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IAppetite>> ReadAppetiteAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IAppetite>> DeleteAppetiteAsync(Guid id);
    public Task<ResponseItemDto<IAppetite>> UpdateAppetiteAsync(AppetiteCuDto item);
    public Task<ResponseItemDto<IAppetite>> CreateAppetiteAsync(AppetiteCuDto item);

}