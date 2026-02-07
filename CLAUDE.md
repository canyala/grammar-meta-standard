# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

A unified grammar repository for programming languages and DSLs. Grammars are written in EBNF (legacy) or XEBNF (the newer extended format with version annotations, Unicode support, and conformance levels). The project serves code generation, syntax highlighting, tooling, and language analysis use cases.

## Repository Layout

- `grammars/<language>/<version>/` — grammar files organized by language and version (legacy layout, being migrated)
- `grammars/xebnf/v0.1/XEBNF-SPEC.md` — the XEBNF specification
- `meta/` — metadata: changelogs, coverage tracking, language lists, schema
- `tools/` — C# .NET tooling (source generator, string literal transformer) and a Python grammar-diff placeholder

## Active Migration (RESTRUCTURE-PLAN.md)

The repo is transitioning from version-per-directory (`grammars/<lang>/<version>/`) to language-per-directory with XEBNF as the canonical format. XEBNF embeds version info inline via `@[since: "X.Y"]` annotations, making version directories redundant. C# v14 is the pilot — its grammars are already in `.xebnf` format. Other languages remain as legacy `.ebnf` stubs.

## Grammar Formats

### XEBNF (`.xebnf`) — preferred for new work
- `@grammar { ... }` header block with language, version, conformance metadata
- `@[since: "X.Y"]` annotations on version-specific productions
- `@predicates { ... }` for context-sensitivity documentation
- Unicode character classes: `\p{L}`, `\p{Nd}`
- Semantic predicates: `{? condition ?}`
- Conformance levels: 0 (Structural) → 3 (Contextually Complete)
- Conservative guarantee: grammar-valid implies compiler-valid

### Legacy EBNF (`.ebnf`) — W3C-inspired style per STYLE.md
- `::=` for rule definitions (not `=`, not `←`)
- `{ ... }` repetition, `[ ... ]` optional, `( ... )` grouping
- `|` for alternation, whitespace for sequencing (no commas)
- `(* ... *)` comments
- Literals in single or double quotes
- No ISO special-sequences (`? ... ?`)

## Build System

The .NET solution (`grammar-meta-standard.sln`) contains:
- `tools/EbnfSourceGenerator/` — C# source generator for EBNF
- `tools/EbnfSourceGenerator/EbnfExampleHost.csproj` — example host project
- `tools/csharp/StringLiteralTransformer/Tests.csproj` — unit tests for string literal handling

```bash
# Build all .NET projects
dotnet build grammar-meta-standard.sln

# Run StringLiteralTransformer tests
dotnet test tools/csharp/StringLiteralTransformer/Tests.csproj
```

## Grammar File Conventions

- Multi-version languages split into `lexical.ebnf`/`.xebnf` (tokens) and `syntax.ebnf`/`.xebnf` (structure)
- Single-version small languages (Turtle, SPARQL) use a combined `grammar.ebnf`
- Grammar changes are recorded in `meta/changelog-<lang>.md`
- Coverage status tracked in `meta/coverage.md`

## Key Reference Files

- `STYLE.md` — canonical EBNF formatting rules (for legacy `.ebnf` files)
- `RESTRUCTURE-PLAN.md` — detailed migration plan with execution steps
- `grammars/xebnf/v0.1/XEBNF-SPEC.md` — full XEBNF specification
- `meta/schema.md` — YAML metadata schema for grammar files
