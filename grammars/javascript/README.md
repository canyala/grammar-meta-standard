# JavaScript (ECMAScript) Grammar — XEBNF

## Coverage

| File | Lines | Versions | Conformance |
|------|-------|----------|-------------|
| `lexical.xebnf` | ~615 | ES2015 – ES2024 | Level 1 (Partial) |
| `syntax.xebnf` | ~1,152 | ES2015 – ES2024 | Level 1 (Partial) |

## Source Authority

- **[ECMA-262](https://tc39.es/ecma262/)** (primary, Annex A formal grammar)
- **TC39 proposal documents** per edition (feature specifications)
- **MDN JavaScript reference** (cross-reference)

## Version Annotations

All version-specific constructs are annotated with `@[since: "ESXXXX"]`. To find what changed in a specific version:

```bash
grep 'since: "ES2020"' lexical.xebnf syntax.xebnf
```

## Lexical Grammar — 11 Sections

1. Source text
2. Line terminators
3. Whitespace
4. Comments (single-line, multi-line, hashbang)
5. Tokens
6. Identifiers (Unicode ID_Start/ID_Continue, private identifiers)
7. Keywords (38 reserved), strict mode reserved words, contextual keywords
8. Literals (numeric with BigInt/separators, string, template, regex)
9. Punctuators (operators, assignment, optional chaining, nullish coalescing)
10. Automatic semicolon insertion (context-sensitive)
11. Version history (non-normative)

## Syntactic Grammar — 9 Sections

1. Scripts and modules (import/export declarations)
2. Import and export declarations (dynamic import, import.meta)
3. Statements (block, variable, iteration, try/catch, switch, etc.)
4. Function and generator declarations (async functions, async generators)
5. Class declarations (fields, static blocks, private members)
6. Expressions (precedence hierarchy, arrows, yield, await, optional chaining)
7. Destructuring assignment (cover grammars)
8. Common productions
9. Version history (non-normative)

## Known Context-Sensitive Constructs

| Construct | Grammar Approach | Details |
|-----------|-----------------|---------|
| Automatic semicolon insertion | `@[description: ...]` + predicate | Three ASI rules; [no LineTerminator here] restrictions |
| Regex vs. division | Predicate | `/regex/` vs. `a / b` — requires parser feedback to lexer |
| Template expression holes | `@[description: ...]` + predicate | `${}` requires lexer mode switching |
| `yield`/`await` as identifier vs. keyword | Predicate | Context-dependent: keyword in generators/async, identifier elsewhere |
| `async` as identifier vs. keyword | Annotation | [no LineTerminator here] between `async` and `function`/arrow |
| Arrow function cover grammar | Annotation | `(a, b)` reinterpreted as arrow params when followed by `=>` |
| Destructuring cover grammar | Annotation | Object/array literal reinterpreted as pattern on left of `=` |

## Previous Versions

This grammar replaces the `lexical.ebnf` and `syntax.ebnf` files, which were structural stubs (47 and 60 lines respectively). The XEBNF files are comprehensive and contain all version information inline via `@[since:]` annotations.
