using Microsoft.EntityFrameworkCore;
using NAC2_Gestao_de_estoque.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura o banco de dados em memória
builder.Services.AddDbContext<EstoqueDbContext>(options =>
    options.UseInMemoryDatabase("EstoqueDB"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();