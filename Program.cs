using Data.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//CORS

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000", "https://localhost:7042")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();

                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOptions();
builder.Services.AddSwaggerGen();


//DbContext
builder.Services.AddDbContext<agremiaciong11Context>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddScoped<Services.IEstadoPagoService, Services.EstadoPagoService>();
builder.Services.AddScoped<Services.ICobranzaService, Services.CobranzaService>();

//Service Layer

builder.Services.AddScoped<Services.IFacturaService, Services.FacturaService>();
builder.Services.AddScoped<Services.IObraSocialService, Services.ObraSocialService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//CORS
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
