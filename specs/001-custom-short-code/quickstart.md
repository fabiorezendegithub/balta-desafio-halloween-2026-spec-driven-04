# Quickstart: Validate Custom Short Codes

## Prerequisites

From the repository root:

```bash
cd src/EncurtaUrl/src/EncurtaUrl.Api
dotnet build
dotnet run
```

The application uses the existing development URL `http://localhost:5080`. The
SQLite database is created automatically as in the current project.

## Scenarios

Use `EncurtaUrl.Api.http` or equivalent HTTP requests.

1. **Custom code succeeds**: `POST /urls` with `url` and `code: "balta-io"` returns
   201 and the response uses `/balta-io`.
2. **Custom redirect succeeds**: `GET /balta-io` returns a redirect to the original
   URL; repeat or inspect details to confirm the click count increases.
3. **Automatic generation remains compatible**: `POST /urls` with only `url` returns
   201 with a generated code and the existing response fields.
4. **Listing and details**: `GET /urls` and `GET /urls/balta-io` include the custom
   URL with the same fields as generated URLs.
5. **Invalid format/length**: submit codes containing spaces or unsupported symbols,
   fewer than 3 characters, and more than 32 characters; each returns 400 with a
   Portuguese validation message.
6. **Empty code**: submit an empty or whitespace-only code; it returns 400 rather
   than generating a code.
7. **Reserved code**: submit `urls`, `URLs`, `openapi`, or `OpenApi`; each returns
   400 because the name conflicts with a reserved route.
8. **Duplicate code**: create `balta-io`, then submit another URL with the same
   exact code; the second request returns 409 and says the code is in use.
9. **Case-sensitive identity**: create `Balta` and `balta`; both are accepted as
   distinct codes unless either value is reserved.

## Completion checks

- All success and error scenarios are present in `EncurtaUrl.Api.http`.
- `dotnet build` completes without new warnings.
- No NuGet package, migration, schema change, endpoint, test project, edit flow,
  removal flow, authentication, or expiration behavior was added.
