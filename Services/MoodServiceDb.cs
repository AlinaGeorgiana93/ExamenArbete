using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class MoodServiceDb : IMoodService {

    private readonly MoodDbRepos _MoodRepo;
    private readonly AnimalDbRepos _animalRepo;
    private readonly EmployeeDbRepos _employeeRepo;
    private readonly CreditCardDbRepos _creditcardRepo;
    private readonly ILogger<MoodServiceDb> _logger;    
    
    public MoodServiceDb(MoodDbRepos MoodRepo, AnimalDbRepos animalRepo, EmployeeDbRepos employeeRepo, 
        CreditCardDbRepos creditcardRepo, ILogger<MoodServiceDb> logger)
    {
        _MoodRepo = MoodRepo;
        _animalRepo = animalRepo;
        _employeeRepo = employeeRepo;
        _creditcardRepo = creditcardRepo;
        _logger = logger;
    }

    public Task<ResponsePageDto<IMood>> ReadMoodsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _zooRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IMood>> ReadMoodAsync(Guid id, bool flat) => _MoodRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IMood>> DeleteMoodAsync(Guid id) => _MoodRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IMood>> UpdateMoodAsync(MoodCuDto item) => _MoodRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IMood>> CreateMoodAsync(MoodCuDto item) => _MoodRepo.CreateItemAsync(item);

    public Task<ResponsePageDto<IAnimal>> ReadAnimalsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _animalRepo.ReadItemsAsync(seeded, flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IAnimal>> ReadAnimalAsync(Guid id, bool flat) => _animalRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IAnimal>> DeleteAnimalAsync(Guid id) => _animalRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IAnimal>> UpdateAnimalAsync(AnimalCuDto item) => _animalRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IAnimal>> CreateAnimalAsync(AnimalCuDto item) => _animalRepo.CreateItemAsync(item);

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