namespace EcoControl.API.Models;

public class LogDesligamento
{
    public long IdLog { get; set; }
    public long IdEquipamento { get; set; }
    public DateTime DtDesligamento { get; set; } = DateTime.Now;
    public string Motivo { get; set; } = string.Empty;
    public string TipoDesligamento { get; set; } = "AUTOMATICO";

    public Equipamento Equipamento { get; set; } = null!;
}