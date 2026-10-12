using EcoControl.API.Models;

namespace EcoControl.API.Repositories.Interfaces;

public interface ILeituraSensorRepository
{
    Task<(List<LeituraSensor> Items, int Total)> ListarPorEquipamentoPaginadoAsync(long idEquipamento, int page, int pageSize);
    Task<LeituraSensor> CriarAsync(LeituraSensor leitura);
}