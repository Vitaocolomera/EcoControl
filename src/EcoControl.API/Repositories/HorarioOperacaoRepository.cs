using EcoControl.API.Data;
using EcoControl.API.Models;
using EcoControl.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoControl.API.Repositories;

public class HorarioOperacaoRepository : IHorarioOperacaoRepository
{
    private readonly AppDbContext _ctx;
    public HorarioOperacaoRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<(List<HorarioOperacao> Items, int Total)> ListarPorEquipamentoAsync(long idEquipamento, int page, int pageSize)
    {
        var query = _ctx.HorariosOperacao
            .Include(h => h.Equipamento)
            .Where(h => h.IdEquipamento == idEquipamento)
            .OrderBy(h => h.IdHorario);

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, total);
    }

    public async Task<HorarioOperacao> CriarAsync(HorarioOperacao h)
    {
        _ctx.HorariosOperacao.Add(h);
        await _ctx.SaveChangesAsync();
        return h;
    }
}