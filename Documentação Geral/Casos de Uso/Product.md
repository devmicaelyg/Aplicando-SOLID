---
tags:
  - casodeuso
descrição: Produtos disponíveis no estabelecimento para tirar pedidos
---
-------------------------------------------------------------------------
#### Cadastrar Produto

**Descrição**: Esse caso de uso permite que um administrador do sistema adicione novos produtos ao menu da cafeteria. Cada produto terá informações básicas, como nome, descrição e preço. Como o sistema não possui autenticação, assumimos que o usuário tem permissões para realizar a operação de cadastro.
##### Fluxo Principal

1.  O administrador acessa a funcionalidade de cadastro de produto.
2.  O administrador fornece os seguintes dados obrigatórios do produto:
    - Nome
    - Descrição
    - Preço
3. O sistema verifica se os dados fornecidos são válidos. Se válidos, o sistema gera um `Id` único para o produto e o salva na memória.
4. O sistema exibe uma mensagem de confirmação indicando que o produto foi cadastrado com sucesso.
##### Fluxo Alternativo

- **Dados Inválidos**: Se o sistema detectar que algum dado obrigatório está ausente ou inválido, ele exibe uma mensagem de erro e permite que o administrador corrija os dados e reenvie.
	- A mensagem deve seguir o padrão: "Não é possível continuar. O XXX é obrigatório"
##### Regras de Negócio

1. **Nome é obrigatórios**: O produto deve ter um nome para ser adicionado.
2. **Preço deve ser positivo** e maior que zero: O preço do produto deve ser um valor positivo para garantir que o sistema não processe valores incorretos.
#### Listar Produto

**Descrição**: Esse caso de uso permite que um usuário visualize todos os produtos disponíveis no sistema. Cada produto possui informações como `Id`, `Nome`, `Descrição` e `Preço`.
##### Fluxo Principal

1. O usuário acessa a funcionalidade de listagem de produtos.
2. O sistema recupera todos os produtos armazenados.
3. O sistema exibe uma lista contendo os dados de cada produto (Nome, Descrição e Preço).
4. O caso de uso é encerrado.
##### Regras de Negócio

- **Listagem Completa**: Todos os produtos cadastrados devem ser exibidos.
- **Sem Modificação dos Dados**: O caso de uso é apenas para leitura. Nenhum dado será alterado.
#### Pegar Produto pelo Id 

**Descrição**: Esse caso de uso permite que o usuário consulte informações detalhadas de um produto específico no sistema, usando seu `Id`. A informação retornada inclui `Id`, `Nome`, `Descrição` e `Preço` do produto.

##### Fluxo Principal

1. O usuário acessa a funcionalidade de consulta de produto e informa o `Id` do produto desejado.
2. O sistema verifica se existe um produto com o `Id` fornecido.
3. Se o produto for encontrado, o sistema exibe seus detalhes.
4. O caso de uso é encerrado.
##### Fluxo Alternativo

- **Produto Não Encontrado**: Se não existir um produto com o `Id` especificado, o sistema exibe uma mensagem de erro informando que o produto não foi encontrado.
	- A mensagem deve seguir o padrão: "Não é possível continuar. Não foi encontrado um XXX com o identificador informado."
##### Regras de Negócio

1. **Id Obrigatório**: O usuário deve fornecer um `Id` válido para consultar um produto.
2. **Produto Não Encontrado**: Se o `Id` não corresponder a nenhum produto, o sistema retorna um erro 404.
#### Atualizar Produto

**Descrição**: Esse caso de uso permite que o usuário modifique os detalhes de um produto já cadastrado, utilizando o `Id` do produto para identificá-lo. O usuário pode alterar o `Nome`, `Descrição` e `Preço` do produto.

##### Fluxo Principal

1. O usuário acessa a funcionalidade de atualização de produto e fornece o `Id` do produto junto com os novos dados.
2. O sistema verifica se existe um produto com o `Id` fornecido.
3. Se o produto for encontrado, o sistema atualiza os dados (nome, descrição e preço) com as novas informações.
4. O sistema exibe uma mensagem de confirmação indicando que o produto foi atualizado com sucesso.
5. O caso de uso é encerrado.

##### Fluxo Alternativo

- **Produto Não Encontrado**: Se não existir um produto com o `Id` especificado, o sistema exibe uma mensagem de erro informando que o produto não foi encontrado.

##### Regras de Negócio

1. **Id Obrigatório**: O usuário deve fornecer um `Id` válido para localizar o produto a ser atualizado.
2. **Nome e Preço Válidos**: O sistema deve validar que o nome não está vazios e que o preço é positivo e maior que zero
#### Deletar Produto

**Descrição**: Esse caso de uso permite que o usuário remova um produto do sistema, utilizando o `Id` do produto para identificá-lo. No entanto, o sistema deve verificar se o produto não está vinculado a nenhum pedido ativo antes de permitir a exclusão.

##### Fluxo Principal

1. O usuário acessa a funcionalidade de exclusão de produto e fornece o `Id` do produto que deseja deletar.
2. O sistema verifica se existe um produto com o `Id` fornecido.
3. O sistema verifica se o produto está vinculado a algum pedido ativo.
4. Se o produto não estiver vinculado a nenhum pedido ativo, o sistema o remove da lista de produtos.
5. O sistema exibe uma mensagem de confirmação indicando que o produto foi deletado com sucesso.
6. O caso de uso é encerrado.
##### Fluxo Alternativo

- **Produto Não Encontrado**: Se não existir um produto com o `Id` especificado, o sistema exibe uma mensagem de erro informando que o produto não foi encontrado.
- **Produto Vinculado a Pedido Ativo**: Se o produto estiver vinculado a um pedido ativo, o sistema exibe uma mensagem de erro indicando que o produto não pode ser excluído.

##### Regras de Negócio

1. **Id Obrigatório**: O usuário deve fornecer um `Id` válido para deletar o produto.
2. **Verificação de Pedidos Ativos**: O produto não pode ser deletado se estiver vinculado a um pedido ativo.