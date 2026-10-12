using EcoControl.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoControl.API.Data;

public static class SeedData
{
    public static async Task InitializeAsync(AppDbContext db)
    {
        if (await db.Equipamentos.AnyAsync())
            return;

        var compressor = new Equipamento
        {
            NomeEquipamento = "Compressor de Ar Industrial",
            Setor = "Producao",
            PotenciaKw = 15.5m,
            StatusAtual = "LIGADO"
        };

        var torno = new Equipamento
        {
            NomeEquipamento = "Torno CNC T-200",
            Setor = "Usinagem",
            PotenciaKw = 7.2m,
            StatusAtual = "OCIOSO"
        };

        var arCondicionado = new Equipamento
        {
            NomeEquipamento = "Ar-Condicionado Central",
            Setor = "Administrativo",
            PotenciaKw = 12m,
            StatusAtual = "LIGADO"
        };

        db.Equipamentos.AddRange(compressor, torno, arCondicionado);
        await db.SaveChangesAsync();

        db.HorariosOperacao.AddRange(
            new HorarioOperacao
            {
                Equipamento = compressor,
                DiaSemana = "SEG-SEX",
                HoraInicio = 7m,
                HoraFim = 18m,
                TempoMaxOcioso = 20
            },
            new HorarioOperacao
            {
                Equipamento = torno,
                DiaSemana = "SEG-SEX",
                HoraInicio = 8m,
                HoraFim = 17.5m,
                TempoMaxOcioso = 15
            }
        );

        db.LeiturasSensor.AddRange(
            new LeituraSensor
            {
                Equipamento = compressor,
                EquipamentoAtivo = "S",
                ConsumoInstKw = 15.2m
            },
            new LeituraSensor
            {
                Equipamento = torno,
                EquipamentoAtivo = "N",
                ConsumoInstKw = 0.08m
            }
        );

        db.LogsDesligamento.Add(
            new LogDesligamento
            {
                Equipamento = torno,
                Motivo = "Equipamento ocioso acima do limite configurado",
                TipoDesligamento = "AUTOMATICO"
            }
        );

        db.ConsumosEnergia.AddRange(
            new ConsumoEnergia
            {
                Equipamento = compressor,
                KwhConsumido = 15.2m,
                KwhEconomizado = 0m,
                PeriodoHoras = 1m,
                CustoEstimadoR = 0m
            },
            new ConsumoEnergia
            {
                Equipamento = torno,
                KwhConsumido = 0m,
                KwhEconomizado = 2.7m,
                PeriodoHoras = 0.37m,
                CustoEstimadoR = 1.62m
            }
        );

        await db.SaveChangesAsync();
    }
}
