using EcoControl.API.Data;
using EcoControl.API.Middleware;
using EcoControl.API.Repositories;
using EcoControl.API.Repositories.Interfaces;
using EcoControl.API.Services;
using EcoControl.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var databaseProvider = builder.Configuration["Database:Provider"] ?? "Oracle";

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (databaseProvider.Equals("InMemory", StringComparison.OrdinalIgnoreCase))
        options.UseInMemoryDatabase("EcoControl");
    else
        options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"));
});

builder.Services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();
builder.Services.AddScoped<ILeituraSensorRepository, LeituraSensorRepository>();
builder.Services.AddScoped<ILogDesligamentoRepository, LogDesligamentoRepository>();
builder.Services.AddScoped<IConsumoEnergiaRepository, ConsumoEnergiaRepository>();
builder.Services.AddScoped<IHorarioOperacaoRepository, HorarioOperacaoRepository>();

builder.Services.AddScoped<IEquipamentoService, EquipamentoService>();
builder.Services.AddScoped<ILeituraSensorService, LeituraSensorService>();
builder.Services.AddScoped<ILogDesligamentoService, LogDesligamentoService>();
builder.Services.AddScoped<IConsumoEnergiaService, ConsumoEnergiaService>();
builder.Services.AddScoped<IHorarioOperacaoService, HorarioOperacaoService>();

builder.Services.AddAuthentication("BasicAuth")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("BasicAuth", null);

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EcoControl API", Version = "v1" });
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Name = "Authorization"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "basic" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (databaseProvider.Equals("InMemory", StringComparison.OrdinalIgnoreCase))
        await db.Database.EnsureCreatedAsync();
    else if (app.Configuration.GetValue<bool>("Database:ApplyMigrations"))
        await db.Database.MigrateAsync();

    if (app.Configuration.GetValue<bool>("Database:SeedData"))
        await SeedData.InitializeAsync(db);
}


app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");
app.Run();

public partial class Program { }
