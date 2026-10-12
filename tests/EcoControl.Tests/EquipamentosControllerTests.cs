using System.Net;
using System.Text;
using Xunit;

namespace EcoControl.Tests;

public class EquipamentosControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public EquipamentosControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetEquipamentos_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/api/equipamentos");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetLogs_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/api/logsdesligamento");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetConsumo_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/api/consumoenergia");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetResumoConsumo_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/api/consumoenergia/resumo");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PostEquipamento_SemAuth_Returns401()
    {
        _client.DefaultRequestHeaders.Authorization = null;
        var json = """{"nomeEquipamento":"Teste","setor":"TI","potenciaKw":1.5,"statusAtual":"LIGADO"}""";
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/api/equipamentos", content);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetLeiturasSensor_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/api/LeiturasSensor/equipamento/1");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Health_ReturnsHttpStatusCode200()
    {
        var response = await _client.GetAsync("/health");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}