using EcoControl.API.DTOs;

namespace EcoControl.API.Services.Interfaces;

public interface IHorarioOperacaoService
{
    Task<PagedResultDto<HorarioOperacaoResponseDto>> ListarPorEquipamentoAsync(long idEquipamento, int page, int pageSize);
    Task<HorarioOperacaoResponseDto> CriarAsync(HorarioOperacaoRequestDto dto);
}