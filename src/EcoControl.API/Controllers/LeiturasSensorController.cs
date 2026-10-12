using EcoControl.API.DTOs;
using EcoControl.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeiturasSensorController : ControllerBase
{
    private readonly ILeituraSensorService _service;
    public LeiturasSensorController(ILeituraSensorService service) => _service = service;

    [HttpGet("equipamento/{idEquipamento}")]
    [AllowAnonymous]
    public async Task<IActionResult> ListarPorEquipamento(
        long idEquipamento, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        => Ok(await _service.ListarPorEquipamentoAsync(idEquipamento, page, pageSize));

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> RegistrarLeitura([FromBody] LeituraSensorRequestDto dto)
    {
        var criada = await _service.RegistrarLeituraAsync(dto);
        return CreatedAtAction(nameof(ListarPorEquipamento), new { idEquipamento = criada.IdEquipamento }, criada);
    }
}