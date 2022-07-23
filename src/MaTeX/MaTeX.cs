using System;
using System.Collections.Generic;

using MathNet.Numerics.LinearAlgebra.Double;
using Expr = MathNet.Symbolics.SymbolicExpression;

using CSharpMath;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace MaTeX
{
    static public class Config
    {
        static public bool PrettyFormat = false;
    }

    static public class Wrapper
    {
        static public String PrettyFormat(String str) { return Config.PrettyFormat ? str : ""; }
    }

    static public class Conv
    {
        // Vector -> Latex
        static public String MathToLatex(Vector vec)
        {                                                            
            String latex = @"\begin{pmatrix}{c}" + Wrapper.PrettyFormat("\n");
            // Zeilen des Vektors auflösen
            for (int row=0; row<vec.Count; row++)
            {
                latex += Convert.ToString(vec[row]);
                latex += Wrapper.PrettyFormat(" ") + (row != vec.Count-1 ? @"\\" : "") + Wrapper.PrettyFormat("\n");
            }
            return latex + @"\end{pmatrix}";
        }

        // Matrix -> Latex
        static public String MathToLatex(Matrix mtr)
        {
            String latex = @"\begin{bmatrix}{rrr}" + Wrapper.PrettyFormat("\n");
            // Zeilen der Matrix auflösen
            for (int row=0, col=0; row<mtr.RowCount; row++)                           
            {
                // Spalten der Matrix auflösen
                for (col=0; col<mtr.ColumnCount-1; col++)
                {
                    latex += Convert.ToString(mtr[row,col]);
                    latex += Wrapper.PrettyFormat(" ") + "&" + Wrapper.PrettyFormat(" ");
                }
                latex += Convert.ToString(mtr[row,col]);
                latex += Wrapper.PrettyFormat(" ") + (row != mtr.RowCount-1 ? @"\\" : "") + Wrapper.PrettyFormat("\n");
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
                for (int i=0; i<equations.Count; i++)
                {
                    latex += MathToLatex(equations[i]);
                    latex += (i != equations.Count-1 ? Wrapper.PrettyFormat(" ") + "=" + Wrapper.PrettyFormat(" ") : "");
                }
                return latex;
            }
            // Ausdruck in Latex umwandeln
            latex = Expr.Parse(str).ToLaTeX();
            return Config.PrettyFormat ? latex : latex.Replace(" ", "");
        }
    }
}
