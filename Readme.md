
## Sistema de Gerenciamento de Pedidos para Cafeteria

### Descrição Geral

Este repositório contém um **sistema de gerenciamento de pedidos para uma cafeteria**, desenvolvido com o objetivo de organizar o cadastro de produtos e facilitar o gerenciamento dos pedidos de clientes. O projeto foi criado com foco no estudo e aplicação dos princípios SOLID de design de software, permitindo uma comparação entre uma implementação básica sem SOLID e outra com SOLID aplicado.

### Objetivo do Sistema

O sistema permite realizar operações básicas de gerenciamento de produtos para uma cafeteria, incluindo:

- **Cadastro de Produtos**: Adicionar novos produtos ao menu.
- **Listagem de Produtos**: Visualizar todos os produtos cadastrados.
- **Consulta de Produto por Id**: Visualizar detalhes específicos de um produto.
- **Atualização de Produto**: Alterar informações de um produto existente.
- **Exclusão de Produto**: Remover um produto, desde que não esteja vinculado a um pedido ativo.

Essas operações são implementadas em dois modelos de projeto: uma versão inicial sem a aplicação dos princípios SOLID e outra versão com os princípios SOLID implementados.

## Estrutura do Repositório

- **Pasta `Projeto sem SOLID/`**: Contém o projeto implementado sem os princípios SOLID. Neste projeto, as classes estão diretamente acopladas, sem abstrações e sem a separação de responsabilidades que os princípios SOLID promovem.
    
- **Pasta `Projeto com SOLID/`**: Contém o projeto implementado aplicando os princípios SOLID. Esta versão segue boas práticas de design, garantindo que o sistema seja modular, flexível e de fácil manutenção. 
    
- **Pasta `Documentação Geral/`**:
    
    - **Casos de Uso**: Arquivos descrevendo os principais casos de uso do sistema, detalhando os passos para cada funcionalidade.
    - **Dicionário de Dados**: Documento que detalha as entidades e atributos usados no sistema, como `Product` e `Order`, com descrições de cada campo.

- **Raiz do Repositório**:
    
    - **README.md** (este arquivo): Explica o propósito e a estrutura do repositório, orientando sobre as diferenças entre as implementações.
    - **Entendendo SOLID.md**: Explicação dos cinco princípios SOLID, abordando o que são e como são aplicados no projeto.