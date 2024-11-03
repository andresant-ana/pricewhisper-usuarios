using Application.Interfaces;
using Application.Services;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.ExternalServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Configurar serviços
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PriceWhisper Usuários API",
        Version = "v1",
        Description = "API para gerenciamento de usuários e empresas.",
        Contact = new OpenApiContact
        {
            Name = "Seu Nome",
            Email = "seuemail@example.com"
        }
    });

    // Incluir comentários XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddDbContext<OracleDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Registro de repositórios e serviços
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ICNPJaService, CNPJaService>();

// Configuração do CNPJaService com HttpClient
builder.Services.AddHttpClient<ICNPJaService, CNPJaService>()
    .ConfigureHttpClient(client =>
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "cae710ff-9e16-45d7-86f7-5ca5941da93d-68647162-57f9-4531-be42-98068541137d");
        client.BaseAddress = new Uri("https://api.cnpja.com/");
    });

var app = builder.Build();

// Configure o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();