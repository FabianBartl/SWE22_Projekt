using System;
using System.IO;
using System.Collections.Generic;

using MathNet.Numerics.LinearAlgebra.Double;
using Expr = MathNet.Symbolics.SymbolicExpression;

using CSharpMath;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace MaTeX
{
    static public class Config
    {
        static public bool PrettyPrinting = false;
        static public ImageFormats ImageFormat = ImageFormats.JPG;
        static public TextFormats TextFormat = TextFormats.TXT;
        static public String DefaultSaveLocation = Path.Combine(Path.GetTempPath(), "MaTeX");
    }

    public enum ImageFormats { JPG, PNG, GIF, SVG };
    public enum TextFormats { TEX, MD, TXT };

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
        static public String MathToLatex(Vector vec)
        {
            String latex = @"\begin{pmatrix}{c}" + Wrapper.PrettyPrint("\n");
            // Zeilen des Vektors auflösen
            for (int row = 0; row < vec.Count; row++)
            {
                latex += Convert.ToString(vec[row]);
                latex += Wrapper.PrettyPrint(" ") + (row != vec.Count - 1 ? @"\\" : "") + Wrapper.PrettyPrint("\n");
            }
            return latex + @"\end{pmatrix}";
        }

        // Matrix -> Latex
        static public String MathToLatex(Matrix mtr)
        {
            String latex = @"\begin{bmatrix}{rrr}" + Wrapper.PrettyPrint("\n");
            // Zeilen der Matrix auflösen
            for (int row = 0, col = 0; row < mtr.RowCount; row++)
            {
                // Spalten der Matrix auflösen
                for (col = 0; col < mtr.ColumnCount - 1; col++)
                {
                    latex += Convert.ToString(mtr[row, col]);
                    latex += Wrapper.PrettyPrint(" ") + "&" + Wrapper.PrettyPrint(" ");
                }
                latex += Convert.ToString(mtr[row, col]);
                latex += Wrapper.PrettyPrint(" ") + (row != mtr.RowCount - 1 ? @"\\" : "") + Wrapper.PrettyPrint("\n");
            }
            return latex + @"\end{bmatrix}";
        }

        // Term, Gleichung -> Latex
        static public String MathToLatex(String str)
        {
            String latex = "";
            // Gleichungen rekursiv auflösen
            if (str.Contains("="))
            {
                List<String> equations = new List<String>(str.Split("="));
                for (int i = 0; i < equations.Count; i++)
                {
                    latex += MathToLatex(equations[i]);
                    latex += (i != equations.Count - 1 ? Wrapper.PrettyPrint(" ") + "=" + Wrapper.PrettyPrint(" ") : "");
                }
                return latex;
            }
            // Ausdruck in Latex umwandeln
            latex = Expr.Parse(str).ToLaTeX();
            return Config.PrettyPrinting ? latex : latex.Replace(" ", "");
        }
    }
}
