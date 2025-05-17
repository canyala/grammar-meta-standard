# EBNF Style Guide

This document defines the canonical formatting and structure for all `.ebnf` files in the `grammar-meta-standard` repository.

## 🎯 Purpose

To ensure consistency, tool compatibility, and clarity across all language grammars and versions.

---

## ✅ EBNF Syntax Rules (W3C-Inspired Style)

All grammar files in this repository **must** follow these conventions:

### Rule Definitions

Use `::=` to define rules:

```ebnf
identifier ::= letter { letter | digit } ;
```

### Sequence

Do **not** use `,` between rule elements in a sequence. Whitespace implies sequencing.

```ebnf
expression ::= term { "+" term } ;
```

### Alternation

Use `|` to represent alternatives:

```ebnf
digit ::= "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" ;
```

### Repetition

Use `{ ... }` to represent "zero or more":

```ebnf
digits ::= digit { digit } ;
```

### Optional Elements

Use `[ ... ]` to represent "zero or one":

```ebnf
sign ::= [ "+" | "-" ] ;
```

### Grouping

Use `( ... )` to group expressions:

```ebnf
fraction ::= "." ( digit { digit } ) ;
```

### Literals

Use either single `'` or double `"` quotes consistently within a file:

```ebnf
string-literal ::= "'" character { character } "'" ;
```

### Comments

Use `(* ... *)` for non-grammatical commentary:

```ebnf
(* This is a comment *)
```

---

## 🚫 Not Allowed

- Do not use `=` instead of `::=`
- Do not use `,` to separate terms
- Do not use PEG-style `←` or ANTLR syntax
- Do not use `? ... ?` (ISO special-sequences)

---

## 📂 File Placement

- All grammar files live under `grammars/<language>/<version>/`
- Each version must have:
  - `lexical.ebnf`
  - `syntax.ebnf`

---

## 🧠 Meta Notes

- Each grammar is parsed and diffed by tools under `tools/`
- Grammar changes should be recorded in `meta/changelog-<lang>.md`
- This format is inspired by W3C Turtle and SPARQL grammar publications

