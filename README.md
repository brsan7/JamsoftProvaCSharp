Jamsoft - Prova C#
===================================

Projeto proposto no processo de seleção da empresa Jamsoft.  
Para resolver o problema proposto, foi utilizado:  
* **Microsoft.EntityFrameworkCore.SqlServer com a abordagem Code First**  
* *Microsoft.AspNet.WebApi.Client*  
* *Aplicado os conceitos S.O.L.I.D.*  
  
*Problema Proposto*  
Você deve desenvolver uma API REST para compra de produtos utilizando a forma de pagamento cartão de crédito.  
A API deve possibilitar todo o gerenciamento de estoque e venda de produtos, para isso os seguintes endpoints devem ser implementados:  
  
-------
* **Adicionar produtos ao estoque**  
`POST http://localhost:8080/api/produtos`
-------	
* **Listar produtos**  
`GET http://localhost:8080/api/produtos`
-------	
* **Detalhar um produto**  
`GET http://localhost:8080/api/produtos/{id}`
------- 
* **Comprar produto**  
`POST http://localhost:8080/api/compras`
------- 
* **Remover um produto do estoque**  
`DELETE http://localhost:8080/api/produtos/produto_id`
------- 
* **API de pagamentos (autorizar transações de pagamentos online)**  
`POST http://localhost:5000/api/pagamentos`
------- 

