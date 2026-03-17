# SPARQL Grammar

## Status: Legacy EBNF (Level 2 — Syntactically Complete)

| File | Lines | Version | Notes |
|------|-------|---------|-------|
| `grammar.ebnf` | ~244 | SPARQL 1.1 | Based on W3C sparql11-query spec |

## Source Authority

- [W3C SPARQL 1.1 Query Language](https://www.w3.org/TR/sparql11-query/)

## Notes

Most complete of the legacy grammars at 244 productions. An XEBNF rewrite could add version annotations if SPARQL 1.2 materializes, plus Unicode character classes for the lexical rules.

## W3C Specification Status (reviewed 2026-02-07)

SPARQL 1.1 (W3C Recommendation, Mar 2013) remains the current standard. The grammar here is accurate.

[SPARQL 1.2 Query Language](https://www.w3.org/TR/sparql12-query/) is a **Working Draft** (latest: Jan 29, 2026) being developed by the [RDF & SPARQL Working Group](https://www.w3.org/groups/wg/rdf-star/). The original charter targeted W3C Recommendation by Q4 2025, but the timeline has slipped and the group has been rechartered through April 2027.

Key grammar changes in 1.2 (~20 new productions):
- **RDF-star**: reified triples (`<<...>>`), triple terms (`<<(...)>>`), reifiers (`~`), annotation blocks (`{|...|}`)
- **Version declarations**: `VERSION "1.2"`
- **Directional language tags**: `LANGTAG` replaced by `LANG_DIR` (`@ar--rtl`)
- **New built-in functions**: `isTRIPLE`, `TRIPLE`, `SUBJECT`, `PREDICATE`, `OBJECT`, `LANGDIR`, `STRLANGDIR`, `hasLANGDIR`, `hasLANG`
- **Removed production**: `GraphTerm` folded into `VarOrTerm`

**Action**: Update grammar when SPARQL 1.2 reaches Candidate Recommendation. An XEBNF rewrite with `@[since: "1.2"]` annotations on new/modified productions would then be appropriate.
