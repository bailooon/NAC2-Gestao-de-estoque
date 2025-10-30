using Microsoft.EntityFrameworkCore;
using NAC2_Gestao_de_estoque.Data;
using NAC2_Gestao_de_estoque.Models;

namespace NAC2_Gestao_de_estoque.Services
{
    public class RelatorioService
    {
        private readonly EstoqueDbContext _context;
        public RelatorioService(EstoqueDbContext context)
        {
            _context = context;
        }

        public decimal ValorTotalEstoque()
        {
            return _context.Produtos.Sum(p => p.QuantidadeAtual * p.PrecoUnitario);
        }

        public List<Produto> ProdutosVencendoEm7Dias()
        {
            var hoje = DateTime.Now;
            var limite = hoje.AddDays(7);
            var skusVencendo = _context.Movimentacoes
                .Where(m => m.DataValidade != null && m.DataValidade <= limite && m.DataValidade > hoje)
                .Select(m => m.SKUProduto)
                .Distinct();
            return _context.Produtos.Where(p => skusVencendo.Contains(p.SKU)).ToList();
        }

        public List<Produto> ProdutosAbaixoDoMinimo()
        {
            return _context.Produtos.Where(p => p.QuantidadeAtual < p.QuantidadeMinimaEstoque).ToList();
        }

        public List<AlertaEstoque> GerarAlertasEstoqueMinimo()
        {
            var produtosAlerta = _context.Produtos.Where(p => p.QuantidadeAtual < p.QuantidadeMinimaEstoque).ToList();
            var alertas = new List<AlertaEstoque>();
            foreach (var produto in produtosAlerta)
            {
                alertas.Add(new AlertaEstoque
                {
                    SKUProduto = produto.SKU,
                    Mensagem = $"Produto {produto.Nome} abaixo do estoque mínimo!",
                    DataAlerta = DateTime.Now
                });
            }
            return alertas;
        }
    }
}