using System;
using System.IO;

using MathNet.Numerics.LinearAlgebra.Double;

//matex funktionen importieren
using Mtx = MaTeX;

namespace MyApp
{
    class Program
    {
        static void Main(String[] args)
        {
            Mtx.Config.PrettyPrinting = (args.Length >= 1 && (args[0] == "-pp" || args[0] == "--PrettyPrinting"));
            Mtx.Config.SaveLocation = Path.GetFullPath("tmp");
            
            // ---

            /*
            String S = "f=0=3*3+sqrt(sqrt(a))";
            Vector V = DenseVector.OfArray(new double[] {1,2,3});
            Matrix M = DenseMatrix.OfArray(new double[,] {
                {1,1,1,5},
                {1,2,3,5},
                {4,3,2,5}
            });

            String S_latex = Mtx.Conv.MathToLatex(S);
            String V_latex = Mtx.Conv.MathToLatex(V);
            String M_latex = Mtx.Conv.MathToLatex(M);

            Console.Write(S_latex + Mtx.Wrapper.PrettyPrint("\n"));
            Console.Write(V_latex + Mtx.Wrapper.PrettyPrint("\n"));
            Console.Write(M_latex + "\n");

            bool S_check = Mtx.Export.AsText(S_latex, "S_AsText",     Mtx.WriteModes.OVERRIDE, Mtx.TextFormats.TXT);
            bool V_check = Mtx.Export.AsText(V_latex, "V_AsText",     Mtx.WriteModes.OVERRIDE, Mtx.TextFormats.MD);
            bool M_check = Mtx.Export.AsText(M_latex, "M_AsText.ltx", Mtx.WriteModes.OVERRIDE, Mtx.TextFormats.TEX_WITH_HEADER);

            Console.WriteLine("S: " + Convert.ToString(S_check));
            Console.WriteLine("V: " + Convert.ToString(V_check));
            Console.WriteLine("M: " + Convert.ToString(M_check));
            */

            // ---

            // /*
            Mtx.Config.BracketMode = new Mtx.BracketModes[] {};
            
            Vector y = DenseVector.OfArray(new double[] {4,7,1});
            Matrix I = DenseMatrix.OfArray(new double[,] {
                {1,0,0},
                {0,1,0},
                {0,0,1}
            });
            //Py-Gen: print("".join(["{0},{1},{2}".format(random.randint(0,9),random.randint(0,9),random.randint(0,9)) + "\n" for i in range(3)]))
            Matrix A = DenseMatrix.OfArray(new double[,] {
                {0,5,4},
                {4,8,0},
                {2,9,9}
            });

            String y_latex = Mtx.Conv.MathToLatex(y);
            String I_latex = Mtx.Conv.MathToLatex(I);
            String A_latex = Mtx.Conv.MathToLatex(A);
            String R_latex = Mtx.Conv.MathToLatex("R");

            String latex = A_latex
                + "-" + Mtx.Wrapper.PrettyPrint("\n")
                + y_latex + @"\cdot" + Mtx.Wrapper.PrettyPrint("\n")
                + I_latex + "=" + R_latex;
            Console.WriteLine(latex);

            String FileName = "R_AsText";
            Console.WriteLine("R: " + (Mtx.Export.AsText(
                latex,
                FileName,
                Mtx.WriteModes.OVERRIDE,
                Mtx.TextFormats.TEX_WITH_HEADER,
                new Mtx.BracketModes[] {Mtx.BracketModes.BEGIN, Mtx.BracketModes.END}
            )));
            // */

            // ---

            /*
            Mtx.Config.BracketMode = new Mtx.BracketModes[] {};
            String FileName = "debug.tex";

            Console.WriteLine(Mtx.Export.AsText(
                Mtx.Wrapper.PrettyPrint("\n")
                    + @"\text{OVERRIDE TEX_WITH_HEADER}"
                    + Mtx.Wrapper.PrettyPrint("\n"),
                FileName,
                Mtx.WriteModes.OVERRIDE,
                Mtx.TextFormats.TEX_WITH_HEADER
            ));

            // Console.WriteLine(Mtx.Export.AsText(
            //     Mtx.Wrapper.PrettyPrint("\n")
            //         + @"\text{APPEND TXT}"
            //         + Mtx.Wrapper.PrettyPrint("\n"),
            //     FileName,
            //     Mtx.WriteModes.APPEND,
            //     Mtx.TextFormats.TXT
            // ));

            // Console.WriteLine(Mtx.Export.AsText(
            //     Mtx.Wrapper.PrettyPrint("\n")
            //         + @"\text{AT_START TXT}"
            //         + Mtx.Wrapper.PrettyPrint("\n"),
            //     FileName,
            //     Mtx.WriteModes.AT_START,
            //     Mtx.TextFormats.TXT
            // ));

            Console.WriteLine(Mtx.Export.AsText(
                Mtx.Wrapper.PrettyPrint("\n")
                    + @"\text{INSERT_AFTER_DOCUMENT_START TEX}"
                    + Mtx.Wrapper.PrettyPrint("\n"),
                FileName,
                Mtx.WriteModes.INSERT_AFTER_DOCUMENT_START,
                Mtx.TextFormats.TEX,
                Mtx.BracketModes.BEGIN
            ));

            Console.WriteLine(Mtx.Export.AsText(
                Mtx.Wrapper.PrettyPrint("\n")
                    + @"\text{INSERT_BEFORE_DOCUMENT_END TEX}"
                    + Mtx.Wrapper.PrettyPrint("\n"),
                FileName,
                Mtx.WriteModes.INSERT_BEFORE_DOCUMENT_END,
                Mtx.TextFormats.TEX,
                Mtx.BracketModes.END                
            ));
            */
        }
    }
}
