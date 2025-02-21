using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class MoodServiceDb : IMoodService {

    private readonly MoodDbRepos _moodRepo;
    private readonly ActivityDbRepos _activityRepo;
    private readonly EmployeeDbRepos _employeeRepo;
    private readonly CreditCardDbRepos _creditcardRepo;
    private readonly ILogger<MoodServiceDb> _logger;    
    
    public MoodServiceDb(MoodDbRepos moodRepo, ActivityDbRepos activityRepo, EmployeeDbRepos employeeRepo, 
        CreditCardDbRepos creditcardRepo, ILogger<MoodServiceDb> logger)
    {
        _moodRepo = moodRepo;
        _activityRepo = activityRepo;
        _employeeRepo = employeeRepo;
        _creditcardRepo = creditcardRepo;
        _logger = logger;
    }

    public Task<ResponsePageDto<IMood>> ReadMoodsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _zooRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IMood>> ReadMoodAsync(Guid id, bool flat) => _moodRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IMood>> DeleteMoodAsync(Guid id) => _moodRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IMood>> UpdateMoodAsync(MoodCuDto item) => _moodRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IMood>> CreateMoodAsync(MoodCuDto item) => _moodRepo.CreateItemAsync(item);

    public Task<ResponsePageDto<IActivity>> ReadActivitysAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _animalRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IActivity>> ReadActivityAsync(Guid id, bool flat) => _activityRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IActivity>> DeleteActivityAsync(Guid id) => _activityRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IActivity>> UpdateActivityAsync(ActivityCuDto item) => _activityRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IActivity>> CreateActivityAsync(ActivityCuDto item) => _activityRepo.CreateItemAsync(item);

    public Task<ResponsePageDto<IEmployee>> ReadEmployeesAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _employeeRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IEmployee>> ReadEmployeeAsync(Guid id, bool flat) => _employeeRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IEmployee>> DeleteEmployeeAsync(Guid id) => _employeeRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IEmployee>> UpdateEmployeeAsync(EmployeeCuDto item) => _employeeRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IEmployee>> CreateEmployeeAsync(EmployeeCuDto item) => _employeeRepo.CreateItemAsync(item);


    public Task<ResponsePageDto<ICreditCard>> ReadCreditCardsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _creditcardRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<ICreditCard>> ReadCreditCardAsync(Guid id, bool flat) => _creditcardRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<ICreditCard>> DeleteCreditCardAsync(Guid id) => _creditcardRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<ICreditCard>> CreateCreditCardAsync(CreditCardCuDto item) => _creditcardRepo.CreateItemAsync(item);

    public Task<ResponsePageDto<IEmployee>> ReadEmployeesWithCCAsync(bool hasCreditCard, int pageNumber, int pageSize) => _creditcardRepo.ReadEmployeesWithCCAsync(hasCreditCard, pageNumber, pageSize);
    public Task<ResponseItemDto<ICreditCard>> ReadDecryptedCCAsync(Guid id) => _creditcardRepo.ReadDecryptedCCAsync(id);

}