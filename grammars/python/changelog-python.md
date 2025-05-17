# Python Grammar Changelog

This changelog tracks notable changes to the Python grammar across recent versions.

---

## Python 3.12 → 3.13

- Added support for:
  - `except*` syntax for `try` statements (PEP 654)
  - Refinements in pattern matching grammar (PEP 634+)
  - Nested expressions in `f-strings`
  - Type parameter declarations in function/class headers (PEP 695)
- Clarified parsing rules around `match` statements and `case` blocks
- Extended function call parsing for partial application

---

## Python 3.11 → 3.12

- Allowed annotations and types in more flexible contexts
- Introduced `soft keywords` (e.g., `match`, `case`)
- Introduced syntax for exception groups (PEP 654 foundation)
- Added explicit suite constructs for indented blocks

---

Each version’s grammar is stored under:
- `grammars/python/v3.12/`
- `grammars/python/v3.13/`
