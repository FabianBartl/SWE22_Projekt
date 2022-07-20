using System;
using System.IO;
using MathNet.Numerics.LinearAlgebra.Double;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace _MyApp
{
    class _Program
    {
        static string MathToLatex(object o)
            {
                //zuerst Abfrage ob Vector
                if (o is Vector)          
                {                                                              
                    int m;
                    Vector V=(Vector)o;
                    string Latex=@"\begin{pmatrix}{c}" + "\n";
                    //die einzelnen Zeilen des Vektors werden nun in Latex Schreibweise umgewandelt
                    for (m = 0; m < V.Count; m++)
                    {
                        Latex=Latex + Convert.ToString(V[m])+ @" \\" + "\n";
                    }
                return Latex + @"\end{pmatrix}";

                //nun Abfrage ob Matrix
                }else if(o is Matrix)
                {
                    int m;
                    int n;
                    Matrix M=(Matrix)o;
                    string Latex=@"\begin{bmatrix}{rrr}" + "\n";
                    //die einzelnen Zeilen der Matrix werden nun in Latex Schreibweise umgewandelt
                    for (m = 0; m < M.RowCount; m++)                           
                    {
                        for (n = 0; n < M.ColumnCount-1; n++)
                        {
                            Latex=Latex + Convert.ToString(M[m,n])+" & ";
                        }
                        Latex=Latex + Convert.ToString(M[m,n]) + @" \\" + "\n";
                    }
                return Latex + @"\end{bmatrix}";

                //zuletzt Abfrage ob String (Term, Gleichung)
                }else if (o is string)
                {
                    string neu="";
                    int i;
                    string s = (string)o;
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
                    }else return Expr.Parse(s).ToLaTeX();
                    
                //mögliche Erweiterungen
                }else return null;
            }

        static void _Main(string[] args)
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
