using System;
using System.ComponentModel.DataAnnotations;

namespace NAC2_Gestao_de_estoque.Models;

    public enum CategoriaProduto
    {
        PERECIVEL,
        NAO_PERECIVEL
    }

    public class Produto
    {
        [Key]
        public string SKU { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public CategoriaProduto Categoria { get; set; }
        [Range(0, double.MaxValue)]
        public decimal PrecoUnitario { get; set; }
        [Range(0, int.MaxValue)]
        public int QuantidadeMinimaEstoque { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public int QuantidadeAtual { get; set; }
    }