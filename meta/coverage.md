# Grammar Coverage

This document tracks grammar coverage status across all languages.

## Coverage Matrix

| Language | Format | Conformance | Versions Covered | Lexical | Syntactic | Notes |
|----------|--------|-------------|------------------|---------|-----------|-------|
| C# | **XEBNF** | **Level 1** | 1.0 – 14.0 | ~907 lines | ~2,308 lines | Comprehensive, version-annotated |
| Python | EBNF | Level 0 | 3.13 | ~21 lines | ~33 lines | Structural placeholder |
| JavaScript | EBNF | Level 0 | ES2024 | ~46 lines | ~59 lines | Structural placeholder |
| Java | EBNF | Level 0 | 21 | ~17 lines | ~36 lines | Structural placeholder |
| Go | EBNF | Level 0 | 1.22 | ~15 lines | ~34 lines | Structural placeholder |
| Turtle | EBNF | Level 0 | 1.1 | — | ~55 lines | Single grammar file |
| SPARQL | EBNF | Level 1 | 1.1 | — | ~244 lines | Most complete legacy grammar |

## Conformance Levels

See [conformance.md](conformance.md) for level definitions.

## Migration Status

C# is the first language fully migrated to XEBNF. Other languages retain their legacy EBNF files and are candidates for XEBNF rewrite. Priority candidates:

1. **SPARQL** — already 244 lines, small language, high relevance to Sky Omega
2. **Turtle** — small, single-version, straightforward conversion
3. **Python** — large language, PEG grammar available as authoritative source
4. **JavaScript** — large language, ECMA-262 appendix grammar available
