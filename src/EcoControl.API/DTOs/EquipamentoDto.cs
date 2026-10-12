using System.ComponentModel.DataAnnotations;

namespace EcoControl.API.DTOs;

public class EquipamentoRequestDto
{
    [Required(ErrorMessage = "Nome do equipamento é obrigatório.")]
    [MaxLength(100)]
    public string NomeEquipamento { get; set; } = string.Empty;

    [Required(ErrorMessage = "Setor é obrigatório.")]
    [MaxLength(80)]
    public string Setor { get; set; } = string.Empty;

    [Required(ErrorMessage = "Potência KW é obrigatória.")]
    [Range(0.01, 9999999.99, ErrorMessage = "Potência deve ser positiva.")]
    public decimal PotenciaKw { get; set; }

    [RegularExpression("LIGADO|OCIOSO|DESLIGADO",
        ErrorMessage = "Status deve ser LIGADO, OCIOSO ou DESLIGADO.")]
    public string StatusAtual { get; set; } = "DESLIGADO";
}

public class EquipamentoResponseDto
{
    public long IdEquipamento { get; set; }
    public string NomeEquipamento { get; set; } = string.Empty;
    public string Setor { get; set; } = string.Empty;
    public decimal PotenciaKw { get; set; }
    public string StatusAtual { get; set; } = string.Empty;
    public DateTime DtCadastro { get; set; }
}