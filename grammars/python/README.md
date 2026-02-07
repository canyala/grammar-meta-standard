# Python Grammar

## Status: Legacy EBNF (Level 0 — Structural)

These grammar files predate the XEBNF migration and cover basic constructs only.

| File | Lines | Version | Notes |
|------|-------|---------|-------|
| `lexical.ebnf` | ~21 | 3.13 | Core token definitions |
| `syntax.ebnf` | ~33 | 3.13 | Core syntax structures |

## Source Authority

- [Python Language Reference — Grammar](https://docs.python.org/3/reference/grammar.html)
- PEG grammar from CPython source (`Grammar/python.gram`)

## Migration Path

These files are candidates for XEBNF rewrite covering Python 3.9–3.13+ with version annotations. The CPython PEG grammar is the authoritative machine-readable source and would serve as the primary reference for a comprehensive XEBNF grammar.

## Changelog

See [meta/changelog-python.md](../../meta/changelog-python.md).
