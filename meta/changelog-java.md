# Java Grammar Changelog

## XEBNF Migration (2026-02-07)

The Java grammar has been migrated from per-version EBNF stubs to comprehensive XEBNF files covering Java 1.0 through 21.

- **lexical.xebnf** — 575 lines, 11 sections, ~90 productions
- **syntax.xebnf** — 1,309 lines, 13 sections, ~200 productions
- **Version annotations** tracking feature introductions across Java versions (5 generics, 8 lambdas, 9 modules, 14 switch expressions, 16 records, 17 sealed classes, 21 record patterns)
- Conformance Level 1 (Partial) with documented gaps

### Previous state

- `lexical.ebnf` (18 lines) — structural placeholder
- `syntax.ebnf` (37 lines) — structural placeholder

These have been removed. Their content is fully subsumed by the XEBNF grammar's `@[since:]` annotations.

## Version History (by Java version)

The grammar's non-normative Section 13 (in syntax.xebnf) contains a complete version-by-version changelog of syntactic additions from Java 1.0 through Java 21. The lexical grammar's Section 11 contains the corresponding lexical changes. Rather than duplicate that here, refer to those sections directly:

```bash
# View syntactic version history
grep -A 80 'VERSION HISTORY' grammars/java/syntax.xebnf

# View lexical version history
grep -A 60 'VERSION HISTORY' grammars/java/lexical.xebnf

# Find all features added in a specific version
grep 'since: "17"' grammars/java/lexical.xebnf grammars/java/syntax.xebnf
```
