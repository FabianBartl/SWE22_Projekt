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
        static public String LatexHeader = @"\documentclass[10pt]{article}"; 
        static public String DefaultSaveLocation = Path.Combine(Path.GetTempPath(), "MaTeX");
    }

    // Enum's u.a. zur Config-Optionsauswahl
    public enum TextFormats { TXT, MD, TEX, /* erstellt zusätzlich LaTex header */ TEX_WITH_HEADER };
    public enum ImageFormats { JPG, JPEG, BMP, PNG, GIF, SVG };
    public enum WriteModes { OVERRIDE, APPEND, AT_START, /* nur für Latex Dateien: */ INSERT_AT_DOCUMENT_START, INSERT_BEFORE_DOCUMENT_END };

    // Wrapper Funktionen für z.B. Config-Optionen
    static public class Wrapper
    {
        static public String PrettyPrint(String text) { return PrettyPrint(text, ""); }
        static public String NotPrettyPrint(String text) { return PrettyPrint("", text); }
        static public String PrettyPrint(String text, String alternative) { return Config.PrettyPrinting ? text : alternative; }
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
                    _text = String.Format("{0}{1}{2}",
                        (format == TextFormats.TEX_WITH_HEADER) ? (
                            Config.LatexHeader
                            + Wrapper.PrettyPrint("\n")
                            + @"\begin{document}"
                            + Wrapper.PrettyPrint("\n")
                        ) : "",
                        (
                            @"\begin{equation*}"
                            + Wrapper.PrettyPrint("\n")
                            + latex
                            + Wrapper.PrettyPrint("\n")
                            + @"\end{equation*}"
                            + Wrapper.PrettyPrint("\n")
                        ),
                        (format == TextFormats.TEX_WITH_HEADER) ? (
                            @"\end{document}"
                            + Wrapper.PrettyPrint("\n")
                        ) : ""
                    );
                    break;
                case TextFormats.MD:
                    _text = String.Format("{0}{1}{0}",
                        Wrapper.PrettyPrint("\n$$\n", "$"),
                        latex
                    );
                    break;
                case TextFormats.TXT:
                    _text = latex;
                    break;
            }

            // Datei speichern
            String _path = Path.GetFullPath(Path.Combine(Config.DefaultSaveLocation, filename));
            return WriteFile(_path, _text);
        }
        static public bool AsText(String latex, String filename, WriteModes mode)
        {
            throw new NotImplementedException();
        }
        static public bool AsText(String latex, String filename, WriteModes mode, TextFormats format)
        {
            throw new NotImplementedException();
        }

        // Als Bild exportieren
        static public bool AsImage(String latex, String filename, ImageFormats fmt=ImageFormats.JPG)
        {
            throw new NotImplementedException();
        }
    }
}
