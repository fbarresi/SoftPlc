using Microsoft.OpenApi.Models;
using SoftPlc.Exceptions;
using SoftPlc.Interfaces;
using SoftPlc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IPlcService, PlcService>();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "SoftPlc API",
        Description = "REST API to SoftPlc",
        Contact = new OpenApiContact
        {
            Name = "Federico Barresi",
            Email = string.Empty,
            Url = new Uri("https://github.com/fbarresi/SoftPlc")
        },
        License = new OpenApiLicense()
        {
            Name = "MIT",
            Url = new Uri("https://github.com/fbarresi/SoftPlc/blob/master/LICENSE")
        }
    });
});
builder.Services.AddExceptionHandler<DbAccessExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SoftPlc API V1");
    c.RoutePrefix = "";
    c.EnableTryItOutByDefault();
});

app.UseCors(options => options.AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler();
app.MapControllers();

var plcService = app.Services.GetService<IPlcService>();

app.Run();