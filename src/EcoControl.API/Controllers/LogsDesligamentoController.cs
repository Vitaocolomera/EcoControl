using EcoControl.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcoControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogsDesligamentoController : ControllerBase
{
    private readonly ILogDesligamentoService _service;
    public LogsDesligamentoController(ILogDesligamentoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Listar(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] long? idEquipamento = null, [FromQuery] string? tipo = null)
        => Ok(await _service.ListarAsync(page, pageSize, idEquipamento, tipo));
}