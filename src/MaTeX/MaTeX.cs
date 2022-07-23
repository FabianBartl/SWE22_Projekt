using System;
using MathNet.Numerics.LinearAlgebra.Double;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace MaTeX
{
    static public class Config
    {
        static public bool PrettyFormat = false;
    }

    static public class Wrapper
    {
        static public string PrettyFormat(string str) { return Config.PrettyFormat ? str : ""; }
    }

    static public class Conv
    {
        static public string MathToLatex(Vector vec) //Latexumwandlung für Vektoren
        {                                                            
            string Latex = @"\begin{pmatrix}{c}" + Wrapper.PrettyFormat("\n");
            //die einzelnen Zeilen des Vektors werden nun in Latex Schreibweise umgewandelt
            for (int row=0; row<vec.Count; row++)
            {
                Latex += Convert.ToString(vec[row]);
                Latex += Wrapper.PrettyFormat(" ") + @"\\" + Wrapper.PrettyFormat("\n");
            }
            return Latex + @"\end{pmatrix}";
        }

        static public string MathToLatex(Matrix mtr) //Latexumwandlung für Matrizen
        {
            string Latex = @"\begin{bmatrix}{rrr}" + Wrapper.PrettyFormat("\n");
            //die einzelnen Zeilen der Matrix werden nun in Latex Schreibweise umgewandelt
            for (int row=0, col=0; row<mtr.RowCount; row++)                           
            {
                for (col=0; col<mtr.ColumnCount-1; col++)
                {
                    Latex += Convert.ToString(mtr[row,col]);
                    Latex += Wrapper.PrettyFormat(" ") + "&" + Wrapper.PrettyFormat(" ");
                }
                Latex += Convert.ToString(mtr[row,col]);
                Latex += Wrapper.PrettyFormat(" ") + @"\\" + Wrapper.PrettyFormat("\n");
            }
            return Latex + @"\end{bmatrix}";
        }

        static public string MathToLatex(String str) //Latexumwandlung für Terme und Gleichungen
        {
            string neu = "";
            int i;
            //zunächst wird abgefragt ob der String eine Gleichung ist
            if (str.Contains("="))
            {
                string[] gleichungen = str.Split("=");
                for (i = 0; i < gleichungen.Length-1; i++)
                {
                     neu = neu + Expr.Parse(gleichungen[i]).ToLaTeX() + "=";
                }
                return neu + Expr.Parse(gleichungen[i]).ToLaTeX();
            /* ist dies nicht der Fall, 
            so ist der String ein Term und kann einfach umgewandelt werden */
            }
            else return Expr.Parse(str).ToLaTeX();
        }

    }
}
