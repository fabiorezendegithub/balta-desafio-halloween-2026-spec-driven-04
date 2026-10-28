# Pesquisa: Códigos Curtos Personalizados

## Decisão 1: Estender o request de criação existente

- **Decisão**: Adicionar um campo `Code` opcional a `CreateShortUrlRequest` e manter
  `POST /urls` como o único endpoint de criação.
- **Justificativa**: Isso preserva os clientes existentes que enviam somente `url` e
  segue a regra de compatibilidade da constituição para novos campos de request.
- **Alternativas consideradas**: Um segundo endpoint ou um campo obrigatório foram
  rejeitados porque a feature é uma extensão do fluxo atual e quebraria clientes.

## Decisão 2: Manter as regras de código em um serviço pequeno

- **Decisão**: Adicionar um validator focado em `Services/` para tratar códigos
  opcionais/personalizados. Ele valida caracteres permitidos, tamanho 3–32 e nomes
  reservados `urls` e `openapi` sem diferenciar maiúsculas de minúsculas. A unicidade
  dos códigos continua diferenciando maiúsculas e minúsculas.
- **Justificativa**: Isso isola regras reutilizáveis sem introduzir um pacote de
  validação ou uma camada de aplicação adicional.
- **Alternativas consideradas**: Checks com regular expression espalhados pelo
  endpoint e um pacote de validação de terceiros foram rejeitados pelo custo de
  duplicação, dependência e complexidade.

## Decisão 3: Preservar o modelo de banco existente

- **Decisão**: Reutilizar a coluna `ShortUrls.Code`, o limite de 32 caracteres e o
  índice único existentes. Não adicionar migrations ou alterações de schema.
- **Justificativa**: A entidade atual já armazena códigos gerados e personalizados. A
  feature altera a escolha da entrada, não o formato da persistência.
- **Alternativas consideradas**: Uma tabela separada de aliases ou uma migration de
  schema foram rejeitadas porque adicionariam complexidade desnecessária ao modelo e
  violariam o escopo.

## Decisão 4: Tratar colisões concorrentes de código personalizado como HTTP 409

- **Decisão**: Manter a verificação antes da inserção para fornecer um erro antecipado
  claro e capturar a `DbUpdateException` de restrição única durante o save. Quando o
  código solicitado for personalizado, retornar `ProblemDetails` em português com
  status 409. Para geração automática, preservar o comportamento e o tratamento de
  erro atuais.
- **Justificativa**: A restrição do banco fecha a corrida entre verificação e inserção;
  409 comunica corretamente que o identificador solicitado já está em uso.
- **Alternativas consideradas**: Depender somente da verificação anterior foi rejeitado
  porque duas requisições concorrentes podem passar por ela. Retornar 500 foi rejeitado
  porque o conflito pode ser corrigido pelo cliente.

## Decisão 5: Validar nomes reservados sem diferenciar maiúsculas

- **Decisão**: Rejeitar `urls`, `openapi` e variações como `URLs` e `OpenApi`; preservar
  a capitalização exata dos demais códigos personalizados aceitos.
- **Justificativa**: Nomes de rotas reservadas não podem ser contornados por variações
  de capitalização, enquanto a identidade dos códigos existentes continua sensível a
  maiúsculas e minúsculas.
- **Alternativas consideradas**: Reservar uma lista ampla de nomes comuns foi
  rejeitado porque somente os segmentos estáticos atuais estão no escopo.
