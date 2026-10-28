# Especificação da Feature: [FEATURE NAME]

**Branch da Feature**: `[###-feature-name]`

**Criado em**: [DATE]

**Status**: Rascunho

**Entrada**: Descrição do usuário: "$ARGUMENTS"

## Cenários de Usuário e Testes *(obrigatório)*

<!--
  IMPORTANTE: As histórias de usuário devem ser JORNADAS DE USUÁRIO PRIORIZADAS,
  ordenadas por importância. Cada história/jornada deve ser TESTÁVEL DE FORMA
  INDEPENDENTE — isto é, se apenas UMA for implementada, ainda deve existir um MVP
  viável que entregue valor.

  Atribua prioridades (P1, P2, P3 etc.) a cada história, em que P1 é a mais crítica.
  Pense em cada história como uma fatia independente que pode ser:
  - Desenvolvida de forma independente
  - Testada de forma independente
  - Publicada de forma independente
  - Demonstrada de forma independente
-->

### História de Usuário 1 - [Brief Title] (Prioridade: P1)

[Descreva esta jornada do usuário em linguagem simples]

**Por que esta prioridade**: [Explique o valor e por que esta história possui esta prioridade]

**Teste independente**: [Descreva como testar esta história de forma independente — ex.: "Pode ser totalmente testada por [ação específica] e entrega [valor específico]"]

**Cenários de Aceitação**:

1. **Dado** [estado inicial], **Quando** [ação], **Então** [resultado esperado]
2. **Dado** [estado inicial], **Quando** [ação], **Então** [resultado esperado]

---

### História de Usuário 2 - [Brief Title] (Prioridade: P2)

[Descreva esta jornada do usuário em linguagem simples]

**Por que esta prioridade**: [Explique o valor e por que esta história possui esta prioridade]

**Teste independente**: [Descreva como testar esta história de forma independente]

**Cenários de Aceitação**:

1. **Dado** [estado inicial], **Quando** [ação], **Então** [resultado esperado]

---

### História de Usuário 3 - [Brief Title] (Prioridade: P3)

[Descreva esta jornada do usuário em linguagem simples]

**Por que esta prioridade**: [Explique o valor e por que esta história possui esta prioridade]

**Teste independente**: [Descreva como testar esta história de forma independente]

**Cenários de Aceitação**:

1. **Dado** [estado inicial], **Quando** [ação], **Então** [resultado esperado]

---

[Adicione mais histórias de usuário conforme necessário, cada uma com uma prioridade atribuída]

### Casos Extremos

<!--
  AÇÃO NECESSÁRIA: O conteúdo desta seção representa placeholders.
  Preencha-os com os casos extremos adequados.
-->

- O que acontece quando [condição de limite]?
- Como o sistema trata [cenário de erro]?

## Requisitos *(obrigatório)*

<!--
  AÇÃO NECESSÁRIA: O conteúdo desta seção representa placeholders.
  Preencha-os com os requisitos funcionais adequados.
-->

### Requisitos Funcionais

- **FR-001**: O sistema MUST [capacidade específica, ex.: "permitir que usuários criem contas"]
- **FR-002**: O sistema MUST [capacidade específica, ex.: "validar endereços de e-mail"]
- **FR-003**: Usuários MUST poder [interação principal, ex.: "redefinir a senha"]
- **FR-004**: O sistema MUST [requisito de dados, ex.: "persistir preferências do usuário"]
- **FR-005**: O sistema MUST [comportamento, ex.: "registrar todos os eventos de segurança"]

*Exemplo de como marcar requisitos pouco claros*:

- **FR-006**: O sistema MUST autenticar usuários por [NEEDS CLARIFICATION: método de autenticação não especificado — e-mail/senha, SSO, OAuth?]
- **FR-007**: O sistema MUST manter os dados por [NEEDS CLARIFICATION: período de retenção não especificado]

### Entidades Principais *(inclua se a feature envolver dados)*

- **[Entity 1]**: [O que representa, atributos principais sem implementação]
- **[Entity 2]**: [O que representa, relacionamentos com outras entidades]

## Critérios de Sucesso *(obrigatório)*

<!--
  AÇÃO NECESSÁRIA: Defina critérios de sucesso mensuráveis.
  Eles devem ser independentes de tecnologia e mensuráveis.
-->

### Resultados Mensuráveis

- **SC-001**: [Métrica mensurável, ex.: "Usuários conseguem concluir a criação de uma conta em menos de 2 minutos"]
- **SC-002**: [Métrica mensurável, ex.: "O sistema suporta 1000 usuários simultâneos sem degradação"]
- **SC-003**: [Métrica de satisfação do usuário, ex.: "90% dos usuários concluem a tarefa principal na primeira tentativa"]
- **SC-004**: [Métrica de negócio, ex.: "Reduzir em 50% os chamados de suporte relacionados a [X]"]

## Premissas

<!--
  AÇÃO NECESSÁRIA: O conteúdo desta seção representa placeholders.
  Preencha-os com as premissas adequadas, com base nos padrões razoáveis escolhidos
  quando a descrição da feature não especificou determinados pontos.
-->

- [Premissa sobre os usuários-alvo, ex.: "Usuários possuem conexão estável com a internet"]
- [Premissa sobre limites de escopo, ex.: "Suporte móvel está fora do escopo da v1"]
- [Premissa sobre dados/ambiente, ex.: "O sistema de autenticação existente será reutilizado"]
- [Dependência de sistema/serviço existente, ex.: "É necessário acesso à API existente de perfil do usuário"]
