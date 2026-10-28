<img width="100%" alt="Halloween 2026" src="https://baltaio.blob.core.windows.net/static/images/v4/challenges/halloween-2026/banner.jpg" />

## 🎃 Halloween - Desafio 4

Este projeto faz parte do desafio **Halloween 2026**, realizado pelo
[balta.io](https://balta.io). 👻

Aqui você vai encontrar projetos, exercícios e códigos que estou desenvolvendo durante o desafio.

### Sobre este desafio
Neste desafio o objetivo é adicionar o SpecKit a um projeto já existente e adicionar novas funcionalidades a ele.

#### O que foi implementado

- API de encurtamento de URLs com .NET 10 e Minimal APIs;
- geração automática de códigos curtos;
- criação de URLs com código personalizado opcional;
- validação de tamanho, caracteres permitidos e códigos reservados;
- tratamento de códigos duplicados com HTTP 409;
- redirecionamento com contagem de cliques;
- listagem e consulta de detalhes das URLs;
- persistência com Entity Framework Core e SQLite;
- especificação, plano e tarefas documentados com Spec Kit.

## Como executar

```bash
cd src/EncurtaUrl/src/EncurtaUrl.Api
dotnet build
dotnet run
```

A API estará disponível em `http://localhost:5080`. O banco SQLite é criado
automaticamente na primeira execução.

## Documentação

Consulte o [manual da API](docs/manual.md) para ver os endpoints, exemplos com
`curl`, regras dos códigos personalizados, respostas de erro e teste de
concorrência. Há também um [manual de testes com Thunder Client](docs/manual-thunder-client.md)
para executar as requisições pelo Visual Studio Code.

Os cenários HTTP também estão disponíveis em
`src/EncurtaUrl/src/EncurtaUrl.Api/EncurtaUrl.Api.http`.

## Bagde
<img src="https://baltaio.blob.core.windows.net/static/images/v4/challenges/halloween-2026/04.png" width="200" />

## Problema

O desafio consiste em evoluir uma API existente usando desenvolvimento orientado
por especificação, mantendo compatibilidade com o fluxo de geração automática e
adicionando códigos curtos escolhidos pelo usuário.

## Sobre o Halloween 2026
O desafio **Halloween 2026** consiste em implementar implementar o modelo Spec Driven Development de ponta a ponta, criando apps completas com IA.

### Veja meu progresso no desafio

Este repositório contém a implementação do quarto desafio do Halloween 2026.
