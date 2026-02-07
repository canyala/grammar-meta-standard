# Go Grammar Changelog

## XEBNF Migration (2026-02-07)

The Go grammar has been migrated from per-version EBNF stubs to comprehensive XEBNF files covering Go 1.0 through 1.22.

- **lexical.xebnf** — 506 lines, 11 sections, ~80 productions
- **syntax.xebnf** — 870 lines, 11 sections, ~120 productions
- **Version annotations** tracking feature introductions across Go versions (1.13 numeric literals, 1.18 generics, 1.22 range-over-int)
- Conformance Level 1 (Partial) with documented gaps

### Previous state

- `lexical.ebnf` (16 lines) — structural placeholder
- `syntax.ebnf` (35 lines) — structural placeholder

These have been removed. Their content is fully subsumed by the XEBNF grammar's `@[since:]` annotations.

## Version History (by Go version)

The grammar's non-normative Section 11 (in syntax.xebnf) contains a complete version-by-version changelog of syntactic additions from Go 1.0 through Go 1.22. The lexical grammar's Section 11 contains the corresponding lexical changes. Rather than duplicate that here, refer to those sections directly:

```bash
# View syntactic version history
grep -A 50 'VERSION HISTORY' grammars/go/syntax.xebnf

# View lexical version history
grep -A 50 'VERSION HISTORY' grammars/go/lexical.xebnf

# Find all features added in a specific version
grep 'since: "1.18"' grammars/go/lexical.xebnf grammars/go/syntax.xebnf
```
