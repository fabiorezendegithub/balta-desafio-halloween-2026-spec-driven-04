---

description: "Tarefas de implementação para códigos curtos personalizados"
---

# Tarefas: Códigos Curtos Personalizados

**Entrada**: Documentos de design em `/specs/001-custom-short-code/`

**Pré-requisitos**: `plan.md`, `spec.md`, `research.md`, `data-model.md`,
`contracts/http-api.md` e `quickstart.md`

**Testes**: Nenhum projeto de testes será adicionado. Os cenários manuais de
sucesso/erro ficam em `EncurtaUrl.Api.http`; o projeto será verificado com
`dotnet build`.

**Organização**: As tarefas estão agrupadas por história de usuário. Os pré-requisitos
compartilhados de validação e contrato são concluídos antes do trabalho das histórias.

## Fase 1: Preparação (Infraestrutura Compartilhada)

**Objetivo**: Confirmar os pontos do projeto existente que a feature deve preservar.

- [ ] T001 [P] Revisar o fluxo existente de request, endpoint e response em `src/EncurtaUrl/src/EncurtaUrl.Api/Contracts/ShortUrlContracts.cs` e `src/EncurtaUrl/src/EncurtaUrl.Api/Endpoints/ShortUrlEndpoints.cs` antes de editar.
- [ ] T002 [P] Confirmar o limite de 32 caracteres de `Code` e o índice único em `src/EncurtaUrl/src/EncurtaUrl.Api/Data/AppDbContext.cs`, e confirmar que nenhuma alteração de schema ou pacote é necessária em `src/EncurtaUrl/src/EncurtaUrl.Api/EncurtaUrl.Api.csproj`.

---

## Fase 2: Fundação (Pré-requisitos Bloqueadores)

**Objetivo**: Adicionar a capacidade compartilhada de validação de códigos
personalizados sem alterar a persistência ou introduzir uma nova dependência.

**⚠️ CRÍTICO**: Concluir esta fase antes de implementar o comportamento das histórias.

- [ ] T003 Criar `IShortCodeValidator` e `ShortCodeValidator` em `src/EncurtaUrl/src/EncurtaUrl.Api/Services/ShortCodeValidator.cs`, validando somente letras, números, hífen e underscore, exigindo tamanho de 3–32 inclusive, rejeitando `urls` e `openapi` sem diferenciar maiúsculas de minúsculas, preservando a capitalização aceita e retornando motivos de validação em português.
- [ ] T004 Registrar o validator em `src/EncurtaUrl/src/EncurtaUrl.Api/Program.cs` usando o estilo existente de injeção de dependência, sem adicionar pacote NuGet ou outra camada de aplicação.

**Checkpoint**: O validator compartilhado está disponível e o modelo de banco
existente permanece inalterado.

---

## Fase 3: História de Usuário 1 - Criar URL com código escolhido (Prioridade: P1) 🎯 MVP

**Objetivo**: Permitir que um código personalizado válido e disponível seja usado pelo
fluxo de criação e pela rota de redirecionamento existentes.

**Teste independente**: Fazer POST com um código personalizado válido, confirmar que a
resposta usa esse código e depois fazer GET do código para confirmar o redirecionamento
e a contagem de cliques.

### Implementação da História de Usuário 1

- [ ] T005 [US1] Estender `CreateShortUrlRequest` em `src/EncurtaUrl/src/EncurtaUrl.Api/Contracts/ShortUrlContracts.cs` com um campo `Code` opcional, mantendo compatíveis o `Url` existente e os contratos de response.
- [ ] T006 [US1] Atualizar `CreateAsync` em `src/EncurtaUrl/src/EncurtaUrl.Api/Endpoints/ShortUrlEndpoints.cs` para diferenciar código ausente/nulo (geração automática) de código informado, validar códigos informados por meio de `IShortCodeValidator` e atribuir um código personalizado válido sem adicionar endpoint.
- [ ] T007 [US1] Preservar o comportamento existente de `ToResponse`, listagem, detalhes e redirecionamento em `src/EncurtaUrl/src/EncurtaUrl.Api/Endpoints/ShortUrlEndpoints.cs`, para que códigos personalizados apareçam em `shortUrl`, detalhes, listagem e URLs de redirecionamento e incrementem `Clicks` exatamente como códigos gerados.
- [ ] T008 [US1] Adicionar as requisições de sucesso de criação e redirecionamento com código personalizado em `src/EncurtaUrl/src/EncurtaUrl.Api/EncurtaUrl.Api.http`, incluindo um valor de código que possa ser reutilizado nas requisições seguintes.

**Checkpoint**: A História de Usuário 1 funciona de forma independente com um código
personalizado válido e o caminho de código automático continua disponível.

---

## Fase 4: História de Usuário 2 - Continuar usando código automático (Prioridade: P1)

**Objetivo**: Comprovar que clientes que omitem `Code` mantêm o comportamento atual de
geração, listagem e detalhes.

**Teste independente**: Fazer POST somente com `url`, depois fazer GET do código
gerado, listar URLs e consultar seus detalhes usando os campos de response existentes.

### Implementação da História de Usuário 2

- [ ] T009 [US2] Verificar e, se necessário, ajustar a ramificação em `CreateAsync`, em `src/EncurtaUrl/src/EncurtaUrl.Api/Endpoints/ShortUrlEndpoints.cs`, para que `Code` omitido ou nulo use o gerador existente e mantenha inalterado o limite de tentativas.
- [ ] T010 [US2] Adicionar cenários de sucesso de geração automática, `"code": null`, listagem e detalhes em `src/EncurtaUrl/src/EncurtaUrl.Api/EncurtaUrl.Api.http`, confirmando que URLs geradas e personalizadas expõem os mesmos campos de response.

**Checkpoint**: Clientes existentes que enviam apenas `url` continuam funcionando sem
quebra no contrato de request ou response.

---

## Fase 5: História de Usuário 3 - Feedback para código inválido ou indisponível (Prioridade: P1)

**Objetivo**: Rejeitar códigos personalizados inválidos, reservados, duplicados ou
em conflito por corrida, usando os resultados HTTP tipados e mensagens em português.

**Teste independente**: Exercitar formato inválido, limites, vazio/espaços, reservado,
duplicado e duplicidade concorrente, verificando respostas 400/409.

### Implementação da História de Usuário 3

- [ ] T011 [US3] Estender a assinatura de resultados tipados e os ramos de validação em `src/EncurtaUrl/src/EncurtaUrl.Api/Endpoints/ShortUrlEndpoints.cs`, para que falhas de formato, tamanho, vazio/espaços e código reservado retornem `TypedResults.ValidationProblem` com HTTP 400 e mensagens de campo em português.
- [ ] T012 [US3] Adicionar a verificação de existência de código personalizado e capturar somente a `DbUpdateException` de restrição única do SQLite em `src/EncurtaUrl/src/EncurtaUrl.Api/Endpoints/ShortUrlEndpoints.cs`, retornando `TypedResults.Problem` com HTTP 409 e mensagem em português de código em uso para códigos personalizados, preservando o comportamento de falha da geração automática.
- [ ] T013 [US3] Adicionar cenários de erro em `src/EncurtaUrl/src/EncurtaUrl.Api/EncurtaUrl.Api.http` para caracteres não permitidos, menos de 3 caracteres, mais de 32 caracteres, valores vazios/com espaços, `urls`, `URLs`, `openapi`, `OpenApi`, código personalizado duplicado e resultados 400/409 esperados; documentar a verificação de duplicidade concorrente em `specs/001-custom-short-code/quickstart.md`.
- [ ] T014 [US3] Verificar em `src/EncurtaUrl/src/EncurtaUrl.Api/Endpoints/ShortUrlEndpoints.cs` que a identidade diferencia maiúsculas de minúsculas, de modo que códigos válidos distintos como `Balta` e `balta` não entrem em conflito, exceto quando reservados.

**Checkpoint**: Todos os caminhos de rejeição de códigos personalizados especificados
são testáveis de forma independente e retornam a resposta tipada correta.

---

## Fase 6: Acabamento e Preocupações Transversais

**Objetivo**: Executar os gates de qualidade do projeto e verificar que a implementação
permaneceu dentro da arquitetura e do escopo aprovados.

- [ ] T015 [P] Executar `dotnet build` em `src/EncurtaUrl/src/EncurtaUrl.Api/` e confirmar que não existem novos warnings de compilação.
- [ ] T016 [P] Executar todos os cenários de `specs/001-custom-short-code/quickstart.md` contra `src/EncurtaUrl/src/EncurtaUrl.Api/EncurtaUrl.Api.http`, incluindo a verificação da contagem de cliques no redirecionamento.
- [ ] T017 Revisar `src/EncurtaUrl/src/EncurtaUrl.Api/EncurtaUrl.Api.csproj`, `src/EncurtaUrl/src/EncurtaUrl.Api/Data/AppDbContext.cs` e o diff final para confirmar que nenhum pacote NuGet, migration, alteração de schema, endpoint, projeto de testes, fluxo de edição/remoção, autenticação ou expiração foi adicionado.

---

## Dependências e Ordem de Execução

### Dependências entre Fases

- **Preparação (Fase 1)**: Sem dependência de implementação; T001 e T002 podem ser executadas em paralelo.
- **Fundação (Fase 2)**: Depende da Preparação; T003 precede T004, e ambas bloqueiam as histórias.
- **Histórias de Usuário (Fases 3–5)**: Dependem da Fundação. US1 estabelece o caminho de código personalizado; US2 verifica a compatibilidade; US3 conclui o tratamento de erros e corridas no mesmo endpoint.
- **Acabamento (Fase 6)**: Depende de todas as histórias desejadas.

### Dependências entre Histórias

- **US1 (P1)**: Depende de T003–T004; é a história do MVP e o primeiro incremento de implementação.
- **US2 (P1)**: Depende de US1 porque verifica a ramificação final de criação preservando o gerador existente.
- **US3 (P1)**: Depende de US1 e US2 porque conclui os resultados tipados do endpoint e exercita o fluxo completo de criação.

### Oportunidades de Paralelismo

- T001 e T002 podem ser executadas em paralelo porque inspecionam preocupações existentes diferentes.
- T015 e T016 podem ser executadas em paralelo após a implementação porque build e validação HTTP são verificações independentes.
- Nenhuma tarefa de implementação das histórias está marcada como paralela porque US1–US3 alteram intencionalmente os mesmos arquivos de contrato, endpoint e `.http`.

## Exemplo de Paralelismo: Preparação e Validação Final

```text
Tarefa T001: Revisar contratos e fluxo do endpoint
Tarefa T002: Confirmar restrições de schema e pacotes

Após a implementação:
Tarefa T015: Executar dotnet build
Tarefa T016: Executar cenários do quickstart
```

## Estratégia de Implementação

### Primeiro o MVP (Somente História de Usuário 1)

1. Concluir as fases de Preparação e Fundação.
2. Implementar US1 com código opcional no request, validação, atribuição e verificação de redirecionamento.
3. Parar e validar o fluxo de sucesso do código personalizado de forma independente.

### Entrega Incremental

1. Adicionar as verificações de compatibilidade da US2 sem alterar o comportamento de código gerado.
2. Adicionar validação, detecção de duplicidade e tratamento de 409 seguro contra corrida na US3.
3. Executar o build final e concluir os cenários do quickstart.

## Observações

- Toda tarefa usa checkbox, ID sequencial, marcador `[P]` opcional, label de história quando aplicável e um caminho explícito do repositório.
- Nenhuma tarefa de projeto de testes separado foi incluída porque a constituição exige cenários `.http` e `dotnet build`, não um novo projeto de testes.
