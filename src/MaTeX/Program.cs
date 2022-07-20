using System;
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
        static void Main()
        {
            MathObject v = new MathObject(DenseVector.OfArray(new double[] {5,3,8}));
            MathObject m = new MathObject(DenseMatrix.OfArray(new double[,] {
                {1,1,1},
                {1,2,3},
                {4,3,2}
            }));

            MathCollection a = m*v;
            MathCollection b = 2*v;
            MathCollection c = new MathObject((Vector)a.Object) + new MathObject((Vector)b.Object);

            Console.WriteLine((string)v.Object + "; " + (int)v.Type);

            for (int i=0; i<a.Collection.Count; i++) { Console.Write("(" + (int)a.Collection[i].Type + ")" + (string)a.Collection[i].Object); }
            Console.WriteLine(" = " + (string)a.Object + "; " + (int)a.Type);
        }
    }
}
