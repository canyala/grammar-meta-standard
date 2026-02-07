# Python Grammar Changelog

## XEBNF Migration (2026-02-07)

The Python grammar has been migrated from per-version EBNF stubs to comprehensive XEBNF files covering Python 3.0 through 3.13.

- **lexical.xebnf** — 658 lines, 12 sections, ~100 productions
- **syntax.xebnf** — 984 lines, 7 sections, ~150 productions
- **Version annotations** tracking feature introductions across Python versions (3.5 async/await, 3.6 f-strings, 3.8 walrus operator, 3.10 match/case, 3.12 type params)
- Conformance Level 1 (Partial) with documented gaps

### Previous state

- `lexical.ebnf` (22 lines) — structural placeholder
- `syntax.ebnf` (34 lines) — structural placeholder

These have been removed. Their content is fully subsumed by the XEBNF grammar's `@[since:]` annotations.

## Version History (by Python version)

The grammar's non-normative Section 12 (in lexical.xebnf) and Section 7 (in syntax.xebnf) contain version-by-version changelogs. Rather than duplicate that here, refer to those sections directly:

```bash
# View syntactic version history
grep -A 60 'VERSION HISTORY' grammars/python/syntax.xebnf

# View lexical version history
grep -A 60 'VERSION HISTORY' grammars/python/lexical.xebnf

# Find all features added in a specific version
grep 'since: "3.10"' grammars/python/lexical.xebnf grammars/python/syntax.xebnf
```
