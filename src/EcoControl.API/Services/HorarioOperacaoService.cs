using EcoControl.API.DTOs;
using EcoControl.API.Exceptions;
using EcoControl.API.Models;
using EcoControl.API.Repositories.Interfaces;
using EcoControl.API.Services.Interfaces;

namespace EcoControl.API.Services;

public class HorarioOperacaoService : IHorarioOperacaoService
{
    private readonly IHorarioOperacaoRepository _repo;
    private readonly IEquipamentoRepository _equipRepo;

    public HorarioOperacaoService(IHorarioOperacaoRepository repo, IEquipamentoRepository equipRepo)
    {
        _repo = repo;
        _equipRepo = equipRepo;
    }

    public async Task<PagedResultDto<HorarioOperacaoResponseDto>> ListarPorEquipamentoAsync(long idEquipamento, int page, int pageSize)
    {
        var (items, total) = await _repo.ListarPorEquipamentoAsync(idEquipamento, page, pageSize);
        return new PagedResultDto<HorarioOperacaoResponseDto>
        {
            Page = page,
            PageSize = pageSize,
            TotalItems = total,
            Data = items.Select(h => new HorarioOperacaoResponseDto
            {
                IdHorario = h.IdHorario,
                IdEquipamento = h.IdEquipamento,
                NomeEquipamento = h.Equipamento?.NomeEquipamento ?? "",
                DiaSemana = h.DiaSemana,
                HoraInicio = h.HoraInicio,
                HoraFim = h.HoraFim,
                TempoMaxOcioso = h.TempoMaxOcioso
            }).ToList()
        };
    }

    public async Task<HorarioOperacaoResponseDto> CriarAsync(HorarioOperacaoRequestDto dto)
    {
        var equip = await _equipRepo.BuscarPorIdAsync(dto.IdEquipamento)
            ?? throw new NotFoundException($"Equipamento {dto.IdEquipamento} não encontrado.");

        var h = new HorarioOperacao
        {
            IdEquipamento = dto.IdEquipamento,
            DiaSemana = dto.DiaSemana,
            HoraInicio = dto.HoraInicio,
            HoraFim = dto.HoraFim,
            TempoMaxOcioso = dto.TempoMaxOcioso
        };

        var criado = await _repo.CriarAsync(h);
        return new HorarioOperacaoResponseDto
        {
            IdHorario = criado.IdHorario,
            IdEquipamento = criado.IdEquipamento,
            NomeEquipamento = equip.NomeEquipamento,
            DiaSemana = criado.DiaSemana,
            HoraInicio = criado.HoraInicio,
            HoraFim = criado.HoraFim,
            TempoMaxOcioso = criado.TempoMaxOcioso
        };
    }
}