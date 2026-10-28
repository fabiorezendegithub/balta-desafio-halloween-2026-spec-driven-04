# Modelo de Dados: Códigos Curtos Personalizados

## Entidade existente: ShortUrl

A entidade `ShortUrl` existente permanece inalterada.

| Campo | Regra existente | Impacto da feature |
|---|---|---|
| `Id` | Identidade inteira | Nenhum |
| `Code` | Obrigatório, máximo de 32 caracteres, índice único | Armazena código gerado ou personalizado; a unicidade continua diferenciando maiúsculas e minúsculas |
| `OriginalUrl` | Obrigatório, máximo de 2048 caracteres | A validação existente da URL permanece inalterada |
| `CreatedAt` | Data/hora de criação em UTC | Nenhum |
| `Clicks` | Contador inteiro | O redirecionamento por código personalizado o incrementa como hoje |

## Alteração do contrato de entrada

`CreateShortUrlRequest` recebe um valor `Code` opcional.

- `Code` ausente ou nulo: manter a geração automática.
- `Code` vazio ou somente com espaços: erro de validação.
- `Code` não vazio: validar caracteres permitidos, tamanho e nomes reservados antes da
  persistência.
- A capitalização do código personalizado aceito é preservada exatamente.

## Regras de validação

- Caracteres permitidos: letras, números, hífen e underscore.
- Tamanho: de 3 a 32 caracteres, inclusive.
- Nomes reservados: `urls` e `openapi`, comparados sem diferenciar maiúsculas de
  minúsculas.
- Unicidade de códigos existentes: comparada diferenciando maiúsculas de minúsculas e
  garantida pelo índice único existente do banco.

## Estado e ciclo de vida

Nenhum estado ou ciclo de vida novo é introduzido. Uma URL curta é criada uma única vez
com um código gerado ou personalizado. Edição e remoção permanecem fora do escopo.
