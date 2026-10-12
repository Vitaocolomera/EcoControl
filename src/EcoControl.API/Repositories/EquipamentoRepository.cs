using EcoControl.API.Data;
using EcoControl.API.Models;
using EcoControl.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoControl.API.Repositories;

public class EquipamentoRepository : IEquipamentoRepository
{
    private readonly AppDbContext _ctx;
    public EquipamentoRepository(AppDbContext ctx) => _ctx = ctx;

    public async Task<(List<Equipamento> Items, int Total)> ListarPaginadoAsync(int page, int pageSize, string? setor, string? status)
    {
        var query = _ctx.Equipamentos.AsQueryable();
        if (!string.IsNullOrWhiteSpace(setor))
            query = query.Where(e => e.Setor.ToUpper() == setor.ToUpper());
        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(e => e.StatusAtual.ToUpper() == status.ToUpper());
        var total = await query.CountAsync();
        var items = await query.OrderBy(e => e.IdEquipamento)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, total);
    }

    public async Task<Equipamento?> BuscarPorIdAsync(long id) =>
        await _ctx.Equipamentos.FindAsync(id);

    public async Task<Equipamento> CriarAsync(Equipamento e)
    {
        _ctx.Equipamentos.Add(e);
        await _ctx.SaveChangesAsync();
        return e;
    }

    public async Task<Equipamento> AtualizarAsync(Equipamento e)
    {
        _ctx.Equipamentos.Update(e);
        await _ctx.SaveChangesAsync();
        return e;
    }

    public async Task DeletarAsync(long id)
    {
        var e = await _ctx.Equipamentos.FindAsync(id);
        if (e != null) { _ctx.Equipamentos.Remove(e); await _ctx.SaveChangesAsync(); }
    }

    public async Task<bool> ExisteAsync(long id) =>
        await _ctx.Equipamentos.AnyAsync(e => e.IdEquipamento == id);
}