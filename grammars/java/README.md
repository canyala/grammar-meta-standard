# Java Grammar — XEBNF

## Coverage

| File | Lines | Versions | Conformance |
|------|-------|----------|-------------|
| `lexical.xebnf` | ~575 | Java 5 – 21 | Level 1 (Partial) |
| `syntax.xebnf` | ~1,309 | Java 1.0 – 21 | Level 1 (Partial) |

## Source Authority

- **[The Java Language Specification](https://docs.oracle.com/javase/specs/)** (primary)
  - Chapter 3 — Lexical Structure
  - Chapter 19 — Syntax (formal grammar)
- **JEP documents** per version (feature specifications)

## Version Annotations

All version-specific constructs are annotated with `@[since: "X"]`. To find what changed in a specific version:

```bash
grep 'since: "17"' lexical.xebnf syntax.xebnf
```

## Lexical Grammar — 11 Sections

1. Unicode input (Unicode escapes processed before tokenization)
2. Line terminators
3. Whitespace
4. Comments (traditional and end-of-line)
5. Tokens
6. Identifiers (Unicode categories)
7. Keywords (52 reserved), contextual keywords (var, yield, record, sealed, etc.)
8. Literals (integer, float, boolean, character, string, text block)
9. Separators
10. Operators
11. Version history (non-normative)

## Syntactic Grammar — 13 Sections

1. Compilation unit (ordinary and modular)
2. Type declarations (class, enum, record, interface, annotation type)
3. Types (primitive, reference, generics, type parameters)
4. Field declarations
5. Method declarations
6. Constructor declarations
7. Initializers
8. Expressions (precedence hierarchy, lambdas, switch expressions, method references)
9. Patterns (type patterns, record patterns, guarded patterns)
10. Statements (if, for, while, switch, try, synchronized, etc.)
11. Annotations
12. Module declarations
13. Version history (non-normative)

## Known Context-Sensitive Constructs

| Construct | Grammar Approach | Details |
|-----------|-----------------|---------|
| `>>` in generics | Annotation | Two `>` tokens, not one `>>` — avoids `List<List<int>>` ambiguity |
| `var` as type | Annotation | Reserved type name, not keyword — valid as variable name |
| `yield` in switch | Annotation + predicate | Restricted identifier — only keyword in switch expression context |
| Module keywords | Annotation + predicate | `open`, `module`, `requires`, etc. — only keywords inside module declarations |
| `sealed`/`non-sealed` | Annotation | Restricted identifiers — keyword semantics only in class/interface declarations |
| Cast ambiguity | Annotation | `(Type)expr` vs `(expr)` — resolved by parser heuristics |

## Previous Versions

This grammar replaces the `lexical.ebnf` and `syntax.ebnf` files, which were structural stubs (18 and 37 lines respectively). The XEBNF files are comprehensive and contain all version information inline via `@[since:]` annotations.
