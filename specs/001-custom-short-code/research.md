# Research: Custom Short Codes

## Decision 1: Extend the existing create request

- **Decision**: Add an optional `Code` field to `CreateShortUrlRequest` and keep
  `POST /urls` as the only creation endpoint.
- **Rationale**: This preserves existing clients that send only `url` and follows
  the constitution's compatibility rule for new request fields.
- **Alternatives considered**: A second endpoint or a required field were rejected
  because the feature is an extension of the current flow and would break clients.

## Decision 2: Keep code rules in a small service

- **Decision**: Add a focused validator in `Services/` for optional/custom code
  handling. It validates allowed characters, length 3–32, and reserved names
  `urls` and `openapi` case-insensitively. Code uniqueness remains case-sensitive.
- **Rationale**: This isolates reusable domain rules without introducing a validation
  package or an additional application layer.
- **Alternatives considered**: Inline regular-expression checks throughout the
  endpoint and a third-party validation package were rejected for duplication and
  dependency/complexity cost.

## Decision 3: Preserve the existing database model

- **Decision**: Reuse the existing `ShortUrls.Code` column, 32-character limit, and
  unique index. Do not add migrations or schema changes.
- **Rationale**: The current entity already stores both generated and custom codes.
  The requested feature changes input selection, not persistence shape.
- **Alternatives considered**: A separate alias table or schema migration was
  rejected because it adds unnecessary data-model complexity and violates scope.

## Decision 4: Treat concurrent custom-code collisions as HTTP 409

- **Decision**: Keep the pre-insert existence check for a clear early error, then
  catch the SQLite unique-constraint `DbUpdateException` during save. When the
  requested code is custom, return a Portuguese `ProblemDetails` response with
  status 409. For automatic generation, preserve the current behavior and error
  handling.
- **Rationale**: The database constraint closes the race between check and insert;
  409 accurately communicates that the requested identifier is already in use.
- **Alternatives considered**: Relying only on the pre-check was rejected because
  two concurrent requests can pass it. Returning 500 was rejected because the
  conflict is a client-correctable condition.

## Decision 5: Validate reserved names independently of case

- **Decision**: Reject `urls`, `openapi`, and case variations such as `URLs` and
  `OpenApi`; preserve the exact casing of all other accepted custom codes.
- **Rationale**: Reserved route names must not be bypassed by casing variations,
  while existing code identity remains case-sensitive.
- **Alternatives considered**: Reserving a broad list of common names was rejected
  because only current static route segments are in scope.
