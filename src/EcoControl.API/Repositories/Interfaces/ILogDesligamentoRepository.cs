using EcoControl.API.Models;

namespace EcoControl.API.Repositories.Interfaces;

public interface ILogDesligamentoRepository
{
    Task<(List<LogDesligamento> Items, int Total)> ListarPaginadoAsync(int page, int pageSize, long? idEquipamento, string? tipo);
}