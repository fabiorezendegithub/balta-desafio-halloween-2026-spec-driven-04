<!--
Sync Impact Report
- Version change: scaffold/unversioned → 1.0.0
- Modified principles: template placeholders → eight project-specific principles
- Added sections: Technical Constraints; Development Workflow and Quality Gates
- Removed sections: none
- Follow-up TODOs: confirm the historical ratification date
-->

# EncurtaUrl Constitution

## Core Principles

### I. Simplicidade da API

The application MUST remain a single .NET 10 Minimal API. Features MUST NOT add
application layers, MediatR, repositories, or equivalent indirection. Endpoints MUST
use `AppDbContext` directly when accessing persistence. This keeps the existing
architecture small and explicit.

### II. Organização por Responsabilidade

Endpoints MUST live in `Endpoints/` as extension methods over
`IEndpointRouteBuilder` and MUST group related routes with `MapGroup`. Request and
response records MUST live in `Contracts/`, entities MUST live in `Models/`, and
small reusable services MUST live in `Services/` behind an interface. New code MUST
follow these locations unless the plan explicitly justifies an exception.

### III. Contratos de Resposta HTTP

Endpoints MUST use `TypedResults` and declare their possible outcomes with
`Results<...>` when more than one result is possible. Validation failures MUST
return `ValidationProblem` with HTTP 400. Other failures MUST return
`ProblemDetails` with the status code that represents the failure. Existing response
contracts MUST remain compatible.

### IV. Persistência com SQLite e EF Core

Persistence MUST use Entity Framework Core with SQLite. Application startup MUST
create the database with `EnsureCreated`; migrations MUST NOT be introduced.
Any schema change MUST be explicitly justified in the implementation plan and must
account for the existing database-creation behavior.

### V. Compatibilidade de Contratos

Existing request and response contracts MUST NOT be broken. Any new request field
MUST be optional so existing clients continue to work. Changes that alter existing
semantics, required fields, routes, or response shapes MUST include an explicit
compatibility rationale in the plan.

### VI. Dependências Controladas

No new NuGet package MAY be added without an explicit justification in the
implementation plan. Existing framework and package choices are the default; a new
dependency must demonstrate why the current platform and codebase cannot support
the requirement without it.

### VII. Verificação sem Projeto de Testes

Because the project has no test project, every feature MUST add success and error
scenarios to `EncurtaUrl.Api.http`. The API project MUST pass `dotnet build` and
features MUST introduce no new compiler warnings. The scenarios MUST cover the
feature's externally observable behavior, including relevant validation and failure
paths.

### VIII. Mensagens em Português

User-facing error messages MUST be written in Portuguese. Source code identifiers,
technical comments, and other implementation terminology MAY follow the existing
project convention. This distinction preserves a Portuguese user experience while
keeping code consistent with the current repository.

## Technical Constraints

The canonical application is under `src/EncurtaUrl/src/EncurtaUrl.Api`. New work
MUST preserve the current project layout and target `net10.0`. Route registration
MUST remain centralized through endpoint mapping extensions. Database initialization,
exception handling, status-code handling, and OpenAPI registration MUST remain
consistent with the existing `Program.cs` unless a plan documents the reason for a
change.

## Development Workflow and Quality Gates

Before implementation, the plan MUST identify contract compatibility impact, schema
impact, and any proposed dependency. During implementation, changes MUST be kept
within the existing architecture and the `.http` scenarios MUST be updated with both
success and error cases. Before completion, the project MUST be checked with
`dotnet build`, and the resulting diff MUST be reviewed for new warnings, accidental
contract breaks, untranslated user-facing errors, and unexplained architectural
complexity.

## Governance

This constitution governs all new features and supersedes informal practices when
they conflict. Every feature plan and implementation review MUST verify compliance
with the Core Principles, Technical Constraints, and Quality Gates. Exceptions MUST
be documented in the plan with scope, rationale, compatibility impact, and a
follow-up decision or migration path.

Amendments MUST update this document, include a Sync Impact Report, and increment the
version according to semantic versioning: MAJOR for incompatible governance changes,
MINOR for new or materially expanded principles or sections, and PATCH for clarifying
or editorial changes. The last-amended date MUST be updated for every accepted
amendment. The constitution MUST be reviewed whenever the project architecture or
quality workflow changes materially.

**Version**: 1.0.0 | **Ratified**: TODO(RATIFICATION_DATE): historical adoption date not recorded | **Last Amended**: 2026-10-27
