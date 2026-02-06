# XEBNF — Extended EBNF for Production Systems

**Version:** 0.1.0-draft  
**Status:** Working Draft  
**Date:** 2026-02-06  
**Authors:** Martin (Canyala Innovation AB)  
**License:** MIT  
**Repository:** https://github.com/canyala/grammar-meta-standard

---

## 1. Purpose and Scope

XEBNF is a notation for specifying the complete syntactic structure of programming languages and domain-specific languages, including aspects that standard EBNF cannot express.

### 1.1 Problem Statement

ISO/IEC 14977 EBNF describes context-free grammars. Real programming languages are not context-free. Every production compiler contains parsing logic that goes beyond what its grammar captures: contextual keywords, lookahead-driven disambiguation, operator precedence encoded as rule hierarchies, version-dependent syntax, and Unicode-aware lexical rules. This implicit logic lives in handwritten parser code, unreachable by tools that reason about grammars.

XEBNF makes this implicit logic explicit, declarative, and machine-readable.

### 1.2 Design Goals

1. **Strict superset of ISO/IEC 14977.** Every valid EBNF grammar is a valid XEBNF grammar with identical semantics. XEBNF adds; it never redefines.

2. **Descriptive, not generative.** XEBNF describes what a language accepts. It is not a parser generator input format, though parser generators may consume it. The primary consumers are reasoning tools, code generators, and humans.

3. **Machine-parseable.** The XEBNF notation is itself formally specified and can be parsed mechanically. An XEBNF grammar for XEBNF is provided in Appendix A.

4. **Monotonically improvable.** A grammar can be incomplete. Unknown context-sensitivity can be documented as such and refined over time without invalidating existing rules. Partial grammars are useful grammars.

5. **Version-aware by design.** Language evolution is a first-class concern. Rules carry version metadata enabling mechanical comparison between language versions.

6. **Conservative guarantee.** If code validates against an XEBNF grammar, it is syntactically valid in the described language. The converse need not hold — the grammar may reject some valid programs. Soundness over completeness.

### 1.3 Non-Goals

- Replacing parser generators (ANTLR, PEG, Yacc). XEBNF is a specification notation, not a toolchain.
- Capturing full semantic analysis. Type checking, overload resolution, and name binding are outside scope.
- Defining a universal intermediate representation for ASTs.

### 1.4 Relationship to Existing Standards

| Standard | Relationship |
|---|---|
| ISO/IEC 14977 (EBNF) | XEBNF is a strict superset. All ISO EBNF is valid XEBNF. |
| ANTLR notation | XEBNF can represent everything ANTLR grammars express, but uses different syntax. Mechanical conversion is possible in both directions with documented limitations. |
| W3C EBNF (as used in SPARQL, TURTLE, XML) | W3C EBNF is a subset. XEBNF can import W3C grammars directly. |
| PEG (Parsing Expression Grammars) | PEG's ordered choice and greedy semantics differ from XEBNF's declarative alternatives. XEBNF can annotate ordering when needed but does not require it. |

---

## 2. Conventions and Terminology

### 2.1 Typographic Conventions

In this specification:

- `monospace` denotes XEBNF syntax elements.
- *Italic* denotes defined terms on first use.
- Examples are presented in fenced code blocks marked `xebnf`.
- Non-normative notes are marked with **Note:**.

### 2.2 Key Terms

- **Production**: A named rule mapping a non-terminal to its definition.
- **Terminal**: A literal string or character class that matches input directly.
- **Non-terminal**: A named syntactic category defined by a production.
- **Extension**: An XEBNF construct beyond ISO/IEC 14977 EBNF.
- **Annotation**: Metadata attached to a production or alternative that does not affect matching semantics but provides information to tools and humans.
- **Predicate**: A condition that must hold for an alternative to be considered. Predicates extend context-free matching with context-sensitive constraints.

---

## 3. Core Notation (ISO/IEC 14977 Compatible)

This section restates the ISO/IEC 14977 EBNF constructs that XEBNF inherits unchanged. Readers familiar with standard EBNF may skim this section.

### 3.1 Productions

A production assigns a definition to a non-terminal name.

```xebnf
non_terminal = definition ;
```

The non-terminal name consists of letters, digits, underscores, and spaces. The `=` separates name from definition. The `;` terminates the production.

**Note:** XEBNF also accepts `::=` as the definition operator for compatibility with common usage. The two forms are semantically identical.

### 3.2 Terminals

Terminal strings are enclosed in single quotes `'...'` or double quotes `"..."`.

```xebnf
keyword_if = "if" ;
single_char = 'x' ;
```

### 3.3 Alternation

The `|` operator separates alternatives.

```xebnf
boolean_literal = "true" | "false" ;
```

### 3.4 Concatenation

Juxtaposition (with separating whitespace) denotes concatenation.

```xebnf
if_statement = "if" "(" expression ")" statement ;
```

The `,` operator may also be used for concatenation per ISO/IEC 14977.

### 3.5 Grouping

Parentheses group sub-expressions.

```xebnf
sign = ( "+" | "-" ) ;
```

### 3.6 Optional

Square brackets denote optional elements (zero or one occurrence).

```xebnf
return_statement = "return" [ expression ] ";" ;
```

### 3.7 Repetition

Curly braces denote repetition (zero or more occurrences).

```xebnf
argument_list = expression { "," expression } ;
```

### 3.8 Exception

The `-` operator excludes a set from another.

```xebnf
letter_except_e = letter - "e" ;
```

### 3.9 Comments

Comments are enclosed in `(* ... *)`.

```xebnf
(* This is a comment *)
```

### 3.10 Special Sequences

Special sequences are enclosed in `? ... ?` and denote constructs described in prose.

```xebnf
any_unicode_char = ? any Unicode code point ? ;
```

**Note:** XEBNF extensions (Sections 4–9) replace most uses of special sequences with formal notation, reducing reliance on prose.

---

## 4. Extension: Character Classes and Unicode

**Addresses:** ISO EBNF has no notation for character ranges, Unicode categories, or character class algebra. Languages like C# define identifiers using Unicode General Categories.

### 4.1 Character Ranges

A range of characters is specified with `..` between two quoted single characters.

```xebnf
digit = '0'..'9' ;
hex_digit = '0'..'9' | 'a'..'f' | 'A'..'F' ;
```

### 4.2 Unicode Categories

Unicode General Category classes are denoted with `\p{Category}` and their negation with `\P{Category}`.

```xebnf
unicode_letter = \p{Lu} | \p{Ll} | \p{Lt} | \p{Lm} | \p{Lo} | \p{Nl} ;
unicode_digit = \p{Nd} ;
```

Supported category abbreviations follow the Unicode Standard (UAX #44). Commonly used categories:

| Category | Meaning |
|---|---|
| `Lu` | Uppercase letter |
| `Ll` | Lowercase letter |
| `Lt` | Titlecase letter |
| `Lm` | Modifier letter |
| `Lo` | Other letter |
| `Nl` | Letter number |
| `Nd` | Decimal digit |
| `Mn` | Non-spacing mark |
| `Mc` | Spacing combining mark |
| `Cf` | Format character |
| `Pc` | Connector punctuation |

### 4.3 Unicode Code Points

Individual code points are specified with `\u{XXXX}` notation.

```xebnf
bom = \u{FEFF} ;
null_char = \u{0000} ;
```

### 4.4 Character Class Negation

The `~` prefix operator negates a character class or terminal, matching any single character NOT in the specified set.

```xebnf
not_newline = ~( '\r' | '\n' ) ;
not_star = ~'*' ;
```

### 4.5 Wildcard

The `.` matches any single character.

```xebnf
any_char = . ;
```

---

## 5. Extension: Quantifiers

**Addresses:** ISO EBNF only provides `{ }` for zero-or-more repetition. Real grammars frequently need one-or-more, exact counts, and bounded repetition.

### 5.1 One-or-More

The `+` suffix operator denotes one or more occurrences.

```xebnf
digits = digit+ ;
```

This is equivalent to `digit { digit }` in ISO EBNF but communicates intent more clearly.

### 5.2 Zero-or-One

The `?` suffix operator denotes an optional element.

```xebnf
sign = ( "+" | "-" )? ;
```

This is equivalent to `[ "+" | "-" ]` in ISO EBNF. Both forms remain valid in XEBNF.

### 5.3 Bounded Repetition

Repetition with explicit bounds is specified as `{n}`, `{n,}`, `{,m}`, or `{n,m}`.

```xebnf
ipv4_octet = digit{1,3} ;
exactly_four = hex_digit{4} ;
two_or_more = element{2,} ;
```

---

## 6. Extension: Lookahead and Lookbehind

**Addresses:** Many language constructs are syntactically ambiguous without examining subsequent tokens. Standard EBNF provides no mechanism to express disambiguation by lookahead.

### 6.1 Positive Lookahead

`(?= pattern )` succeeds if `pattern` matches at the current position but consumes no input.

```xebnf
(* In C#, distinguish lambda from parenthesized expression *)
lambda_expression = parameter_list (?= "=>" ) "=>" body ;
```

### 6.2 Negative Lookahead

`(?! pattern )` succeeds if `pattern` does NOT match at the current position. Consumes no input.

```xebnf
(* Identifier that is not a keyword *)
identifier = identifier_start identifier_part* (?! keyword ) ;
```

**Note:** Negative lookahead is the primary mechanism for expressing contextual keywords in XEBNF.

### 6.3 Positive Lookbehind

`(?<= pattern )` succeeds if `pattern` matches immediately before the current position. Consumes no input.

### 6.4 Negative Lookbehind

`(?<! pattern )` succeeds if `pattern` does NOT match immediately before the current position. Consumes no input.

### 6.5 Constraints on Lookaround

Lookaround patterns should be finite and non-recursive. An XEBNF processor may reject grammars with unbounded lookaround. This constraint is intentional — unbounded lookaround conflates specification with implementation.

---

## 7. Extension: Predicates

**Addresses:** Context-sensitive parsing decisions that depend on state outside the grammar itself — whether we are inside an async method, which language version is targeted, whether a particular feature flag is active.

### 7.1 Semantic Predicates

A semantic predicate is a named boolean condition enclosed in `{? ... ?}` that gates an alternative.

```xebnf
(* await is only valid in async context *)
await_expression = {? in_async ?} "await" expression ;
```

Predicate names are declared in the grammar's preamble (Section 10) with prose descriptions of their meaning. The grammar does not define how predicates are evaluated — that is the responsibility of the consuming tool.

### 7.2 Version Predicates

A version predicate is a specific semantic predicate that tests the target language version. It uses the reserved predicate syntax `{? version ... ?}`.

```xebnf
(* Extension members introduced in C# 14 *)
extension_declaration = {? version >= 14.0 ?} "extension" "(" type ")" extension_body ;

(* Record types introduced in C# 9 *)
record_declaration = {? version >= 9.0 ?} "record" identifier type_parameter_list? parameter_list? record_body ;
```

Version predicates are the primary mechanism for expressing multi-version grammars in a single file. See Section 11 for the version model.

### 7.3 Predicate Composition

Predicates may be combined with logical operators:

```xebnf
{? in_async && version >= 8.0 ?}   (* both must hold *)
{? version >= 9.0 || preview ?}     (* either suffices *)
{? !in_unsafe ?}                     (* negation *)
```

---

## 8. Extension: Annotations

**Addresses:** Grammars need metadata that does not affect matching but informs tools, documentation, and reasoning. ISO EBNF offers only comments for this purpose.

### 8.1 Annotation Syntax

Annotations are key-value pairs enclosed in `@[...]` and attached to the production or alternative that follows.

```xebnf
@[since: "7.0", description: "Pattern matching with type patterns"]
is_pattern = expression "is" type identifier? ;
```

### 8.2 Reserved Annotation Keys

The following annotation keys have defined semantics in XEBNF:

| Key | Type | Meaning |
|---|---|---|
| `since` | version string | Language version that introduced this production. |
| `deprecated` | version string | Language version that deprecated this production. |
| `removed` | version string | Language version that removed this production. |
| `description` | string | Human-readable description. |
| `see` | URI | Reference to specification or documentation. |
| `category` | string | Classification: `"lexical"`, `"syntactic"`, `"contextual"`. |
| `context` | string | Required parsing context (e.g., `"async_method"`, `"unsafe_block"`). |
| `precedence` | integer | Operator precedence level (higher binds tighter). |
| `associativity` | string | `"left"`, `"right"`, or `"none"`. |
| `recovery` | string | Error recovery hint for parser generators. |

### 8.3 Custom Annotations

Annotations with keys not listed above are permitted. Their semantics are defined by the consuming tool. Custom annotation keys should use a namespace prefix to avoid future collisions.

```xebnf
@[sky_omega:epistemic_weight: "high", sky_omega:transformation_safe: true]
simple_assignment = identifier "=" expression ";" ;
```

### 8.4 Production-Level vs Alternative-Level Annotations

Annotations before a production name apply to the entire production. Annotations before an alternative (after `|`) apply to that alternative only.

```xebnf
@[since: "1.0"]
literal
    = integer_literal
    | real_literal
    | @[since: "11.0"] raw_string_literal
    | @[since: "6.0"] interpolated_string_literal
    ;
```

---

## 9. Extension: Parameterized Rules

**Addresses:** Some syntactic constructs have identical structure but differ by context. Expression parsing inside `for` initializers disallows comma expressions; expression inside interpolation holes disallows certain braces. Rather than duplicating rules, parameterized rules express the variation.

### 9.1 Syntax

Parameters are declared in angle brackets after the rule name.

```xebnf
expression<allow_comma: bool, allow_colon: bool>
    = assignment_expression
    | {? allow_comma ?} expression "," assignment_expression
    ;
```

### 9.2 Instantiation

Parameterized rules are referenced with explicit arguments.

```xebnf
for_initializer = expression<allow_comma: false, allow_colon: true> ;
argument = expression<allow_comma: true, allow_colon: true> ;
```

### 9.3 Defaults

Parameters may have default values.

```xebnf
expression<allow_comma: bool = true>
    = assignment_expression
    | {? allow_comma ?} expression "," assignment_expression
    ;

(* Called without argument, uses default *)
return_statement = "return" expression ";" ;
```

---

## 10. Grammar Structure

An XEBNF grammar file has the following top-level structure:

```xebnf
(* ============================================================
   Grammar preamble
   ============================================================ *)

@grammar {
    name: "C#" ;
    version: "14.0" ;
    family: "csharp" ;
    spec: "ECMA-334" ;
    uri: "https://github.com/canyala/grammar-meta-standard/grammars/csharp/v14" ;
}

@predicates {
    in_async: "Currently within an async method or lambda body" ;
    in_unsafe: "Currently within an unsafe block or method" ;
    in_checked: "Currently within a checked expression context" ;
    preview: "Preview language features are enabled" ;
}

@imports {
    (* Import shared definitions *)
    unicode_classes from "../../shared/unicode.xebnf" ;
}

(* ============================================================
   Lexical rules
   ============================================================ *)

@[category: "lexical"]
(* Lexical rules by convention use UPPER_CASE names *)

IDENTIFIER = identifier_start identifier_part* ;

(* ============================================================
   Syntactic rules
   ============================================================ *)

@[category: "syntactic"]
(* Syntactic rules by convention use lower_case names *)

compilation_unit = extern_alias_directive* using_directive* 
                   global_attribute* namespace_member_declaration* ;
```

### 10.1 The `@grammar` Block

Required. Declares metadata about the grammar. All fields are string-valued.

Required fields: `name`, `version`.

Optional fields: `family` (for grouping versions), `spec` (normative reference), `uri` (canonical location), `encoding` (default: `"UTF-8"`).

### 10.2 The `@predicates` Block

Optional. Declares all semantic predicates used in the grammar with human-readable descriptions. A predicate that appears in a rule but is not declared in the preamble is a grammar error.

### 10.3 The `@imports` Block

Optional. Imports productions from other XEBNF files. Enables sharing of common definitions (Unicode classes, shared expression forms) across language versions.

Imported names may be aliased:

```xebnf
@imports {
    unicode_letter as letter from "../../shared/unicode.xebnf" ;
}
```

---

## 11. Version Model

XEBNF's version model supports the grammar-meta-standard use case of comparing language versions and guiding version-specific code generation.

### 11.1 Single-Version Grammars

The simplest model: one `.xebnf` file per language version. Version comparison is performed by external diffing tools operating on the grammar files.

```
grammars/csharp/v12/lexical.xebnf
grammars/csharp/v12/syntax.xebnf
grammars/csharp/v13/lexical.xebnf
grammars/csharp/v13/syntax.xebnf
grammars/csharp/v14/lexical.xebnf
grammars/csharp/v14/syntax.xebnf
```

### 11.2 Multi-Version Grammars

A single grammar file may describe multiple versions using version predicates and `@[since]` / `@[removed]` annotations. A tool targeting a specific version evaluates all version predicates and includes only the productions valid for that version.

```xebnf
@grammar {
    name: "C#" ;
    version: "7.0-14.0" ;
    family: "csharp" ;
}

@[since: "1.0"]
class_declaration = "class" identifier class_body ;

@[since: "9.0"]
record_declaration = "record" identifier parameter_list? record_body ;

@[since: "14.0"]
extension_declaration = "extension" "(" type ")" extension_body ;
```

### 11.3 Version Extraction

Given a multi-version grammar and a target version V, the *extracted grammar* for V consists of all productions where:

- `since` ≤ V (or `since` is absent), AND
- `deprecated` > V (or `deprecated` is absent), AND
- `removed` > V (or `removed` is absent), AND
- All version predicates in the production evaluate to true for V.

This extraction is a mechanical operation. The result is a single-version grammar.

### 11.4 Version Diff

Given two extracted grammars for versions V1 and V2, the *version diff* identifies:

- **Added productions**: present in V2, absent in V1.
- **Removed productions**: present in V1, absent in V2.
- **Modified productions**: present in both but with different definitions.

The diff is the basis for code migration guidance: "this syntax is new in V2" or "this syntax was removed in V2."

---

## 12. Conformance Levels

XEBNF recognizes that grammars improve over time. Rather than requiring perfection, it defines explicit conformance levels.

### 12.1 Level 0: Structural

The grammar is syntactically valid XEBNF. It parses. No claim is made about its accuracy as a language description.

### 12.2 Level 1: Partial

The grammar correctly describes a documented subset of the target language. The covered subset is declared in the grammar metadata. Uncovered areas are either absent or marked with `? ... ?` special sequences.

### 12.3 Level 2: Syntactically Complete

The grammar contains productions for every syntactic construct in the target language. All valid programs should match the grammar (completeness), and no invalid programs should match (soundness). Context-sensitive aspects may use predicates or annotations rather than being fully resolved.

### 12.4 Level 3: Contextually Complete

The grammar captures all context-sensitive parsing decisions via predicates, parameterized rules, and lookahead. A conformant parser can be mechanically derived from the grammar with no additional disambiguation logic.

**Note:** Level 3 may be unachievable for some languages (notably C++) where parsing is undecidable in the general case. The level system makes this limitation explicit rather than hiding it.

### 12.5 Declaring Conformance

```xebnf
@grammar {
    name: "C#" ;
    version: "14.0" ;
    conformance: 2 ;
    conformance_notes: "Preprocessor directives and string interpolation 
                        nesting use semantic predicates. Full contextual 
                        keyword disambiguation requires Level 3." ;
}
```

---

## 13. Operator Precedence and Associativity

**Addresses:** Expression grammars in standard EBNF encode precedence through rule hierarchy (e.g., `multiplicative_expression` wrapping `unary_expression`). This obscures the actual precedence table and makes version diffing harder — adding a new operator at a precedence level requires restructuring multiple rules.

### 13.1 Precedence Annotations

Precedence and associativity are expressed through annotations on alternatives within an expression rule.

```xebnf
expression
    = primary_expression
    | @[precedence: 14, associativity: "left"]  expression "*"  expression
    | @[precedence: 14, associativity: "left"]  expression "/"  expression
    | @[precedence: 13, associativity: "left"]  expression "+"  expression
    | @[precedence: 13, associativity: "left"]  expression "-"  expression
    | @[precedence: 6,  associativity: "left"]  expression "==" expression
    | @[precedence: 2,  associativity: "right"] expression "="  expression
    ;
```

Higher precedence values bind tighter.

### 13.2 Equivalence

A precedence-annotated expression rule is semantically equivalent to the traditional rule hierarchy. Tools may expand one form to the other mechanically. XEBNF allows both — an author may use whichever is clearer for the language being described.

---

## 14. Error Productions

**Addresses:** Grammars used for code generation benefit from explicitly marking common error patterns, enabling tools to suggest corrections.

### 14.1 Syntax

Error productions use the `@[error]` annotation with a message.

```xebnf
@[error: "Missing semicolon after statement"]
statement_error = expression (?= "}" | EOF ) ;
```

Error productions are never part of the valid language. They exist to improve diagnostic quality in tools that consume the grammar.

---

## 15. File Format

### 15.1 Encoding

XEBNF files are UTF-8 encoded.

### 15.2 File Extension

`.xebnf`

### 15.3 Line Endings

LF or CRLF. Tools must accept both.

### 15.4 Naming Conventions

- Lexical rules: `UPPER_CASE` or `PascalCase` (representing tokens).
- Syntactic rules: `lower_case` with underscores (representing grammar structure).
- Predicate names: `lower_case` with underscores.
- Annotation keys: `lower_case` with underscores.

---

## 16. XEBNF Grammar for XEBNF (Self-Description)

The following is a Level 2 XEBNF grammar describing the XEBNF notation itself.

```xebnf
@grammar {
    name: "XEBNF" ;
    version: "0.1.0" ;
    conformance: 2 ;
}

(* ============================================================
   Top-level structure
   ============================================================ *)

grammar_file = preamble_block* production* ;

preamble_block
    = grammar_block
    | predicates_block
    | imports_block
    ;

grammar_block = "@grammar" "{" grammar_field+ "}" ;
grammar_field = identifier ":" string_literal ";" ;

predicates_block = "@predicates" "{" predicate_decl+ "}" ;
predicate_decl = identifier ":" string_literal ";" ;

imports_block = "@imports" "{" import_decl+ "}" ;
import_decl = identifier [ "as" identifier ] "from" string_literal ";" ;

(* ============================================================
   Productions
   ============================================================ *)

production = annotation* identifier [ type_parameters ] "=" definition_list ";" ;

type_parameters = "<" param_decl ( "," param_decl )* ">" ;
param_decl = identifier ":" type_name [ "=" literal ] ;
type_name = "bool" | "int" | "string" ;

definition_list = definition ( "|" definition )* ;
definition = annotation* term+ ;

term
    = factor quantifier?
    | predicate
    ;

factor
    = identifier [ "<" argument ( "," argument )* ">" ]   (* non-terminal reference *)
    | string_literal                                        (* terminal string *)
    | char_range                                            (* character range *)
    | unicode_category                                      (* \p{...} or \P{...} *)
    | unicode_codepoint                                     (* \u{XXXX} *)
    | "~" factor                                            (* negation *)
    | "."                                                   (* wildcard *)
    | "(" definition_list ")"                               (* grouping *)
    | "[" definition_list "]"                                (* optional — ISO EBNF *)
    | "{" definition_list "}"                                (* repetition — ISO EBNF *)
    | lookaround
    | special_sequence
    ;

quantifier
    = "?"                           (* zero or one *)
    | "+"                           (* one or more *)
    | "*"                           (* zero or more — alias for { } *)
    | "{" INTEGER "}"               (* exactly N *)
    | "{" INTEGER "," "}"           (* N or more *)
    | "{" "," INTEGER "}"           (* at most M *)
    | "{" INTEGER "," INTEGER "}"   (* between N and M *)
    ;

lookaround
    = "(?=" definition_list ")"     (* positive lookahead *)
    | "(?!" definition_list ")"     (* negative lookahead *)
    | "(?<=" definition_list ")"    (* positive lookbehind *)
    | "(?<!" definition_list ")"    (* negative lookbehind *)
    ;

predicate = "{?" predicate_expr "?}" ;
predicate_expr
    = identifier
    | "!" predicate_expr
    | predicate_expr "&&" predicate_expr
    | predicate_expr "||" predicate_expr
    | "version" comparison_op version_literal
    | "(" predicate_expr ")"
    ;

comparison_op = ">=" | "<=" | ">" | "<" | "==" | "!=" ;

(* ============================================================
   Annotations
   ============================================================ *)

annotation = "@[" annotation_pair ( "," annotation_pair )* "]" ;
annotation_pair = identifier ":" literal ;

(* ============================================================
   Lexical foundations
   ============================================================ *)

char_range = "'" CHAR "'" ".." "'" CHAR "'" ;
unicode_category = ( "\\p{" | "\\P{" ) CATEGORY_NAME "}" ;
unicode_codepoint = "\\u{" HEX_DIGIT+ "}" ;
special_sequence = "?" { ~'?' | '?' (?! '?') }* "?" ;

string_literal = '"' { ~'"' }* '"' | "'" { ~"'" }* "'" ;
version_literal = INTEGER ( "." INTEGER )* ;
literal = string_literal | INTEGER | "true" | "false" ;

identifier = ( \p{L} | '_' ) ( \p{L} | \p{Nd} | '_' )* ;
INTEGER = \p{Nd}+ ;
HEX_DIGIT = '0'..'9' | 'a'..'f' | 'A'..'F' ;
CHAR = ? any single Unicode character ? ;
CATEGORY_NAME = \p{L}+ ;

(* ============================================================
   Comments (inherited from ISO EBNF)
   ============================================================ *)

COMMENT = "(*" { ~"*)" }* "*)" ;
LINE_COMMENT = "//" { ~'\n' }* '\n' ;
```

**Note:** The `//` line comment is an XEBNF extension to ISO EBNF, included for practical convenience and widespread convention.

---

## Appendix A: Extension Summary

| Extension | Section | Syntax | Addresses |
|---|---|---|---|
| Character ranges | 4.1 | `'a'..'z'` | Character set specification |
| Unicode categories | 4.2 | `\p{Lu}` | Unicode-aware lexing |
| Unicode code points | 4.3 | `\u{FEFF}` | Specific character matching |
| Character negation | 4.4 | `~'x'` | Complement sets |
| Wildcard | 4.5 | `.` | Any character |
| One-or-more | 5.1 | `x+` | Quantification |
| Zero-or-one | 5.2 | `x?` | Quantification |
| Bounded repetition | 5.3 | `x{2,4}` | Quantification |
| Positive lookahead | 6.1 | `(?= ...)` | Disambiguation |
| Negative lookahead | 6.2 | `(?! ...)` | Contextual keywords |
| Lookbehind | 6.3–6.4 | `(?<= ...)`, `(?<! ...)` | Context-dependent tokens |
| Semantic predicates | 7.1 | `{? name ?}` | Context-sensitive parsing |
| Version predicates | 7.2 | `{? version >= N ?}` | Multi-version grammars |
| Annotations | 8 | `@[key: value]` | Metadata |
| Parameterized rules | 9 | `rule<p: type>` | Context variation |
| Precedence | 13 | `@[precedence: N]` | Expression parsing |
| Error productions | 14 | `@[error: "msg"]` | Diagnostics |

---

## Appendix B: Comparison with ANTLR Notation

For users converting between ANTLR and XEBNF:

| Concept | ANTLR | XEBNF |
|---|---|---|
| Production | `rule : alt1 \| alt2 ;` | `rule = alt1 \| alt2 ;` |
| Terminal string | `'if'` | `"if"` or `'if'` |
| Character range | `'a'..'z'` | `'a'..'z'` |
| Negation | `~[abc]` | `~('a' \| 'b' \| 'c')` |
| One-or-more | `x+` | `x+` |
| Zero-or-more | `x*` | `x*` or `{ x }` |
| Optional | `x?` | `x?` or `[ x ]` |
| Fragment rule | `fragment X` | `@[category: "fragment"]` |
| Semantic predicate | `{condition}?` | `{? condition ?}` |
| Lexer mode | `mode X;` | `@[context: "X"]` (via predicate) |
| Channel | `-> channel(HIDDEN)` | `@[channel: "hidden"]` |
| Label | `x=expr` | Not in grammar; tool concern |
| Action | `{code}` | Not supported; XEBNF is declarative |

---

## Appendix C: Conformance Checklist

A tool claiming to process XEBNF should state which level it supports:

- **XEBNF/Core**: Sections 3–5 (ISO EBNF + character classes + quantifiers).
- **XEBNF/Lookahead**: Core + Section 6 (lookaround).
- **XEBNF/Predicates**: Lookahead + Section 7 (semantic and version predicates).
- **XEBNF/Full**: All sections.

---

## Appendix D: Design Rationale

### Why not just use ANTLR notation?

ANTLR is a parser generator. Its notation makes choices (ordered alternatives, greedy quantifiers, embedded actions) that serve parser construction but conflate specification with implementation. XEBNF is descriptive: it says what a language accepts, not how to build a parser for it. This separation is essential for grammar-meta-standard's purpose — the same XEBNF grammar can inform an LLM code generator, a human reader, a migration tool, and (via conversion) a parser generator.

### Why strict ISO EBNF superset?

Because existing grammars (W3C specifications, ECMA standards, textbooks) should not need modification to be valid XEBNF. The migration cost must be zero for the base case.

### Why conformance levels?

Because demanding perfection prevents adoption. A Level 1 grammar for C# 14 that correctly covers 60% of the syntax is immediately useful for code generation in that 60%. Level 2 coverage arrives through iterative refinement. The level system makes the current state of knowledge explicit — epistemic discipline applied to grammar specification.

### Why version predicates rather than separate files only?

Both models are supported. Separate files are simpler; multi-version files are more compact and make diffs visible inline. The choice is pragmatic, not dogmatic.
