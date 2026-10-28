# Implementation Plan: Custom Short Codes

**Branch**: `001-custom-short-code` | **Date**: 2026-10-27 | **Spec**: [spec.md](./spec.md)

**Input**: Feature specification from `/specs/001-custom-short-code/spec.md`

## Summary

Extend the existing `POST /urls` flow with an optional custom code while preserving
automatic generation when the field is absent. The request contract will gain an
optional `Code` field. A small service in `Services/` will validate format, length,
reserved route names, and the case-sensitive code rules. The existing unique database
index remains the source of truth for races; a unique-constraint failure for a
custom code is translated to an HTTP 409 ProblemDetails response. No endpoint,
schema, NuGet package, or test project is added.

## Technical Context

**Language/Version**: C# on .NET 10 (`net10.0`)

**Primary Dependencies**: ASP.NET Core Minimal APIs, Entity Framework Core SQLite,
Microsoft.AspNetCore.OpenApi; no new dependencies

**Storage**: Existing SQLite database created with `EnsureCreated`; existing
`ShortUrls.Code` maximum length 32 and unique index are reused

**Testing**: `EncurtaUrl.Api.http` success/error scenarios plus `dotnet build`; no
test project is introduced

**Target Platform**: Existing .NET web API host on the project's current runtime
platform

**Project Type**: Single web service / Minimal API

**Performance Goals**: Preserve current request behavior and add only one validation
path and, for custom codes, the existing uniqueness lookup before persistence; no
new latency or throughput target is required by the feature

**Constraints**: No new endpoint, no schema change, no migrations, no new NuGet
package, no authentication, no expiration, and no edit/remove flow

**Scale/Scope**: One existing URL-shortening API and its current SQLite data model;
the feature changes creation input and conflict handling only

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Simplicity**: PASS — one existing Minimal API flow; no extra application layer,
  MediatR, or repository.
- **Organization**: PASS — request contract remains in `Contracts/`, validation
  service is in `Services/`, and endpoint logic remains in `Endpoints/`.
- **HTTP responses**: PASS — use `TypedResults`, `Results<...>`,
  `ValidationProblem` for 400, and `ProblemDetails` for 409.
- **Data**: PASS — SQLite and `EnsureCreated` remain unchanged; no schema change
  or migration is planned.
- **Compatibility**: PASS — `Code` is optional and omitted requests retain the
  current generated-code behavior and response shape.
- **Dependencies**: PASS — no NuGet package is added.
- **Validation**: PASS — `.http` receives success and error scenarios and the
  project is checked with `dotnet build` for new warnings.
- **Language**: PASS — all new user-facing validation and conflict messages are in
  Portuguese.

## Project Structure

### Documentation (this feature)

```text
specs/001-custom-short-code/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── http-api.md
└── tasks.md                  # created by $speckit-tasks
```

### Source Code (repository root)

```text
src/EncurtaUrl/src/EncurtaUrl.Api/
├── Contracts/
│   └── ShortUrlContracts.cs       # add optional Code to CreateShortUrlRequest
├── Data/
│   └── AppDbContext.cs            # unchanged; existing unique Code index
├── Endpoints/
│   └── ShortUrlEndpoints.cs       # choose/validate code and map conflicts
├── Models/
│   └── ShortUrl.cs                # unchanged
├── Services/
│   ├── ShortCodeGenerator.cs      # unchanged automatic generation
│   └── ShortCodeValidator.cs      # new format/reserved-name rules
└── EncurtaUrl.Api.http            # add success and error scenarios
```

**Structure Decision**: Keep the existing single-project Minimal API structure.
Extend the current create endpoint and request contract; add only the small
validation service required by the feature. Persistence and existing routes remain
in place.

## Complexity Tracking

No constitution violations. No complexity exception is required.
