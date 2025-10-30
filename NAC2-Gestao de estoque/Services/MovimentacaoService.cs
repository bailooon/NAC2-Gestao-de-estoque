using Microsoft.EntityFrameworkCore;
using NAC2_Gestao_de_estoque.Data;
using NAC2_Gestao_de_estoque.Models;

namespace NAC2_Gestao_de_estoque.Services
{
    public class MovimentacaoService
    {
        private readonly EstoqueDbContext _context;
        public MovimentacaoService(EstoqueDbContext context)
        {
            _context = context;
        }

        public void RegistrarMovimentacao(MovimentacaoEstoque mov)
        {
            if (mov.Quantidade <= 0)
                throw new Exception("Quantidade deve ser positiva.");

            var produto = _context.Produtos.FirstOrDefault(p => p.SKU == mov.SKUProduto);
            if (produto == null)
                throw new Exception("Produto não encontrado.");

            if (produto.Categoria == CategoriaProduto.PERECIVEL)
            {
                if (string.IsNullOrWhiteSpace(mov.Lote) || mov.DataValidade == null)
                    throw new Exception("Perecíveis exigem lote e data de validade.");
                if (mov.DataValidade <= DateTime.Now)
                    throw new Exception("Data de validade inválida.");
            }

            if (mov.Tipo == TipoMovimentacao.SAIDA)
            {
                if (produto.QuantidadeAtual < mov.Quantidade)
                    throw new Exception("Estoque insuficiente.");
                produto.QuantidadeAtual -= mov.Quantidade;
            }
            else
            {
                produto.QuantidadeAtual += mov.Quantidade;
            }

            _context.Movimentacoes.Add(mov);
            _context.SaveChanges();
        }
    }
}