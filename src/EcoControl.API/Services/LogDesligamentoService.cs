using EcoControl.API.DTOs;
using EcoControl.API.Repositories.Interfaces;
using EcoControl.API.Services.Interfaces;

namespace EcoControl.API.Services;

public class LogDesligamentoService : ILogDesligamentoService
{
    private readonly ILogDesligamentoRepository _repo;
    public LogDesligamentoService(ILogDesligamentoRepository repo) => _repo = repo;

    public async Task<PagedResultDto<LogDesligamentoResponseDto>> ListarAsync(int page, int pageSize, long? idEquipamento, string? tipo)
    {
        var (items, total) = await _repo.ListarPaginadoAsync(page, pageSize, idEquipamento, tipo);
        return new PagedResultDto<LogDesligamentoResponseDto>
        {
            Page = page,
            PageSize = pageSize,
            TotalItems = total,
            Data = items.Select(l => new LogDesligamentoResponseDto
            {
                IdLog = l.IdLog,
                IdEquipamento = l.IdEquipamento,
                NomeEquipamento = l.Equipamento?.NomeEquipamento ?? "",
                DtDesligamento = l.DtDesligamento,
                Motivo = l.Motivo,
                TipoDesligamento = l.TipoDesligamento
            }).ToList()
        };
    }
}