using Models;
using Models.DTO;

namespace Services;

public interface IMoodService
{

    public Task<ResponsePageDto<IMood>> ReadMoodsAsync(bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IMood>> ReadMoodAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IMood>> DeleteMoodAsync(Guid id);
    public Task<ResponseItemDto<IMood>> UpdateMoodAsync(MoodCuDto item);
    public Task<ResponseItemDto<IMood>> CreateMoodAsync(MoodCuDto item);



}