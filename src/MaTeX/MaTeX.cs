using System;
using System.IO;
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
        static public String DefaultSaveLocation = Path.Combine(Path.GetTempPath(), "MaTeX");
    }

    // Enum's u.a. für Config's
    public enum TextFormats { TXT, MD, TEX };
    public enum ImageFormats { JPG, JPEG, BMP, PNG, GIF, SVG };

    // Wrapper Funktionen für z.B. Config-Optionen
    static public class Wrapper
    {
        static public String PrettyPrint(String str) { return Config.PrettyPrinting ? str : ""; }
    }

    // Converter Funktionen
    // -> "Conv", damit es keine Konflikte mit "Convert" aus "System.Convert" gibt
    static public class Conv
    {
        // Vector -> Latex
        static public String MathToLatex(Vector vector)
        {                                                            
            String _latex = @"\begin{pmatrix}{c}" + Wrapper.PrettyPrint("\n");
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
            String _latex = @"\begin{bmatrix}{rrr}" + Wrapper.PrettyPrint("\n");
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
        static public String MathToLatex(String str)
        {
            String _latex = "";
            // Gleichungen rekursiv auflösen
            if (str.Contains("="))
            {
                List<String> _equations = new List<String>(str.Split("="));
                for (int i=0; i < _equations.Count; i++)
                {
                    _latex += MathToLatex(_equations[i]);
                    _latex += (i != _equations.Count-1 ? Wrapper.PrettyPrint(" ") + "=" + Wrapper.PrettyPrint(" ") : "");
                }
                return _latex;
            }
            // Ausdruck in Latex umwandeln
            _latex = Expr.Parse(str).ToLaTeX();
            return Config.PrettyPrinting ? _latex : _latex.Replace(" ", "");
        }
    }

    // Funktionen zum Exportieren des Latex als Text oder Bild
    static public class Export
    {
        // Als Text exportieren
        static public void AsText(String latex, String filename, TextFormats format=TextFormats.TXT)
        {
            // Dateiendung wählen
            if (!filename.Contains("."))
            {
                switch (format)
                {
                    case TextFormats.TEX:
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

            // Datei speichern
            
        }

        // Als Bild exportieren
        static public void AsImage(String latex, String filename, ImageFormats fmt=ImageFormats.JPG)
        {
            throw new NotImplementedException();
        }
    }
}
