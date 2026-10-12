namespace EcoControl.API.Models;

public class Equipamento
{
    public long IdEquipamento { get; set; }
    public string NomeEquipamento { get; set; } = string.Empty;
    public string Setor { get; set; } = string.Empty;
    public decimal PotenciaKw { get; set; }
    public string StatusAtual { get; set; } = "LIGADO";
    public DateTime DtCadastro { get; set; } = DateTime.Now;

    public ICollection<LeituraSensor> LeiturasSensor { get; set; } = new List<LeituraSensor>();
    public ICollection<LogDesligamento> LogsDesligamento { get; set; } = new List<LogDesligamento>();
    public ICollection<ConsumoEnergia> ConsumosEnergia { get; set; } = new List<ConsumoEnergia>();
    public ICollection<HorarioOperacao> HorariosOperacao { get; set; } = new List<HorarioOperacao>();
}