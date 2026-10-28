# Checklist de [CHECKLIST TYPE]: [FEATURE NAME]

**Objetivo**: [Breve descrição do que este checklist cobre]
**Criado em**: [DATE]
**Feature**: [Link para spec.md ou documentação relevante]

**Observação**: Este checklist personalizado é gerado pelo comando `$speckit-checklist`
com base no contexto e nos requisitos da feature.
**Responsabilidade da Revisão**: Este checklist é um artefato de revisão da qualidade
dos requisitos sob responsabilidade do revisor. Marque um item como `[x]` somente
quando o revisor determinar que o critério de qualidade dos requisitos foi satisfeito.
**Semântica dos Marcadores**: `[x]` significa que o critério foi revisado e satisfeito
quanto à qualidade dos requisitos. Isso não significa que o trabalho de implementação
foi concluído.

<!--
  ============================================================================
  IMPORTANTE: Os itens de checklist abaixo são apenas EXEMPLOS ilustrativos.

  O comando $speckit-checklist MUST substituí-los por itens reais com base em:
  - Pedido específico de checklist feito pelo usuário
  - Requisitos da feature em spec.md
  - Contexto técnico em plan.md
  - Detalhes de implementação em tasks.md

  NÃO mantenha estes itens de exemplo no arquivo de checklist gerado.
  ============================================================================
-->

## [Category 1]

- [ ] CHK001 Primeiro item do checklist com ação clara
- [ ] CHK002 Segundo item do checklist
- [ ] CHK003 Terceiro item do checklist

## [Category 2]

- [ ] CHK004 Outro item da categoria
- [ ] CHK005 Item com critérios específicos
- [ ] CHK006 Último item desta categoria

## Observações

- Marque os itens como `[x]` somente depois que a revisão confirmar que o critério de qualidade dos requisitos foi satisfeito
- Deixe os itens desmarcados quando ainda exigirem esclarecimento, correção ou avaliação do revisor
- `$speckit-implement` lê o estado dos checkboxes como um gate e MUST NOT modificar os marcadores
- `checklists/requirements.md` possui um ciclo de vida interno separado, mantido por `$speckit-specify` e `$speckit-clarify`
- Adicione comentários ou achados diretamente no arquivo
- Inclua links para recursos ou documentação relevante
- Os itens são numerados sequencialmente para facilitar a referência
