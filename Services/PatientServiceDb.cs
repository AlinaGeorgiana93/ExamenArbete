using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class PatientServiceDb : IPatientService
{

    private readonly PatientDbRepos _patientRepo;

    private readonly ILogger<PatientServiceDb> _logger;

    public PatientServiceDb(PatientDbRepos patientRepo, ILogger<PatientServiceDb> logger)
    {
        _patientRepo = patientRepo;
        _logger = logger;
    }

    public Task<ResponsePageDto<IPatient>> ReadPatientsAsync(bool flat, string filter, int pageNumber, int pageSize) => _patientRepo.ReadItemsAsync(flat, filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IPatient>> ReadPatientAsync(Guid id, bool flat) => _patientRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IPatient>> DeletePatientAsync(Guid id) => _patientRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IPatient>> UpdatePatientAsync(PatientCuDto item) => _patientRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IPatient>> CreatePatientAsync(PatientCuDto item) => _patientRepo.CreateItemAsync(item);


}