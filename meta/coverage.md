# Grammar Coverage

This document tracks grammar coverage status across all languages.

## Coverage Matrix

| Language | Format | Conformance | Versions Covered | Lexical | Syntactic | Notes |
|----------|--------|-------------|------------------|---------|-----------|-------|
| C# | **XEBNF** | **Level 1** | 1.0 – 14.0 | ~907 lines | ~2,308 lines | Comprehensive, version-annotated |
| Go | **XEBNF** | **Level 1** | 1.13 – 1.22 | ~506 lines | ~870 lines | Comprehensive, version-annotated |
| Python | **XEBNF** | **Level 1** | 3.6 – 3.13 | ~658 lines | ~984 lines | Comprehensive, version-annotated |
| Java | **XEBNF** | **Level 1** | 5 – 21 | ~575 lines | ~1,309 lines | Comprehensive, version-annotated |
| JavaScript | **XEBNF** | **Level 1** | ES2015 – ES2024 | ~615 lines | ~1,152 lines | Comprehensive, version-annotated |
| Turtle | EBNF | Level 2 | 1.1 | — | ~55 lines | Single grammar file |
| SPARQL | EBNF | Level 2 | 1.1 | — | ~244 lines | Complete W3C grammar |

## Conformance Levels

See [conformance.md](conformance.md) for level definitions.

## Migration Status

Five languages have been migrated to comprehensive XEBNF Level 1 grammars: C#, Go, Python, Java, and JavaScript. Remaining legacy EBNF candidates:

1. **SPARQL** — already 244 lines, small language, high relevance to Sky Omega
2. **Turtle** — small, single-version, straightforward conversion
