using EcoControl.API.DTOs;
using EcoControl.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipamentosController : ControllerBase
{
    private readonly IEquipamentoService _service;
    public EquipamentosController(IEquipamentoService service) => _service = service;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Listar(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        [FromQuery] string? setor = null, [FromQuery] string? status = null)
        => Ok(await _service.ListarAsync(page, pageSize, setor, status));

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> BuscarPorId(long id)
        => Ok(await _service.BuscarPorIdAsync(id));

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Criar([FromBody] EquipamentoRequestDto dto)
    {
        var criado = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(BuscarPorId), new { id = criado.IdEquipamento }, criado);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Atualizar(long id, [FromBody] EquipamentoRequestDto dto)
        => Ok(await _service.AtualizarAsync(id, dto));

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Deletar(long id)
    {
        await _service.DeletarAsync(id);
        return NoContent();
    }
}