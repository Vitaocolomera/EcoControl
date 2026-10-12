using EcoControl.API.DTOs;

namespace EcoControl.API.Services.Interfaces;

public interface IEquipamentoService
{
    Task<PagedResultDto<EquipamentoResponseDto>> ListarAsync(int page, int pageSize, string? setor, string? status);
    Task<EquipamentoResponseDto> BuscarPorIdAsync(long id);
    Task<EquipamentoResponseDto> CriarAsync(EquipamentoRequestDto dto);
    Task<EquipamentoResponseDto> AtualizarAsync(long id, EquipamentoRequestDto dto);
    Task DeletarAsync(long id);
}