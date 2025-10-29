using Microsoft.AspNetCore.Mvc;
using NAC2_Gestao_de_estoque.Data;
using NAC2_Gestao_de_estoque.Models;
using NAC2_Gestao_de_estoque.Models.Enums;

namespace NAC2_Gestao_de_estoque.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly EstoqueDbContext _db;

    public ProdutosController(EstoqueDbContext db)
    {
        _db = db;
    }

    // Lista produtos
    [HttpGet]
    public IActionResult Get() => Ok(_db.Produtos.ToList());

    // Adiciona produto de teste
    [HttpPost("seed")]
    public IActionResult Seed()
    {
        var produto = new Produto
        {
            CodigoSKU = "ABC123",
            Nome = "Leite Integral",
            Categoria = CategoriaProduto.PERECIVEL,
            PrecoUnitario = 5.99m,
            QuantidadeMinima = 10
        };

        _db.Produtos.Add(produto);
        _db.SaveChanges();

        return Ok("Produto de teste adicionado!");
    }
}
