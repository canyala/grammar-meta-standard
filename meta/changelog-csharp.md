# C# Grammar Changelog

## XEBNF Migration (2026-02-07)

The C# grammar has been migrated from per-version EBNF stubs to comprehensive XEBNF files covering C# 1.0 through 14.0.

- **lexical.xebnf** — 907 lines, 12 sections, ~150 productions
- **syntax.xebnf** — 2,308 lines, 15 sections, ~400 productions
- **353 version annotations** tracking feature introductions across 14 C# versions
- Conformance Level 1 (Partial) with documented gaps

### Previous state

- `v12/lexical.ebnf` (38 lines) — structural placeholder
- `v12/syntax.ebnf` (106 lines) — structural placeholder
- `v13/lexical.ebnf` (43 lines) — structural placeholder
- `v13/syntax.ebnf` (106 lines) — structural placeholder

These have been removed. Their content is fully subsumed by the XEBNF grammar's `@[since:]` annotations.

## Version History (by C# version)

The grammar's non-normative Section 15 (in syntax.xebnf) contains a complete version-by-version changelog of syntactic additions from C# 1.0 through C# 14.0. The lexical grammar's Section 12 contains the corresponding lexical changes. Rather than duplicate that here, refer to those sections directly:

```bash
# View syntactic version history
grep -A 100 'VERSION HISTORY' grammars/csharp/syntax.xebnf

# View lexical version history
grep -A 60 'Version History' grammars/csharp/lexical.xebnf

# Find all features added in a specific version
grep 'since: "11.0"' grammars/csharp/lexical.xebnf grammars/csharp/syntax.xebnf
```
