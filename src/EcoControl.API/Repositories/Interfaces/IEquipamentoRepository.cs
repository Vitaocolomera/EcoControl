using EcoControl.API.Models;

namespace EcoControl.API.Repositories.Interfaces;

public interface IEquipamentoRepository
{
    Task<(List<Equipamento> Items, int Total)> ListarPaginadoAsync(int page, int pageSize, string? setor, string? status);
    Task<Equipamento?> BuscarPorIdAsync(long id);
    Task<Equipamento> CriarAsync(Equipamento equipamento);
    Task<Equipamento> AtualizarAsync(Equipamento equipamento);
    Task DeletarAsync(long id);
    Task<bool> ExisteAsync(long id);
}