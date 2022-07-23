using System;
using MathNet.Numerics.LinearAlgebra.Double;
using Expr = MathNet.Symbolics.SymbolicExpression;

//matex funktionen importieren
using Mtx = MaTeX;

namespace MyApp
{
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
            Console.WriteLine(Mtx.Conv.MathToLatex(S));
            Console.WriteLine(Mtx.Conv.MathToLatex(A));
            Console.WriteLine(Mtx.Conv.MathToLatex(B));
        }
    }
}
