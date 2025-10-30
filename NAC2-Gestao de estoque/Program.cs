using Microsoft.EntityFrameworkCore;
using NAC2_Gestao_de_estoque.Data;
using NAC2_Gestao_de_estoque.Models;
using NAC2_Gestao_de_estoque.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura o banco de dados em memória
builder.Services.AddDbContext<EstoqueDbContext>(options =>
    options.UseInMemoryDatabase("EstoqueDB"));
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<MovimentacaoService>();
builder.Services.AddScoped<RelatorioService>();

var app = builder.Build();

// Adiciona dados de exemplo ao banco
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EstoqueDbContext>();
    db.Produtos.AddRange(new List<Produto>
    {
        new Produto { SKU = "SKU001", Nome = "Leite", Categoria = CategoriaProduto.PERECIVEL, PrecoUnitario = 5.5m, QuantidadeMinimaEstoque = 10, QuantidadeAtual = 8, DataCriacao = DateTime.Now },
        new Produto { SKU = "SKU002", Nome = "Arroz", Categoria = CategoriaProduto.NAO_PERECIVEL, PrecoUnitario = 3.2m, QuantidadeMinimaEstoque = 20, QuantidadeAtual = 25, DataCriacao = DateTime.Now },
        new Produto { SKU = "SKU003", Nome = "Iogurte", Categoria = CategoriaProduto.PERECIVEL, PrecoUnitario = 4.0m, QuantidadeMinimaEstoque = 5, QuantidadeAtual = 2, DataCriacao = DateTime.Now },
        new Produto { SKU = "SKU004", Nome = "Pão", Categoria = CategoriaProduto.PERECIVEL, PrecoUnitario = 2.5m, QuantidadeMinimaEstoque = 15, QuantidadeAtual = 20, DataCriacao = DateTime.Now },
        new Produto { SKU = "SKU005", Nome = "Feijão", Categoria = CategoriaProduto.NAO_PERECIVEL, PrecoUnitario = 7.0m, QuantidadeMinimaEstoque = 10, QuantidadeAtual = 9, DataCriacao = DateTime.Now },
        new Produto { SKU = "SKU006", Nome = "Queijo", Categoria = CategoriaProduto.PERECIVEL, PrecoUnitario = 12.0m, QuantidadeMinimaEstoque = 5, QuantidadeAtual = 6, DataCriacao = DateTime.Now }
    });
    db.Movimentacoes.AddRange(new List<MovimentacaoEstoque>
    {
        new MovimentacaoEstoque { Tipo = TipoMovimentacao.ENTRADA, Quantidade = 10, SKUProduto = "SKU001", Lote = "L001", DataValidade = DateTime.Now.AddDays(5), DataMovimentacao = DateTime.Now },
        new MovimentacaoEstoque { Tipo = TipoMovimentacao.ENTRADA, Quantidade = 20, SKUProduto = "SKU002", DataMovimentacao = DateTime.Now },
        new MovimentacaoEstoque { Tipo = TipoMovimentacao.ENTRADA, Quantidade = 5, SKUProduto = "SKU003", Lote = "L002", DataValidade = DateTime.Now.AddDays(3), DataMovimentacao = DateTime.Now },
        new MovimentacaoEstoque { Tipo = TipoMovimentacao.ENTRADA, Quantidade = 20, SKUProduto = "SKU004", Lote = "L003", DataValidade = DateTime.Now.AddDays(2), DataMovimentacao = DateTime.Now },
        new MovimentacaoEstoque { Tipo = TipoMovimentacao.ENTRADA, Quantidade = 10, SKUProduto = "SKU005", DataMovimentacao = DateTime.Now },
        new MovimentacaoEstoque { Tipo = TipoMovimentacao.ENTRADA, Quantidade = 6, SKUProduto = "SKU006", Lote = "L004", DataValidade = DateTime.Now.AddDays(10), DataMovimentacao = DateTime.Now }
    });
    db.SaveChanges();
}

app.MapGet("/", () => "Hello World!");

// Endpoint para testar RelatorioService
app.MapGet("/relatorio/estoque-total", (RelatorioService relatorioService) =>
{
    return relatorioService.ValorTotalEstoque();
});

app.MapGet("/relatorio/produtos-vencendo", (RelatorioService relatorioService) =>
{
    return relatorioService.ProdutosVencendoEm7Dias();
});

app.MapGet("/relatorio/produtos-abaixo-minimo", (RelatorioService relatorioService) =>
{
    return relatorioService.ProdutosAbaixoDoMinimo();
});

app.Run();