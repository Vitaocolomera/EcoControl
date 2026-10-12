using EcoControl.API.DTOs;
using EcoControl.API.Exceptions;
using EcoControl.API.Models;
using EcoControl.API.Repositories.Interfaces;
using EcoControl.API.Services.Interfaces;

namespace EcoControl.API.Services;

public class LeituraSensorService : ILeituraSensorService
{
    private readonly ILeituraSensorRepository _repo;
    private readonly IEquipamentoRepository _equipRepo;
    public LeituraSensorService(ILeituraSensorRepository repo, IEquipamentoRepository equipRepo)
    { _repo = repo; _equipRepo = equipRepo; }

    public async Task<PagedResultDto<LeituraSensorResponseDto>> ListarPorEquipamentoAsync(long idEquipamento, int page, int pageSize)
    {
        var (items, total) = await _repo.ListarPorEquipamentoPaginadoAsync(idEquipamento, page, pageSize);
        return new PagedResultDto<LeituraSensorResponseDto>
        {
            Page = page,
            PageSize = pageSize,
            TotalItems = total,
            Data = items.Select(l => new LeituraSensorResponseDto
            {
                IdLeitura = l.IdLeitura,
                IdEquipamento = l.IdEquipamento,
                NomeEquipamento = l.Equipamento?.NomeEquipamento ?? "",
                DtLeitura = l.DtLeitura,
                EquipamentoAtivo = l.EquipamentoAtivo,
                ConsumoInstKw = l.ConsumoInstKw
            }).ToList()
        };
    }

    public async Task<LeituraSensorResponseDto> RegistrarLeituraAsync(LeituraSensorRequestDto dto)
    {
        var equip = await _equipRepo.BuscarPorIdAsync(dto.IdEquipamento)
            ?? throw new NotFoundException($"Equipamento {dto.IdEquipamento} não encontrado.");
        var leitura = new LeituraSensor
        {
            IdEquipamento = dto.IdEquipamento,
            EquipamentoAtivo = dto.EquipamentoAtivo.ToUpper(),
            ConsumoInstKw = dto.ConsumoInstKw
        };
        var criada = await _repo.CriarAsync(leitura);
        return new LeituraSensorResponseDto
        {
            IdLeitura = criada.IdLeitura,
            IdEquipamento = criada.IdEquipamento,
            NomeEquipamento = equip.NomeEquipamento,
            DtLeitura = criada.DtLeitura,
            EquipamentoAtivo = criada.EquipamentoAtivo,
            ConsumoInstKw = criada.ConsumoInstKw
        };
    }
}