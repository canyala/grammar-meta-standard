# Turtle Grammar

## Status: Legacy EBNF (Level 2 — Syntactically Complete)

| File | Lines | Version | Notes |
|------|-------|---------|-------|
| `grammar.ebnf` | ~55 | Turtle 1.1 | W3C grammar extracted |

## Source Authority

- [W3C RDF 1.1 Turtle](https://www.w3.org/TR/turtle/)

## Notes

Turtle is a single-version language with a small grammar. An XEBNF rewrite would add Unicode character class annotations and conformance metadata but would not benefit from version annotations.

## W3C Specification Status (reviewed 2026-02-07)

Turtle 1.1 (RDF 1.1 Turtle, W3C Recommendation, Feb 2014) remains the current standard. The grammar here is accurate.

[RDF 1.2 Turtle](https://www.w3.org/TR/rdf12-turtle/) is a **Working Draft** (latest: Jan 29, 2026) being developed by the [RDF & SPARQL Working Group](https://www.w3.org/groups/wg/rdf-star/). The original charter targeted W3C Recommendation by Q3 2025, but the timeline has slipped and the group has been rechartered through April 2027.

Key grammar changes in 1.2 (~13 new productions):
- **RDF-star**: reified triples (`<<...>>`), triple terms (`<<(...)>>`), reifiers (`~`), annotation blocks (`{|...|}`)
- **Version declarations**: `@version "1.2"` / `VERSION "1.2"`
- **Directional language tags**: `LANGTAG` replaced by `LANG_DIR` (`@ar--rtl`)

**Action**: Update grammar when Turtle 1.2 reaches Candidate Recommendation. An XEBNF rewrite with `@[since: "1.2"]` annotations on new productions would then be appropriate.
