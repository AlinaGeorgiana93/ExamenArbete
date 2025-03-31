using DbRepos;
using Microsoft.Extensions.Logging;
using Models;
using Models.DTO;

namespace Services;


public class GraphServiceDb : IGraphService {

    private readonly GraphDbRepos _graphRepo;
  
    private readonly ILogger<GraphServiceDb> _logger;    
    
    public GraphServiceDb(GraphDbRepos graphRepo, ILogger<GraphServiceDb> logger)
    {
        _graphRepo = graphRepo;
        _logger = logger;
    }

    public Task<ResponsePageDto<IGraph>> ReadGraphsAsync( string filter, int pageNumber, int pageSize) => _graphRepo.ReadItemsAsync(true,filter, pageNumber, pageSize);
    public Task<ResponseItemDto<IGraph>> ReadGraphAsync(Guid id, bool flat) => _graphRepo.ReadItemAsync(id, flat);
    public Task<ResponseItemDto<IGraph>> DeleteGraphAsync(Guid id) => _graphRepo.DeleteItemAsync(id);
    public Task<ResponseItemDto<IGraph>> UpdateGraphAsync(GraphCuDto item) => _graphRepo.UpdateItemAsync(item);
    public Task<ResponseItemDto<IGraph>> CreateGraphAsync(GraphCuDto item) => _graphRepo.CreateItemAsync(item);


    }