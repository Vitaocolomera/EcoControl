using System.ComponentModel.DataAnnotations;

namespace EcoControl.API.DTOs;

public class LeituraSensorRequestDto
{
    [Required]
    public long IdEquipamento { get; set; }

    [RegularExpression("S|N", ErrorMessage = "EquipamentoAtivo deve ser 'S' ou 'N'.")]
    public string EquipamentoAtivo { get; set; } = "N";

    [Range(0, 99999.999)]
    public decimal ConsumoInstKw { get; set; } = 0;
}

public class LeituraSensorResponseDto
{
    public long IdLeitura { get; set; }
    public long IdEquipamento { get; set; }
    public string NomeEquipamento { get; set; } = string.Empty;
    public DateTime DtLeitura { get; set; }
    public string EquipamentoAtivo { get; set; } = string.Empty;
    public decimal ConsumoInstKw { get; set; }
}