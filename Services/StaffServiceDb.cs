using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class StaffServiceDb : IStaffService
{
    private readonly StaffDbRepos _staffRepo;

    private readonly ILogger<StaffServiceDb> _logger;



    public StaffServiceDb(StaffDbRepos staffRepo, ILogger<StaffServiceDb> logger)
    {
        _staffRepo = staffRepo;
        _logger = logger;
    }

    public Task<ResponsePageDto<IStaff>> ReadStaffsAsync(bool flat, string filter, int pageNumber, int pageSize) => _staffRepo.ReadItemsAsync(flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IStaff>> ReadStaffAsync(Guid id, bool flat) => _staffRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IStaff>> DeleteStaffAsync(Guid id) => _staffRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IStaff>> UpdateStaffAsync(StaffCuDto item) => _staffRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IStaff>> CreateStaffAsync(StaffCuDto item) => _staffRepo.CreateItemAsync(item);

}