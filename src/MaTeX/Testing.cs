using Xunit;
using System;
using System.IO;
using MathNet.Numerics.LinearAlgebra.Double;

public class Testings
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void MathToLatex_Vector_ReturnLatex(bool pp)
    {
        MaTeX.Config.PrettyPrinting=pp;
        Vector A = DenseVector.OfArray(new double[] {1,2,3});
        string Latex;
        if (pp==false) { Latex = @"\begin{pmatrix}1\\2\\3\end{pmatrix}";}
        else  {Latex = @"\begin{pmatrix}" + "\n" + @"1 \\" + "\n" + @"2 \\" + "\n" 
        + "3 \n" + @"\end{pmatrix}";}
        Assert.Equal(Latex, MaTeX.Conv.MathToLatex(A));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void MathToLatex_Matrix_ReturnLatex(bool pp)
    {
        MaTeX.Config.PrettyPrinting=pp;
        Matrix B = DenseMatrix.OfArray(new double[,] {
            {1,1,1},
            {1,2,3},
            {4,3,2}
        });
        string Latex;
        if (pp==false)  {Latex = @"\begin{bmatrix}1&1&1\\1&2&3\\4&3&2\end{bmatrix}";}
        else {Latex = @"\begin{bmatrix}" + "\n" + @"1 & 1 & 1 \\" + "\n" + @"1 & 2 & 3 \\" + "\n" + "4 & 3 & 2 " 
        + "\n" + @"\end{bmatrix}";}
        Assert.Equal(Latex, MaTeX.Conv.MathToLatex(B));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void MathToLatex_Gleichung_ReturnLatex(bool pp)
    {
        MaTeX.Config.PrettyPrinting=pp;
        string Gleichung = "f=0=3*3+sqrt(sqrt(a))";
        string Latex;
        if (pp==false) {Latex = @"f=0=9+\sqrt{\sqrt{a}}";}
        else {Latex = @"f = 0 = 9 + \sqrt{\sqrt{a}}";}
        Assert.Equal(Latex, MaTeX.Conv.MathToLatex(Gleichung));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void MathToLatex_Term_ReturnLatex(bool pp)
    {
        MaTeX.Config.PrettyPrinting=pp;
        string Term = @"3*3+sqrt(sqrt(a))";
        string Latex;
        if (pp==false) {Latex = @"9+\sqrt{\sqrt{a}}";}
        else {Latex = @"9 + \sqrt{\sqrt{a}}";}
        Assert.Equal(Latex, MaTeX.Conv.MathToLatex(Term));
    }

private bool FileCompare(string file1, string file2)
{
    int file1byte;
    int file2byte;
    FileStream fs1;
    FileStream fs2;

    if (file1 == file2)
    {
        return true;
    }

    fs1 = new FileStream(file1, FileMode.Open);
    fs2 = new FileStream(file2, FileMode.Open);

    if (fs1.Length != fs2.Length)
    {
        fs1.Close();
        fs2.Close();

        return false;
    }

    do
    {
        file1byte = fs1.ReadByte();
        file2byte = fs2.ReadByte();
    }
    while ((file1byte == file2byte) && (file1byte != -1));

    fs1.Close();
    fs2.Close();

    return ((file1byte - file2byte) == 0);
}

    [Theory]
    [InlineData("R_AsText.tex", @"D:\Schule\Studium\Software\SWE22_Projekt\src\MaTeX\TestFiles\R_AsText.tex")]
    [InlineData("S_AsText.txt", @"D:\Schule\Studium\Software\SWE22_Projekt\src\MaTeX\TestFiles\S_AsText.txt")]
    [InlineData("V_AsText.md", @"D:\Schule\Studium\Software\SWE22_Projekt\src\MaTeX\TestFiles\V_AsText.md")]
    [InlineData("M_AsText.ltx", @"D:\Schule\Studium\Software\SWE22_Projekt\src\MaTeX\TestFiles\M_AsText.ltx")]
    [InlineData("debug.tex", @"D:\Schule\Studium\Software\SWE22_Projekt\src\MaTeX\TestFiles\debug.tex")]


    public void AsText_Latex_File(string textformat, string comparepath)
    {
        File.Create(textformat);
        MaTeX.Config.SaveLocation = Path.GetFullPath(@"D:\Schule\Studium\Software\SWE22_Projekt\src\MaTeX");
        MaTeX.Config.PrettyPrinting=true;
        if (textformat=="R_AsText.tex")
        {
            Vector y = DenseVector.OfArray(new double[] {4,7,1});
            Matrix I = DenseMatrix.OfArray(new double[,] {
                {1,0,0},
                {0,1,0},
                {0,0,1}
            });
            Matrix A = DenseMatrix.OfArray(new double[,] {
                {0,5,4},
                {4,8,0},
                {2,9,9}
            });

            string y_latex = MaTeX.Conv.MathToLatex(y);
            string I_latex = MaTeX.Conv.MathToLatex(I);
            string A_latex = MaTeX.Conv.MathToLatex(A);
            string R_latex = MaTeX.Conv.MathToLatex("R");

            string latex = A_latex
                + "-" + MaTeX.Wrapper.PrettyPrint("\n")
                + y_latex + @"\cdot" + MaTeX.Wrapper.PrettyPrint("\n")
                + I_latex + "=" + R_latex;
            Console.WriteLine(latex);

            string FileName = textformat;
            MaTeX.Export.AsText(
                latex,
                FileName,
                MaTeX.WriteModes.OVERRIDE,
                MaTeX.TextFormats.TEX_DOCUMENT,
                new MaTeX.BracketModes[] {MaTeX.BracketModes.BEGIN, MaTeX.BracketModes.END}
            );
        }else if (textformat=="S_AsText.txt")
        {
            String S = "0 = 3*2-sqrt(x)";
            String S_latex = MaTeX.Conv.MathToLatex(S);
            MaTeX.Export.AsText(S_latex, "S_AsText", MaTeX.WriteModes.OVERRIDE, MaTeX.TextFormats.TXT);
        }else if (textformat=="V_AsText.md")
        {
            Vector V = DenseVector.OfArray(new double[] {4,7,1});
            String V_latex = MaTeX.Conv.MathToLatex(V);
            MaTeX.Export.AsText(V_latex, "V_AsText", MaTeX.WriteModes.OVERRIDE, MaTeX.TextFormats.MD);
        }else if (textformat=="M_AsText.ltx")
        {
             Matrix M = DenseMatrix.OfArray(new double[,] {
                {1,5,0},
                {0,3,0},
                {4,0,1}
            });
            String M_latex = MaTeX.Conv.MathToLatex(M);
            MaTeX.Export.AsText(M_latex, "M_AsText.ltx", MaTeX.WriteModes.OVERRIDE, MaTeX.TextFormats.TEX_DOCUMENT);
        }else if (textformat=="debug.tex")
        {
            MaTeX.Config.BracketMode = new MaTeX.BracketModes[] {};
            String FileName = "debug.tex";

            Console.WriteLine(MaTeX.Export.AsText(
                MaTeX.Wrapper.PrettyPrint("\n")
                    + @"\text{OVERRIDE TEX_DOCUMENT}"
                    + MaTeX.Wrapper.PrettyPrint("\n"),
                FileName,
                MaTeX.WriteModes.OVERRIDE,
                MaTeX.TextFormats.TEX_DOCUMENT
            ));
              
            MaTeX.Export.AsText(
                MaTeX.Wrapper.PrettyPrint("\n")
                    + @"\text{INSERT_AFTER_DOCUMENT_START TEX}"
                    + MaTeX.Wrapper.PrettyPrint("\n"),
                FileName,
                MaTeX.WriteModes.INSERT_AFTER_DOCUMENT_START,
                MaTeX.TextFormats.TEX,
                MaTeX.BracketModes.BEGIN
            );

            MaTeX.Export.AsText(
                MaTeX.Wrapper.PrettyPrint("\n")
                    + @"\text{INSERT_BEFORE_DOCUMENT_END TEX}"
                    + MaTeX.Wrapper.PrettyPrint("\n"),
                FileName,
                MaTeX.WriteModes.INSERT_BEFORE_DOCUMENT_END,
                MaTeX.TextFormats.TEX,
                MaTeX.BracketModes.END                
            );
        }
        string path = Path.Combine(@"D:\Schule\Studium\Software\SWE22_Projekt\src\MaTeX",textformat);
        Assert.True(FileCompare(path, comparepath));
        File.Delete(path);
    }
}