using System;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using Expr = MathNet.Symbolics.SymbolicExpression;


namespace MaTeX
{
    public class MathObject
    {
        public enum Types { MATRIX, VECTOR, DOUBLE, INT, NULL }

        public object Type { get; private set; } = Types.NULL;
        public object Object
        {
            get
            {
                switch (this.Type)
                {
                    case Types.MATRIX: return (Matrix)(this.Object);
                    case Types.VECTOR: return (Vector)(this.Object);
                    case Types.DOUBLE: return (double)(this.Object);
                    case Types.INT:    return (int)(this.Object);
                    default:
                        return this.Object;
                }
            }
            private set { Object = value; }
        }
        
        public MathObject(Matrix obj) { this.Type = Types.MATRIX; this.Object = obj; }
        public MathObject(Vector obj) { this.Type = Types.VECTOR; this.Object = obj; }
        public MathObject(double obj) { this.Type = Types.DOUBLE; this.Object = obj; }
        public MathObject(int obj)    { this.Type = Types.INT;    this.Object = obj; }
    }

    static public class RenderEngine
    {
        static public string MathToLatex(Vector V)
        {
            int m;
            string Latex=@"\begin{pmatrix}{c}" + "\n";
            //die einzelnen Zeilen des Vektors werden nun in Latex Schreibweise umgewandelt
            for (m = 0; m < V.Count; m++)
            {
                Latex=Latex + Convert.ToString(V[m])+ @" \\" + "\n";
            }
            return Latex + @"\end{pmatrix}";
        }

        static public string MathToLatex(Matrix M)
        {
            int m;
            int n;
            string Latex=@"\begin{bmatrix}{rrr}" + "\n";
            //die einzelnen Zeilen der Matrix werden nun in Latex Schreibweise umgewandelt
            for (m = 0; m < M.RowCount; m++)                           
            {
                for (n = 0; n < M.ColumnCount-1; n++)
                {
                    Latex=Latex + Convert.ToString(M[m,n])+" & ";
                }
                Latex=Latex + Convert.ToString(M[m,n]) + @" \\" + "\n";
            }
            return Latex + @"\end{bmatrix}";
        }

        static public string MathToLatex(string s)
        {
            string neu="";
            int i;
            //zunächst wird abgefragt ob der String eine Gleichung ist
            if (s.Contains("="))
            {
                string[] gleichungen = s.Split("=");
                for (i = 0; i < gleichungen.Length-1; i++)
                {
                        neu = neu + Expr.Parse(gleichungen[i]).ToLaTeX() + "=";
                }
                return neu + Expr.Parse(gleichungen[i]).ToLaTeX();
            /* ist dies nicht der Fall, 
            so ist der String ein Term und kann einfach umgewandelt werden */
            }else return Expr.Parse(s).ToLaTeX();
        }
    }
}

