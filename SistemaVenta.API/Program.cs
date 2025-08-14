using SistemaVenta.IOC;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "MicroservicioClean API",
        Version = "v1",
        Description = "API del microservicio con arquitectura Clean"
    });
});

builder.Services.InyectarDependencias(builder.Configuration);

var app = builder.Build();

// Middleware de Swagger (siempre habilitado)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MicroservicioClean API v1");
});

app.UseAuthorization();
app.MapControllers();
app.Run();
