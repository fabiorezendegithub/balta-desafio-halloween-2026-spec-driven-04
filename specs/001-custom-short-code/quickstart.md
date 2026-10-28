# Guia Rápido: Validar Códigos Curtos Personalizados

## Pré-requisitos

A partir da raiz do repositório:

```bash
cd src/EncurtaUrl/src/EncurtaUrl.Api
dotnet build
dotnet run
```

A aplicação usa a URL de desenvolvimento existente `http://localhost:5080`. O banco
SQLite é criado automaticamente, como no projeto atual.

## Cenários

Use `EncurtaUrl.Api.http` ou requisições HTTP equivalentes.

1. **Código personalizado com sucesso**: `POST /urls` com `url` e `code: "balta-io"`
   retorna 201 e a resposta usa `/balta-io`.
2. **Redirecionamento personalizado com sucesso**: `GET /balta-io` retorna um
   redirecionamento para a URL original; repita ou consulte os detalhes para confirmar
   que a contagem de cliques aumentou.
3. **Geração automática compatível**: `POST /urls` somente com `url` retorna 201 com
   um código gerado e os campos de response existentes.
4. **Código nulo compatível**: `POST /urls` com `"code": null` retorna 201 com um
   código gerado, assim como quando o campo é omitido.
5. **Listagem e detalhes**: `GET /urls` e `GET /urls/balta-io` incluem a URL
   personalizada com os mesmos campos das URLs geradas.
6. **Formato/tamanho inválidos**: envie códigos com espaços ou símbolos não permitidos,
   menos de 3 caracteres e mais de 32 caracteres; cada requisição retorna 400 com uma
   mensagem de validação em português.
7. **Código vazio**: envie um código vazio ou somente com espaços; a resposta deve
   ser 400, sem gerar um código.
8. **Código reservado**: envie `urls`, `URLs`, `openapi` ou `OpenApi`; cada requisição
   retorna 400 porque o nome conflita com uma rota reservada.
9. **Código duplicado**: crie `balta-io` e depois envie outra URL com o mesmo código
   exato; a segunda requisição retorna 409 e informa que o código está em uso.
10. **Identidade sensível a maiúsculas**: crie `Balta` e `balta`; ambos são aceitos
    como códigos distintos, salvo se algum valor for reservado.
11. **Código duplicado concorrente**: envie duas requisições quase simultâneas com o
    mesmo código personalizado ainda não usado. Exatamente uma deve retornar 201 e a
    outra 409. Um comando para exercitar o cenário é:

    ```bash
    for i in 1 2; do
      curl -sS -o "/tmp/custom-code-$i.json" -w "%{http_code}\n" \
        -X POST http://localhost:5080/urls \
        -H 'Content-Type: application/json' \
        -d '{"url":"https://balta.io/concurrent-'"$i"'","code":"concorrente-unico"}' &
    done
    wait
    ```

    Os dois códigos de status devem ser um `201` e um `409`.

## Verificações de conclusão

- Todos os cenários de sucesso e erro estão presentes em `EncurtaUrl.Api.http`.
- `dotnet build` termina sem novos warnings.
- Nenhum pacote NuGet, migration, alteração de schema, endpoint, projeto de testes,
  fluxo de edição, remoção, autenticação ou expiração foi adicionado.
