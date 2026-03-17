# XEBNF Conformance Levels

Extracted from [XEBNF-SPEC.md](XEBNF-SPEC.md) §12 for contributor reference.

---

## Levels

| Level | Name                   | Meaning |
|-------|------------------------|---------|
| 0     | Structural             | Grammar structure exists but productions may be incomplete or placeholder. |
| 1     | Partial                | Grammar covers the core language constructs. Known gaps are documented. |
| 2     | Syntactically Complete  | Grammar covers all syntactic constructs in the spec. Context-sensitive gaps documented. |
| 3     | Contextually Complete   | Grammar includes semantic predicates, lookahead, and annotations for all known context-sensitive constructs. |

## Conservative Guarantee

At any conformance level: **grammar-valid implies compiler-valid**. A string accepted by the grammar will be accepted by the compiler. The grammar may reject strings the compiler accepts (it under-approximates), but never the reverse.

This means grammars are safe to use for code generation — if your generated code parses against the grammar, it will compile.

## Declaring Conformance

Every `.xebnf` file declares its conformance level in the `@grammar` header:

```
@grammar {
  conformance: 1 ;
  conformance_notes: "…" ;
}
```

Legacy `.ebnf` files default to Level 0 (Structural) unless they demonstrably cover the reference specification, in which case they may claim up to Level 2. Level 3 requires XEBNF features (semantic predicates, annotations) and is not achievable in legacy EBNF.

## Gap Documentation

At Level 1+, all known gaps must be documented either:
- Inline via `@[description: "..."]` annotations on the relevant production
- In the language's `README.md` under a "Known Gaps" section

Examples of gaps that should be documented:
- Context-sensitive constructs the grammar describes but cannot enforce (e.g. raw string delimiter matching)
- Parser disambiguation rules not captured in the grammar (e.g. cast vs parenthesized expression)
- Features the grammar omits entirely (should be listed explicitly)
