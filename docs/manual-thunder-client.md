# Manual de Testes com Thunder Client

Este guia mostra como testar a API EncurtaUrl usando a extensão Thunder Client
no Visual Studio Code.

## 1. Pré-requisitos

1. Instale o [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).
2. Abra o projeto no Visual Studio Code.
3. Inicie a API no terminal integrado:

   ```bash
   cd src/EncurtaUrl/src/EncurtaUrl.Api
   dotnet run
   ```

4. Confirme que a API está disponível em `http://localhost:5080`.

## 2. Instalar o Thunder Client

1. No Visual Studio Code, abra a área de extensões com `Ctrl+Shift+X`.
2. Pesquise por `Thunder Client`.
3. Instale a extensão publicada pela Thunder Client.
4. Depois da instalação, o ícone do Thunder Client aparecerá na barra lateral.

## 3. Criar uma requisição

1. Clique no ícone do Thunder Client.
2. Selecione **New Request**.
3. Escolha o método HTTP, informe a URL e clique em **Send**.
4. Para requisições com JSON, abra a aba **Body**, selecione **JSON** e informe o
   conteúdo no editor.

Para evitar repetir a URL, use:

```text
http://localhost:5080
```

## 4. Criar uma URL com código personalizado

Configure a requisição:

- Método: `POST`
- URL: `http://localhost:5080/urls`
- Aba **Body** → **JSON**

```json
{
  "url": "https://balta.io/thunder-client",
  "code": "thunder-demo"
}
```

Clique em **Send**. O resultado esperado é `201 Created` e a resposta deve
conter `code: "thunder-demo"`.

## 5. Testar o redirecionamento

Crie uma nova requisição:

- Método: `GET`
- URL: `http://localhost:5080/thunder-demo`

Clique em **Send**. O resultado esperado é `302 Found`, com redirecionamento
para `https://balta.io/thunder-client`.

O acesso também incrementa o campo `clicks` da URL curta.

## 6. Consultar detalhes e listar URLs

### Detalhes

- Método: `GET`
- URL: `http://localhost:5080/urls/thunder-demo`

O resultado esperado é `200 OK`, com os campos `code`, `shortUrl`,
`originalUrl`, `createdAt` e `clicks`.

### Listagem

- Método: `GET`
- URL: `http://localhost:5080/urls`

O resultado esperado é `200 OK`, com uma lista contendo as URLs criadas.

## 7. Testar geração automática

Crie uma requisição `POST` para `http://localhost:5080/urls` com o seguinte
corpo:

```json
{
  "url": "https://balta.io/automatico"
}
```

O resultado esperado é `201 Created`, com um código gerado automaticamente.

Também é válido enviar explicitamente `"code": null`:

```json
{
  "url": "https://balta.io/nulo",
  "code": null
}
```

## 8. Testar validações e conflitos

Use uma nova requisição `POST` para `http://localhost:5080/urls` em cada caso.

| Corpo | Resultado esperado |
| --- | --- |
| `{"url":"https://balta.io/a","code":"ab"}` | `400` — código menor que 3 caracteres |
| `{"url":"https://balta.io/a","code":"inválido"}` | `400` — caractere não permitido |
| `{"url":"https://balta.io/a","code":""}` | `400` — código vazio |
| `{"url":"https://balta.io/a","code":"   "}` | `400` — código só com espaços |
| `{"url":"https://balta.io/a","code":"OpenApi"}` | `400` — código reservado |
| Reutilizar `thunder-demo` | `409` — código em uso |
| `{"url":"nao-e-url","code":"codigo-valido"}` | `400` — URL original inválida |

As mensagens aparecem na resposta JSON, na seção **Response** do Thunder Client.

## 9. Salvar e organizar requisições

Para manter os testes organizados:

1. Clique em **Save** depois de configurar uma requisição.
2. Crie uma coleção chamada `EncurtaUrl`.
3. Separe as requisições em grupos, por exemplo:
   - Criação;
   - Consulta;
   - Redirecionamento;
   - Validações;
   - Conflitos.
4. Use nomes descritivos, como `Criar com código personalizado` e `Código
   duplicado`.

## 10. Conferência final

Uma verificação básica deve confirmar:

- `POST /urls` com código personalizado retorna `201`;
- `GET /{code}` retorna `302` e incrementa `clicks`;
- `GET /urls` e `GET /urls/{code}` retornam `200`;
- código inválido retorna `400`;
- código duplicado retorna `409`;
- criação sem `code` continua gerando um código automaticamente.

