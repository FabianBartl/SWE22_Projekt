using Xunit;
using MathNet.Numerics.LinearAlgebra.Double;

public class Testings
{
    [Fact]
    public void MathToLatex_Vector_ReturnLatex()
    {
        Vector A = DenseVector.OfArray(new double[] {1,2,3});
        string Latex = @"\begin{pmatrix}{c}1\\2\\3\end{pmatrix}";
        Assert.Equal(Latex, MaTeX.Conv.MathToLatex(A));
    }

    [Fact]
    public void MathToLatex_Matrix_ReturnLatex()
    {
        Matrix B = DenseMatrix.OfArray(new double[,] {
            {1,1,1},
            {1,2,3},
            {4,3,2}
        });
        string Latex = @"\begin{bmatrix}{rrr}1&1&1\\1&2&3\\4&3&2\end{bmatrix}";
        Assert.Equal(Latex, MaTeX.Conv.MathToLatex(B));
    }

    [Fact]
    public void MathToLatex_Gleichung_ReturnLatex()
    {
         string Gleichung = "f=0=3*3+sqrt(sqrt(a))";
         string Latex = @"f=0=9+\sqrt{\sqrt{a}}";
         Assert.Equal(Latex, MaTeX.Conv.MathToLatex(Gleichung));
    }

    [Fact]
    public void MathToLatex_Term_ReturnLatex()
    {
        string Term = @"3*3+sqrt(sqrt(a))";
        string Latex = @"9+\sqrt{\sqrt{a}}";
        Assert.Equal(Latex, MaTeX.Conv.MathToLatex(Term));
    }
}