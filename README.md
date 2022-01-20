Jamsoft - Prova C#
===================================

Projeto proposto no processo de sele��o da empresa Jamsoft.  
  
Voc� deve desenvolver uma API REST para compra de produtos utilizando a forma de pagamento cart�o de cr�dito.  
A API deve possibilitar todo o gerenciamento de estoque e venda de produtos, para isso os seguintes endpoints devem ser implementados:  
  
-------
* **Adicionar produtos ao estoque**  
*POST http://localhost:8080/api/produtos*
-------	
* **Listar produtos**  
*GET http://localhost:8080/api/produtos*
-------	
* **Detalhar um produto**  
*GET http://localhost:8080/api/produtos/{id}*
------- 
* **Comprar produto**  
*POST http://localhost:8080/api/compras*
------- 
* **Remover um produto do estoque**  
*DELETE http://localhost:8080/api/produtos/produto_id*
------- 
* **API de pagamentos (autorizar transa��es de pagamentos online)**  
*POST http://localhost:8080/api/pagamento/compras*
------- 

