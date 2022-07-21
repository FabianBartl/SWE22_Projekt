using System;
using System.IO;
using MathNet.Numerics.LinearAlgebra.Double;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace MaTeX
{
    class Program
    {
        static string MathToLatex(Vector v) //Latexumwandlung für Vektoren
        {                                                            
            int m;
            string Latex=@"\begin{pmatrix}{c}" + "\n";
            //die einzelnen Zeilen des Vektors werden nun in Latex Schreibweise umgewandelt
            for (m = 0; m < v.Count; m++)
            {
                Latex=Latex + Convert.ToString(v[m])+ @" \\" + "\n";
            }
        return Latex + @"\end{pmatrix}";
        }

        static string MathToLatex(Matrix m) //Latexumwandlung für Matrizen
        {
            int i;
            int n;
            string Latex=@"\begin{bmatrix}{rrr}" + "\n";
            //die einzelnen Zeilen der Matrix werden nun in Latex Schreibweise umgewandelt
            for (i = 0; i < m.RowCount; i++)                           
            {
                for (n = 0; n < m.ColumnCount-1; n++)
                {
                    Latex=Latex + Convert.ToString(m[i,n])+" & ";
                }
                Latex=Latex + Convert.ToString(m[i,n]) + @" \\" + "\n";
            }
        return Latex + @"\end{bmatrix}";
        }

        static string MathToLatex(String s) //Latexumwandlung für Terme und Gleichungen
        {
            string neu="";
            int i;
            //zunächst wird abgefragt ob der String eine Gleichung ist
            if (s.Contains("="))
            {
                string[] gleichungen = s.Split("=");
                for (i = 0; i < gleichungen.Length-1; i++)
                {
                     neu = neu + Expr.Parse(gleichungen[i]).ToLaTeX() + "=";
                }
                return neu + Expr.Parse(gleichungen[i]).ToLaTeX();
            /* ist dies nicht der Fall, 
            so ist der String ein Term und kann einfach umgewandelt werden */
            }
            else return Expr.Parse(s).ToLaTeX();
        }

        static void Main(string[] args)
        {
            //zum Probieren
            string S = "3*3+sqrt(sqrt(a))";
            Vector A = DenseVector.OfArray(new double[] {1,2,3});
            Matrix B = DenseMatrix.OfArray(new double[,] {
            {1,1,1},
            {1,2,3},
            {4,3,2}});
            Console.WriteLine(MathToLatex(S));
            Console.WriteLine(MathToLatex(A));
            Console.WriteLine(MathToLatex(B));
        }
    }
}
