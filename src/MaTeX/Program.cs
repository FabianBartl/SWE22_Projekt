using System;
using System.IO;

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
            Vector V = DenseVector.OfArray(new double[] {1,2,3});
            Matrix M = DenseMatrix.OfArray(new double[,] {
                {1,1,1},
                {1,2,3},
                {4,3,2}
            });

            Mtx.Config.PrettyPrinting = (args.Length >= 1 && (args[0] == "-pp" || args[0] == "--PrettyPrinting"));
            String S_latex = Mtx.Conv.MathToLatex(S);
            String V_latex = Mtx.Conv.MathToLatex(V);
            String M_latex = Mtx.Conv.MathToLatex(M);

            Console.Write(S_latex + Mtx.Wrapper.PrettyPrint("\n"));
            Console.Write(V_latex + Mtx.Wrapper.PrettyPrint("\n"));
            Console.Write(M_latex + Mtx.Wrapper.PrettyPrint("\n"));

            Mtx.Config.DefaultSaveLocation = @"C:\Users\fabia\Downloads";
            Mtx.Export.AsText(S_latex, "S_AsText.md", Mtx.TextFormats.TXT);
            Mtx.Export.AsText(V_latex, "V_AsText", Mtx.TextFormats.MD);
            Mtx.Export.AsText(M_latex, "M_AsText", Mtx.TextFormats.TEX);
        }
    }
}
