using Models;
using Models.DTO;

namespace Services;

public interface IPatientService {

    public Task<ResponsePageDto<IPatient>> ReadPatientsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IPatient>> ReadPatientAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IPatient>> DeletePatientAsync(Guid id);
    public Task<ResponseItemDto<IPatient>> UpdatePatientAsync(PatientCuDto item);
    public Task<ResponseItemDto<IPatient>> CreatePatientAsync(PatientCuDto item);

    public Task<ResponsePageDto<IMood>> ReadMoodsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IMood>> ReadMoodAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IMood>> DeleteMoodAsync(Guid id);
    public Task<ResponseItemDto<IMood>> UpdateAnimalAsync(MoodCuDto item);
    public Task<ResponseItemDto<IMood>> CreateAnimalAsync(MoodCuDto item);

    public Task<ResponsePageDto<IAppetite>> ReadAppetitesAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IAppetite>> ReadAppetiteAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IAppetite>> DeleteAppetiteAsync(Guid id);
    public Task<ResponseItemDto<IAppetite>> UpdateAppetiteAsync(AppetiteCuDto item);
    public Task<ResponseItemDto<IAppetite>> CreateAppetiteAsync(AppetiteCuDto item);

    // Activity Methods
    public Task<ResponsePageDto<IActivity>> ReadActivitiesAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IActivity>> ReadActivityAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IActivity>> DeleteActivityAsync(Guid id);
    public Task<ResponseItemDto<IActivity>> UpdateActivityAsync(ActivityCuDto item);
    public Task<ResponseItemDto<IActivity>> CreateActivityAsync(ActivityCuDto item);

    // Sleep Methods
    public Task<ResponsePageDto<ISleep>> ReadSleepsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<ISleep>> ReadSleepAsync(Guid id, bool flat);
    public Task<ResponseItemDto<ISleep>> DeleteSleepAsync(Guid id);
    public Task<ResponseItemDto<ISleep>> UpdateSleepAsync(SleepCuDto item);
    public Task<ResponseItemDto<ISleep>> CreateSleepAsync(SleepCuDto item);

 // Graph Methods
    public Task<ResponsePageDto<ISleep>> ReadGraphsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<ISleep>> ReadGraphAsync(Guid id, bool flat);
    public Task<ResponseItemDto<ISleep>> DeleteGraphAsync(Guid id);
    public Task<ResponseItemDto<ISleep>> UpdateGraphAsync(SleepCuDto item);
    public Task<ResponseItemDto<ISleep>> CreateGraphAsync(SleepCuDto item);

}