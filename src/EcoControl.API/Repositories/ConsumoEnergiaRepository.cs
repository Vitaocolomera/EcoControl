using EcoControl.API.Data;
using EcoControl.API.Models;
using EcoControl.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoControl.API.Repositories;

public class ConsumoEnergiaRepository : IConsumoEnergiaRepository
{
    private readonly AppDbContext _ctx;
    public ConsumoEnergiaRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<(List<ConsumoEnergia> Items, int Total)> ListarPaginadoAsync(int page, int pageSize, long? idEquipamento)
    {
        var query = _ctx.ConsumosEnergia.Include(c => c.Equipamento).AsQueryable();
        if (idEquipamento.HasValue)
            query = query.Where(c => c.IdEquipamento == idEquipamento.Value);
        var total = await query.CountAsync();
        var items = await query.OrderByDescending(c => c.DtRegistro)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, total);
    }

    public async Task<List<ConsumoEnergia>> ResumoGeralAsync() =>
        await _ctx.ConsumosEnergia.Include(c => c.Equipamento).ToListAsync();
}