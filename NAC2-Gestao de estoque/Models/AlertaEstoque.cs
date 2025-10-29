
using System;
using System.ComponentModel.DataAnnotations;

namespace NAC2_Gestao_de_estoque.Models
{
 
    
        public class AlertaEstoque
        {
            public int Id { get; set; }
            public string SKUProduto { get; set; }
            public string Mensagem { get; set; }
            public DateTime DataAlerta { get; set; } = DateTime.Now;
        }
    }

