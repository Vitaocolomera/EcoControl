using System.ComponentModel.DataAnnotations;

namespace EcoControl.API.DTOs;

public class HorarioOperacaoRequestDto
{
    [Required]
    public long IdEquipamento { get; set; }

    [Required]
    [MaxLength(15)]
    public string DiaSemana { get; set; } = string.Empty;

    [Required]
    [Range(0, 23.99)]
    public decimal HoraInicio { get; set; }

    [Required]
    [Range(0, 23.99)]
    public decimal HoraFim { get; set; }

    [Range(1, 999)]
    public int TempoMaxOcioso { get; set; } = 30;
}

public class HorarioOperacaoResponseDto
{
    public long IdHorario { get; set; }
    public long IdEquipamento { get; set; }
    public string NomeEquipamento { get; set; } = string.Empty;
    public string DiaSemana { get; set; } = string.Empty;
    public decimal HoraInicio { get; set; }
    public decimal HoraFim { get; set; }
    public int TempoMaxOcioso { get; set; }
}