using EcoControl.API.Data;
using EcoControl.API.Models;
using EcoControl.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoControl.API.Repositories;

public class LogDesligamentoRepository : ILogDesligamentoRepository
{
    private readonly AppDbContext _ctx;
    public LogDesligamentoRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<(List<LogDesligamento> Items, int Total)> ListarPaginadoAsync(int page, int pageSize, long? idEquipamento, string? tipo)
    {
        var query = _ctx.LogsDesligamento.Include(l => l.Equipamento).AsQueryable();
        if (idEquipamento.HasValue)
            query = query.Where(l => l.IdEquipamento == idEquipamento.Value);
        if (!string.IsNullOrWhiteSpace(tipo))
            query = query.Where(l => l.TipoDesligamento.ToUpper() == tipo.ToUpper());
        var total = await query.CountAsync();
        var items = await query.OrderByDescending(l => l.DtDesligamento)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, total);
    }
}