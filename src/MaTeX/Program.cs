using System;
using System.IO;

using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;

//matex funktionen importieren
using Mtx = MaTeX;

namespace MyApp
{
    class Program
    {
        static void Main(String[] args)
        {
            String S = "f=0=3*3+sqrt(sqrt(a))";
            Vector V = DenseVector.OfArray(new double[] {1,2,3});
            Matrix M = DenseMatrix.OfArray(new double[,] {
                {1,1,1,5},
                {1,2,3,5},
                {4,3,2,5}
            });

            Mtx.Config.PrettyPrinting = (args.Length >= 1 && (args[0] == "-pp" || args[0] == "--PrettyPrinting"));
            String S_latex = Mtx.Conv.MathToLatex(S);
            String V_latex = Mtx.Conv.MathToLatex(V);
            String M_latex = Mtx.Conv.MathToLatex(M);

            Console.Write(S_latex + Mtx.Wrapper.PrettyPrint("\n"));
            Console.Write(V_latex + Mtx.Wrapper.PrettyPrint("\n"));
            Console.Write(M_latex + "\n");

            Mtx.Config.SaveLocation = Path.GetFullPath("tmp");
            bool S_check = Mtx.Export.AsText(S_latex, "S_AsText",     Mtx.WriteModes.OVERRIDE, Mtx.TextFormats.TXT);
            bool V_check = Mtx.Export.AsText(V_latex, "V_AsText",     Mtx.WriteModes.OVERRIDE, Mtx.TextFormats.MD);
            bool M_check = Mtx.Export.AsText(M_latex, "M_AsText.ltx", Mtx.WriteModes.OVERRIDE, Mtx.TextFormats.TEX_WITH_HEADER);

            Console.WriteLine("S: " + Convert.ToString(S_check));
            Console.WriteLine("V: " + Convert.ToString(V_check));
            Console.WriteLine("M: " + Convert.ToString(M_check));

            var I = Matrix.Build;
            var R = Matrix.Build;
            var y = Vector.Build;

            I.DenseDiagonal(1, 1, 1);
            R.Random(3, 3);
            y.Random(3);
        }
    }
}
