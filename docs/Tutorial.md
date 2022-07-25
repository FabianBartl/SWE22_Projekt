
# Dokumentation

- [Dokumentation](#dokumentation)
  - [Installation](#installation)
  - [Funktionen](#funktionen)
    - [`Conv.MathToLatex()`](#convmathtolatex)
    - [`Export.AsText()`](#exportastext)
    - [`Wrapper.PrettyPrint()`](#wrapperprettyprint)
    - [`Wrapper.PrintBrackets()`](#wrapperprintbrackets)
  - [Objekte](#objekte)
    - [`TextFormats`](#textformats)
    - [`WriteModes`](#writemodes)
    - [`BracketModes`](#bracketmodes)
  - [`Config`-Optionen](#config-optionen)

## Installation

Die Datei [MaTeX.cs](../src/MaTeX/MaTeX.cs) im Projektordner speichern. Und mit `using MaTeX;` importieren.

## Funktionen

### `Conv.MathToLatex()`

>Wandelt *Terme* bzw. *Gleichungen* in Form von `string`'s sowie verschiedene C#-Objekte aus `MathNet.Numerics.LinearAlgebra` in LaTeX-Schreibweise um.

```cs
string MathToLatex(object obj)
```

**Unterstützte *Terme* bzw. *Gleichungen*:**

- mehrere `=`-Zeichen
- Operationen: `+`, `-`, `*`, `/`, `^`
- mathematische Funktionen wie z.B.: `sin()`, `cos()`, `tan()`, ... , `arcsin()`, ... , `sqrt()`, `exp()`
<!-- -->
- z.B. nicht Unterstützt: `f(x)`

**Unterstützte `MathNet.Numerics.LinearAlgebra`-Objekte:**

- `Matrix`
- `Vector`

**Beispiel:**

```cs
string S = "0 = 3*2-sqrt(x)";
Vector V = DenseVector.OfArray(new double[] {4,7,1});
Matrix M = DenseMatrix.OfArray(new double[,] {
    {1,5,0},
    {0,3,0},
    {4,0,1}
});

string S_latex = Conv.MathToLatex(S);
string V_latex = Conv.MathToLatex(V);
string M_latex = Conv.MathToLatex(M);

Console.WriteLine(S_latex + "\n");
Console.WriteLine(V_latex + "\n");
Console.WriteLine(M_latex + "\n");
```

**Ausgabe:**

```latex
0 = 6 - \sqrt{x}

\begin{pmatrix}
4 \\
7 \\
1 
\end{pmatrix}

\begin{bmatrix}
1 & 5 & 0 \\
0 & 3 & 0 \\
4 & 0 & 1 
\end{bmatrix}
```

### `Export.AsText()`

>Speichert LaTeX-Code in Form von `string`'s in unterschiedlichen Dateiformaten aus `TextFormats` in der Datei `filename` ab. Dabei kann der Schreibmodus aus `WriteModes` gewählt werden. Zusätzlich können Klammermodi aus `BracketModes` für *Markdown* bzw. *LaTeX* Formate verwendet werden.

```cs
bool AsText(string latex, string filename, WriteModes writeMode, TextFormats textFormat, BracketModes[] bracketModes)
```

**Unterstützte Schreibmodi aus `WriteModes`:**

| Dateiformat        | `WriteModes`                             | Aktion                                                                               |
| ------------------ | ---------------------------------------- | ------------------------------------------------------------------------------------ |
| alle `TextFormats` | `WriteModes.OVERRIDE`                    | Datei mit `latex` wird erstellt bzw. ersetzt                                         |
| alle `TextFormats` | `WriteModes.APPEND`                      | `latex` wird an das Ende der Datei angehängt                                         |
| alle `TextFormats` | `WriteModes.AT_START`                    | `latex` wird am Anfang der Datei eingefügt                                           |
| `TextFormats.TEX`  | `WriteModes.INSERT_AFTER_DOCUMENT_START` | `latex` wird in der Datei nach dem ersten auftreten von `\begin{document}` eingefügt |
| `TextFormats.TEX`  | `WriteModes.INSERT_BEFORE_DOCUMENT_END`  | `latex` wird in der Datei vor dem zuletzt aufgetretenem `\end{document}` eingefügt   |

<br>

**Unterstützte Dateiformate aus `TextFormats`:**

| Dateiformat    | `TextFormats`              | Bsp.: Dateiinhalt                                                                                         |
| -------------- | -------------------------- | --------------------------------------------------------------------------------------------------------- |
| Text           | `TextFormats.TXT`          | `0=6-\sqrt{x}`                                                                                            |
| Markdown       | `TextFormats.MD`           | `$0=6-\sqrt{x}$`                                                                                          |
| LaTeX          | `TextFormats.TEX`          | `\begin{equation*}0=6-\sqrt{x}\end{equation*}`                                                            |
| LaTeX Dokument | `TextFormats.TEX_DOCUMENT` | `\documentclass[10pt]{article}\begin{document}\begin{equation*}0=6-\sqrt{x}\end{equation*}\end{document}` |

<br>

**Unterstützte Klammermodi aus `BracketModes` für `TextFormats.TEX`, `TextFormats.TEX_DOCUMENT` und `TextFormats.MD`:**

| Klammermodus        | `BracketModes`                                              | Bsp.: Dateiinhalt für `TextFormats.TEX`        | Bsp.: Dateiinhalt für `TextFormats.MD` |
| ------------------- | ----------------------------------------------------------- | ---------------------------------------------- | -------------------------------------- |
| Keine Klammern      | `new BracketModes[] {}`                                     | `                 0=6-\sqrt{x}               ` | ` 0=6-\sqrt{x} `                       |
| Öffnende Klammer    | `new BracketModes[] {BracketModes.BEGIN}`                   | `\begin{equation*}0=6-\sqrt{x}               ` | `$0=6-\sqrt{x} `                       |
| Schließende Klammer | `new BracketModes[] {BracketModes.END}`                     | `                 0=6-\sqrt{x}\end{equation*}` | ` 0=6-\sqrt{x}$`                       |
| Beide Klammern      | `new BracketModes[] {BracketModes.BEGIN, BracketModes.END}` | `\begin{equation*}0=6-\sqrt{x}\end{equation*}` | `$0=6-\sqrt{x}$`                       |

### `Wrapper.PrettyPrint()`

>Gibt `text` aus, wenn `Config.PrettyPrinting` mit `true` konfiguriert ist, sonst `alternative`.

```cs
string PrettyPrint(string text, string alternative)
```

### `Wrapper.PrintBrackets()`

>Gibt `bracketText` aus, wenn der Klammermodus `currentMode` im Array der akzeptierten Klammermodi `compareModes` enthalten ist, sont `alternative`.

```cs
string PrintBrackets(string bracketText, string alternative, BracketModes currentMode, BracketModes[] compareModes)
```

## Objekte

### `TextFormats`

```cs
enum TextFormats { TXT, MD, TEX, TEX_DOCUMENT }
```

### `WriteModes`

```cs
enum WriteModes { OVERRIDE, APPEND, AT_START, INSERT_AFTER_DOCUMENT_START, INSERT_BEFORE_DOCUMENT_END }
```

### `BracketModes`

```cs
enum BracketModes { BEGIN, END }
```

## `Config`-Optionen

In der `Config`-Klasse können einheitlich Formate, Modi und Eigenschaften etc. konfiguriert werden. Diese Werte werden zum Beispiel zur Vervollständigung fehlender Parameter in Kurzformen von verschiedenen Funktionen sowie als Bedingung in `Wrapper`-Funktionen verwendet. Sie können aber auch einfach mit `Config.[OPTION]` abgefragt oder geändert werden.

**Vervollständigung fehlender Parameter:**

Hier wird eine definierte Kurzform der `Export.AsText()`-Funktion verwendet, nämlich:

```cs
bool AsText(string latex, string filename, TextFormats textFormat)
```

```cs
Export.AsText(
    Conv.MathToLatex("0=6-\sqrt{x}"),
    "ergebnis",
    TextFormats.TEX_DOCUMENT
);
```

Die in dieser Kurzform fehlenden Paramter sind `writeMode` und `bracketModes`. Beim Aufruf der vollständigen `Export.AsText()`-Funktion werden für diese fehlenden Parameter die konfigurierten Werte `Config.WriteMode` und `Config.BracketModes` eingesetzt.
