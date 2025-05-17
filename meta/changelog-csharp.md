# C# Grammar Changelog

This changelog highlights important grammar-level changes between C# versions.

---

## C# 12 → C# 13

- Introduced **list patterns**
- Added support for **raw string literals** in `string-literal`
- Extended `primary-expression` to handle new pattern matching constructs
- Enabled attributes and modifiers on lambda expressions
- Improved handling of UTF-8 string literals

---

## C# 13 → C# 14 (Preview)

- Refined **raw string literal** grammar with flexible quote handling
- Introduced `params` span-like parameter support
- Support for `inline arrays` and enhanced `field` declarations
- Added support for **required** members in object initializers
- `ref readonly` and `scoped` contextual keywords expanded
- Method-level attributes and generic constraints refined

---

Each version’s grammar is stored under:
- `grammars/csharp/v12/`
- `grammars/csharp/v13/`
- `grammars/csharp/v14/`
