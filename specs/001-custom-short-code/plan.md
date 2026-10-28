# Plano de Implementação: Códigos Curtos Personalizados

**Branch**: `001-custom-short-code` | **Data**: 2026-10-27 | **Spec**: [spec.md](./spec.md)

**Entrada**: Especificação da feature em `/specs/001-custom-short-code/spec.md`

## Resumo

Estender o fluxo existente de `POST /urls` com um código personalizado opcional,
preservando a geração automática quando o campo não for informado. O contrato de
request ganhará um campo opcional `Code`. Um serviço pequeno em `Services/` validará
formato, tamanho, nomes de rotas reservados e as regras de diferenciação de maiúsculas
e minúsculas. O índice único existente do banco continuará sendo a fonte de verdade
para corridas; uma falha de restrição única para código personalizado será convertida
em uma resposta HTTP 409 `ProblemDetails`. Nenhum endpoint, schema, pacote NuGet ou
projeto de testes será adicionado.

## Contexto Técnico

**Linguagem/Versão**: C# em .NET 10 (`net10.0`)

**Dependências Principais**: ASP.NET Core Minimal APIs, Entity Framework Core SQLite,
Microsoft.AspNetCore.OpenApi; nenhuma dependência nova

**Armazenamento**: Banco SQLite existente, criado com `EnsureCreated`; o limite
máximo de 32 caracteres e o índice único de `ShortUrls.Code` serão reutilizados

**Testes**: Cenários de sucesso/erro em `EncurtaUrl.Api.http` e `dotnet build`; nenhum
projeto de testes será introduzido

**Plataforma-Alvo**: Host atual da API web em .NET

**Tipo de Projeto**: Serviço web único / Minimal API

**Metas de Desempenho**: Preservar o comportamento atual das requisições e adicionar
somente o caminho de validação e, para códigos personalizados, a consulta de
unicidade já prevista antes da persistência; a feature não exige nova meta de
latência ou vazão

**Restrições**: Nenhum endpoint novo, alteração de schema, migration, pacote NuGet
novo, autenticação, expiração ou fluxo de edição/remoção

**Escala/Escopo**: Uma API existente de encurtamento de URLs e seu modelo SQLite
atual; a feature altera apenas a entrada de criação e o tratamento de conflitos

## Verificação da Constituição

*GATE: Deve passar antes da pesquisa da Fase 0. Reavaliar após o design da Fase 1.*

- **Simplicidade**: PASS — um único fluxo de Minimal API; sem camada de aplicação
  adicional, MediatR ou repository.
- **Organização**: PASS — o contrato de request permanece em `Contracts/`, o serviço
  de validação fica em `Services/` e a lógica do endpoint permanece em `Endpoints/`.
- **Respostas HTTP**: PASS — uso de `TypedResults`, `Results<...>`,
  `ValidationProblem` para 400 e `ProblemDetails` para 409.
- **Dados**: PASS — SQLite e `EnsureCreated` permanecem inalterados; nenhuma
  alteração de schema ou migration está planejada.
- **Compatibilidade**: PASS — `Code` é opcional e requests sem ele mantêm o
  comportamento e o formato de response atuais.
- **Dependências**: PASS — nenhum pacote NuGet será adicionado.
- **Validação**: PASS — `.http` receberá cenários de sucesso e erro e o projeto será
  verificado com `dotnet build` quanto a novos warnings.
- **Idioma**: PASS — todas as novas mensagens de validação e conflito estarão em
  português.

## Estrutura do Projeto

### Documentação desta feature

```text
specs/001-custom-short-code/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── http-api.md
└── tasks.md                  # criado pelo $speckit-tasks
```

### Código-fonte (raiz do repositório)

```text
src/EncurtaUrl/src/EncurtaUrl.Api/
├── Contracts/
│   └── ShortUrlContracts.cs       # adicionar Code opcional ao CreateShortUrlRequest
├── Data/
│   └── AppDbContext.cs            # inalterado; índice único de Code existente
├── Endpoints/
│   └── ShortUrlEndpoints.cs       # escolher/validar código e mapear conflitos
├── Models/
│   └── ShortUrl.cs                # inalterado
├── Services/
│   ├── ShortCodeGenerator.cs      # geração automática inalterada
│   └── ShortCodeValidator.cs      # novas regras de formato/nomes reservados
└── EncurtaUrl.Api.http            # adicionar cenários de sucesso e erro
```

**Decisão de Estrutura**: Manter a estrutura atual de Minimal API em um único
projeto. Estender o endpoint de criação e o contrato de request existentes, adicionando
apenas o serviço pequeno de validação exigido pela feature. A persistência e as rotas
existentes permanecem no lugar.

## Controle de Complexidade

Nenhuma violação da constituição. Nenhuma exceção de complexidade é necessária.
