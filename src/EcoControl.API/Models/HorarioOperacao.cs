namespace EcoControl.API.Models;

public class HorarioOperacao
{
    public long IdHorario { get; set; }
    public long IdEquipamento { get; set; }
    public string DiaSemana { get; set; } = string.Empty;
    public decimal HoraInicio { get; set; }
    public decimal HoraFim { get; set; }
    public int TempoMaxOcioso { get; set; } = 30;

    public Equipamento Equipamento { get; set; } = null!;
}