
# Dokumentation <!-- omit in toc -->

- [Installation](#installation)
- [Funktionen](#funktionen)
  - [`Conv.MathToLatex()`](#convmathtolatex)
  - [`Export.AsText()`](#exportastext)
  - [`Export.AsImage()` *(Coming Soon)*](#exportasimage-coming-soon)
  - [`Wrapper.PrettyPrint()`](#wrapperprettyprint)
  - [`Wrapper.PrintBrackets()`](#wrapperprintbrackets)
- [Objekte](#objekte)
  - [`TextFormats`](#textformats)
  - [`ImageFormats`](#imageformats)
  - [`WriteModes`](#writemodes)
  - [`BracketModes`](#bracketmodes)
- [Konfiguration](#konfiguration)
  - [Alle `Config`-Optionen](#alle-config-optionen)

## Installation

Die Datei [MaTeX.cs](https://github.com/Ifi-Softwareentwicklung-SoSe2022/SWE22_Projekt/blob/main/src/MaTeX/MaTeX.cs) im Projektordner speichern. Und mit `using MaTeX;` importieren.

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
// Objekte definieren
string S = "0 = 3*2-sqrt(x)";
Vector V = DenseVector.OfArray(new double[] {4,7,1});
Matrix M = DenseMatrix.OfArray(new double[,] {
    {1,5,0},
    {0,3,0},
    {4,0,1}
});

// LaTeX-Code genrerieren
string S_latex = Conv.MathToLatex(S);
string V_latex = Conv.MathToLatex(V);
string M_latex = Conv.MathToLatex(M);

// LaTeX-Code anzeigen
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

>Speichert LaTeX-Code in Form von `string`'s in unterschiedlichen Formaten aus `TextFormats` in der Datei `filename` ab. Dabei kann der Schreibmodus aus `WriteModes` gewählt werden. Zusätzlich können Klammermodi aus `BracketModes` für *Markdown* bzw. *LaTeX* Formate verwendet werden.

```cs
bool AsText(string latex, string filename, WriteModes writeMode, TextFormats textFormat, BracketModes[] bracketModes)
```

**Unterstützte Dateinamen:**

- `filename` darf nur den Dateinamen, keinen Dateipfad enthalten.
- Der Standard Dateipfad kann in `Config.SaveLocation` festgelegt werden.
- Wenn `filename` keine Dateiendung enthält, wird diese durch `textFormat` festgelegt:

| `TextFormats`              | Dateiendung |
| :------------------------- | :---------- |
| `TextFormats.TXT`          | `.txt`      |
| `TextFormats.MD`           | `.md`       |
| `TextFormats.TEX`          | `.tex`      |
| `TextFormats.TEX_DOCUMENT` | `.tex`      |

<br>

**Unterstützte Schreibmodi aus `WriteModes`:**

| Dateiformat        | `WriteModes`                             | Aktion                                                                               |
| :----------------- | :--------------------------------------- | :----------------------------------------------------------------------------------- |
| alle `TextFormats` | `WriteModes.OVERRIDE`                    | Datei mit `latex` wird erstellt bzw. ersetzt                                         |
| alle `TextFormats` | `WriteModes.APPEND`                      | `latex` wird an das Ende der Datei angehängt                                         |
| alle `TextFormats` | `WriteModes.AT_START`                    | `latex` wird am Anfang der Datei eingefügt                                           |
| `TextFormats.TEX`  | `WriteModes.INSERT_AFTER_DOCUMENT_START` | `latex` wird in der Datei nach dem ersten auftreten von `\begin{document}` eingefügt |
| `TextFormats.TEX`  | `WriteModes.INSERT_BEFORE_DOCUMENT_END`  | `latex` wird in der Datei vor dem zuletzt aufgetretendem `\end{document}` eingefügt  |

<br>

**Unterstützte Dateiformate aus `TextFormats`:**

| Dateiformat    | `TextFormats`              | Bsp.: Dateiinhalt                                                                                         |
| :------------- | :------------------------- | :-------------------------------------------------------------------------------------------------------- |
| Text           | `TextFormats.TXT`          | `0=6-\sqrt{x}`                                                                                            |
| Markdown       | `TextFormats.MD`           | `$0=6-\sqrt{x}$`                                                                                          |
| LaTeX          | `TextFormats.TEX`          | `\begin{equation*}0=6-\sqrt{x}\end{equation*}`                                                            |
| LaTeX Dokument | `TextFormats.TEX_DOCUMENT` | `\documentclass[10pt]{article}\begin{document}\begin{equation*}0=6-\sqrt{x}\end{equation*}\end{document}` |

<br>

**Unterstützte Klammermodi aus `BracketModes` für `TextFormats.TEX`, `TextFormats.TEX_DOCUMENT` und `TextFormats.MD`:**

| Klammermodus        | `BracketModes`                                              | Bsp.: Dateiinhalt für `TextFormats.TEX`        | Bsp.: Dateiinhalt für `TextFormats.MD` |
| :------------------ | :---------------------------------------------------------- | :--------------------------------------------- | :------------------------------------- |
| Keine Klammern      | `new BracketModes[] {}`                                     | `                 0=6-\sqrt{x}               ` | ` 0=6-\sqrt{x} `                       |
| Öffnende Klammer    | `new BracketModes[] {BracketModes.BEGIN}`                   | `\begin{equation*}0=6-\sqrt{x}               ` | `$0=6-\sqrt{x} `                       |
| Schließende Klammer | `new BracketModes[] {BracketModes.END}`                     | `                 0=6-\sqrt{x}\end{equation*}` | ` 0=6-\sqrt{x}$`                       |
| Beide Klammern      | `new BracketModes[] {BracketModes.BEGIN, BracketModes.END}` | `\begin{equation*}0=6-\sqrt{x}\end{equation*}` | `$0=6-\sqrt{x}$`                       |

**Beispiel:**

>Objekte `S`, `V`, `M` und deren LaTeX-Code `S_latex`, `V_latex`, `M_latex` aus dem [MathToLatex](#convmathtolatex)-Beispiel verwenden

```cs
string FileName = "Beispiel_ExportAsText";

// Export (I)
Export.AsText(
    S_latex,
    FileName,
    WriteModes.OVERRIDE, // (Default)
    TextFormats.TEX_DOCUMENT,
    new BracketModes[] {BracketModes.BEGIN, BracketModes.END} // (Default)
);

// Export (II)
Export.AsText(
    M_latex + @"\cdot",
    FileName,
    WriteModes.INSERT_BEFORE_DOCUMENT_END,
    TextFormats.TEX, // (Default)
    new BracketModes[] {BracketModes.BEGIN}
);

// Export (III)
Export.AsText(
    V_latex,
    FileName,
    WriteModes.INSERT_BEFORE_DOCUMENT_END,
    TextFormats.TEX, // (Default)
    new BracketModes[] {BracketModes.END}
);
```

```cs
// oder mit Export (II) und (III) kombiniert
Export.AsText(
    M_latex + @"\cdot" + V_latex,
    FileName,
    WriteModes.INSERT_BEFORE_DOCUMENT_END,
    TextFormats.TEX, // (Default)
    new BracketModes[] {BracketModes.BEGIN, BracketModes.END} // (Default)
);
```

Mit `Default` markierte Parameter sind optional und können über die jeweilige [`Config`-Option](#alle-config-optionen) global konfiguriert werden.

**Ausgabe:** *[Beispiel_ExportAsText.tex](Beispiel_ExportAsText.tex)*

```latex
\documentclass[10pt]{article}   % Export (I)
                                %  |
\begin{document}                %  |
                                %  |
\begin{equation*}               %  |
0 = 6 - \sqrt{x}                %  |
\end{equation*}                 %  |

\begin{equation*}               % Export (II)
\begin{bmatrix}                 %  |
1 & 5 & 0 \\                    %  |
0 & 3 & 0 \\                    %  |
4 & 0 & 1                       %  |
\end{bmatrix}\cdot              %  |

\begin{pmatrix}                 % Export (III)
4 \\                            %  |
7 \\                            %  |
1                               %  |
\end{pmatrix}                   %  |
\end{equation*}                 %  |

\end{document}                  % Export (I)
```

### `Export.AsImage()` *(Coming Soon)*

>Rendert LaTeX-Code in Form von `string`'s und speichert es als Bild.

```cs
bool AsImage(string latex, string filename, ImageFormats imageFormat)
```

**Unterstützte Dateinamen:**

- `filename` darf nur den Dateinamen, keinen Dateipfad enthalten.
- Der Standard Dateipfad kann in `Config.SaveLocation` festgelegt werden.
- Die Dateiendung wird durch `imageFormat` festgelegt:

| `ImageFormats`      | Dateiendung |
| :------------------ | :---------- |
| `ImageFormats.JPG`  | `.jpg`      |
| `ImageFormats.JEPG` | `.jepg`     |
| `ImageFormats.BMP`  | `.bmp`      |
| `ImageFormats.PNG`  | `.png`      |
| `ImageFormats.GIF`  | `.gif`      |
| `ImageFormats.SVG`  | `.svg`      |

### `Wrapper.PrettyPrint()`

>Gibt `text` zurück, wenn `Config.PrettyPrinting` mit `true` konfiguriert ist, sonst `alternative`.

```cs
string PrettyPrint(string text, string alternative)
```

### `Wrapper.PrintBrackets()`

>Gibt `bracketText` zurück, wenn der Klammermodus `currentMode` im Array der akzeptierten Klammermodi `compareModes` enthalten ist, sont `alternative`.

```cs
string PrintBrackets(string bracketText, string alternative, BracketModes currentMode, BracketModes[] compareModes)
```

## Objekte

### `TextFormats`

```cs
enum TextFormats { TXT, MD, TEX, TEX_DOCUMENT }
```

### `ImageFormats`

```cs
enum ImageFormats { JPG, JPEG, BMP, PNG, GIF, SVG }
```

### `WriteModes`

```cs
enum WriteModes { OVERRIDE, APPEND, AT_START, INSERT_AFTER_DOCUMENT_START, INSERT_BEFORE_DOCUMENT_END }
```

### `BracketModes`

```cs
enum BracketModes { BEGIN, END }
```

## Konfiguration

In der `Config`-Klasse können einheitlich Formate, Modi und Eigenschaften etc. konfiguriert werden. Diese Werte werden zum Beispiel zur Vervollständigung von Überladenen Funktionen sowie als Bedingung in `Wrapper`-Funktionen verwendet. Sie können aber auch einfach abgefragt oder geändert werden:

```cs
Config.SaveLocation = Path.GetFullPath("dein/neuer/Speicherpfad");
Config.PrettyPrinting = false;
Config.TextFormat = TextFormats.MD;
...
```

**Vervollständigung von Überladenen Funktionen:**

Hier wird eine Überladung der [`Export.AsText()`](#exportastext)-Funktion mit weniger Parametern verwendet, nämlich:

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

Die in dieser Überladung 'fehlenden Paramter' sind `writeMode` und `bracketModes`. Beim Aufruf der 'vollständigen' `Export.AsText()`-Funktion werden für diese fehlenden Parameter die konfigurierten Werte `Config.WriteMode` und `Config.BracketModes` eingesetzt.

### Alle `Config`-Optionen

| Attribut               | Datentyp       | Standardwert                                                | Bsp. Verwendung                                                                                                                                  |
| :--------------------- | :------------- | :---------------------------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------- |
| `PrettyPrinting`       | `bool`         | `true`                                                      | Fügt beim generieren von LaTeX-Code mit `Conv.MathToLatex()` oder beim Schreiben mit `Export.AsText()` Leerzeichen- und zeilen ein, wenn `true`. |
| `IgnoreFileExceptions` | `bool`         | `false`                                                     | `Export.AsText()` wirft keine `Exception` wenn bei Lese- oder Schreiboperationen ein Fehler auftritt, sondern gibt `false` zurück, wenn `true`.  |
| `TextFormat`           | `TextFormats`  | `TextFormats.TEX`                                           | Legt das Standard Dateiformat für `Export.AsText()` fest und die Dateiendung, wenn diese nicht schon durch `filename` gesetzt wurde.             |
| `ImageFormat`          | `ImageFormats` | `ImageFormats.JPG`                                          | Legt das Standard Dateiformat für `Export.AsImage()` fest.                                                                                       |
| `WriteMode`            | `WriteModes`   | `WriteModes.OVERRIDE`                                       | Legt den Standard Schreibmodus für `Export.AsText()` fest.                                                                                       |
| `BracketMode`          | `BracketModes` | `new BracketModes[] {BracketModes.BEGIN, BracketModes.END}` | Legt die Standard Klammermodi für `Export.AsText()` fest.                                                                                        | 
| `LatexHeader`          | `string`       | `@"\documentclass[10pt]{article}"`                          | Legt den Standard LaTeX-Header fest und wird bei `Export.AsText()` mit `TextFormats.TEX_DOCUMENT` vor `\document{begin}` geschrieben.            |
| `SaveLocation`         | `string`       | `System.IO.Directory.GetCurrentDirectory()`                 | Legt den Standard Ordnerpfad für die mit `Export.AsText()` erstellten bzw. modifizierten Dateien fest.                                           |
