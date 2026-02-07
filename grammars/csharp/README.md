# C# Grammar — XEBNF

## Coverage

| File | Lines | Versions | Conformance |
|------|-------|----------|-------------|
| `lexical.xebnf` | ~907 | C# 1.0 – 14.0 | Level 1 (Partial) |
| `syntax.xebnf` | ~2,308 | C# 1.0 – 14.0 | Level 1 (Partial) |

## Source Authority

- **ECMA-334** (C# 7 standard, via [dotnet/csharpstandard](https://github.com/dotnet/csharpstandard))
- **Microsoft Learn** C# language reference and feature specifications
- **dotnet/roslyn** CSharp.Generated.g4 (for cross-reference)
- **csharplang/proposals** for C# 8–14 feature specifications

## Version Annotations

All version-specific constructs are annotated with `@[since: "X.Y"]`. To find what changed in a specific version:

```bash
grep 'since: "11.0"' lexical.xebnf syntax.xebnf
```

## Lexical Grammar — 12 Sections

1. Input structure
1. Line terminators
1. Comments
1. Whitespace
1. Tokens
1. Unicode escape sequences
1. Identifiers (with Unicode categories)
1. Keywords (47 reserved)
1. Contextual keywords (42, version-annotated)
1. Literals (boolean, integer, real, character, string including raw/UTF-8)
1. Operators and punctuators (60+)
1. Preprocessor directives

## Syntactic Grammar — 15 Sections

1. Compilation unit (including top-level statements)
1. Namespace and type names
1. Types (reference, value, function pointer, scoped, pointer)
1. Patterns (declaration through list patterns, full C# 7–11 evolution)
1. Expressions (20-level precedence hierarchy)
1. Statements
1. Namespace declarations (including file-scoped)
1. Attributes
1. Modifiers
1. Type declarations (class, struct, interface, enum, delegate, record, extension)
1. Type parameter constraints
1. Class/struct members
1. Interface members
1. Field keyword (C# 14)
1. Version history (non-normative)

## Known Context-Sensitive Constructs

These constructs are documented in the grammar via annotations and predicates but cannot be fully captured in a context-free grammar:

|Construct                          |Grammar Approach                 |Details                                                |
|-----------------------------------|---------------------------------|-------------------------------------------------------|
|Raw string delimiter matching      |`@[description: ...]` + predicate|N opening quotes must match N closing quotes (N ≥ 3)   |
|Interpolated raw string brace depth|`@[description: ...]`            |N dollar signs determine brace interpolation depth     |
|Right-shift tokenization           |`right_shift = '>' '>'`          |Not a single token — avoids `List<List<int>>` ambiguity|
|Cast vs. parenthesized expression  |Annotation                       |`(T)x` resolved by parser heuristics                   |
|Generic `<` vs. comparison `<`     |Annotation                       |ECMA-334 §6.2.4 disambiguation rules                   |
|`field` keyword                    |Context-sensitive identifier     |Only recognized inside property accessor bodies        |

## Previous Versions

This grammar replaces the separate `v12/`, `v13/`, `v14/` directories. The old `.ebnf` files were structural stubs (38–106 lines each). The XEBNF files are comprehensive and contain all version information inline via `@[since:]` annotations, making per-version directories unnecessary.
