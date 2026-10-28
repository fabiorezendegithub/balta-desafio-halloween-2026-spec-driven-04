---

description: "Template de lista de tarefas para implementação de feature"
---

# Tarefas: [FEATURE NAME]

**Entrada**: Documentos de design em `/specs/[###-feature-name]/`

**Pré-requisitos**: plan.md (obrigatório), spec.md (obrigatório para histórias de usuário), research.md, data-model.md, contracts/

**Testes**: Os exemplos abaixo incluem tarefas de testes. Tarefas de testes são OPCIONAIS — inclua-as somente se forem solicitadas explicitamente na especificação da feature.

**Organização**: As tarefas são agrupadas por história de usuário para permitir a implementação e o teste independentes de cada história.

## Formato: `[ID] [P?] [Story] Descrição`

- **[P]**: Pode ser executada em paralelo (arquivos diferentes, sem dependências)
- **[Story]**: A qual história de usuário a tarefa pertence (ex.: US1, US2, US3)
- Inclua caminhos exatos dos arquivos nas descrições

## Convenções de Caminho

- **Projeto único**: `src/`, `tests/` na raiz do repositório
- **Aplicação web**: `backend/src/`, `frontend/src/`
- **Mobile**: `api/src/`, `ios/src/` ou `android/src/`
- Os caminhos abaixo assumem um projeto único — ajuste-os conforme `plan.md`

<!--
  ============================================================================
  IMPORTANTE: As tarefas abaixo são apenas EXEMPLOS ilustrativos.

  O comando $speckit-tasks MUST substituí-las por tarefas reais com base em:
  - Histórias de usuário de spec.md e suas prioridades P1, P2, P3...
  - Requisitos da feature em plan.md
  - Entidades em data-model.md
  - Endpoints em contracts/

  As tarefas MUST ser organizadas por história de usuário para que cada história possa ser:
  - Implementada de forma independente
  - Testada de forma independente
  - Entregue como um incremento de MVP

  NÃO mantenha estas tarefas de exemplo no arquivo gerado.
  ============================================================================
-->

## Fase 1: Preparação (Infraestrutura Compartilhada)

**Objetivo**: Inicialização do projeto e estrutura básica

- [ ] T001 Criar a estrutura do projeto conforme o plano de implementação
- [ ] T002 Inicializar o projeto [language] com as dependências [framework]
- [ ] T003 [P] Configurar ferramentas de lint e formatação

---

## Fase 2: Fundação (Pré-requisitos Bloqueadores)

**Objetivo**: Infraestrutura central que MUST estar concluída antes que QUALQUER história de usuário seja implementada

**⚠️ CRÍTICO**: Nenhum trabalho de história de usuário pode começar até esta fase ser concluída

Exemplos de tarefas de fundação (ajuste conforme o projeto):

- [ ] T004 Configurar schema do banco e framework de migrations
- [ ] T005 [P] Implementar framework de autenticação/autorização
- [ ] T006 [P] Configurar estrutura de rotas da API e middleware
- [ ] T007 Criar modelos/entidades-base dos quais todas as histórias dependem
- [ ] T008 Configurar infraestrutura de tratamento de erros e logging
- [ ] T009 Configurar gerenciamento de ambiente

**Checkpoint**: Fundação pronta — a implementação das histórias de usuário pode começar em paralelo

---

## Fase 3: História de Usuário 1 - [Title] (Prioridade: P1) 🎯 MVP

**Objetivo**: [Breve descrição do que esta história entrega]

**Teste independente**: [Como verificar que esta história funciona sozinha]

### Testes da História de Usuário 1 (OPCIONAL — somente se testes forem solicitados) ⚠️

> **OBSERVAÇÃO: Escreva estes testes PRIMEIRO e garanta que FALHEM antes da implementação**

- [ ] T010 [P] [US1] Teste de contrato para [endpoint] em tests/contract/test_[name].py
- [ ] T011 [P] [US1] Teste de integração para [user journey] em tests/integration/test_[name].py

### Implementação da História de Usuário 1

- [ ] T012 [P] [US1] Criar o modelo [Entity1] em src/models/[entity1].py
- [ ] T013 [P] [US1] Criar o modelo [Entity2] em src/models/[entity2].py
- [ ] T014 [US1] Implementar [Service] em src/services/[service].py (depende de T012, T013)
- [ ] T015 [US1] Implementar [endpoint/feature] em src/[location]/[file].py
- [ ] T016 [US1] Adicionar validação e tratamento de erros
- [ ] T017 [US1] Adicionar logging das operações da história de usuário 1

**Checkpoint**: Neste ponto, a História de Usuário 1 deve estar completamente funcional e testável de forma independente

---

## Fase 4: História de Usuário 2 - [Title] (Prioridade: P2)

**Objetivo**: [Breve descrição do que esta história entrega]

**Teste independente**: [Como verificar que esta história funciona sozinha]

### Testes da História de Usuário 2 (OPCIONAL — somente se testes forem solicitados) ⚠️

- [ ] T018 [P] [US2] Teste de contrato para [endpoint] em tests/contract/test_[name].py
- [ ] T019 [P] [US2] Teste de integração para [user journey] em tests/integration/test_[name].py

### Implementação da História de Usuário 2

- [ ] T020 [P] [US2] Criar o modelo [Entity] em src/models/[entity].py
- [ ] T021 [US2] Implementar [Service] em src/services/[service].py
- [ ] T022 [US2] Implementar [endpoint/feature] em src/[location]/[file].py
- [ ] T023 [US2] Integrar com os componentes da História de Usuário 1 (se necessário)

**Checkpoint**: Neste ponto, as Histórias de Usuário 1 E 2 devem funcionar de forma independente

---

## Fase 5: História de Usuário 3 - [Title] (Prioridade: P3)

**Objetivo**: [Breve descrição do que esta história entrega]

**Teste independente**: [Como verificar que esta história funciona sozinha]

### Testes da História de Usuário 3 (OPCIONAL — somente se testes forem solicitados) ⚠️

- [ ] T024 [P] [US3] Teste de contrato para [endpoint] em tests/contract/test_[name].py
- [ ] T025 [P] [US3] Teste de integração para [user journey] em tests/integration/test_[name].py

### Implementação da História de Usuário 3

- [ ] T026 [P] [US3] Criar o modelo [Entity] em src/models/[entity].py
- [ ] T027 [US3] Implementar [Service] em src/services/[service].py
- [ ] T028 [US3] Implementar [endpoint/feature] em src/[location]/[file].py

**Checkpoint**: Todas as histórias de usuário devem estar funcionando de forma independente

---

[Adicione mais fases de histórias de usuário conforme necessário, seguindo o mesmo padrão]

---

## Fase N: Acabamento e Preocupações Transversais

**Objetivo**: Melhorias que afetam várias histórias de usuário

- [ ] TXXX [P] Atualizações de documentação em docs/
- [ ] TXXX Limpeza de código e refatoração
- [ ] TXXX Otimização de desempenho em todas as histórias
- [ ] TXXX [P] Testes unitários adicionais (se solicitados) em tests/unit/
- [ ] TXXX Fortalecimento de segurança
- [ ] TXXX Executar a validação de quickstart.md

---

## Dependências e Ordem de Execução

### Dependências entre Fases

- **Preparação (Fase 1)**: Sem dependências — pode começar imediatamente
- **Fundação (Fase 2)**: Depende da conclusão da Preparação — BLOQUEIA todas as histórias de usuário
- **Histórias de Usuário (Fase 3+)**: Todas dependem da conclusão da Fundação
  - As histórias podem começar em paralelo (se houver equipe disponível)
  - Ou sequencialmente na ordem de prioridade (P1 → P2 → P3)
- **Acabamento (Fase final)**: Depende da conclusão de todas as histórias desejadas

### Dependências entre Histórias de Usuário

- **História de Usuário 1 (P1)**: Pode começar após a Fundação (Fase 2) — sem dependências de outras histórias
- **História de Usuário 2 (P2)**: Pode começar após a Fundação (Fase 2) — pode integrar com US1, mas deve ser testável de forma independente
- **História de Usuário 3 (P3)**: Pode começar após a Fundação (Fase 2) — pode integrar com US1/US2, mas deve ser testável de forma independente

### Dentro de Cada História de Usuário

- Testes (se incluídos) MUST ser escritos e FALHAR antes da implementação
- Modelos antes de serviços
- Serviços antes de endpoints
- Implementação principal antes da integração
- Concluir uma história antes de avançar para a próxima prioridade

### Oportunidades de Paralelismo

- Todas as tarefas de Preparação marcadas com [P] podem ser executadas em paralelo
- Todas as tarefas de Fundação marcadas com [P] podem ser executadas em paralelo (dentro da Fase 2)
- Depois que a Fundação for concluída, todas as histórias podem começar em paralelo (se houver equipe disponível)
- Todos os testes de uma história marcados com [P] podem ser executados em paralelo
- Modelos dentro de uma história marcados com [P] podem ser executados em paralelo
- Histórias diferentes podem ser trabalhadas em paralelo por pessoas diferentes

---

## Exemplo de Paralelismo: História de Usuário 1

```bash
# Inicie todos os testes da História de Usuário 1 juntos (se testes forem solicitados):
Tarefa: "Teste de contrato para [endpoint] em tests/contract/test_[name].py"
Tarefa: "Teste de integração para [user journey] em tests/integration/test_[name].py"

# Inicie todos os modelos da História de Usuário 1 juntos:
Tarefa: "Criar o modelo [Entity1] em src/models/[entity1].py"
Tarefa: "Criar o modelo [Entity2] em src/models/[entity2].py"
```

---

## Estratégia de Implementação

### Primeiro o MVP (Somente História de Usuário 1)

1. Concluir a Fase 1: Preparação
2. Concluir a Fase 2: Fundação (CRÍTICA — bloqueia todas as histórias)
3. Concluir a Fase 3: História de Usuário 1
4. **PARAR E VALIDAR**: Testar a História de Usuário 1 de forma independente
5. Fazer deploy/demonstração se estiver pronta

### Entrega Incremental

1. Concluir Preparação + Fundação → fundação pronta
2. Adicionar História de Usuário 1 → testar de forma independente → fazer deploy/demonstração (MVP!)
3. Adicionar História de Usuário 2 → testar de forma independente → fazer deploy/demonstração
4. Adicionar História de Usuário 3 → testar de forma independente → fazer deploy/demonstração
5. Cada história adiciona valor sem quebrar as anteriores

### Estratégia de Equipe em Paralelo

Com várias pessoas desenvolvedoras:

1. A equipe conclui Preparação + Fundação em conjunto
2. Depois que a Fundação for concluída:
   - Pessoa A: História de Usuário 1
   - Pessoa B: História de Usuário 2
   - Pessoa C: História de Usuário 3
3. As histórias são concluídas e integradas de forma independente

---

## Observações

- Tarefas [P] = arquivos diferentes, sem dependências
- O label [Story] associa a tarefa a uma história de usuário específica
- Cada história de usuário deve poder ser concluída e testada de forma independente
- Verifique se os testes falham antes da implementação
- Faça commit após cada tarefa ou grupo lógico
- Pare em cada checkpoint para validar a história de forma independente
- Evite: tarefas vagas, conflitos no mesmo arquivo e dependências entre histórias que quebrem a independência
