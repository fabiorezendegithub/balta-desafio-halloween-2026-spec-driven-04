# Manual da API EncurtaUrl

## 1. Visão geral

O EncurtaUrl é uma API de encurtamento de URLs construída com .NET 10, Minimal
APIs, Entity Framework Core e SQLite.

Além da geração automática, a API permite que o usuário escolha um código curto
personalizado.

## 2. Pré-requisitos

- .NET 10 SDK instalado;
- terminal ou ferramenta HTTP, como `curl` ou o arquivo `.http` do projeto.

## 3. Executando a aplicação

Na raiz do repositório, execute:

```bash
cd src/EncurtaUrl/src/EncurtaUrl.Api
dotnet build
dotnet run
```

A API estará disponível em:

```text
http://localhost:5080
```

O banco SQLite `encurtaurl.db` é criado automaticamente na primeira execução.

## 4. Endpoints

| Método | Rota | Descrição |
| --- | --- | --- |
| `POST` | `/urls` | Cria uma URL curta |
| `GET` | `/urls` | Lista as URLs encurtadas |
| `GET` | `/urls/{code}` | Consulta os detalhes de uma URL |
| `GET` | `/{code}` | Redireciona para a URL original e incrementa os cliques |
| `GET` | `/openapi/v1.json` | Exibe o contrato OpenAPI em ambiente de desenvolvimento |

## 5. Criando uma URL com código automático

O campo `code` pode ser omitido. Nesse caso, a API gera um código de 7 caracteres.

```bash
curl -i -X POST http://localhost:5080/urls \
  -H 'Content-Type: application/json' \
  -d '{"url":"https://balta.io"}'
```

Também é possível enviar `code: null`; o comportamento será o mesmo.

## 6. Criando uma URL com código personalizado

O código personalizado deve:

- ter entre 3 e 32 caracteres;
- conter somente letras e números ASCII, hífen (`-`) ou underscore (`_`);
- não ser `urls` ou `openapi`, sem distinção entre maiúsculas e minúsculas;
- ainda não estar associado a outra URL.

Exemplo:

```bash
curl -i -X POST http://localhost:5080/urls \
  -H 'Content-Type: application/json' \
  -d '{"url":"https://balta.io","code":"balta-io"}'
```

A resposta será `201 Created` e conterá o código solicitado:

```json
{
  "code": "balta-io",
  "shortUrl": "http://localhost:5080/balta-io",
  "originalUrl": "https://balta.io",
  "createdAt": "2026-10-27T12:00:00Z",
  "clicks": 0
}
```

A capitalização é preservada. Portanto, `Balta` e `balta` são códigos diferentes.

## 7. Acessando uma URL curta

Use o código na rota raiz:

```bash
curl -i http://localhost:5080/balta-io
```

A API retorna `302 Found`, redireciona para a URL original e incrementa o campo
`clicks`.

## 8. Listagem e detalhes

Para listar todas as URLs:

```bash
curl -i http://localhost:5080/urls
```

Para consultar uma URL específica:

```bash
curl -i http://localhost:5080/urls/balta-io
```

URLs com código automático e URLs com código personalizado usam o mesmo formato
de resposta.

## 9. Erros esperados

### Código inválido — HTTP 400

Ocorre quando o código contém caracteres não permitidos, tem tamanho inválido,
é vazio ou corresponde a uma rota reservada.

```json
{
  "status": 400,
  "errors": {
    "code": [
      "O código personalizado deve ter entre 3 e 32 caracteres."
    ]
  }
}
```

### Código em uso — HTTP 409

Ocorre quando o código já existe ou quando duas requisições concorrentes tentam
criar o mesmo código. Apenas uma delas será aceita.

```json
{
  "status": 409,
  "title": "Código em uso.",
  "detail": "Escolha outro código personalizado; o código informado já está em uso."
}
```

### URL original inválida — HTTP 400

A URL original deve ser absoluta e começar com `http://` ou `https://`.

## 10. Teste de concorrência

O comando abaixo dispara duas criações simultâneas para o mesmo código:

```bash
code="concorrente-unico-1"

curl -sS -o /tmp/resposta-1.json -w "%{http_code}\n" \
  -X POST http://localhost:5080/urls \
  -H 'Content-Type: application/json' \
  -d "{\"url\":\"https://balta.io/1\",\"code\":\"$code\"}" &

curl -sS -o /tmp/resposta-2.json -w "%{http_code}\n" \
  -X POST http://localhost:5080/urls \
  -H 'Content-Type: application/json' \
  -d "{\"url\":\"https://balta.io/2\",\"code\":\"$code\"}" &

wait
```

O resultado esperado é exatamente uma resposta `201` e uma resposta `409`.
Repita o teste usando códigos ainda não utilizados.

## 11. Arquivo de testes HTTP

Para testar a API visualmente pelo Visual Studio Code, consulte o
[manual de testes com Thunder Client](manual-thunder-client.md).

Os cenários prontos estão em:

```text
src/EncurtaUrl/src/EncurtaUrl.Api/EncurtaUrl.Api.http
```

Esse arquivo pode ser executado pelo Visual Studio Code com a extensão REST Client
ou pelo editor HTTP do JetBrains Rider/IntelliJ.

## 12. Estrutura principal

```text
src/EncurtaUrl/src/EncurtaUrl.Api/
├── Contracts/ShortUrlContracts.cs
├── Data/AppDbContext.cs
├── Endpoints/ShortUrlEndpoints.cs
├── Models/ShortUrl.cs
├── Services/ShortCodeGenerator.cs
├── Services/ShortCodeValidator.cs
└── Program.cs
```

O banco mantém o índice único existente em `ShortUrls.Code`; não são necessárias
migrations ou alterações de schema para códigos personalizados.
