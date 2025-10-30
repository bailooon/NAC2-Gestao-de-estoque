
# 🧭 Sistema de Gestão de Estoque

## 📘 Visão Geral
Este projeto é um **sistema de gerenciamento de estoque** voltado para empresas que trabalham com **produtos perecíveis e não perecíveis**.  
O objetivo é garantir controle preciso das movimentações, cumprimento das regras de negócio e emissão de relatórios inteligentes sobre o status do estoque.

---

## ⚙️ Regras de Negócio
O sistema implementa as seguintes validações e comportamentos automáticos:

- ✅ Produtos **perecíveis** exigem **lote** e **data de validade** obrigatórios.  
- 🚫 Não é permitido realizar **movimentações negativas** de estoque.  
- 📦 Saídas de produto só ocorrem quando **há quantidade suficiente** em estoque.  
- ⚠️ Geração de **alertas** para produtos com **estoque abaixo do mínimo**.  
- ⏰ Produtos **perecíveis vencidos** não podem ser movimentados.

---

## 🧩 Estrutura de Entidades

### **Produto**

   ```yaml
Produto
-------------
string SKU  
string Nome  
ENUM Categoria *(PERECIVEL / NAO_PERECIVEL)*  
decimal Preço unitário  
int Quantidade em estoque  
int Estoque mínimo  
DateTime Data de cadastro  

Movimentação
--------------
ENUM Tipo *(Entrada ou Saída)*  
int Quantidade  
DateTime Data  
string SKU  
string Lote  
DateTime DataValidade  

Alerta
------------
string SKU  
string Mensagem  
Datetime Data do alerta

   ```  

---

## 🔗 Endpoints da API

| Método | Endpoint | Descrição |
|--------|-----------|-----------|
| `POST` | `/produto` | Cadastra um novo produto |
| `POST` | `/movimentacao` | Registra uma movimentação de entrada ou saída |
| `GET`  | `/relatorio/estoque-total` | Exibe o saldo total de produtos em estoque |
| `GET`  | `/relatorio/produtos-vencendo` | Lista produtos próximos do vencimento |
| `GET`  | `/relatorio/produtos-abaixo-minimo` | Exibe produtos com estoque abaixo do mínimo |
| `GET`  | `/relatorio/alertas-estoque-minimo` | Mostra os alertas gerados automaticamente |

---

## 🚀 Como Executar o Projeto

1. Abra o terminal na pasta do projeto.  
2. Execute o comando:  
   ```bash
   dotnet run
   ```  
3. Acesse os endpoints pela URL exibida no console (exemplo: [http://localhost:5133](http://localhost:5133)).

--- 
