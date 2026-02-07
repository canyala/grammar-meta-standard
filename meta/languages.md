# Supported Languages

## Languages and Status

| Language | Directory | Format | Versions | Conformance | Reference Spec |
|----------|-----------|--------|----------|-------------|----------------|
| C# | `grammars/csharp/` | XEBNF | 1.0 – 14.0 | Level 1 | [ECMA-334](https://ecma-international.org/publications-and-standards/standards/ecma-334/) / [dotnet/csharpstandard](https://github.com/dotnet/csharpstandard) |
| Python | `grammars/python/` | EBNF | 3.13 | Level 0 | [python.org grammar](https://docs.python.org/3/reference/grammar.html) |
| JavaScript | `grammars/javascript/` | EBNF | ES2024 | Level 0 | [ECMA-262](https://www.ecma-international.org/publications-and-standards/standards/ecma-262/) |
| Java | `grammars/java/` | EBNF | 21 | Level 0 | [Java Language Spec](https://docs.oracle.com/javase/specs/) |
| Go | `grammars/go/` | EBNF | 1.22 | Level 0 | [go.dev/ref/spec](https://go.dev/ref/spec) |
| Turtle | `grammars/turtle/` | EBNF | 1.1 | Level 0 | [W3C Turtle](https://www.w3.org/TR/turtle/) |
| SPARQL | `grammars/sparql/` | EBNF | 1.1 | Level 1 | [W3C SPARQL 1.1](https://www.w3.org/TR/sparql11-query/) |

## Grammar Format

**XEBNF grammars** (`.xebnf`) encode version information inline via `@[since:]` annotations. A single file contains all versions. See [XEBNF-SPEC.md](XEBNF-SPEC.md).

**Legacy EBNF grammars** (`.ebnf`) are structural placeholders from before the XEBNF migration. They cover a single version and use the convention described in the repository's original STYLE.md (now superseded by the XEBNF spec).

## Layout

```
grammars/<language>/
├── README.md           # Language-specific documentation
├── lexical.xebnf       # Lexical grammar (XEBNF) — or lexical.ebnf (legacy)
├── syntax.xebnf        # Syntactic grammar (XEBNF) — or syntax.ebnf (legacy)
└── grammar.xebnf       # Combined grammar for small languages (optional)
```

## Planned Languages

| Language | Priority | Notes |
|----------|----------|-------|
| Rust | Medium | Comprehensive reference grammar available |
| F# | Medium | Shares C# lineage, ECMA-334 parallel |
| SQL | Low | ISO/IEC 9075, very large grammar |
| Haskell | Low | Haskell 2010 Report has formal grammar |
