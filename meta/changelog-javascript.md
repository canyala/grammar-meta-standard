# JavaScript Grammar Changelog

## XEBNF Migration (2026-02-07)

The JavaScript grammar has been migrated from per-version EBNF stubs to comprehensive XEBNF files covering ES2015 through ES2024.

- **lexical.xebnf** — 615 lines, 11 sections, ~90 productions
- **syntax.xebnf** — 1,152 lines, 9 sections, ~180 productions
- **Version annotations** tracking feature introductions across 10 ECMAScript editions (ES2015 classes/arrows/modules, ES2017 async/await, ES2020 optional chaining/nullish coalescing, ES2022 class fields)
- Conformance Level 1 (Partial) with documented gaps

### Previous state

- `lexical.ebnf` (47 lines) — structural placeholder
- `syntax.ebnf` (60 lines) — structural placeholder

These have been removed. Their content is fully subsumed by the XEBNF grammar's `@[since:]` annotations.

## Version History (by ECMAScript edition)

The grammar's non-normative Section 11 (in lexical.xebnf) and Section 9 (in syntax.xebnf) contain version-by-version changelogs. Rather than duplicate that here, refer to those sections directly:

```bash
# View syntactic version history
grep -A 80 'VERSION HISTORY' grammars/javascript/syntax.xebnf

# View lexical version history
grep -A 60 'VERSION HISTORY' grammars/javascript/lexical.xebnf

# Find all features added in a specific version
grep 'since: "ES2020"' grammars/javascript/lexical.xebnf grammars/javascript/syntax.xebnf
```
