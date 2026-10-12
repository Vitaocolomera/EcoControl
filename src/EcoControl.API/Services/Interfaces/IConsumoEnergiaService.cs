using EcoControl.API.DTOs;

namespace EcoControl.API.Services.Interfaces;

public interface IConsumoEnergiaService
{
    Task<PagedResultDto<ConsumoEnergiaResponseDto>> ListarAsync(int page, int pageSize, long? idEquipamento);
    Task<List<ResumoConsumoDto>> ResumoGeralAsync();
}