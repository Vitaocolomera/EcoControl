namespace EcoControl.API.Models;

public class LeituraSensor
{
    public long IdLeitura { get; set; }
    public long IdEquipamento { get; set; }
    public DateTime DtLeitura { get; set; } = DateTime.Now;
    public string EquipamentoAtivo { get; set; } = "N";
    public decimal ConsumoInstKw { get; set; } = 0;

    public Equipamento Equipamento { get; set; } = null!;
}