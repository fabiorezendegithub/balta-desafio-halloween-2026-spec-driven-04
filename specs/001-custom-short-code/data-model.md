# Data Model: Custom Short Codes

## Existing entity: ShortUrl

The existing `ShortUrl` entity remains unchanged.

| Field | Existing rule | Feature impact |
|---|---|---|
| `Id` | Integer identity | None |
| `Code` | Required, maximum 32 characters, unique index | Stores either generated or custom code; uniqueness remains case-sensitive |
| `OriginalUrl` | Required, maximum 2048 characters | Existing URL validation remains unchanged |
| `CreatedAt` | UTC creation timestamp | None |
| `Clicks` | Integer counter | Redirect through a custom code increments it as today |

## Input contract change

`CreateShortUrlRequest` gains an optional `Code` value.

- Missing or null `Code`: retain automatic generation.
- Empty or whitespace-only `Code`: validation error.
- Non-empty `Code`: validate allowed characters, length, and reserved names before
  persistence.
- Accepted custom code casing is preserved exactly.

## Validation rules

- Allowed characters: letters, digits, hyphen, and underscore.
- Length: 3 through 32 characters, inclusive.
- Reserved names: `urls` and `openapi`, compared case-insensitively.
- Existing code uniqueness: compared case-sensitively and enforced by the existing
  unique database index.

## State and lifecycle

No new state or lifecycle is introduced. A URL short is created once with either a
generated or custom code. Editing and removal remain out of scope.
