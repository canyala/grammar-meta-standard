# Contributing to grammar-meta-standard

## How to Contribute

1. Fork the repo and create a branch
2. Make your changes following the guidelines below
3. Submit a pull request
4. Use issues to propose structural or technical improvements

## Grammar Guidelines

### XEBNF Grammars (preferred)

New grammars and rewrites of existing grammars should use XEBNF notation (`.xebnf` extension). See [meta/XEBNF-SPEC.md](meta/XEBNF-SPEC.md) for the full specification.

Key requirements:
- Include a `@grammar { ... }` header with language, version, conformance level
- Annotate version-specific productions with `@[since: "X.Y"]`
- Document known gaps and context-sensitive constructs
- Declare conformance level honestly — see [meta/conformance.md](meta/conformance.md)
- Split into `lexical.xebnf` and `syntax.xebnf` for non-trivial languages

### Legacy EBNF Grammars

Existing `.ebnf` files use the W3C-inspired style:
- `=` for rule definitions (not `::=`)
- `{ ... }` for repetition, `[ ... ]` for optional
- `(* ... *)` for comments
- No commas between sequence elements

### Adding a New Language

1. Create `grammars/<language>/`
2. Add a `README.md` documenting sources, version coverage, and conformance
3. Add grammar files (`lexical.xebnf` + `syntax.xebnf`, or `grammar.xebnf` for small languages)
4. Update `meta/languages.md` and `meta/coverage.md`

### File Placement

- Grammar files: `grammars/<language>/`
- Language changelogs: `meta/changelog-<language>.md`

## Maintainer

This repository is maintained under the Canyala GitHub organization by Martin Fredriksson.
