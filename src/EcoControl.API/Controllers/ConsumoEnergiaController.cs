using EcoControl.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcoControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConsumoEnergiaController : ControllerBase
{
    private readonly IConsumoEnergiaService _service;
    public ConsumoEnergiaController(IConsumoEnergiaService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Listar(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] long? idEquipamento = null)
        => Ok(await _service.ListarAsync(page, pageSize, idEquipamento));

    [HttpGet("resumo")]
    public async Task<IActionResult> ResumoGeral()
        => Ok(await _service.ResumoGeralAsync());
}