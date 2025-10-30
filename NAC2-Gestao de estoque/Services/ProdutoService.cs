using Microsoft.EntityFrameworkCore;
using NAC2_Gestao_de_estoque.Data;
using NAC2_Gestao_de_estoque.Models;
using System;

namespace NAC2_Gestao_de_estoque.Services
{

    public class ProdutoException : Exception
    {
        public ProdutoException(string message) : base(message) { }
    }

    public class ProdutoService
    {
        private readonly EstoqueDbContext _context;
        public ProdutoService(EstoqueDbContext context)
        {
            _context = context;
        }

        public bool ValidarProduto(Produto produto)
        {
            if (produto.Categoria == CategoriaProduto.PERECIVEL && string.IsNullOrWhiteSpace(produto.SKU))
                return false;
            if (string.IsNullOrWhiteSpace(produto.Nome) || produto.PrecoUnitario < 0 || produto.QuantidadeMinimaEstoque < 0)
                return false;
            return true;
        }

        public void CadastrarProduto(Produto produto, DateTime? dataValidade = null)
        {
            if (!ValidarProduto(produto))
                throw new ProdutoException("Dados do produto inválidos.");
            if (produto.Categoria == CategoriaProduto.PERECIVEL && dataValidade == null)
                throw new ProdutoException("Produto perecível deve ter data de validade.");
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public List<Produto> ProdutosAbaixoDoMinimo()
        {
            return _context.Produtos.Where(p => p.QuantidadeAtual < p.QuantidadeMinimaEstoque).ToList();
        }
    }
}