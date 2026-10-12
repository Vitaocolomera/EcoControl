using EcoControl.API.Models;

namespace EcoControl.API.Repositories.Interfaces;

public interface IConsumoEnergiaRepository
{
    Task<(List<ConsumoEnergia> Items, int Total)> ListarPaginadoAsync(int page, int pageSize, long? idEquipamento);
    Task<List<ConsumoEnergia>> ResumoGeralAsync();
}