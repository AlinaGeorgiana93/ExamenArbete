using Models;
using Models.DTO;

namespace Services;

public interface IGraphService
{

    public Task<ResponsePageDto<IGraph>> ReadGraphsAsync(bool flat, string filter, int pageNumber, int pageSize);
    public Task<ResponseItemDto<IGraph>> ReadGraphAsync(Guid id, bool flat);
    public Task<ResponseItemDto<IGraph>> DeleteGraphAsync(Guid id);
    public Task<ResponseItemDto<IGraph>> UpdateGraphAsync(GraphCuDto item);
    public Task<ResponseItemDto<IGraph>> CreateGraphAsync(GraphCuDto item);


}