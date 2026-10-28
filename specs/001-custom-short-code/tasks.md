---

description: "Implementation tasks for custom short codes"
---

# Tasks: Custom Short Codes

**Input**: Design documents from `/specs/001-custom-short-code/`

**Prerequisites**: `plan.md`, `spec.md`, `research.md`, `data-model.md`,
`contracts/http-api.md`, and `quickstart.md`

**Tests**: No test project is added. Manual success/error scenarios belong in
`EncurtaUrl.Api.http`; the project is verified with `dotnet build`.

**Organization**: Tasks are grouped by user story. Shared validation and contract
prerequisites are completed before story work.

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Confirm the existing project surfaces that the feature must preserve.

- [ ] T001 [P] Review the existing request, endpoint, and response flow in `src/EncurtaUrl/src/EncurtaUrl.Api/Contracts/ShortUrlContracts.cs` and `src/EncurtaUrl/src/EncurtaUrl.Api/Endpoints/ShortUrlEndpoints.cs` before editing.
- [ ] T002 [P] Confirm the existing 32-character `Code` limit and unique index in `src/EncurtaUrl/src/EncurtaUrl.Api/Data/AppDbContext.cs`, and confirm no schema or package change is required in `src/EncurtaUrl/src/EncurtaUrl.Api/EncurtaUrl.Api.csproj`.

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Add the shared custom-code validation capability without changing
persistence or introducing a new dependency.

**⚠️ CRITICAL**: Complete this phase before implementing user-story behavior.

- [ ] T003 Create `IShortCodeValidator` and `ShortCodeValidator` in `src/EncurtaUrl/src/EncurtaUrl.Api/Services/ShortCodeValidator.cs`, validating only letters, digits, hyphen, and underscore, requiring length 3–32 inclusive, rejecting `urls` and `openapi` case-insensitively, preserving accepted casing, and returning Portuguese validation reasons.
- [ ] T004 Register the validator in `src/EncurtaUrl/src/EncurtaUrl.Api/Program.cs` using the existing dependency-injection style, without adding a NuGet package or another application layer.

**Checkpoint**: The shared validator is available and the existing database model
remains unchanged.

---

## Phase 3: User Story 1 - Criar URL com código escolhido (Priority: P1) 🎯 MVP

**Goal**: Allow a valid, available custom code to be used by the existing create
flow and redirect route.

**Independent Test**: POST a valid custom code, confirm the response uses that code,
then GET the code and confirm redirection and click counting.

### Implementation for User Story 1

- [ ] T005 [US1] Extend `CreateShortUrlRequest` in `src/EncurtaUrl/src/EncurtaUrl.Api/Contracts/ShortUrlContracts.cs` with an optional `Code` field while keeping the existing `Url` input and response contracts compatible.
- [ ] T006 [US1] Update `CreateAsync` in `src/EncurtaUrl/src/EncurtaUrl.Api/Endpoints/ShortUrlEndpoints.cs` to distinguish missing/null code (automatic generation) from a supplied code, validate supplied codes through `IShortCodeValidator`, and assign a valid custom code without adding an endpoint.
- [ ] T007 [US1] Preserve the existing `ToResponse`, list, details, and redirect behavior in `src/EncurtaUrl/src/EncurtaUrl.Api/Endpoints/ShortUrlEndpoints.cs` so custom codes appear in `shortUrl`, details, listing, and redirect URLs and increment `Clicks` exactly as generated codes do.
- [ ] T008 [US1] Add the successful custom-code create and redirect requests to `src/EncurtaUrl/src/EncurtaUrl.Api/EncurtaUrl.Api.http`, including a response/code value that can be reused by the following requests.

**Checkpoint**: User Story 1 works independently with a valid custom code and the
automatic-code path remains available.

---

## Phase 4: User Story 2 - Continuar usando código automático (Priority: P1)

**Goal**: Prove that clients omitting `Code` retain the current generated-code,
listing, and details behavior.

**Independent Test**: POST with only `url`, then GET the generated code, list URLs,
and query its details using the existing response fields.

### Implementation for User Story 2

- [ ] T009 [US2] Verify and, if needed, adjust the branching in `CreateAsync` at `src/EncurtaUrl/src/EncurtaUrl.Api/Endpoints/ShortUrlEndpoints.cs` so omitted or null `Code` uses the existing generator and retry limit unchanged.
- [ ] T010 [US2] Add automatic-generation, list, and details success scenarios to `src/EncurtaUrl/src/EncurtaUrl.Api/EncurtaUrl.Api.http`, confirming generated and custom URLs expose the same response fields.

**Checkpoint**: Existing clients that send only `url` continue to work without a
request or response contract break.

---

## Phase 5: User Story 3 - Feedback para código inválido ou indisponível (Priority: P1)

**Goal**: Reject invalid, reserved, duplicate, and race-conflicting custom codes
with the required typed HTTP results and Portuguese messages.

**Independent Test**: Exercise invalid format, boundary length, empty/whitespace,
reserved, duplicate, and concurrent duplicate submissions and inspect 400/409
responses.

### Implementation for User Story 3

- [ ] T011 [US3] Extend the typed result signature and validation branches in `src/EncurtaUrl/src/EncurtaUrl.Api/Endpoints/ShortUrlEndpoints.cs` so format, length, empty/whitespace, and reserved-code failures return `TypedResults.ValidationProblem` with HTTP 400 and Portuguese field messages.
- [ ] T012 [US3] Add a custom-code existence check and catch only the SQLite unique-constraint `DbUpdateException` race in `src/EncurtaUrl/src/EncurtaUrl.Api/Endpoints/ShortUrlEndpoints.cs`, returning `TypedResults.Problem` with HTTP 409 and a Portuguese code-in-use message for custom codes while preserving automatic-generation failure behavior.
- [ ] T013 [US3] Add error scenarios to `src/EncurtaUrl/src/EncurtaUrl.Api/EncurtaUrl.Api.http` for unsupported characters, fewer than 3 characters, more than 32 characters, empty/whitespace values, `urls`, `URLs`, `openapi`, `OpenApi`, duplicate custom code, and the expected 400/409 outcomes.
- [ ] T014 [US3] Verify in `src/EncurtaUrl/src/EncurtaUrl.Api/Endpoints/ShortUrlEndpoints.cs` that exact-case identity remains supported, so distinct valid codes such as `Balta` and `balta` do not conflict unless reserved.

**Checkpoint**: All specified custom-code rejection paths are independently
testable and return the correct typed response.

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Run the project quality gates and verify the implementation stayed
within the approved architecture and scope.

- [ ] T015 [P] Run `dotnet build` from `src/EncurtaUrl/src/EncurtaUrl.Api/` and confirm no new compiler warnings.
- [ ] T016 [P] Run every scenario in `specs/001-custom-short-code/quickstart.md` against `src/EncurtaUrl/src/EncurtaUrl.Api/EncurtaUrl.Api.http`, including the redirect click-count check.
- [ ] T017 Review `src/EncurtaUrl/src/EncurtaUrl.Api/EncurtaUrl.Api.csproj`, `src/EncurtaUrl/src/EncurtaUrl.Api/Data/AppDbContext.cs`, and the final diff to confirm no NuGet package, migration, schema change, endpoint, test project, edit/remove flow, authentication, or expiration behavior was added.

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No implementation dependency; T001 and T002 can run in parallel.
- **Foundational (Phase 2)**: Depends on Setup; T003 precedes T004, and both block user stories.
- **User Stories (Phases 3–5)**: Depend on the Foundational phase. US1 establishes the custom-code path; US2 verifies compatibility; US3 completes error and race handling in the same endpoint.
- **Polish (Phase 6)**: Depends on all desired user stories.

### User Story Dependencies

- **US1 (P1)**: Depends on T003–T004; MVP story and first implementation slice.
- **US2 (P1)**: Depends on US1 because it verifies the final create branching while preserving the existing generator path.
- **US3 (P1)**: Depends on US1 and US2 because it finalizes the endpoint's typed outcomes and exercises the complete create flow.

### Parallel Opportunities

- T001 and T002 can run in parallel because they inspect different existing concerns.
- T015 and T016 can run in parallel after implementation because build and HTTP validation are independent checks.
- No user-story implementation tasks are marked parallel because US1–US3 intentionally touch the shared request/endpoint and `.http` files.

## Parallel Example: Setup and Final Validation

```text
Task T001: Review contracts and endpoint flow
Task T002: Confirm schema and package constraints

After implementation:
Task T015: Run dotnet build
Task T016: Run quickstart HTTP scenarios
```

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Setup and Foundational phases.
2. Implement US1 with optional request code, validation, assignment, and redirect verification.
3. Stop and validate the custom-code success flow independently.

### Incremental Delivery

1. Add US2 compatibility checks without changing existing generated-code behavior.
2. Add US3 validation, duplicate detection, and race-safe 409 handling.
3. Run the final build and complete quickstart scenarios.

## Notes

- Every task uses the required checkbox, sequential ID, optional `[P]` marker, story label where applicable, and an explicit repository path.
- No separate test-project tasks are included because the project constitution requires `.http` scenarios and `dotnet build`, not a new test project.
