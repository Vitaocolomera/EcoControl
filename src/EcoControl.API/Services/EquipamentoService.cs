using EcoControl.API.DTOs;
using EcoControl.API.Exceptions;
using EcoControl.API.Models;
using EcoControl.API.Repositories.Interfaces;
using EcoControl.API.Services.Interfaces;

namespace EcoControl.API.Services;

public class EquipamentoService : IEquipamentoService
{
    private readonly IEquipamentoRepository _repo;
    public EquipamentoService(IEquipamentoRepository repo) => _repo = repo;

    public async Task<PagedResultDto<EquipamentoResponseDto>> ListarAsync(int page, int pageSize, string? setor, string? status)
    {
        var (items, total) = await _repo.ListarPaginadoAsync(page, pageSize, setor, status);
        return new PagedResultDto<EquipamentoResponseDto>
        {
            Page = page,
            PageSize = pageSize,
            TotalItems = total,
            Data = items.Select(MapToDto).ToList()
        };
    }

    public async Task<EquipamentoResponseDto> BuscarPorIdAsync(long id)
    {
        var e = await _repo.BuscarPorIdAsync(id)
            ?? throw new NotFoundException($"Equipamento com ID {id} não encontrado.");
        return MapToDto(e);
    }

    public async Task<EquipamentoResponseDto> CriarAsync(EquipamentoRequestDto dto)
    {
        var e = new Equipamento
        {
            NomeEquipamento = dto.NomeEquipamento,
            Setor = dto.Setor,
            PotenciaKw = dto.PotenciaKw,
            StatusAtual = dto.StatusAtual.ToUpper()
        };
        return MapToDto(await _repo.CriarAsync(e));
    }

    public async Task<EquipamentoResponseDto> AtualizarAsync(long id, EquipamentoRequestDto dto)
    {
        var e = await _repo.BuscarPorIdAsync(id)
            ?? throw new NotFoundException($"Equipamento com ID {id} não encontrado.");
        e.NomeEquipamento = dto.NomeEquipamento;
        e.Setor = dto.Setor;
        e.PotenciaKw = dto.PotenciaKw;
        e.StatusAtual = dto.StatusAtual.ToUpper();
        return MapToDto(await _repo.AtualizarAsync(e));
    }

    public async Task DeletarAsync(long id)
    {
        if (!await _repo.ExisteAsync(id))
            throw new NotFoundException($"Equipamento com ID {id} não encontrado.");
        await _repo.DeletarAsync(id);
    }

    private static EquipamentoResponseDto MapToDto(Equipamento e) => new()
    {
        IdEquipamento = e.IdEquipamento,
        NomeEquipamento = e.NomeEquipamento,
        Setor = e.Setor,
        PotenciaKw = e.PotenciaKw,
        StatusAtual = e.StatusAtual,
        DtCadastro = e.DtCadastro
    };
}