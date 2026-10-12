using EcoControl.API.DTOs;

namespace EcoControl.API.Services.Interfaces;

public interface ILeituraSensorService
{
    Task<PagedResultDto<LeituraSensorResponseDto>> ListarPorEquipamentoAsync(long idEquipamento, int page, int pageSize);
    Task<LeituraSensorResponseDto> RegistrarLeituraAsync(LeituraSensorRequestDto dto);
}