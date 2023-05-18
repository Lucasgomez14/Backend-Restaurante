using Application.Interfaces;
using Application.Interfaces.ComandaMercaderia;
using Application.UseCase;
using Infaestructure.Command;
using Infaestructure.Persistence.Config;
using Infaestructure.Querys;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<RestauranteBD>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IComandaService, ComandaService>();
builder.Services.AddScoped<IComandaQuery, ComandaQuerys>();
builder.Services.AddScoped<IComandaCommand, ComandaCommand>();
builder.Services.AddScoped<IMercaderiaService, MercaderiaService>();
builder.Services.AddScoped<IMercaderiaQuery, MercaderiaQuery>();
builder.Services.AddScoped<IMercaderiaCommand, MercaderiaCommand>();
builder.Services.AddScoped<ITipoMercaderiaQuery, TipoMercaderiaQuery>();
builder.Services.AddScoped<ITipoMercaderiaService, TipoMercaderiaService>();
builder.Services.AddScoped<IComandaMercaderiaService, ComandaMercaderiaService>();
builder.Services.AddScoped<IComandaMercaderiaCommand, ComandaMercaderiaCommand>();
//builder.Services.AddScoped<IComandaMercaderiaQuery, ComandaMercaderiaQuery>();
builder.Services.AddScoped<IFormaDeEntregaQuery, FormaDeEntregaQuery>();

//Hola Lucas, ese error puede ser por que definiste algunos servicios con el lifetime diferentes, asegúrate de definir todo como scoped
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



//1.Debe permitir registrar la mercadería (platos, bebida o postre).
//2.Debe permitir registrar las comandas (el pedido del cliente)
//3.Debe enlistar las comandas con el detalle de los platos según la fecha que se le
//ingrese.
//4. Debe enlistar la información de la mercadería y permitir filtrar por nombre y/o tipo y
//ordenar por precio.
//5. Debe permitir modificar la información de la mercadería.
//6. Debe permitir eliminar la mercadería.
//7. Agregar búsqueda de mercadería por id
//8. Agregar búsqueda de comanda por id.

