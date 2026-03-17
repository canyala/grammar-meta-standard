# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

A unified grammar repository for programming languages and DSLs. Grammars are written in XEBNF (preferred) or legacy EBNF. XEBNF extends EBNF with version annotations, Unicode support, semantic predicates, and conformance levels. The project serves code generation, syntax highlighting, tooling, and language analysis use cases.

## Repository Layout

- `grammars/<language>/` — grammar files organized by language (flat layout, no version subdirectories)
- `meta/XEBNF-SPEC.md` — the XEBNF specification
- `meta/conformance.md` — conformance level definitions
- `meta/coverage.md` — grammar coverage tracking
- `meta/languages.md` — supported languages and status
- `meta/changelog-<lang>.md` — per-language changelogs

## Grammar Formats

### XEBNF (`.xebnf`) — preferred for new work
- `@grammar { ... }` header block with language, version, conformance metadata
- `@[since: "X.Y"]` annotations on version-specific productions
- `@predicates { ... }` for context-sensitivity documentation
- Unicode character classes: `\p{L}`, `\p{Nd}`
- Semantic predicates: `{? condition ?}`
- Conformance levels: 0 (Structural) → 3 (Contextually Complete)
- Conservative guarantee: grammar-valid implies compiler-valid

### Legacy EBNF (`.ebnf`)
- `::=` for rule definitions
- `{ ... }` repetition, `[ ... ]` optional, `( ... )` grouping
- `|` for alternation, whitespace for sequencing (no commas)
- `(* ... *)` comments
- Literals in single or double quotes

## Grammar File Conventions

- Multi-version languages split into `lexical.xebnf`/`.ebnf` (tokens) and `syntax.xebnf`/`.ebnf` (structure)
- Single-version small languages (Turtle, SPARQL) use a combined `grammar.ebnf`
- Each language directory has a `README.md` with sources, coverage, and migration status
- Grammar changes are recorded in `meta/changelog-<lang>.md`
- Coverage status tracked in `meta/coverage.md`
- Version information is embedded in grammar files via `@[since:]` annotations, not in the filesystem

## Key Reference Files

- `meta/XEBNF-SPEC.md` — full XEBNF specification
- `meta/conformance.md` — conformance level definitions
- `meta/coverage.md` — grammar coverage matrix
- `meta/languages.md` — supported languages and layout
