using Models;
using Models.DTO;

namespace Services;

public interface IMoodService {

    public Task<ResponsePageDto<IMood>> ReadMoodsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IMood>> ReadMoodAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IMood>> DeleteMoodAsync(Guid id);
    public Task<ResponseItemDto<IMood>> UpdateMoodAsync(MoodCuDto item);
    public Task<ResponseItemDto<IMood>> CreateMoodAsync(MoodCuDto item);

    public Task<ResponsePageDto<IActivity>> ReadActivityAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IActivity>> ReadActivityAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IActivity>> DeleteActivityAsync(Guid id);
    public Task<ResponseItemDto<IActivity>> UpdateActivityAsync(ActivityCuDto item);
    public Task<ResponseItemDto<IActivity>> CreateActivityAsync(ActivityCuDto item);

    public Task<ResponsePageDto<IEmployee>> ReadEmployeesAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IEmployee>> ReadEmployeeAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IEmployee>> DeleteEmployeeAsync(Guid id);
    public Task<ResponseItemDto<IEmployee>> UpdateEmployeeAsync(EmployeeCuDto item);
    public Task<ResponseItemDto<IEmployee>> CreateEmployeeAsync(EmployeeCuDto item);

    public Task<ResponsePageDto<ICreditCard>> ReadCreditCardsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<ICreditCard>> ReadCreditCardAsync(Guid id, bool flat);
    public Task<ResponseItemDto<ICreditCard>> DeleteCreditCardAsync(Guid id);
    public Task<ResponseItemDto<ICreditCard>> CreateCreditCardAsync(CreditCardCuDto item);

    public Task<ResponsePageDto<IEmployee>> ReadEmployeesWithCCAsync(bool hasCreditCard, int pageNumber, int pageSize);
    public Task<ResponseItemDto<ICreditCard>> ReadDecryptedCCAsync(Guid id);
}