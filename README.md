<div align=”center”>
  <img src=”assets/edgar-badge.svg” alt=”Edgar” width=”200”>
</div>

# grammar-meta-standard

A unified grammar repository for programming languages and DSLs, using [XEBNF](meta/XEBNF-SPEC.md) — an extended EBNF notation with version annotations, Unicode support, semantic predicates, and conformance levels.

---

## Purpose

Machine-readable, versioned formal grammars for:

- Grammar-guided code generation (LLM-assisted and traditional)
- Syntax-valid output verification
- Language version comparison and migration analysis
- Tooling: linters, syntax highlighters, static analyzers
- RDF/SPARQL processing and semantic tooling

---

## Supported Languages

| Language | Format | Versions | Conformance | Lines |
|----------|--------|----------|-------------|-------|
| **C#** | XEBNF | 1.0 – 14.0 | Level 1 | 3,215 |
| **Go** | XEBNF | 1.13 – 1.22 | Level 1 | 1,376 |
| **Python** | XEBNF | 3.6 – 3.13 | Level 1 | 1,642 |
| **Java** | XEBNF | 5 – 21 | Level 1 | 1,884 |
| **JavaScript** | XEBNF | ES2015 – ES2024 | Level 1 | 1,767 |
| Turtle | EBNF | 1.1 | Level 2 | 55 |
| SPARQL | EBNF | 1.1 | Level 2 | 244 |

See [meta/languages.md](meta/languages.md) and [meta/coverage.md](meta/coverage.md) for details.

---

## Grammar Format

### XEBNF (`.xebnf`)

XEBNF extends ISO/IEC 14977 EBNF with features real grammars need:

- **Version annotations**: `@[since: "11.0"]` — embed version history in the grammar itself
- **Unicode character classes**: `\p{L}`, `\p{Nd}` — specify identifier rules precisely
- **Semantic predicates**: `{? in_async ?}` — document context-sensitive constraints
- **Conformance levels**: 0 (Structural) through 3 (Contextually Complete)
- **Conservative guarantee**: grammar-valid implies compiler-valid

Full specification: [meta/XEBNF-SPEC.md](meta/XEBNF-SPEC.md)

### Legacy EBNF (`.ebnf`)

Languages not yet migrated to XEBNF use legacy EBNF files. These may be syntactically complete (Level 2) but cannot reach Level 3 without XEBNF features. They are candidates for XEBNF rewrite.

---

## Repository Layout

```
grammars/<language>/
├── README.md            # Sources, version coverage, known gaps
├── lexical.xebnf        # Token-level grammar (all versions)
├── syntax.xebnf         # Structure-level grammar (all versions)
└── grammar.ebnf         # Combined (for small/single-version languages)
```

Version information lives in the grammar files via `@[since:]` annotations — not in the filesystem. To extract what changed in C# 11:

```bash
grep 'since: "11.0"' grammars/csharp/*.xebnf
```

---

## Contributing

We welcome contributions to expand and refine existing grammars, and to add support for additional languages. See <CONTRIBUTING.md>.

Priority opportunities:

- **XEBNF migration** of remaining EBNF grammars (SPARQL, Turtle)
- **New languages**: Rust, F#, Haskell, SQL, C/C++
- **Tooling**: version extraction, grammar validation, conformance testing

---

## License

MIT License — see <LICENSE>.
