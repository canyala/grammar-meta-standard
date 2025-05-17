# Supported Languages

This project tracks and maintains grammars in EBNF format for multiple languages and versions.

---

## Languages and Status

| Language     | Versions          | Coverage   | Reference Spec |
|--------------|-------------------|------------|----------------|
| C#           | v12, v13, v14      | Partial    | [docs.microsoft.com](https://docs.microsoft.com/en-us/dotnet/csharp/) |
| Python       | v3.12, v3.13       | Partial    | [python.org](https://docs.python.org/3/reference/grammar.html) |
| JavaScript   | v2022, v2023, v2024| Partial    | [ecma-international.org](https://www.ecma-international.org/publications-and-standards/standards/ecma-262/) |
| Java         | v21               | Minimal    | [docs.oracle.com](https://docs.oracle.com/en/java/javase/) |
| Go           | v1.22             | Minimal    | [go.dev/ref/spec](https://go.dev/ref/spec) |
| Turtle       | v1.1              | Partial    | [W3C Turtle](https://www.w3.org/TR/turtle/) |
| SPARQL       | v1.1              | Partial    | [W3C SPARQL 1.1](https://www.w3.org/TR/sparql11-query/) |
| Rust         | (planned)         | Not started| [rust-lang.github.io](https://doc.rust-lang.org/reference/) |
| SQL          | (planned)         | Not started| [ISO/IEC 9075](https://www.iso.org/standard/63555.html) |

---

Each grammar includes:
- `lexical.ebnf` for token-level syntax
- `syntax.ebnf` for language structure
- `changelog-<lang>.md` for tracking changes
