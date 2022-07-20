using System.Formats;
using System.Collections.Generic;
using System.Linq;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;

using MaTeX;


namespace MyApp
{
    class Program
    {
        static void _Main()
        {
            MathObject v = new MathObject(DenseVector.OfArray(new double[] {5,3,8}));
            MathObject m = new MathObject(DenseMatrix.OfArray(new double[,] {
                {1,1,1},
                {1,2,3},
                {4,3,2}
            }));

            MathCollection t = m*v + 2.0*v;
        }
    }
}
