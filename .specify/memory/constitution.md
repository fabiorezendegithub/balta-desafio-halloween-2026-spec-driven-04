<!--
Relatório de Impacto da Sincronização
- Alteração de versão: scaffold/sem versão → 1.0.0
- Princípios modificados: placeholders do template → oito princípios específicos do projeto
- Seções adicionadas: Restrições Técnicas; Fluxo de Desenvolvimento e Gates de Qualidade
- Seções removidas: nenhuma
- Pendências: confirmar a data histórica de ratificação
-->

# Constituição do EncurtaUrl

## Princípios Fundamentais

### I. Simplicidade da API

A aplicação MUST permanecer como uma única Minimal API em .NET 10. Features MUST NOT
adicionar camadas de aplicação, MediatR, repositórios ou indirection equivalente.
Endpoints MUST usar `AppDbContext` diretamente ao acessar a persistência. Isso mantém
a arquitetura existente pequena e explícita.

### II. Organização por Responsabilidade

Endpoints MUST ficar em `Endpoints/` como métodos de extensão sobre
`IEndpointRouteBuilder` e MUST agrupar rotas relacionadas com `MapGroup`. Records de
request e response MUST ficar em `Contracts/`, entidades MUST ficar em `Models/` e
serviços pequenos e reutilizáveis MUST ficar em `Services/` atrás de uma interface.
Código novo MUST seguir esses locais, salvo quando o plano justificar explicitamente
uma exceção.

### III. Contratos de Resposta HTTP

Endpoints MUST usar `TypedResults` e declarar seus possíveis resultados com
`Results<...>` quando houver mais de um resultado possível. Falhas de validação MUST
retornar `ValidationProblem` com HTTP 400. Outras falhas MUST retornar `ProblemDetails`
com o código de status que representa a falha. Contratos de response existentes MUST
permanecer compatíveis.

### IV. Persistência com SQLite e EF Core

A persistência MUST usar Entity Framework Core com SQLite. A inicialização da aplicação
MUST criar o banco com `EnsureCreated`; migrations MUST NOT ser introduzidas. Qualquer
alteração de schema MUST ser justificada explicitamente no plano de implementação e
considerar o comportamento existente de criação do banco.

### V. Compatibilidade de Contratos

Contratos de request e response existentes MUST NOT ser quebrados. Qualquer campo novo
em request MUST ser opcional para que os clientes existentes continuem funcionando.
Alterações que mudem semântica, campos obrigatórios, rotas ou formatos de response
existentes MUST incluir uma justificativa explícita de compatibilidade no plano.

### VI. Dependências Controladas

Nenhum pacote NuGet novo MAY ser adicionado sem justificativa explícita no plano de
implementação. As escolhas atuais de framework e pacotes são o padrão; uma nova
dependência deve demonstrar por que a plataforma e o código atuais não atendem ao
requisito sem ela.

### VII. Verificação sem Projeto de Testes

Como o projeto não possui um projeto de testes, toda feature MUST adicionar cenários
de sucesso e erro ao `EncurtaUrl.Api.http`. O projeto da API MUST passar em
`dotnet build` e as features MUST não introduzir novos warnings de compilação. Os
cenários MUST cobrir o comportamento observável da feature, incluindo validações e
fluxos de falha relevantes.

### VIII. Mensagens em Português

Mensagens de erro exibidas ao usuário MUST estar em português. Identificadores do
código-fonte, comentários técnicos e demais termos de implementação MAY seguir a
convenção atual do projeto. Essa distinção preserva uma experiência em português sem
perder a consistência do código existente.

## Restrições Técnicas

A aplicação canônica está em `src/EncurtaUrl/src/EncurtaUrl.Api`. Código novo MUST
preservar o layout atual do projeto e ter como alvo `net10.0`. O registro de rotas MUST
continuar centralizado por meio de métodos de extensão de endpoints. A inicialização
do banco, o tratamento de exceções, o tratamento de códigos de status e o registro do
OpenAPI MUST permanecer consistentes com o `Program.cs` existente, salvo quando um
plano documentar o motivo da mudança.

## Fluxo de Desenvolvimento e Gates de Qualidade

Antes da implementação, o plano MUST identificar o impacto na compatibilidade de
contratos, o impacto no schema e qualquer dependência proposta. Durante a
implementação, as alterações MUST permanecer dentro da arquitetura existente e os
cenários `.http` MUST ser atualizados com casos de sucesso e erro. Antes da conclusão,
o projeto MUST ser verificado com `dotnet build`, e o diff final MUST ser revisado
quanto a novos warnings, quebras acidentais de contratos, mensagens de usuário não
traduzidas e complexidade arquitetural sem justificativa.

## Governança

Esta constituição governa todas as novas features e prevalece sobre práticas informais
quando houver conflito. Todo plano e toda revisão de implementação MUST verificar a
conformidade com os Princípios Fundamentais, as Restrições Técnicas e os Gates de
Qualidade. Exceções MUST ser documentadas no plano com escopo, justificativa, impacto
de compatibilidade e uma decisão ou rota de migração futura.

Alterações MUST atualizar este documento, incluir um Relatório de Impacto da
Sincronização e incrementar a versão de acordo com versionamento semântico: MAJOR para
alterações incompatíveis de governança, MINOR para princípios ou seções novos ou
ampliados de forma relevante, e PATCH para esclarecimentos ou ajustes editoriais. A
data da última alteração MUST ser atualizada em toda emenda aceita. A constituição
MUST ser revisada sempre que a arquitetura ou o fluxo de qualidade do projeto mudar
de forma relevante.

**Versão**: 1.0.0 | **Ratificada**: TODO(RATIFICATION_DATE): data histórica de adoção não registrada | **Última alteração**: 2026-10-27
