# JavaScript Grammar Changelog

This changelog summarizes grammar-level updates across ECMAScript versions.

---

## ECMAScript 2022 → 2023

- Added support for:
  - `Array.prototype.findLast()` and `findLastIndex()` methods
  - Unicode `v` flag in regular expressions
  - `with`-like blocks in testing syntax contexts (experimental)
- Expanded handling of `class fields` and `private` identifiers
- Enabled `top-level await` in modules

---

## ECMAScript 2021 → 2022

- Introduced `class static blocks`
- Clarified short-circuit assignment parsing (`&&=`, `||=`, `??=`)
- Unified handling of numeric separators in literals
- Improved optional chaining semantics

---

Each version’s grammar is stored under:
- `grammars/javascript/v2023/`

Additional versions can be added later in `v2022/`, `v2021/`, etc.
