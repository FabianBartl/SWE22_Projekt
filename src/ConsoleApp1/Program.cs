﻿using System;
using System.IO;

using MathNet.Numerics.LinearAlgebra.Double;

// MaTeX Funktionen importieren
using MaTeX;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Config.PrettyPrinting = (args.Length >= 1 && (args[0] == "-pp" || args[0] == "--PrettyPrinting"));
            Config.SaveLocation = Path.GetFullPath("tmp");
            
            // ---

            /*
            string S = "0 = 3*2-sqrt(x)";
            Vector V = DenseVector.OfArray(new double[] {4,7,1});
            Matrix M = DenseMatrix.OfArray(new double[,] {
                {1,5,0},
                {0,3,0},
                {4,0,1}
            });

            string S_latex = Conv.MathToLatex(S);
            string V_latex = Conv.MathToLatex(V);
            string M_latex = Conv.MathToLatex(M);

            Console.Write(S_latex + Wrapper.PrettyPrint("\n"));
            Console.Write(V_latex + Wrapper.PrettyPrint("\n"));
            Console.Write(M_latex + "\n");

            bool S_check = Export.AsText(S_latex, "S_AsText",     WriteModes.OVERRIDE, TextFormats.TXT);
            bool V_check = Export.AsText(V_latex, "V_AsText",     WriteModes.OVERRIDE, TextFormats.MD);
            bool M_check = Export.AsText(M_latex, "M_AsText.ltx", WriteModes.OVERRIDE, TextFormats.TEX_DOCUMENT);

            Console.WriteLine("S: " + Convert.ToString(S_check));
            Console.WriteLine("V: " + Convert.ToString(V_check));
            Console.WriteLine("M: " + Convert.ToString(M_check));
            */

            // ---

            // /*
            Config.SaveLocation = Path.GetFullPath(@"../../docs");

            string S = "0 = 3*2-sqrt(x)";
            Vector V = DenseVector.OfArray(new double[] {4,7,1});
            Matrix M = DenseMatrix.OfArray(new double[,] {
                {1,5,0},
                {0,3,0},
                {4,0,1}
            });

            string S_latex = Conv.MathToLatex(S);
            string V_latex = Conv.MathToLatex(V);
            string M_latex = Conv.MathToLatex(M);

            Console.WriteLine(S_latex + "\n");
            Console.WriteLine(M_latex + @"\cdot" + V_latex);

            string FileName = "Beispiel_ExportAsText";
            Export.AsText(
                S_latex,
                FileName,
                WriteModes.OVERRIDE,
                TextFormats.TEX_DOCUMENT
            );
            Export.AsText(
                M_latex + @"\cdot",
                FileName,
                WriteModes.INSERT_BEFORE_DOCUMENT_END,
                TextFormats.TEX,
                new BracketModes[] {BracketModes.BEGIN}
            );
            Export.AsText(
                V_latex,
                FileName,
                WriteModes.INSERT_BEFORE_DOCUMENT_END,
                TextFormats.TEX,
                new BracketModes[] {BracketModes.END}
            );
            // */

            // ---

            /*
            Config.BracketMode = new BracketModes[] {};
            
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

            string y_latex = Conv.MathToLatex(y);
            string I_latex = Conv.MathToLatex(I);
            string A_latex = Conv.MathToLatex(A);
            string R_latex = Conv.MathToLatex("R");

            string latex = A_latex
                + "-" + Wrapper.PrettyPrint("\n")
                + y_latex + @"\cdot" + Wrapper.PrettyPrint("\n")
                + I_latex + "=" + R_latex;
            Console.WriteLine(latex);

            string FileName = "R_AsText";
            Console.WriteLine("R: " + (Export.AsText(
                latex,
                FileName,
                WriteModes.OVERRIDE,
                TextFormats.TEX_DOCUMENT,
                new BracketModes[] {BracketModes.BEGIN, BracketModes.END}
            )));
            */

            // ---

            /*
            Config.BracketMode = new BracketModes[] {};
            string FileName = "debug.tex";

            Console.WriteLine(Export.AsText(
                Wrapper.PrettyPrint("\n")
                    + @"\text{OVERRIDE TEX_DOCUMENT}"
                    + Wrapper.PrettyPrint("\n"),
                FileName,
                WriteModes.OVERRIDE,
                TextFormats.TEX_DOCUMENT
            ));

            // Console.WriteLine(Export.AsText(
            //     Wrapper.PrettyPrint("\n")
            //         + @"\text{APPEND TXT}"
            //         + Wrapper.PrettyPrint("\n"),
            //     FileName,
            //     WriteModes.APPEND,
            //     TextFormats.TXT
            // ));

            // Console.WriteLine(Export.AsText(
            //     Wrapper.PrettyPrint("\n")
            //         + @"\text{AT_START TXT}"
            //         + Wrapper.PrettyPrint("\n"),
            //     FileName,
            //     WriteModes.AT_START,
            //     TextFormats.TXT
            // ));

            Console.WriteLine(Export.AsText(
                Wrapper.PrettyPrint("\n")
                    + @"\text{INSERT_AFTER_DOCUMENT_START TEX}"
                    + Wrapper.PrettyPrint("\n"),
                FileName,
                WriteModes.INSERT_AFTER_DOCUMENT_START,
                TextFormats.TEX,
                BracketModes.BEGIN
            ));

            Console.WriteLine(Export.AsText(
                Wrapper.PrettyPrint("\n")
                    + @"\text{INSERT_BEFORE_DOCUMENT_END TEX}"
                    + Wrapper.PrettyPrint("\n"),
                FileName,
                WriteModes.INSERT_BEFORE_DOCUMENT_END,
                TextFormats.TEX,
                BracketModes.END                
            ));
            */
        }
    }
}