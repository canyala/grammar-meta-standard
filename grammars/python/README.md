# Python Grammar — XEBNF

## Coverage

| File | Lines | Versions | Conformance |
|------|-------|----------|-------------|
| `lexical.xebnf` | ~658 | Python 3.6 – 3.13 | Level 1 (Partial) |
| `syntax.xebnf` | ~984 | Python 3.0 – 3.13 | Level 1 (Partial) |

## Source Authority

- **[The Python Language Reference](https://docs.python.org/3/reference/)** (primary)
- **[CPython Grammar/python.gram](https://github.com/python/cpython/blob/main/Grammar/python.gram)** (PEG grammar, cross-reference)
- **PEP documents** per version (feature specifications)

## Version Annotations

All version-specific constructs are annotated with `@[since: "X.Y"]`. To find what changed in a specific version:

```bash
grep 'since: "3.10"' lexical.xebnf syntax.xebnf
```

## Lexical Grammar — 12 Sections

1. Source encoding
2. Line structure (physical/logical lines, line joining)
3. Indentation (INDENT/DEDENT — context-sensitive)
4. Whitespace
5. Comments
6. Tokens
7. Identifiers (Unicode UAX#31)
8. Keywords (35 reserved) and soft keywords (match, case, type, _)
9. Literals (string, bytes, f-string, integer, float, imaginary)
10. Operators
11. Delimiters
12. Version history (non-normative)

## Syntactic Grammar — 7 Sections

1. File input
2. Statements (simple: assignment, return, yield, raise, import, etc.)
3. Compound statements (if, while, for, try, with, match, async)
4. Function and class definitions (with type parameters, PEP 695)
5. Expressions (17-level precedence hierarchy, comprehensions, generators)
6. Slice list
7. Version history (non-normative)

## Known Context-Sensitive Constructs

| Construct | Grammar Approach | Details |
|-----------|-----------------|---------|
| INDENT/DEDENT generation | `@[description: ...]` + predicate | Requires indentation stack tracking column positions |
| F-string expression holes | `@[description: ...]` + predicate | Nested lexer: expression holes follow Python lexical rules inside string content |
| Soft keywords | Annotation | `match`, `case`, `_` (3.10), `type` (3.12) — identifiers outside match/type contexts |
| Implicit line joining | `@[description: ...]` + predicate | Inside `()`, `[]`, `{}` — newlines don't end logical line |
| `async`/`await` scoping | Predicate | `await` valid only in async functions; `async for`/`async with` only in async context |

## Previous Versions

This grammar replaces the `lexical.ebnf` and `syntax.ebnf` files, which were structural stubs (22 and 34 lines respectively). The XEBNF files are comprehensive and contain all version information inline via `@[since:]` annotations.
