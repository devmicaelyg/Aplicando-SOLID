---
tags:
  - casodeuso
descrição: Pedidos feitos através do sistema
---
-------------------------------------------------------------------------
#### Cadastrar Pedido

**Descrição**: Esse caso de uso permite que o usuário crie um novo pedido, especificando os produtos desejados. O sistema gera um número de pedido e define o status inicial como "Requested".

**Fluxo Principal**:

1. O usuário seleciona os produtos para o pedido.
2. O sistema calcula o preço total com base nos produtos.
3. O sistema cria o pedido com status inicial "Requested".
4. O sistema armazena a data de criação e retorna o número do pedido.

**Regras de Negócio**:

- O pedido deve conter pelo menos um produto para ser criado.
#### Listar Pedidos pelo Status

**Descrição**: Esse caso de uso permite que o usuário visualize todos os pedidos com um determinado status (por exemplo, "Pendente", "Confirmado", "Concluído", etc.). Isso ajuda o usuário a monitorar e gerenciar pedidos de acordo com seu estado atual.

##### Fluxo Principal

1. O usuário acessa a funcionalidade de listagem de pedidos por status e seleciona o status desejado.
2. O sistema recupera todos os pedidos que correspondem ao status selecionado.
3. O sistema exibe uma lista dos pedidos encontrados, com informações resumidas, como `Número`, `Status`, `Data de Criação`, e `Preço`.
4. O caso de uso é encerrado.

##### Fluxo Alternativo

- **Nenhum Pedido com o Status Encontrado**: Se não houver pedidos com o status especificado, o sistema exibe uma mensagem informando que não foram encontrados pedidos com o status solicitado.

##### Regras de Negócio

1. **Status Obrigatório**: O usuário deve selecionar um status válido para filtrar os pedidos.
2. **Visualização Resumida**: A listagem deve exibir uma visão resumida dos pedidos, permitindo ao usuário identificar rapidamente os detalhes principais de cada pedido com o status selecionado.

#### Listar Pedidos pelo Número 

**Descrição**: Esse caso de uso permite que o usuário busque um pedido específico pelo seu número único. Esse recurso é útil para localizar rapidamente um pedido quando o número do pedido é conhecido.

##### Fluxo Principal

1. O usuário acessa a funcionalidade de busca de pedidos e fornece o número do pedido que deseja consultar.
2. O sistema verifica se existe algum pedido com o número fornecido.
3. Se o pedido for encontrado, o sistema exibe os detalhes do pedido, incluindo `Número`, `Status`, `Data de Criação`, `Data da Última Atualização`, `Preço` e a lista de produtos.
4. O caso de uso é encerrado.
##### Fluxo Alternativo

- **Pedido Não Encontrado**: Se não existir um pedido com o número especificado, o sistema exibe uma mensagem de erro informando que o pedido não foi encontrado.
##### Regras de Negócio

1. **Número de Pedido Obrigatório**: O usuário deve fornecer um número de pedido válido.
2. **Visualização Completa**: O sistema deve exibir todos os detalhes do pedido, incluindo os produtos associados.

#### Atualizar Pedido

**Descrição**: Esse caso de uso permite que o status de um pedido existente seja atualizado. Por exemplo, o status pode ser alterado para "Confirmado", "Em Preparação", "Enviado" ou "Concluído".

**Fluxo Principal**:

1. O usuário solicita a atualização do status do pedido, informando o novo status e o número do pedido.
2. O sistema verifica o status atual e se a mudança é permitida.
3. O sistema atualiza o status do pedido e registra a data da última atualização.

**Regras de Negócio**:

- As mudanças de status devem seguir uma sequência lógica (por exemplo, um pedido não pode ir diretamente de "Requested" para "Completed" sem passar por "Accepted").

#### Cancelar Pedido

**Descrição**: Esse caso de uso permite que o usuário cancele um pedido, desde que ele esteja em um status que permita o cancelamento.

**Fluxo Principal**:

1. O usuário solicita o cancelamento do pedido informando o número do pedido.
2. O sistema verifica o status atual do pedido para garantir que ele pode ser cancelado.
3. O sistema atualiza o status do pedido para "Canceled" e registra a data da última atualização.

**Regras de Negócio**:

- Apenas pedidos nos status "Requested" ou "Accepted" podem ser cancelados.
- Pedidos no status "Completed" não podem ser cancelados.

#### Calcular Preço do Pedido com Desconto

**Descrição**: Esse caso de uso permite que o sistema aplique um desconto ao preço total do pedido. O desconto pode ser aplicado com base em regras específicas, como descontos para clientes fiéis, descontos sazonais, entre outros.

**Fluxo Principal**:

1. O sistema calcula o preço total do pedido com base nos produtos adicionados.
2. O sistema verifica se o pedido se qualifica para algum tipo de desconto.
3. O sistema aplica o desconto (se aplicável) e calcula o valor final.

**Regras de Negócio**:

- O sistema deve permitir a aplicação de diferentes tipos de descontos sem modificar a lógica de cálculo de preço.