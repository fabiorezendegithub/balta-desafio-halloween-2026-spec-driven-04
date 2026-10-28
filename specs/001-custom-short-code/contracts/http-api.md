# HTTP Contract: Custom Short Codes

## Create a short URL

`POST /urls`

Existing request:

```json
{
  "url": "https://balta.io"
}
```

Extended request with optional custom code:

```json
{
  "url": "https://balta.io",
  "code": "balta-io"
}
```

The response shape remains the existing `ShortUrlResponse`. When `code` is omitted,
the service generates a code exactly as before. When it is valid, the response's
`code` and `shortUrl` use the requested value.

### Outcomes

| Status | Meaning | Response contract |
|---|---|---|
| 201 | URL created | Existing `ShortUrlResponse` |
| 400 | Original URL or custom code is invalid | `ValidationProblem`, with Portuguese field errors |
| 409 | Requested custom code is already in use, including a save-time race | `ProblemDetails` stating that the code is in use |
| 500 | Existing unexpected generation/persistence failure | Existing problem handling |

Validation messages must distinguish invalid URL, invalid code format/length,
reserved code, and code-in-use conditions. The exact wording may follow the current
Portuguese style, but each message must tell the user how to correct the input.

## Existing endpoints preserved

- `GET /urls`: lists generated and custom-code URLs with the existing response shape.
- `GET /urls/{code}`: returns details using the exact code supplied in the route.
- `GET /{code}`: redirects to the original URL and increments `Clicks` for either
  generated or custom codes.

No new route is introduced and no existing response field is removed or renamed.
