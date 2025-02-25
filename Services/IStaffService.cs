using Models;
using Models.DTO;

namespace Services;

public interface IStaffService {

    public Task<ResponsePageDto<IStaff>> ReadStaffsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IStaff>> ReadStaffAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IStaff>> DeleteStaffAsync(Guid id);
    public Task<ResponseItemDto<IStaff>> UpdateStaffAsync(StaffCuDto item);
    public Task<ResponseItemDto<IStaff>> CreateStaffAsync(StaffCuDto item);

}