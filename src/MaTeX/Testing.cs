using Xunit;
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
        if (pp==false) { Latex = @"\begin{pmatrix}{c}1\\2\\3\end{pmatrix}";}
        else  {Latex = @"\begin{pmatrix}{c}" + "\n" + @"1 \\" + "\n" + @"2 \\" + "\n" 
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
        if (pp==false)  {Latex = @"\begin{bmatrix}{rrr}1&1&1\\1&2&3\\4&3&2\end{bmatrix}";}
        else {Latex = @"\begin{bmatrix}{rrr}" + "\n" + @"1 & 1 & 1 \\" + "\n" + @"1 & 2 & 3 \\" + "\n" + "4 & 3 & 2 " 
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
}