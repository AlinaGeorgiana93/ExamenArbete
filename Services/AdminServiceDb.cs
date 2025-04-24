using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class AdminServiceDb : IAdminService
{

    private readonly AdminDbRepos _adminRepo;
    private readonly ILogger<AdminServiceDb> _logger;

    public AdminServiceDb(AdminDbRepos adminRepo, ILogger<AdminServiceDb> logger)
    {
        _adminRepo = adminRepo;
        _logger = logger;
    }

    public Task<ResponseItemDto<GstUsrInfoAllDto>> InfoAsync() => _adminRepo.InfoAsync();
    public async Task<UsrInfoDto> SeedDefaultUsersAsync()
    {
        await _adminRepo.SeedDefaultUsersAsync();
        return new UsrInfoDto(); // Replace with actual logic to create UsrInfoDto if needed
    }
}