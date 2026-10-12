using EcoControl.API.Models;

namespace EcoControl.API.Repositories.Interfaces;

public interface IHorarioOperacaoRepository
{
    Task<(List<HorarioOperacao> Items, int Total)> ListarPorEquipamentoAsync(long idEquipamento, int page, int pageSize);
    Task<HorarioOperacao> CriarAsync(HorarioOperacao horario);
}