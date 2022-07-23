using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using MathNet.Numerics.LinearAlgebra.Double;
using Expr = MathNet.Symbolics.SymbolicExpression;

using CSharpMath;

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
        static public BracketModes BracketMode = BracketModes.BOTH;
        static public String LatexHeader = @"\documentclass[10pt]{article}"; 
        static public String SaveLocation = Directory.GetCurrentDirectory();
    }

    // Enum's u.a. zur Config-Optionsauswahl
    public enum TextFormats { TXT, MD, TEX, /* erstellt zusätzlich LaTex header */ TEX_WITH_HEADER };
    public enum ImageFormats { JPG, JPEG, BMP, PNG, GIF, SVG };
    public enum WriteModes { OVERRIDE, APPEND, AT_START, /* nur bei TextFormats.TEX_WITH_HEADER */ INSERT_AFTER_DOCUMENT_START, INSERT_BEFORE_DOCUMENT_END };
    public enum BracketModes { START, END, BOTH, NONE };

    // Wrapper Funktionen für z.B. Config-Optionen
    static public class Wrapper
    {
        // Wrapper für PrettyPrinting-Option
        static public String PrettyPrint(String text) { return PrettyPrint(text, ""); }
        static public String NotPrettyPrint(String text) { return PrettyPrint("", text); }
        static public String PrettyPrint(String text, String alternative) { return Config.PrettyPrinting ? text : alternative; }

        // Wrapper für BracketMode-Option
        static public String PrintBrackets(String bracketText, BracketModes mode) { return PrintBrackets(bracketText, "", new BracketModes[] {mode}); }
        static public String NotPrintBrackets(String bracketText, BracketModes mode) { return NotPrintBrackets(bracketText, "", new BracketModes[] {mode}); }
        static public String PrintBrackets(String bracketText, BracketModes mode, BracketModes mode2) { return PrintBrackets(bracketText, "", new BracketModes[] {mode, mode2}); }
        static public String NotPrintBrackets(String bracketText, BracketModes mode, BracketModes mode2) { return NotPrintBrackets(bracketText, "", new BracketModes[] {mode, mode2}); }
        static public String PrintBrackets(String bracketText, String alternative, BracketModes[] mode)
        {
            for (int i=0; i < mode.Length; i++)
                if (mode[i] == Config.BracketMode) return bracketText;
            return alternative;
        }
        static public String NotPrintBrackets(String bracketText, String alternative, BracketModes[] mode)
        {
            for (int i=0; i < mode.Length; i++)
                if (mode[i] != Config.BracketMode) return bracketText;
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
                _latex += Convert.ToString(vector[_row]);
                _latex += Wrapper.PrettyPrint(" ") + (_row != vector.Count-1 ? @"\\" : "") + Wrapper.PrettyPrint("\n");
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
                    _latex += Convert.ToString(matrix[_row,_col]);
                    _latex += Wrapper.PrettyPrint(" ") + "&" + Wrapper.PrettyPrint(" ");
                }
                _latex += Convert.ToString(matrix[_row,_col]);
                _latex += Wrapper.PrettyPrint(" ") + (_row != matrix.RowCount-1 ? @"\\" : "") + Wrapper.PrettyPrint("\n");
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
                    _latex += MathToLatex(_equations[i]);
                    _latex += (i != _equations.Count-1 ? Wrapper.PrettyPrint(" ") + "=" + Wrapper.PrettyPrint(" ") : "");
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
        static public bool AsText(String latex, String filename)
        {
            throw new NotImplementedException();
        }
        static public bool AsText(String latex, String filename, TextFormats format)
        {
            throw new NotImplementedException();
        }
        static public bool AsText(String latex, String filename, WriteModes mode)
        {
            throw new NotImplementedException();
        }
        static public bool AsText(String latex, String filename, WriteModes mode, TextFormats format)
        {
            // Dateiendung wählen
            if (!filename.Contains("."))
            {
                switch (format)
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
            switch (format)
            {
                case TextFormats.TEX:
                case TextFormats.TEX_WITH_HEADER:
                    _text = String.Format("{0}{1}{2}{3}",
                        (format == TextFormats.TEX_WITH_HEADER) ? (
                            Config.LatexHeader
                            + Wrapper.PrettyPrint("\n")
                            + @"\begin{document}"
                            + Wrapper.PrettyPrint("\n")
                        ) : "",
                        (
                            Wrapper.NotPrintBrackets(@"\begin{equation*}" + Wrapper.PrettyPrint("\n"), BracketModes.END, BracketModes.NONE)
                            + latex
                            + Wrapper.PrettyPrint("\n")
                            + Wrapper.NotPrintBrackets(@"\end{equation*}" + Wrapper.PrettyPrint("\n"), BracketModes.START, BracketModes.NONE)
                        ),
                        (format == TextFormats.TEX_WITH_HEADER) ? (
                            @"\end{document}"
                            + Wrapper.PrettyPrint("\n")
                        ) : "",
                        (
                            Wrapper.PrettyPrint("\n", " ")
                        )
                    );
                    break;
                case TextFormats.MD:
                    _text = String.Format("{0}{1}{2}{3}",
                        Wrapper.NotPrintBrackets(Wrapper.PrettyPrint("\n$$\n", "$"), BracketModes.END, BracketModes.NONE),
                        latex,
                        Wrapper.NotPrintBrackets(Wrapper.PrettyPrint("\n$$\n", "$"), BracketModes.START, BracketModes.NONE),
                        Wrapper.PrettyPrint("\n", " ")
                    );
                    break;
                case TextFormats.TXT:
                    _text = latex + Wrapper.PrettyPrint("\n", " ");
                    break;
            }

            // Datei speichern
            String _content, _path = Path.GetFullPath(Path.Combine(Config.SaveLocation, filename));
            switch (mode)
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
                    if (format == TextFormats.TEX_WITH_HEADER)
                    {
                        if (!ReadFile(_path, out _content)) return false;
                        String _substr = (mode == WriteModes.INSERT_BEFORE_DOCUMENT_END) ? @"\begin{document}" : @"\end{document}";
                        int _ind = _content.IndexOf(_substr);
                        switch (_ind)
                        {
                            case -1:
                                return false;
                            case 0:
                                if (!WriteFile(_path, _text)) return false;
                                break;
                        }
                        if (!WriteFile(
                            _path,
                            _content.Substring(0, _ind + _substr.Length) + _text + _content.Substring(_ind + _substr.Length + 1)
                        )) return false;
                    }
                    break;
            }
            return true;
        }

        // Als Bild exportieren
        static public bool AsImage(String latex, String filename, ImageFormats fmt=ImageFormats.JPG)
        {
            throw new NotImplementedException();
        }
    }
}
