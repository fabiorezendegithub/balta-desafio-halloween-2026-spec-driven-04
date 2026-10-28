# Plano de Implementação: [FEATURE]

**Branch**: `[###-feature-name]` | **Data**: [DATE] | **Spec**: [link]

**Entrada**: Especificação da feature em `/specs/[###-feature-name]/spec.md`

**Observação**: Este template é preenchido pelo comando `$speckit-plan`; sua definição
descreve o fluxo de execução.

## Resumo

[Extraia da especificação da feature: requisito principal + abordagem técnica da pesquisa]

## Contexto Técnico

<!--
  AÇÃO NECESSÁRIA: Substitua o conteúdo desta seção pelos detalhes técnicos do projeto.
  A estrutura é apresentada como orientação para conduzir a iteração.
-->

**Linguagem/Versão**: [ex.: Python 3.11, Swift 5.9, Rust 1.75 ou NEEDS CLARIFICATION]

**Dependências Principais**: [ex.: FastAPI, UIKit, LLVM ou NEEDS CLARIFICATION]

**Armazenamento**: [quando aplicável, ex.: PostgreSQL, CoreData, arquivos ou N/A]

**Testes**: [ex.: pytest, XCTest, cargo test ou NEEDS CLARIFICATION]

**Plataforma-Alvo**: [ex.: servidor Linux, iOS 15+, WASM ou NEEDS CLARIFICATION]

**Tipo de Projeto**: [ex.: biblioteca/cli/serviço-web/aplicativo-móvel/compilador/aplicativo desktop ou NEEDS CLARIFICATION]

**Metas de Desempenho**: [específicas do domínio, ex.: 1000 req/s, 10k linhas/s, 60 fps ou NEEDS CLARIFICATION]

**Restrições**: [específicas do domínio, ex.: <200ms p95, <100MB de memória, funcionamento offline ou NEEDS CLARIFICATION]

**Escala/Escopo**: [específicos do domínio, ex.: 10k usuários, 1M LOC, 50 telas ou NEEDS CLARIFICATION]

## Verificação da Constituição

*GATE: Deve passar antes da pesquisa da Fase 0. Reavaliar após o design da Fase 1.*

[Gates determinados com base no arquivo da constituição]

## Estrutura do Projeto

### Documentação desta feature

```text
specs/[###-feature]/
├── plan.md              # Saída deste arquivo pelo comando $speckit-plan
├── research.md          # Saída da Fase 0 pelo comando $speckit-plan
├── data-model.md        # Saída da Fase 1 pelo comando $speckit-plan
├── quickstart.md        # Saída da Fase 1 pelo comando $speckit-plan
├── contracts/           # Saída da Fase 1 pelo comando $speckit-plan
└── tasks.md             # Saída da Fase 2 pelo comando $speckit-tasks - NÃO criado pelo $speckit-plan
```

### Código-fonte (raiz do repositório)
<!--
  AÇÃO NECESSÁRIA: Substitua a árvore de placeholders abaixo pelo layout concreto
  desta feature. Remova opções não utilizadas e amplie a estrutura escolhida com
  caminhos reais (ex.: apps/admin, packages/something). O plano entregue não deve
  conter rótulos de Opção.
-->

```text
# [REMOVE IF UNUSED] Opção 1: Projeto único (PADRÃO)
src/
├── models/
├── services/
├── cli/
└── lib/

tests/
├── contract/
├── integration/
└── unit/

# [REMOVE IF UNUSED] Opção 2: Aplicação web (quando "frontend" + "backend" forem detectados)
backend/
├── src/
│   ├── models/
│   ├── services/
│   └── api/
└── tests/

frontend/
├── src/
│   ├── components/
│   ├── pages/
│   └── services/
└── tests/

# [REMOVE IF UNUSED] Opção 3: Mobile + API (quando "iOS/Android" forem detectados)
api/
└── [same as backend above]

ios/ ou android/
└── [platform-specific structure: feature modules, UI flows, platform tests]
```

**Decisão de Estrutura**: [Documente a estrutura selecionada e faça referência aos
diretórios reais registrados acima]

## Controle de Complexidade

> **Preencha SOMENTE se a Verificação da Constituição tiver violações que precisem ser justificadas**

| Violação | Por que é necessária | Alternativa mais simples rejeitada porque |
|---|---|---|
| [ex.: 4º projeto] | [necessidade atual] | [por que 3 projetos não são suficientes] |
| [ex.: Padrão Repository] | [problema específico] | [por que o acesso direto ao banco é insuficiente] |
