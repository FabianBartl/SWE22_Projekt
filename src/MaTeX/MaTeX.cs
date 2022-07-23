using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using MathNet.Numerics.LinearAlgebra.Double;
using Expr = MathNet.Symbolics.SymbolicExpression;

using Microsoft.CodeAnalysis.CSharp.Scripting;


namespace MaTeX
{
    // globale Config's
    static public class Config
    {
        static public bool PrettyPrinting = false;
        static public TextFormats TextFormat = TextFormats.MD;
        static public ImageFormats ImageFormat = ImageFormats.JPG;
        static public WriteModes WriteMode = WriteModes.OVERRIDE;
        static public BracketModes[] BracketMode = new BracketModes[] {BracketModes.BEGIN, BracketModes.END};
        static public String LatexHeader = @"\documentclass[10pt]{article}"; 
        static public String SaveLocation = Directory.GetCurrentDirectory();
    }

    // Enum's u.a. zur Config-Optionsauswahl
    public enum TextFormats { TXT, MD, TEX, /* erstellt zusätzlich LaTex header */ TEX_WITH_HEADER };
    public enum ImageFormats { JPG, JPEG, BMP, PNG, GIF, SVG };
    public enum WriteModes { OVERRIDE, APPEND, AT_START, /* nur bei TextFormats.TEX */ INSERT_AFTER_DOCUMENT_START, INSERT_BEFORE_DOCUMENT_END };
    public enum BracketModes { BEGIN, END };

    // Wrapper Funktionen für z.B. Config-Optionen
    static public class Wrapper
    {
        // Wrapper für PrettyPrinting-Option
        static public String PrettyPrint(String text) { return PrettyPrint(text, ""); }
        static public String NotPrettyPrint(String text) { return PrettyPrint("", text); }
        static public String PrettyPrint(String text, String alternative) { return Config.PrettyPrinting ? text : alternative; }

        // Wrapper für BracketMode-Option
        static public String PrintBrackets(String bracketText, BracketModes currentMode) { return PrintBrackets(bracketText, "", currentMode, Config.BracketMode); }
        static public String PrintBrackets(String bracketText, BracketModes currentMode, BracketModes compareMode) { return PrintBrackets(bracketText, "", currentMode, new BracketModes[] {compareMode}); }
        static public String PrintBrackets(String bracketText, BracketModes currentMode, BracketModes[] compareModes) { return PrintBrackets(bracketText, "", currentMode, compareModes); }
        static public String PrintBrackets(String bracketText, String alternative, BracketModes currentMode, BracketModes[] compareModes)
        {
            for (int i=0; i < compareModes.Length; i++)
                if (compareModes[i] == currentMode) return bracketText;
            return alternative;
        }
    }

    // Converter Funktionen
    // -> "Conv", damit es keine Konflikte mit "Convert" aus "System.Convert" gibt
    static public class Conv
    {
        // Vector -> Latex
        static public String MathToLatex(Vector vector)
        {                                                            
            String _latex = @"\begin{pmatrix}" + Wrapper.PrettyPrint("\n");
            // Zeilen des Vektors auflösen
            for (int _row=0; _row < vector.Count; _row++)
            {
                _latex += Convert.ToString(vector[_row])
                    + Wrapper.PrettyPrint(" ")
                    + (_row != vector.Count-1 ? @"\\" : "")
                    + Wrapper.PrettyPrint("\n");
            }
            return _latex + @"\end{pmatrix}";
        }

        // Matrix -> Latex
        static public String MathToLatex(Matrix matrix)
        {
            String _latex = @"\begin{bmatrix}" + Wrapper.PrettyPrint("\n");
            // Zeilen der Matrix auflösen
            for (int _row=0, _col=0; _row < matrix.RowCount; _row++)                           
            {
                // Spalten der Matrix auflösen
                for (_col=0; _col < matrix.ColumnCount-1; _col++)
                {
                    _latex += Convert.ToString(matrix[_row,_col])
                        + Wrapper.PrettyPrint(" ")
                        + "&"
                        + Wrapper.PrettyPrint(" ");
                }
                _latex += Convert.ToString(matrix[_row,_col])
                    + Wrapper.PrettyPrint(" ")
                    + (_row != matrix.RowCount-1 ? @"\\" : "")
                    + Wrapper.PrettyPrint("\n");
            }
            return _latex + @"\end{bmatrix}";
        }

        // Term, Gleichung -> Latex
        static public String MathToLatex(String text)
        {
            String _latex = "";
            // Gleichungen rekursiv auflösen
            if (text.Contains("="))
            {
                List<String> _equations = new List<String>(text.Split("="));
                for (int i=0; i < _equations.Count; i++)
                {
                    _latex += MathToLatex(_equations[i])
                        + (i != _equations.Count-1 ? Wrapper.PrettyPrint(" ")
                        + "="
                        + Wrapper.PrettyPrint(" ") : "");
                }
                return _latex;
            }
            // Ausdruck in Latex umwandeln
            _latex = Expr.Parse(text).ToLaTeX();
            return Config.PrettyPrinting ? _latex : _latex.Replace(" ", "");
        }
    }

    // Funktionen zum Exportieren des Latex als Text oder Bild
    static public class Export
    {
        // Private Wrapper-Funktionen für Dateioperationen
        // Datei schreiben
        static private bool WriteFile(String file, String buffer)
        {
            String _path = Path.GetFullPath(file);
            using (FileStream _fileStream = File.Create(_path))
            {
                Byte[] _byteCode = new UTF8Encoding(true).GetBytes(buffer);
                _fileStream.Write(_byteCode, 0, _byteCode.Length);
                return true;
            }
        }

        static private bool WriteFile(String file, String buffer, bool ignoreExceptions)
        {
            try
            {
                return WriteFile(file, buffer); //immer "true"
            }
            catch (Exception _exc)
            {
                if (!ignoreExceptions) throw _exc;
                return false;
            }
        }

        // Datei lesen
        static private bool ReadFile(String file, out String buffer)
        {
            String _path = Path.GetFullPath(file);
            using (StreamReader _streamReader = File.OpenText(_path))
            {
                String _line; buffer = "";
                while ((_line = _streamReader.ReadLine()) != null) buffer += _line + "\n";
                return true;
            }
        } 
        static private bool ReadFile(String file, out String buffer, bool ignoreExceptions)
        {
            try
            {
                return ReadFile(file, out buffer); //immer "true"
            }
            catch (Exception _exc)
            {
                if (!ignoreExceptions) throw _exc;
                buffer = null;
                return false;
            }
        }

        // Als Text exportieren
        static public bool AsText(String latex, String filename) { return AsText(latex, filename, Config.WriteMode, Config.TextFormat, Config.BracketMode); }
        static public bool AsText(String latex, String filename, TextFormats textFormat) { return AsText(latex, filename, Config.WriteMode, textFormat, Config.BracketMode); }
        static public bool AsText(String latex, String filename, WriteModes writeMode) { return AsText(latex, filename, writeMode, Config.TextFormat, Config.BracketMode); }
        static public bool AsText(String latex, String filename, BracketModes bracketMode) { return AsText(latex, filename, Config.WriteMode, Config.TextFormat, new BracketModes[] {bracketMode}); }
        static public bool AsText(String latex, String filename, BracketModes[] bracketModes) { return AsText(latex, filename, Config.WriteMode, Config.TextFormat, bracketModes); }
        static public bool AsText(String latex, String filename, WriteModes writeMode, TextFormats textFormat) { return AsText(latex, filename, writeMode, textFormat, Config.BracketMode); }
        static public bool AsText(String latex, String filename, WriteModes writeMode, TextFormats textFormat, BracketModes bracketMode) { return AsText(latex, filename, writeMode, textFormat, new BracketModes[] {bracketMode}); }
        static public bool AsText(String latex, String filename, WriteModes writeMode, TextFormats textFormat, BracketModes[] bracketModes)
        {
            // Dateiendung wählen
            if (!filename.Contains("."))
            {
                switch (textFormat)
                {
                    case TextFormats.TEX:
                    case TextFormats.TEX_WITH_HEADER:
                        filename += ".tex";
                        break;
                    case TextFormats.MD:
                        filename += ".md";
                        break;
                    case TextFormats.TXT:
                        filename += ".txt";
                        break;
                }
            }

            // Text zusammenstellen
            String _text = "";
            switch (textFormat)
            {
                case TextFormats.TEX:
                case TextFormats.TEX_WITH_HEADER:
                    _text = String.Format("{0}{1}{2}{3}",
                        (textFormat == TextFormats.TEX_WITH_HEADER) ? (
                            Config.LatexHeader
                                + Wrapper.PrettyPrint("\n")
                                + @"\begin{document}"
                                + Wrapper.PrettyPrint("\n")
                        ) : "",
                        (
                            Wrapper.PrintBrackets(@"\begin{equation*}" + Wrapper.PrettyPrint("\n"), BracketModes.BEGIN, bracketModes)
                                + latex
                                + Wrapper.PrettyPrint("\n")
                                + Wrapper.PrintBrackets(@"\end{equation*}" + Wrapper.PrettyPrint("\n"), BracketModes.END, bracketModes)
                        ),
                        (textFormat == TextFormats.TEX_WITH_HEADER) ? (
                            @"\end{document}"
                                + Wrapper.PrettyPrint("\n")
                        ) : "",
                        (
                            Wrapper.PrettyPrint("\n", " ")
                        )
                    );
                    break;
                case TextFormats.MD:
                    _text = String.Format("{0}",
                        Wrapper.PrintBrackets("\n$$\n", "$" + Wrapper.PrettyPrint("\n"), BracketModes.BEGIN, bracketModes)
                            + latex
                            + Wrapper.PrintBrackets("\n$$\n", "$" + Wrapper.PrettyPrint("\n"), BracketModes.END, bracketModes)
                            + Wrapper.PrettyPrint("\n", " ")
                    );
                    break;
                case TextFormats.TXT:
                    _text = latex + Wrapper.PrettyPrint("\n", " ");
                    break;
            }

            // Datei speichern
            String _content, _path = Path.GetFullPath(Path.Combine(Config.SaveLocation, filename));
            switch (writeMode)
            {
                case WriteModes.OVERRIDE:
                    if (!WriteFile(_path, _text)) return false;
                    break;
                case WriteModes.APPEND:
                    if (!ReadFile(_path, out _content)) return false;
                    if (!WriteFile(_path, _content + _text)) return false;
                    break;
                case WriteModes.AT_START:
                    if (!ReadFile(_path, out _content)) return false;
                    if (!WriteFile(_path, _text + _content)) return false;
                    break;
                case WriteModes.INSERT_BEFORE_DOCUMENT_END:
                case WriteModes.INSERT_AFTER_DOCUMENT_START:
                    if (textFormat == TextFormats.TEX)
                    {
                        if (!ReadFile(_path, out _content)) return false;
                        String _beginStr = @"\begin{document}", _endStr = @"\end{document}";
                        int _beginInd = _content.IndexOf(_beginStr), _endInd = _content.LastIndexOf(_endStr);
                        switch (_beginInd)
                        {
                            case -1:
                                return false;
                            case 0:
                                if (!WriteFile(_path, _text)) return false;
                                break;
                        }
                        switch (writeMode)
                        {
                            case WriteModes.INSERT_BEFORE_DOCUMENT_END:
                                if (!WriteFile(
                                    _path,
                                    _content.Substring(0, _endInd - 1)
                                        + _text
                                        + _content.Substring(_endInd)
                                )) return false;
                                break;
                            case WriteModes.INSERT_AFTER_DOCUMENT_START:
                                if (!WriteFile(
                                    _path,
                                    _content.Substring(0, _beginInd + _beginStr.Length)
                                        + _text
                                        + _content.Substring(_beginInd + _beginStr.Length + 1)
                                )) return false;
                                break;
                        }
                    }
                    break;
            }
            return true;
        }

        // Als Bild exportieren
        static public bool AsImage(String latex, String filename) { return AsImage(latex, filename, ImageFormats.JPG); }
        static public bool AsImage(String latex, String filename, ImageFormats imageFormat)
        {
            throw new NotImplementedException();
        }
    }
}
