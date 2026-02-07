# Repository Restructuring Plan: XEBNF Migration

## Context

The grammar-meta-standard repository currently uses a **version-per-directory** layout:

```
grammars/<language>/<version>/lexical.ebnf
grammars/<language>/<version>/syntax.ebnf
```

This was designed before XEBNF existed. XEBNF encodes version information inline via `@[since: ”X.Y”]` annotations, making the version-directory model redundant. A single grammar file now contains all versions, with mechanical extraction possible for any specific version.

This plan migrates the repository to a **language-per-directory** layout with XEBNF as the canonical grammar format.

## Target Structure

```
grammar-meta-standard/
├── README.md                           # Updated — describes XEBNF-first approach
├── LICENSE                             # Unchanged
├── CONTRIBUTING.md                     # Updated — references XEBNF, new layout
├── .gitignore                          # Unchanged
├── grammar-meta-standard.sln           # Unchanged
│
├── meta/
│   ├── XEBNF-SPEC.md                  # Moved from grammars/xebnf/v0.1/
│   ├── conformance.md                  # New — conformance level definitions
│   ├── coverage.md                     # Updated — reflects XEBNF grammars
│   └── languages.md                    # Updated — reflects new layout
│
├── grammars/
│   ├── csharp/
│   │   ├── README.md                   # New — C# specific docs
│   │   ├── lexical.xebnf               # Moved from v14/, covers 1.0–14.0
│   │   └── syntax.xebnf                # Moved from v14/, covers 1.0–14.0
│   │
│   ├── python/
│   │   ├── README.md                   # New — placeholder, documents legacy status
│   │   ├── lexical.ebnf                # Best existing content, preserved
│   │   └── syntax.ebnf                 # Best existing content, preserved
│   │
│   ├── javascript/
│   │   ├── README.md                   # New — placeholder, documents legacy status
│   │   ├── lexical.ebnf                # Best existing content, preserved
│   │   └── syntax.ebnf                 # Best existing content, preserved
│   │
│   ├── java/
│   │   ├── README.md                   # New — placeholder, documents legacy status
│   │   ├── lexical.ebnf                # Preserved from v21/
│   │   └── syntax.ebnf                 # Preserved from v21/
│   │
│   ├── go/
│   │   ├── README.md                   # New — placeholder, documents legacy status
│   │   ├── lexical.ebnf                # Preserved from v1.22/
│   │   └── syntax.ebnf                 # Preserved from v1.22/
│   │
│   ├── turtle/
│   │   ├── README.md                   # New — placeholder, documents legacy status
│   │   └── grammar.ebnf                # Preserved — single-version language
│   │
│   └── sparql/
│       ├── README.md                   # New — placeholder, documents legacy status
│       └── grammar.ebnf                # Preserved — single-version language
│
└── tools/                              # Unchanged internally
    ├── grammar-diff.py
    ├── EbnfSourceGenerator/
    └── csharp/
```

——

## Execution Steps

Execute these steps in order. Each step is atomic and can be committed separately.

### Step 1: Move the XEBNF spec to meta/

```bash
mv grammars/xebnf/v0.1/XEBNF-SPEC.md meta/XEBNF-SPEC.md
rm -rf grammars/xebnf
```

### Step 2: Create meta/conformance.md

Create the file `meta/conformance.md` with the following content:

```markdown
# XEBNF Conformance Levels

Extracted from [XEBNF-SPEC.md](XEBNF-SPEC.md) §7 for contributor reference.

—

## Levels

| Level | Name                   | Meaning |
|-——|————————|———|
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
conformance_notes: “…” ;
}

```
Legacy `.ebnf` files that predate XEBNF are treated as Level 0 (Structural).

## Gap Documentation

At Level 1+, all known gaps must be documented either:
- Inline via `@[description: ”...”]` annotations on the relevant production
- In the language’s `README.md` under a ”Known Gaps” section

Examples of gaps that should be documented:
- Context-sensitive constructs the grammar describes but cannot enforce (e.g. raw string delimiter matching)
- Parser disambiguation rules not captured in the grammar (e.g. cast vs parenthesized expression)
- Features the grammar omits entirely (should be listed explicitly)
```

### Step 3: Restructure grammars/csharp/

```bash
# Move XEBNF files up, rename to generic names
mv grammars/csharp/v14/csharp-v14-lexical.xebnf grammars/csharp/lexical.xebnf
mv grammars/csharp/v14/csharp-v14-syntax.xebnf grammars/csharp/syntax.xebnf

# Remove old version directories (content is subsumed by XEBNF files)
rm -rf grammars/csharp/v12
rm -rf grammars/csharp/v13
rm -rf grammars/csharp/v14
```

Create `grammars/csharp/README.md` with the following content:

```markdown
# C# Grammar — XEBNF

## Coverage

| File | Lines | Versions | Conformance |
|——|-——|-———|-————|
| `lexical.xebnf` | ~907 | C# 1.0 – 14.0 | Level 1 (Partial) |
| `syntax.xebnf` | ~2,308 | C# 1.0 – 14.0 | Level 1 (Partial) |

## Source Authority

- **ECMA-334** (C# 7 standard, via [dotnet/csharpstandard](https://github.com/dotnet/csharpstandard))
- **Microsoft Learn** C# language reference and feature specifications
- **dotnet/roslyn** CSharp.Generated.g4 (for cross-reference)
- **csharplang/proposals** for C# 8–14 feature specifications

## Version Annotations

All version-specific constructs are annotated with `@[since: ”X.Y”]`. To find what changed in a specific version:

```bash
grep ’since: ”11.0”’ lexical.xebnf syntax.xebnf
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
|————————————|———————————|-——————————————————|
|Raw string delimiter matching      |`@[description: ...]` + predicate|N opening quotes must match N closing quotes (N ≥ 3)   |
|Interpolated raw string brace depth|`@[description: ...]`            |N dollar signs determine brace interpolation depth     |
|Right-shift tokenization           |`right_shift = ’>’ ’>’`          |Not a single token — avoids `List<List<int>>` ambiguity|
|Cast vs. parenthesized expression  |Annotation                       |`(T)x` resolved by parser heuristics                   |
|Generic `<` vs. comparison `<`     |Annotation                       |ECMA-334 §6.2.4 disambiguation rules                   |
|`field` keyword                    |Context-sensitive identifier     |Only recognized inside property accessor bodies        |

## Previous Versions

This grammar replaces the separate `v12/`, `v13/`, `v14/` directories. The old `.ebnf` files were structural stubs (38–106 lines each). The XEBNF files are comprehensive and contain all version information inline via `@[since:]` annotations, making per-version directories unnecessary.

```
### Step 4: Flatten remaining language directories

For each language, keep the best/latest version’s files and remove version subdirectories.

**Python:**
```bash
# ecmascript dirs are larger, but python’s are all the same size — use latest
cp grammars/python/v3.13/lexical.ebnf grammars/python/lexical.ebnf
cp grammars/python/v3.13/syntax.ebnf grammars/python/syntax.ebnf
rm -rf grammars/python/v3.12 grammars/python/v3.13
# Keep changelog in meta/ (already exists as meta/changelog-python.md)
rm -f grammars/python/changelog-python.md
```

**JavaScript:**

```bash
# The ecmascript-2024 versions are the largest — use those
cp grammars/javascript/ecmascript-2024/lexical.ebnf grammars/javascript/lexical.ebnf
cp grammars/javascript/ecmascript-2024/syntax.ebnf grammars/javascript/syntax.ebnf
rm -rf grammars/javascript/v2022 grammars/javascript/v2023 grammars/javascript/v2024
rm -rf grammars/javascript/ecmascript-2022 grammars/javascript/ecmascript-2023 grammars/javascript/ecmascript-2024
# Keep changelog in meta/ (already exists as meta/changelog-javascript.md)
rm -f grammars/javascript/changelog-javascript.md
```

**Java:**

```bash
cp grammars/java/v21/lexical.ebnf grammars/java/lexical.ebnf
cp grammars/java/v21/syntax.ebnf grammars/java/syntax.ebnf
rm -rf grammars/java/v21
```

**Go:**

```bash
cp grammars/go/v1.22/lexical.ebnf grammars/go/lexical.ebnf
cp grammars/go/v1.22/syntax.ebnf grammars/go/syntax.ebnf
rm -rf grammars/go/v1.22
```

**Turtle:**

```bash
# grammar.ebnf is the complete one; lexical.ebnf and syntax.ebnf are stubs
cp grammars/turtle/v1.1/grammar.ebnf grammars/turtle/grammar.ebnf
rm -rf grammars/turtle/v1.1
```

**SPARQL:**

```bash
# grammar.ebnf is the complete one (244 lines); lexical/syntax are stubs
cp grammars/sparql/v1.1/grammar.ebnf grammars/sparql/grammar.ebnf
rm -rf grammars/sparql/v1.1
```

### Step 5: Create per-language README.md files for non-C# languages

Create a README.md in each language directory. These are brief placeholder docs marking legacy status. Use this template, customized per language:

**`grammars/python/README.md`:**

```markdown
# Python Grammar

## Status: Legacy EBNF (Level 0 — Structural)

These grammar files predate the XEBNF migration and cover basic constructs only.

| File | Lines | Version | Notes |
|——|-——|———|-——|
| `lexical.ebnf` | ~21 | 3.13 | Core token definitions |
| `syntax.ebnf` | ~33 | 3.13 | Core syntax structures |

## Source Authority

- [Python Language Reference — Grammar](https://docs.python.org/3/reference/grammar.html)
- PEG grammar from CPython source (`Grammar/python.gram`)

## Migration Path

These files are candidates for XEBNF rewrite covering Python 3.9–3.13+ with version annotations. The CPython PEG grammar is the authoritative machine-readable source and would serve as the primary reference for a comprehensive XEBNF grammar.

## Changelog

See [meta/changelog-python.md](../../meta/changelog-python.md).
```

**`grammars/javascript/README.md`:**

```markdown
# JavaScript (ECMAScript) Grammar

## Status: Legacy EBNF (Level 0 — Structural)

These grammar files predate the XEBNF migration and cover basic constructs only.

| File | Lines | Version | Notes |
|——|-——|———|-——|
| `lexical.ebnf` | ~46 | ECMAScript 2024 | Core token definitions |
| `syntax.ebnf` | ~59 | ECMAScript 2024 | Core syntax structures |

## Source Authority

- [ECMA-262](https://www.ecma-international.org/publications-and-standards/standards/ecma-262/) (ECMAScript Language Specification)

## Migration Path

Candidate for XEBNF rewrite covering ES2015–ES2024+ with version annotations. The ECMA-262 spec includes a formal grammar appendix that maps well to XEBNF.

## Changelog

See [meta/changelog-javascript.md](../../meta/changelog-javascript.md).
```

**`grammars/java/README.md`:**

```markdown
# Java Grammar

## Status: Legacy EBNF (Level 0 — Structural)

| File | Lines | Version | Notes |
|——|-——|———|-——|
| `lexical.ebnf` | ~17 | Java 21 | Minimal token definitions |
| `syntax.ebnf` | ~36 | Java 21 | Basic class and method structure |

## Source Authority

- [Java Language Specification](https://docs.oracle.com/javase/specs/)

## Migration Path

Candidate for XEBNF rewrite covering Java 8–21+ with version annotations.
```

**`grammars/go/README.md`:**

```markdown
# Go Grammar

## Status: Legacy EBNF (Level 0 — Structural)

| File | Lines | Version | Notes |
|——|-——|———|-——|
| `lexical.ebnf` | ~15 | Go 1.22 | Minimal token definitions |
| `syntax.ebnf` | ~34 | Go 1.22 | Basic syntax structures |

## Source Authority

- [The Go Programming Language Specification](https://go.dev/ref/spec)

## Migration Path

Candidate for XEBNF rewrite. Go’s spec already uses EBNF notation, making conversion relatively direct. Version annotations would track Go 1.18+ (generics) through current.
```

**`grammars/turtle/README.md`:**

```markdown
# Turtle Grammar

## Status: Legacy EBNF (Level 0 — Structural)

| File | Lines | Version | Notes |
|——|-——|———|-——|
| `grammar.ebnf` | ~55 | Turtle 1.1 | W3C grammar extracted |

## Source Authority

- [W3C RDF 1.1 Turtle](https://www.w3.org/TR/turtle/)

## Notes

Turtle is a single-version language with a small grammar. An XEBNF rewrite would add Unicode character class annotations and conformance metadata but would not benefit from version annotations.
```

**`grammars/sparql/README.md`:**

```markdown
# SPARQL Grammar

## Status: Legacy EBNF (Level 1 — Partial)

| File | Lines | Version | Notes |
|——|-——|———|-——|
| `grammar.ebnf` | ~244 | SPARQL 1.1 | Based on W3C sparql11-query spec |

## Source Authority

- [W3C SPARQL 1.1 Query Language](https://www.w3.org/TR/sparql11-query/)

## Notes

Most complete of the legacy grammars at 244 productions. An XEBNF rewrite could add version annotations if SPARQL 1.2 materializes, plus Unicode character classes for the lexical rules.
```

### Step 6: Delete root-level duplicates and obsolete files

```bash
# Root-level files that are duplicated in meta/ or replaced
rm coverage.md      # duplicated as meta/coverage.md
rm languages.md     # duplicated as meta/languages.md
rm STYLE.md         # replaced by meta/XEBNF-SPEC.md
```

### Step 7: Update meta/coverage.md

Replace the content of `meta/coverage.md` with:

```markdown
# Grammar Coverage

This document tracks grammar coverage status across all languages.

## Coverage Matrix

| Language | Format | Conformance | Versions Covered | Lexical | Syntactic | Notes |
|-———|———|-————|——————|———|————|-——|
| C# | **XEBNF** | **Level 1** | 1.0 – 14.0 | ~907 lines | ~2,308 lines | Comprehensive, version-annotated |
| Python | EBNF | Level 0 | 3.13 | ~21 lines | ~33 lines | Structural placeholder |
| JavaScript | EBNF | Level 0 | ES2024 | ~46 lines | ~59 lines | Structural placeholder |
| Java | EBNF | Level 0 | 21 | ~17 lines | ~36 lines | Structural placeholder |
| Go | EBNF | Level 0 | 1.22 | ~15 lines | ~34 lines | Structural placeholder |
| Turtle | EBNF | Level 0 | 1.1 | — | ~55 lines | Single grammar file |
| SPARQL | EBNF | Level 1 | 1.1 | — | ~244 lines | Most complete legacy grammar |

## Conformance Levels

See [conformance.md](conformance.md) for level definitions.

## Migration Status

C# is the first language fully migrated to XEBNF. Other languages retain their legacy EBNF files and are candidates for XEBNF rewrite. Priority candidates:

1. **SPARQL** — already 244 lines, small language, high relevance to Sky Omega
2. **Turtle** — small, single-version, straightforward conversion
3. **Python** — large language, PEG grammar available as authoritative source
4. **JavaScript** — large language, ECMA-262 appendix grammar available
```

### Step 8: Update meta/languages.md

Replace the content of `meta/languages.md` with:

```markdown
# Supported Languages

## Languages and Status

| Language | Directory | Format | Versions | Conformance | Reference Spec |
|-———|————|———|-———|-————|-—————|
| C# | `grammars/csharp/` | XEBNF | 1.0 – 14.0 | Level 1 | [ECMA-334](https://ecma-international.org/publications-and-standards/standards/ecma-334/) / [dotnet/csharpstandard](https://github.com/dotnet/csharpstandard) |
| Python | `grammars/python/` | EBNF | 3.13 | Level 0 | [python.org grammar](https://docs.python.org/3/reference/grammar.html) |
| JavaScript | `grammars/javascript/` | EBNF | ES2024 | Level 0 | [ECMA-262](https://www.ecma-international.org/publications-and-standards/standards/ecma-262/) |
| Java | `grammars/java/` | EBNF | 21 | Level 0 | [Java Language Spec](https://docs.oracle.com/javase/specs/) |
| Go | `grammars/go/` | EBNF | 1.22 | Level 0 | [go.dev/ref/spec](https://go.dev/ref/spec) |
| Turtle | `grammars/turtle/` | EBNF | 1.1 | Level 0 | [W3C Turtle](https://www.w3.org/TR/turtle/) |
| SPARQL | `grammars/sparql/` | EBNF | 1.1 | Level 1 | [W3C SPARQL 1.1](https://www.w3.org/TR/sparql11-query/) |

## Grammar Format

**XEBNF grammars** (`.xebnf`) encode version information inline via `@[since:]` annotations. A single file contains all versions. See [XEBNF-SPEC.md](XEBNF-SPEC.md).

**Legacy EBNF grammars** (`.ebnf`) are structural placeholders from before the XEBNF migration. They cover a single version and use the convention described in the repository’s original STYLE.md (now superseded by the XEBNF spec).

## Layout
```

grammars/<language>/
├── README.md           # Language-specific documentation
├── lexical.xebnf       # Lexical grammar (XEBNF) — or lexical.ebnf (legacy)
├── syntax.xebnf        # Syntactic grammar (XEBNF) — or syntax.ebnf (legacy)
└── grammar.xebnf       # Combined grammar for small languages (optional)

```
## Planned Languages

| Language | Priority | Notes |
|-———|-———|-——|
| Rust | Medium | Comprehensive reference grammar available |
| F# | Medium | Shares C# lineage, ECMA-334 parallel |
| SQL | Low | ISO/IEC 9075, very large grammar |
| Haskell | Low | Haskell 2010 Report has formal grammar |
```

### Step 9: Update meta/changelog-csharp.md

Replace the content of `meta/changelog-csharp.md` with:

```markdown
# C# Grammar Changelog

## XEBNF Migration (2026-02-07)

The C# grammar has been migrated from per-version EBNF stubs to comprehensive XEBNF files covering C# 1.0 through 14.0.

- **lexical.xebnf** — 907 lines, 12 sections, ~150 productions
- **syntax.xebnf** — 2,308 lines, 15 sections, ~400 productions
- **353 version annotations** tracking feature introductions across 14 C# versions
- Conformance Level 1 (Partial) with documented gaps

### Previous state

- `v12/lexical.ebnf` (38 lines) — structural placeholder
- `v12/syntax.ebnf` (106 lines) — structural placeholder  
- `v13/lexical.ebnf` (43 lines) — structural placeholder
- `v13/syntax.ebnf` (106 lines) — structural placeholder

These have been removed. Their content is fully subsumed by the XEBNF grammar’s `@[since:]` annotations.

## Version History (by C# version)

The grammar’s non-normative Section 15 (in syntax.xebnf) contains a complete version-by-version changelog of syntactic additions from C# 1.0 through C# 14.0. The lexical grammar’s Section 12 contains the corresponding lexical changes. Rather than duplicate that here, refer to those sections directly:

```bash
# View syntactic version history
grep -A 100 ’VERSION HISTORY’ grammars/csharp/syntax.xebnf

# View lexical version history  
grep -A 60 ’Version History’ grammars/csharp/lexical.xebnf

# Find all features added in a specific version
grep ’since: ”11.0”’ grammars/csharp/lexical.xebnf grammars/csharp/syntax.xebnf
```

```
### Step 10: Update root README.md

Replace the content of `README.md` with:

```markdown
# grammar-meta-standard

A unified grammar repository for programming languages and DSLs, using [XEBNF](meta/XEBNF-SPEC.md) — an extended EBNF notation with version annotations, Unicode support, semantic predicates, and conformance levels.

—

## Purpose

Machine-readable, versioned formal grammars for:

- Grammar-guided code generation (LLM-assisted and traditional)
- Syntax-valid output verification
- Language version comparison and migration analysis
- Tooling: linters, syntax highlighters, static analyzers
- RDF/SPARQL processing and semantic tooling

—

## Supported Languages

| Language | Format | Versions | Conformance | Lines |
|-———|———|-———|-————|-——|
| **C#** | XEBNF | 1.0 – 14.0 | Level 1 | 3,215 |
| Python | EBNF | 3.13 | Level 0 | 54 |
| JavaScript | EBNF | ES2024 | Level 0 | 105 |
| Java | EBNF | 21 | Level 0 | 53 |
| Go | EBNF | 1.22 | Level 0 | 49 |
| Turtle | EBNF | 1.1 | Level 0 | 55 |
| SPARQL | EBNF | 1.1 | Level 1 | 244 |

See [meta/languages.md](meta/languages.md) and [meta/coverage.md](meta/coverage.md) for details.

—

## Grammar Format

### XEBNF (`.xebnf`)

XEBNF extends ISO/IEC 14977 EBNF with features real grammars need:

- **Version annotations**: `@[since: ”11.0”]` — embed version history in the grammar itself
- **Unicode character classes**: `\p{L}`, `\p{Nd}` — specify identifier rules precisely
- **Semantic predicates**: `{? in_async ?}` — document context-sensitive constraints
- **Conformance levels**: 0 (Structural) through 3 (Contextually Complete)
- **Conservative guarantee**: grammar-valid implies compiler-valid

Full specification: [meta/XEBNF-SPEC.md](meta/XEBNF-SPEC.md)

### Legacy EBNF (`.ebnf`)

Languages not yet migrated to XEBNF retain structural EBNF files. These are candidates for comprehensive rewrite.

—

## Repository Layout
```

grammars/<language>/
├── README.md            # Sources, version coverage, known gaps
├── lexical.xebnf        # Token-level grammar (all versions)
├── syntax.xebnf         # Structure-level grammar (all versions)
└── grammar.xebnf        # Combined (for small languages)

```
Version information lives in the grammar files via `@[since:]` annotations — not in the filesystem. To extract what changed in C# 11:

```bash
grep ’since: ”11.0”’ grammars/csharp/*.xebnf
```

——

## Contributing

We welcome contributions to expand and refine existing grammars, and to add support for additional languages. See <CONTRIBUTING.md>.

Priority opportunities:

- **XEBNF migration** of existing EBNF grammars (Python, JavaScript, SPARQL)
- **New languages**: Rust, F#, Haskell, SQL, C/C++
- **Tooling**: version extraction, grammar validation, conformance testing

——

## License

MIT License — see <LICENSE>.

```
### Step 11: Update CONTRIBUTING.md

Replace the content of `CONTRIBUTING.md` with:

```markdown
# Contributing to grammar-meta-standard

## How to Contribute

1. Fork the repo and create a branch
2. Make your changes following the guidelines below
3. Submit a pull request
4. Use issues to propose structural or technical improvements

## Grammar Guidelines

### XEBNF Grammars (preferred)

New grammars and rewrites of existing grammars should use XEBNF notation (`.xebnf` extension). See [meta/XEBNF-SPEC.md](meta/XEBNF-SPEC.md) for the full specification.

Key requirements:
- Include a `@grammar { ... }` header with language, version, conformance level
- Annotate version-specific productions with `@[since: ”X.Y”]`
- Document known gaps and context-sensitive constructs
- Declare conformance level honestly — see [meta/conformance.md](meta/conformance.md)
- Split into `lexical.xebnf` and `syntax.xebnf` for non-trivial languages

### Legacy EBNF Grammars

Existing `.ebnf` files use the W3C-inspired style:
- `=` for rule definitions (not `::=`)
- `{ ... }` for repetition, `[ ... ]` for optional
- `(* ... *)` for comments
- No commas between sequence elements

### Adding a New Language

1. Create `grammars/<language>/`
2. Add a `README.md` documenting sources, version coverage, and conformance
3. Add grammar files (`lexical.xebnf` + `syntax.xebnf`, or `grammar.xebnf` for small languages)
4. Update `meta/languages.md` and `meta/coverage.md`

### File Placement

- Grammar files: `grammars/<language>/`
- Language changelogs: `meta/changelog-<language>.md`
- Tooling: `tools/`

## Maintainer

This repository is maintained under the Canyala GitHub organization by Martin Fredriksson.
```

### Step 12: Delete empty/obsolete meta files

```bash
# schema.md is superseded by the @grammar header in XEBNF files
rm meta/schema.md
```

——

## Commit Strategy

Suggested commits (can be squashed into fewer if preferred):

1. **“Move XEBNF spec to meta/, add conformance.md”** — Steps 1–2
1. **“Restructure C# grammars to XEBNF layout”** — Step 3
1. **“Flatten language directories, remove version subdirs”** — Step 4
1. **“Add per-language README.md files”** — Step 5
1. **“Remove root-level duplicates, update meta docs”** — Steps 6–9
1. **“Update root README.md and CONTRIBUTING.md”** — Steps 10–12

Or as a single commit: **“Restructure repository for XEBNF: version-in-grammar replaces version-in-filesystem”**

——

## What This Preserves

- All existing grammar content (nothing is deleted without replacement)
- Git history (moves, not delete+recreate where possible)
- The `tools/` directory (unchanged)
- The `.sln` file and C# tooling projects (unchanged)
- The MIT license

## What This Removes

- `STYLE.md` — replaced by `meta/XEBNF-SPEC.md`
- `coverage.md` (root) — was a duplicate of `meta/coverage.md`
- `languages.md` (root) — was a duplicate of `meta/languages.md`
- `meta/schema.md` — replaced by `@grammar` headers in XEBNF
- `grammars/xebnf/v0.1/` — spec moved to `meta/`
- `grammars/*/v*/` subdirectories — content promoted to parent
- Old C# `.ebnf` stubs (38–106 lines) — subsumed by 3,215-line XEBNF grammar

## What This Adds

- `meta/conformance.md` — conformance level reference
- `grammars/*/README.md` — per-language documentation (7 files)
- Updated root docs reflecting the XEBNF-first approach