using Microsoft.EntityFrameworkCore;
using API_GerenciamentoProdutos.Entidades;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<API_GerenciamentoProdutosContexto>(opt =>
    opt.UseSqlServer("JamsoftProvaCSharp"));

builder.Services.AddEndpointsApiExplorer();

//configura uma url fixa
builder.WebHost.UseUrls("http://localhost:8080/");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
