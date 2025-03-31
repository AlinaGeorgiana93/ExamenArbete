<<<<<<< HEAD
=======

>>>>>>> Debug-Patient-activity
using Models;
using Models.DTO;

namespace Services;

<<<<<<< HEAD
public interface ISleepService
{

    public Task<ResponsePageDto<ISleep>> ReadSleepsAsync(bool flat, string filter, int pageNumber, int pageSize);
=======
public interface ISleepService {

    public Task<ResponsePageDto<ISleep>> ReadSleepsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
>>>>>>> Debug-Patient-activity
    public Task<ResponseItemDto<ISleep>> ReadSleepAsync(Guid id, bool flat);
    public Task<ResponseItemDto<ISleep>> DeleteSleepAsync(Guid id);
    public Task<ResponseItemDto<ISleep>> UpdateSleepAsync(SleepCuDto item);
    public Task<ResponseItemDto<ISleep>> CreateSleepAsync(SleepCuDto item);

<<<<<<< HEAD
}
=======
}
>>>>>>> Debug-Patient-activity
