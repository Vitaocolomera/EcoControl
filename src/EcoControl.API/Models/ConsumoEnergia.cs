namespace EcoControl.API.Models;

public class ConsumoEnergia
{
    public long IdConsumo { get; set; }
    public long IdEquipamento { get; set; }
    public DateTime DtRegistro { get; set; } = DateTime.Now;
    public decimal KwhConsumido { get; set; } = 0;
    public decimal KwhEconomizado { get; set; } = 0;
    public decimal PeriodoHoras { get; set; } = 0;
    public decimal CustoEstimadoR { get; set; } = 0;

    public Equipamento Equipamento { get; set; } = null!;
}