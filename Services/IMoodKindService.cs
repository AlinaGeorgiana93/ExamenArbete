using Models;
using Models.DTO;

namespace Services;

public interface IMoodKindService {

    public Task<ResponsePageDto<IMoodKind>> ReadMoodKindsAsync( bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IMoodKind>> ReadMoodKindAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IMoodKind>> DeleteMoodKindAsync(Guid id);
     public Task<ResponseItemDto<IMoodKind>> UpdateMoodKindAsync(MoodKindCuDto item);
    public Task<ResponseItemDto<IMoodKind>> CreateMoodKindAsync(MoodKindCuDto item);


}