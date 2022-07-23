using System;

using MathNet.Numerics.LinearAlgebra.Double;
using Expr = MathNet.Symbolics.SymbolicExpression;

//matex funktionen importieren
using Mtx = MaTeX;

namespace MyApp
{
    class Program
    {
        static void Main(String[] args)
        {
            //zum Probieren
            String S = "f=0=3*3+sqrt(sqrt(a))";
            Vector A = DenseVector.OfArray(new double[] {1,2,3});
            Matrix B = DenseMatrix.OfArray(new double[,] {
                {1,1,1},
                {1,2,3},
                {4,3,2}
            });

            Mtx.Config.PrettyPrinting = (args.Length >= 1 && (args[0] == "-pp" || args[0] == "--PrettyPrinting"));
            Console.Write(Mtx.Conv.MathToLatex(S) + Mtx.Wrapper.PrettyPrint("\n"));
            Console.Write(Mtx.Conv.MathToLatex(A) + Mtx.Wrapper.PrettyPrint("\n"));
            Console.Write(Mtx.Conv.MathToLatex(B) + Mtx.Wrapper.PrettyPrint("\n"));
        }
    }
}
