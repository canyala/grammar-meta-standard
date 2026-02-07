# Go Grammar — XEBNF

## Coverage

| File | Lines | Versions | Conformance |
|------|-------|----------|-------------|
| `lexical.xebnf` | ~506 | Go 1.13 – 1.22 | Level 1 (Partial) |
| `syntax.xebnf` | ~870 | Go 1.0 – 1.22 | Level 1 (Partial) |

## Source Authority

- **[The Go Programming Language Specification](https://go.dev/ref/spec)** (primary, includes EBNF grammar)
- **Go release notes** (per-version feature tracking)
- **golang/go** repository `src/go/scanner` and `src/go/parser` (cross-reference)

## Version Annotations

All version-specific constructs are annotated with `@[since: "X.Y"]`. To find what changed in a specific version:

```bash
grep 'since: "1.18"' lexical.xebnf syntax.xebnf
```

## Lexical Grammar — 11 Sections

1. Source text
2. Line terminators
3. Whitespace
4. Comments (line and general)
5. Tokens
6. Identifiers (Unicode letter/digit categories)
7. Keywords (25 reserved)
8. Literals (integer, float, imaginary, rune, string)
9. Operators and punctuation
10. Automatic semicolon insertion (context-sensitive)
11. Version history (non-normative)

## Syntactic Grammar — 11 Sections

1. Source file (package clause, imports, top-level declarations)
2. Package clause
3. Import declarations
4. Top-level declarations (const, type, var)
5. Types (array, slice, struct, pointer, function, interface, map, channel)
6. Type parameters and arguments (Go 1.18+)
7. Expressions (operands, composite literals, operators with 5-level precedence)
8. Statements (if, for, switch, select, go, defer, return, etc.)
9. Function and method declarations
10. Common productions
11. Version history (non-normative)

## Known Context-Sensitive Constructs

| Construct | Grammar Approach | Details |
|-----------|-----------------|---------|
| Automatic semicolon insertion | `@[description: ...]` + predicate | Semicolons inserted after identifier, literal, `break`, `continue`, `fallthrough`, `return`, `++`, `--`, `)`, `]`, `}` at end of line |
| Composite literal vs. block | Annotation | `if x == (T{})` requires parens to avoid ambiguity with block |
| Generic `[` vs. index `[` | Annotation | `f[T]` could be index or type instantiation — resolved by parser |
| Send/receive on channel | Annotation | `<-chan` direction parsing depends on context |

## Previous Versions

This grammar replaces the `lexical.ebnf` and `syntax.ebnf` files, which were structural stubs (16 and 35 lines respectively). The XEBNF files are comprehensive and contain all version information inline via `@[since:]` annotations.
