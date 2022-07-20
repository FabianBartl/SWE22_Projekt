using System;

using MathNet.Numerics.LinearAlgebra.Double;
using Expr = MathNet.Symbolics.SymbolicExpression;

using MaTeX;


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
                {4,3,2}
            });

            Console.WriteLine(RenderEngine.MathToLatex(S));
            Console.WriteLine(RenderEngine.MathToLatex(A));
            Console.WriteLine(RenderEngine.MathToLatex(B));

            // Console.WriteLine(RenderEngine.MathToLatex(new MathObject(A).Object));
            Console.WriteLine(new MathObject(A).Type);
        }
    }
}
