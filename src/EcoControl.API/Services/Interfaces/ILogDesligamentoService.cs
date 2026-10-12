using EcoControl.API.DTOs;

namespace EcoControl.API.Services.Interfaces;

public interface ILogDesligamentoService
{
    Task<PagedResultDto<LogDesligamentoResponseDto>> ListarAsync(int page, int pageSize, long? idEquipamento, string? tipo);
}