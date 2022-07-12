using System;
using System.IO;
using MathNet.Numerics.LinearAlgebra.Double;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace MaTeX
{
    class Program
    {
        static string MathToLatex(object o)
            {
                //zuerst Abfrage ob Vector
                if (o is Vector)          
                {                                                              
                    int m;
                    Vector V=(Vector)o;
                    string Latex=@"\left( \begin{array}{c}" + "\n";
                    //die einzelnen Zeilen des Vektors werden nun in Latex Schreibweise umgewandelt
                    for (m = 0; m < V.Count; m++)
                    {
                        Latex=Latex + Convert.ToString(V[m])+ @"\\" + "\n";
                    }
                return Latex + @"\end{array}\right)$";

                //nun Abfrage ob Matrix
                }else if(o is Matrix)
                {
                    int m;
                    int n;
                    Matrix M=(Matrix)o;
                    string Latex=@"$\left( \begin{array}{rrr}" + "\n";
                    //die einzelnen Zeilen der Matrix werden nun in Latex Schreibweise umgewandelt
                    for (m = 0; m < M.RowCount; m++)                           
                    {
                        for (n = 0; n < M.ColumnCount-1; n++)
                        {
                            Latex=Latex + Convert.ToString(M[m,n])+" & ";
                        }
                        Latex=Latex + Convert.ToString(M[m,n]) + @" \\" + "\n";
                    }
                return Latex + @"\end{array}\right)$";

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
                        return @"$ " + neu + Expr.Parse(gleichungen[i]).ToLaTeX() + @" $";
                    /* ist dies nicht der Fall, 
                    so ist der String ein Term und kann einfach umgewandelt werden */
                    }else return @"$ " + Expr.Parse(s).ToLaTeX() + @" $";
                    
                //mögliche Erweiterungen
                }else return null;
            }

        static void Main(string[] args)
        {
            //zum Probieren
            string S = "3*3+sqrt(sqrt(a))";
            Matrix A = DenseMatrix.OfArray(new double[,] {
            {1,1,1},
            {1,2,3},
            {4,3,2}});
            Vector B = DenseVector.OfArray(new double[] {1,2,3});
            Console.WriteLine(MathToLatex(S));
        }
    }
}
