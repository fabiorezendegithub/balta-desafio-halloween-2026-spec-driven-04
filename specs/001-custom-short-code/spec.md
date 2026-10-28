# Feature Specification: Custom Short Codes

**Feature Branch**: `001-custom-short-code`

**Created**: 2026-10-27

**Status**: Draft

**Input**: User description: "Permitir que o usuário informe um código customizado ao encurtar uma URL, para depois navegar por ele, mantendo geração automática quando não informado."

## Clarifications

### Session 2026-10-27

- Q: A unicidade dos códigos deve diferenciar letras maiúsculas e minúsculas? → A: Sim. `Balta` e `balta` são códigos diferentes.
- Q: Quando o campo de código for enviado vazio ou contendo apenas espaços, como ele deve ser tratado? → A: Rejeitar como código inválido.
- Q: Quais códigos devem ser reservados para evitar conflito com as rotas existentes? → A: Reservar somente `urls` e `openapi`.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Criar URL com código escolhido (Priority: P1)

Como usuário, quero informar um código legível ao criar uma URL curta para poder
compartilhar e acessar um endereço que faça sentido para mim.

**Why this priority**: É o objetivo principal da feature e permite que o usuário
controle o endereço curto desde a criação.

**Independent Test**: Criar uma URL com um código válido e confirmar que a resposta
retorna esse código e que acessá-lo redireciona para a URL original.

**Acceptance Scenarios**:

1. **Given** que o código informado atende às regras e ainda não foi usado, **When**
   o usuário cria uma URL com esse código, **Then** a URL curta criada usa exatamente
   o código informado.
2. **Given** uma URL criada com código customizado, **When** o usuário navega pelo
   código, **Then** é redirecionado para a URL original e o contador de cliques aumenta
   como no fluxo existente.

---

### User Story 2 - Continuar usando código automático (Priority: P1)

Como usuário, quero continuar omitindo o código para que o fluxo atual de criação
de URLs não mude.

**Why this priority**: A compatibilidade com clientes e usos existentes é essencial
porque o código customizado é uma extensão do fluxo atual.

**Independent Test**: Criar uma URL sem informar código e confirmar que um código
válido é gerado, que a navegação funciona e que a listagem e os detalhes permanecem
disponíveis.

**Acceptance Scenarios**:

1. **Given** que o campo de código não foi informado, **When** o usuário cria uma
   URL, **Then** o sistema gera um código automaticamente e mantém a resposta atual.
2. **Given** uma URL com código gerado ou customizado, **When** o usuário consulta a
   listagem ou os detalhes, **Then** recebe as mesmas informações e formato de
   resposta já oferecidos.

---

### User Story 3 - Receber feedback para código inválido ou indisponível (Priority: P1)

Como usuário, quero saber por que um código não pode ser usado para poder escolher
outro sem criar uma URL incorreta ou ambígua.

**Why this priority**: Validações claras evitam colisões, rotas inacessíveis e
tentativas repetidas sem orientação.

**Independent Test**: Tentar criar URLs usando códigos duplicados, reservados e fora
do formato e verificar que cada tentativa é recusada com uma mensagem em português.

**Acceptance Scenarios**:

1. **Given** que o código informado já pertence a outra URL, **When** o usuário tenta
   criá-la novamente, **Then** a criação é recusada e informa que o código está em uso.
2. **Given** que o código contém caracteres fora de letras, números, hífen e
   underscore, **When** o usuário tenta criar a URL, **Then** a criação é recusada
   informando a regra de formato.
3. **Given** que o código tem menos de 3 ou mais de 32 caracteres, **When** o usuário
   tenta criar a URL, **Then** a criação é recusada informando os limites permitidos.
4. **Given** que o código coincide com uma rota reservada da API, **When** o usuário
   tenta criar a URL, **Then** a criação é recusada informando que o código não está
   disponível.

### Edge Cases

- O campo de código ausente ou nulo deve ser tratado como ausência de código e manter
  a geração automática; um valor vazio ou composto apenas por espaços deve ser
  rejeitado como código inválido, sem criar um código inesperado.
- A unicidade deve ser preservada mesmo quando duas criações com o mesmo código
  ocorrem praticamente ao mesmo tempo; no máximo uma pode ser aceita.
- A comparação com códigos reservados deve ser independente de maiúsculas e
  minúsculas para impedir que uma variação contorne a proteção de rota.
- O código customizado não deve alterar o comportamento de redirecionamento, da
  contagem de cliques, da listagem ou dos detalhes.
- A URL original continua sujeita às validações existentes, independentemente de o
  código ser gerado ou customizado.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: O sistema MUST aceitar um campo de código opcional na criação de uma
  URL curta.
- **FR-002**: O sistema MUST gerar automaticamente um código quando o campo não for
  informado, preservando o comportamento existente.
- **FR-002a**: O sistema MUST rejeitar um campo de código enviado vazio ou composto
  apenas por espaços, em vez de gerar um código automaticamente.
- **FR-003**: O sistema MUST aceitar códigos customizados contendo somente letras,
  números, hífen e underscore.
- **FR-004**: O sistema MUST exigir que códigos customizados tenham entre 3 e 32
  caracteres, inclusive.
- **FR-005**: O sistema MUST rejeitar códigos customizados já associados a outra URL
  e informar, em português, que o código está em uso.
- **FR-006**: O sistema MUST rejeitar códigos reservados que conflitem com rotas
  estáticas ou áreas funcionais da API, incluindo `urls` e `openapi`, sem diferenciar
  maiúsculas de minúsculas.
- **FR-007**: O sistema MUST informar em português a razão de uma rejeição por
  formato, tamanho, reserva ou duplicidade.
- **FR-008**: O sistema MUST garantir que uma tentativa concorrente de criar o mesmo
  código não resulte em duas URLs aceitas com esse código.
- **FR-009**: O sistema MUST usar o código customizado ou gerado para formar o
  endereço curto retornado ao usuário.
- **FR-010**: O sistema MUST redirecionar pelo código customizado para a URL original
  e contar o clique exatamente como no fluxo existente.
- **FR-011**: O sistema MUST manter a listagem e a consulta de detalhes funcionando
  sem diferença para códigos gerados e customizados.
- **FR-012**: O sistema MUST preservar os contratos existentes quando o campo
  opcional de código não for enviado.
- **FR-013**: A feature MUST NOT incluir edição ou remoção de códigos, autenticação
  ou expiração de links.

### Key Entities *(include if feature involves data)*

- **URL curta**: Recurso criado a partir de uma URL original, com um código único,
  endereço curto, data de criação e contador de cliques. O código pode ser gerado
  pelo sistema ou escolhido pelo usuário.
- **Código reservado**: Identificador que não pode ser atribuído a uma URL curta
  porque é necessário para uma rota ou área funcional da API.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Em 100% das criações com código válido e disponível, o endereço curto
  retornado contém exatamente o código solicitado.
- **SC-002**: Em 100% das criações sem código, o fluxo existente continua gerando um
  código válido sem exigir mudança no cliente.
- **SC-003**: Em 100% das tentativas com código duplicado, reservado ou fora das
  regras, a URL não é criada e o usuário recebe uma mensagem de erro em português.
- **SC-004**: Em 100% dos acessos a um código customizado criado com sucesso, o
  usuário é redirecionado para a URL original e o clique é contabilizado.
- **SC-005**: Em 100% das consultas de listagem e detalhes, URLs com códigos gerados
  e customizados aparecem com o mesmo conjunto de informações do contrato existente.
- **SC-006**: Em testes de concorrência com duas criações simultâneas para o mesmo
  código, exatamente uma criação é aceita e a outra é recusada como duplicada.

## Assumptions

- O limite máximo de 32 caracteres acompanha o limite já suportado para códigos
  existentes; o limite mínimo de 3 caracteres evita códigos ambíguos e muito curtos.
- A comparação de unicidade entre códigos diferencia maiúsculas e minúsculas, em
  compatibilidade com o comportamento atual; portanto, `Balta` e `balta` podem
  coexistir. A comparação de códigos reservados permanece independente de
  maiúsculas e minúsculas.
- Os únicos códigos reservados nesta feature são os segmentos estáticos atualmente
  usados pelas rotas da API: `urls` e `openapi`.
- Mensagens e cenários de erro serão exercitados no arquivo de requisições existente,
  pois o projeto não possui um projeto de testes dedicado.
- Não há alteração de dados necessária para representar o código customizado, pois
  URLs existentes já possuem um campo de código com limite suficiente.
