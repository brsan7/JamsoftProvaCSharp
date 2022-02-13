Jamsoft - Prova C#
===================================

Projeto proposto no processo de seleção da empresa Jamsoft.  
Para resolver o problema proposto, foi utilizado:  

* *Microsoft.EntityFrameworkCore.SqlServer com a abordagem Code First*  
* *Microsoft.AspNet.WebApi.Client*  
* *Aplicado os conceitos S.O.L.I.D.*  
* *Adicionado um projeto mobile(Android e iOS) para consumir a API_GerenciamentoProdutos.*  
  
*Problema Proposto*  
Você deve desenvolver uma API REST para compra de produtos utilizando a forma de pagamento cartão de crédito.  
A API deve possibilitar todo o gerenciamento de estoque e venda de produtos, para isso os seguintes endpoints devem ser implementados:  
  
-------
**1. Adicionar produtos ao estoque.**  
Esta rota insere um novo registro na tabela produtos.  
- Request  
`POST http://localhost:8080/api/produtos`  
  
- Body
````JSON
{  
  "nome": "Macbook 13\" 8GB|256SSD|2.7Ghz",  
  "valor_unitario": 8450.0,  
  "qtde_estoque": 5  
}
````  
  
- Retornos possíveis  
  
Código | Resposta
------ | ------
200 | Produto Cadastrado
400 | Ocorreu um erro desconhecido
412 | Os valores informados não são válidos

-------	
**2. Listar produtos**  
Esta rota lista todos os produtos disponíveis para venda.  
- Request  
`GET http://localhost:8080/api/produtos`  
  
- Retornos possíveis  
Retornar um array com todos os produtos cadastrados. Atributos: nome, valor unitário, quantidade em estoque.  

Código  | Resposta
------- | ------
200(Ok) | []
400     | Ocorreu um erro desconhecido

-------	
**3. Detalhar produtos**  
Esta rota obtém um produto específico por seu id, os dados de retorno são id, nome, valor unitário, quantidade em estoque, data e valor da última venda deste produto, caso o produto não possua vendas a data da última venda deve retornar null;  
- Request  
`GET http://localhost:8080/api/produtos/{id}`  
  
- Retornos possíveis  
Retornar um objeto com atributos: nome, valor unitário, quantidade em estoque, data e valor da última venda.  
  
Código  | Resposta
------- | ------
200(Ok) | {}
400     | Ocorreu um erro desconhecido

------- 
**4. Comprar produto.**  
Esta rota realiza a compra de um produto, ela deve: requisitar a rota Gateway de pagamentos, caso a compra seja aprovada: realizar a baixa do produto no estoque e retornar a resposta de sucesso. Qualquer erro que ocorra durante a compra deve ser retornado a resposta de erro. Não esqueça de validar o número do cartão de crédito antes de enviá-lo ao gateway.  
- Request  
`POST http://localhost:8080/api/compras`  
  
- Body
````JSON
{
    "produto_id": 1,
    "qtde_comprada": 1, 
    "cartao": {
        "titular": "John Doe", 
        "numero_cartao": "4111111111111111", 
        "data_expiracao": "12/2018", 
        "bandeira": "VISA", 
        "cvv": "123"
    }
}
````  
  
- Retornos possíveis  
  
Código | Resposta
------ | ------
200 | Venda realizada com sucesso
400 | Ocorreu um erro desconhecido
412 | Os valores informados não são válidos
------- 
**5. Remover um produto do estoque**  
Esta rota remove um produto da base de dados.  
- Request  
`DELETE http://localhost:8080/api/produtos/{id}`  
  
- Retornos possíveis  
Retornar um objeto com atributos: nome, valor unitário, quantidade em estoque, data e valor da última venda.  
  
Código  | Resposta
------- | ------
200     | Produto excluído com sucesso
400     | Ocorreu um erro desconhecido
------- 
**6. Criar uma API de pagamentos.**  
API de pagamentos destinado a lojas virtuais, por ele é possível autorizar transações de pagamentos online. Essa Api terá apenas um POST.  
- Request  
`POST http://localhost:7070/api/pagamentos`  
  
- Body
````JSON
{
    "valor": 100.00, 
    "cartao": {
        "titular": "John Doe", 
        "numero_cartao": "4111111111111111", 
        "data_expiracao": "12/2018", 
        "bandeira": "VISA", 
        "cvv": "123"
    }
}
````  
  
Possíveis respostas da API:  
- Quando a compra for aprovada  
Será aprovada compras maior que 100  
  
````JSON
{  
  "valor": 100.0,  
  "estado": "APROVADO"  
}
````  
- Quando a compra for rejeitada  
Será rejeitada compras menor ou igual a 100  
  
````JSON
{  
  "valor": 100.0,  
  "estado": "REJEITADO"  
}
````  
------- 

