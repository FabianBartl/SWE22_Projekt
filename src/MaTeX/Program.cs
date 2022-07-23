using System;
using MathNet.Numerics.LinearAlgebra.Double;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace MaTeX
{
    static public class Conv
    {
        static public string MathToLatex(Vector vec) //Latexumwandlung für Vektoren
        {                                                            
            string Latex = @"\begin{pmatrix}{c}" + "\n";
            //die einzelnen Zeilen des Vektors werden nun in Latex Schreibweise umgewandelt
            for (int row=0; row<vec.Count; row++) Latex += Convert.ToString(vec[row]) + @" \\" + "\n";
            return Latex + @"\end{pmatrix}";
        }

        static public string MathToLatex(Matrix mtr) //Latexumwandlung für Matrizen
        {
            string Latex = @"\begin{bmatrix}{rrr}" + "\n";
            //die einzelnen Zeilen der Matrix werden nun in Latex Schreibweise umgewandelt
            for (int row=0, col=0; row<mtr.RowCount; row++)                           
            {
                for (col=0; col<mtr.ColumnCount-1; col++) Latex += Convert.ToString(mtr[row,col])+" & ";
                Latex += Convert.ToString(mtr[row,col]) + @" \\" + "\n";
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

    class Program
    {
        static void Main()
        {
            //zum Probieren
            string S = "3*3+sqrt(sqrt(a))";
            Vector A = DenseVector.OfArray(new double[] {1,2,3});
            Matrix B = DenseMatrix.OfArray(new double[,] {
            {1,1,1},
            {1,2,3},
            {4,3,2}});
            Console.WriteLine(Conv.MathToLatex(S));
            Console.WriteLine(Conv.MathToLatex(A));
            Console.WriteLine(Conv.MathToLatex(B));
        }
    }
}
