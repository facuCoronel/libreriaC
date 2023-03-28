using Api.Producto.Servicio.Interfaz;
using Api.Producto.Servicio.Implementacion;
using AppLibreria;
using Bbl.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Acceso a Datos
//Acceso a base de datos:
builder.Services.AddSingleton<ConexionBd>();
#endregion

#region Inyeccion de Dependencias:
//Inyeccion de dependencias:
builder.Services.AddScoped<IServicioProducto, ServicioProducto>();


#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
