# grammar-meta-standard

A unified grammar repository for modern programming languages and DSLs, using a clear, consistent EBNF style.

---

## 🎯 Purpose

To provide well-structured, versioned grammars for real-world programming languages and DSLs in a common `.ebnf` format, suitable for:

- Code generation
- Syntax highlighting
- Tooling and linters
- Static analysis
- DSL development
- RDF/SPARQL processing and semantic tooling

---

## 📚 Supported Languages

| Language     | Versions                     | Coverage   |
|--------------|------------------------------|------------|
| C#           | v12, v13, v14                 | Partial    |
| Python       | v3.12, v3.13                  | Partial    |
| JavaScript   | v2022, v2023, v2024 (ECMAScript) | Partial |
| Java         | v21                           | Minimal    |
| Go           | v1.22                         | Minimal    |
| Turtle       | v1.1                          | Partial    |
| SPARQL       | v1.1                          | Partial    |

See [meta/languages.md](meta/languages.md) and [meta/coverage.md](meta/coverage.md) for more.

---

## 📐 Grammar Format

All `.ebnf` files follow the shared convention in [STYLE.md](STYLE.md). Files are split per version into:

- `lexical.ebnf` – character-level tokens
- `syntax.ebnf` – language-level structure

Each version is stored under:

```
grammars/<language>/<version>/
```

---

## 🤝 Contributing

We welcome contributions to expand and refine existing grammars, and especially to add support for additional languages.

### ✅ Examples of valuable future additions:

- Haskell
- F#
- C and C++
- Lisp dialects (Scheme, Common Lisp)
- Prolog
- Narrow but expressive DSLs (e.g. templating languages, configuration syntaxes)

If your favorite language isn't listed — you're warmly invited to add it!

---

## 📦 Tools

- See `tools/` for grammar diffing and transformation support.
- All grammars are designed to be tool-agnostic and readable by both humans and machines.

---

## 🔄 License

MIT License — see [LICENSE](LICENSE) for details.

