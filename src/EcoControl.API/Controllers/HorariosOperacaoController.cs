using EcoControl.API.DTOs;
using EcoControl.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HorariosOperacaoController : ControllerBase
{
    private readonly IHorarioOperacaoService _service;
    public HorariosOperacaoController(IHorarioOperacaoService service) => _service = service;

    [HttpGet("equipamento/{idEquipamento}")]
    [AllowAnonymous]
    public async Task<IActionResult> ListarPorEquipamento(
        long idEquipamento,
        [FromQuery] int page = 1,
       [FromQuery] int pageSize = 10)
        => Ok(await _service.ListarPorEquipamentoAsync(idEquipamento, page, pageSize));

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Criar([FromBody] HorarioOperacaoRequestDto dto)
    {
        var criado = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(ListarPorEquipamento), new { idEquipamento = criado.IdEquipamento }, criado);
    }
}