using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Services;


namespace AppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GraphController : Controller
    {
        readonly IGraphService _service = null;
        readonly ILogger<GraphController> _logger = null;

        public GraphController(IGraphService service, ILogger<GraphController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(ResponsePageDto<IGraph>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> ReadItems(string flat = "true",
            string filter = null, string pageNr = "0", string pageSize = "10")
        {
            try
            {
                bool flatArg = bool.Parse(flat);
                int pageNrArg = int.Parse(pageNr);
                int pageSizeArg = int.Parse(pageSize);

                _logger.LogInformation($"{nameof(ReadItems)}:{nameof(flatArg)}: {flatArg}, " +
                    $"{nameof(pageNrArg)}: {pageNrArg}, {nameof(pageSizeArg)}: {pageSizeArg}");

                var resp = await _service.ReadGraphsAsync( filter?.Trim().ToLower(), pageNrArg, pageSizeArg);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadItems)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<IGraph>))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadItem(string id = null, string flat = "false")
        {
            try
            {
                var idArg = Guid.Parse(id);
                bool flatArg = bool.Parse(flat);

                _logger.LogInformation($"{nameof(ReadItem)}: {nameof(idArg)}: {idArg}, {nameof(flatArg)}: {flatArg}");

                var item = await _service.ReadGraphAsync(idArg, flatArg);
                if (item?.Item == null) throw new ArgumentException($"Item with id {id} does not exist");

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadItem)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<IGraph>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> DeleteItem(string id)
        {
            try
            {
                var idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(DeleteItem)}: {nameof(idArg)}: {idArg}");

                var item = await _service.DeleteGraphAsync(idArg);
                if (item?.Item == null) throw new ArgumentException($"Item with id {id} does not exist");

                _logger.LogInformation($"item {idArg} deleted");
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DeleteItem)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<GraphCuDto>))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> ReadItemDto(string id = null)
        {
            try
            {
                var idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(ReadItemDto)}: {nameof(idArg)}: {idArg}");

                var item = await _service.ReadGraphAsync(idArg, false);
                if (item?.Item == null) throw new ArgumentException($"Item with id {id} does not exist");

                return Ok(
                    new ResponseItemDto<GraphCuDto>()
                    {
                        DbConnectionKeyUsed = item.DbConnectionKeyUsed,
                        Item = new GraphCuDto(item.Item)
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ReadItemDto)}: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<IGraph>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> UpdateItem(string id, [FromBody] GraphCuDto item)
        {
            try
            {
                var idArg = Guid.Parse(id);

                _logger.LogInformation($"{nameof(UpdateItem)}: {nameof(idArg)}: {idArg}");

                if (item.GraphId != idArg) throw new ArgumentException("Id mismatch");

                var _item = await _service.UpdateGraphAsync(item);
                _logger.LogInformation($"item {idArg} updated");

                return Ok(_item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UpdateItem)}: {ex.Message}");
                return BadRequest($"Could not update. Error {ex.Message}");
            }
        }

        [HttpPost()]
        [ProducesResponseType(200, Type = typeof(ResponseItemDto<IGraph>))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> CreateItem([FromBody] GraphCuDto item)
        {
            try
            {
                _logger.LogInformation($"{nameof(CreateItem)}:");

                var _item = await _service.CreateGraphAsync(item);
                _logger.LogInformation($"item {_item.Item.GraphId} created");

                return Ok(_item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(CreateItem)}: {ex.Message}");
                return BadRequest($"Could not create. Error {ex.Message}");
            }
        }
    }
}

