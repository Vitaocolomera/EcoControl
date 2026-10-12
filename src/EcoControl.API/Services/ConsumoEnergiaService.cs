using EcoControl.API.DTOs;
using EcoControl.API.Repositories.Interfaces;
using EcoControl.API.Services.Interfaces;

namespace EcoControl.API.Services;

public class ConsumoEnergiaService : IConsumoEnergiaService
{
    private readonly IConsumoEnergiaRepository _repo;
    public ConsumoEnergiaService(IConsumoEnergiaRepository repo) => _repo = repo;

    public async Task<PagedResultDto<ConsumoEnergiaResponseDto>> ListarAsync(int page, int pageSize, long? idEquipamento)
    {
        var (items, total) = await _repo.ListarPaginadoAsync(page, pageSize, idEquipamento);
        return new PagedResultDto<ConsumoEnergiaResponseDto>
        {
            Page = page,
            PageSize = pageSize,
            TotalItems = total,
            Data = items.Select(c => new ConsumoEnergiaResponseDto
            {
                IdConsumo = c.IdConsumo,
                IdEquipamento = c.IdEquipamento,
                NomeEquipamento = c.Equipamento?.NomeEquipamento ?? "",
                DtRegistro = c.DtRegistro,
                KwhConsumido = c.KwhConsumido,
                KwhEconomizado = c.KwhEconomizado,
                PeriodoHoras = c.PeriodoHoras,
                CustoEstimadoR = c.CustoEstimadoR
            }).ToList()
        };
    }

    public async Task<List<ResumoConsumoDto>> ResumoGeralAsync()
    {
        var dados = await _repo.ResumoGeralAsync();
        return dados.GroupBy(c => c.IdEquipamento).Select(g => new ResumoConsumoDto
        {
            NomeEquipamento = g.First().Equipamento?.NomeEquipamento ?? "",
            Setor = g.First().Equipamento?.Setor ?? "",
            TotalKwhConsumido = g.Sum(c => c.KwhConsumido),
            TotalKwhEconomizado = g.Sum(c => c.KwhEconomizado),
            TotalCustoR = g.Sum(c => c.CustoEstimadoR)
        }).ToList();
    }
}