namespace EcoControl.API.DTOs;

public class LogDesligamentoResponseDto
{
    public long IdLog { get; set; }
    public long IdEquipamento { get; set; }
    public string NomeEquipamento { get; set; } = string.Empty;
    public DateTime DtDesligamento { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public string TipoDesligamento { get; set; } = string.Empty;
}

public class ConsumoEnergiaResponseDto
{
    public long IdConsumo { get; set; }
    public long IdEquipamento { get; set; }
    public string NomeEquipamento { get; set; } = string.Empty;
    public DateTime DtRegistro { get; set; }
    public decimal KwhConsumido { get; set; }
    public decimal KwhEconomizado { get; set; }
    public decimal PeriodoHoras { get; set; }
    public decimal CustoEstimadoR { get; set; }
}

public class ResumoConsumoDto
{
    public string NomeEquipamento { get; set; } = string.Empty;
    public string Setor { get; set; } = string.Empty;
    public decimal TotalKwhConsumido { get; set; }
    public decimal TotalKwhEconomizado { get; set; }
    public decimal TotalCustoR { get; set; }
}