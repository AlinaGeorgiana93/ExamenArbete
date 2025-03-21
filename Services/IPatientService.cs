using Models;
using Models.DTO;

namespace Services;

public interface IPatientService {

    public Task<ResponsePageDto<IPatient>> ReadPatientsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IPatient>> ReadPatientAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IPatient>> DeletePatientAsync(Guid id);
    public Task<ResponseItemDto<IPatient>> UpdatePatientAsync(PatientCuDto item);
    public Task<ResponseItemDto<IPatient>> CreatePatientAsync(PatientCuDto item);


}