using EcoControl.API.Data;
using EcoControl.API.Models;
using EcoControl.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoControl.API.Repositories;

public class LeituraSensorRepository : ILeituraSensorRepository
{
    private readonly AppDbContext _ctx;
    public LeituraSensorRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<(List<LeituraSensor> Items, int Total)> ListarPorEquipamentoPaginadoAsync(long idEquipamento, int page, int pageSize)
    {
        var query = _ctx.LeiturasSensor
            .Include(l => l.Equipamento)
            .Where(l => l.IdEquipamento == idEquipamento)
            .OrderByDescending(l => l.DtLeitura);
        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, total);
    }

    public async Task<LeituraSensor> CriarAsync(LeituraSensor l)
    {
        _ctx.LeiturasSensor.Add(l);
        await _ctx.SaveChangesAsync();
        return l;
    }
}