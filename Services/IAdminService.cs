using Models;
using Models.DTO;

namespace Services;

public interface IAdminService
{

    public Task<ResponseItemDto<GstUsrInfoAllDto>> InfoAsync();
    public Task<UsrInfoDto> SeedUsersAsync(int nrOfUsers, int nrOfSysAdmin);
}