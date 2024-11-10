---
tags:
  - dicionariodedados
descrição: Pedidos feitos pelo sistema
---
---

| Campo           | Obrigatório? | Tipo       | Descrição                               | Valor Default                                       |
| --------------- | ------------ | ---------- | --------------------------------------- | --------------------------------------------------- |
| Id              | Sim          | Guid       | Identificador da tabela                 | new Guid()                                          |
| Number          | Sim          | Int        | Número único identificador do pedido    | Random                                              |
| Status          | Sim          | StatusType | Possíveis status que um pedido pode ter | Requested                                           |
| Price           | Sim          | Double     | Valor do pedido                         | A soma dos preciso dos produtos que compõe o pedido |
| DiscountedPrice | Não          | Double     | Valor final com desconto aplicado       | -                                                   |
| CreatedDate     | Sim          | DateTime   | Data de criação do pedido               | Data default do sistema no momento do pedido        |
| LastUpdatedDate | Sim          | DateTime   | Data de atualização do pedido           | Data default do sistema no momento da atualização   |

##### Coleções:
- [[Boas Praticas/Solid/Documentação Geral/Dicionário de Dados/Product|Product]]