using System;
using System.Formats;
using System.Collections.Generic;
using System.Linq;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;


namespace MaTeX
{
    public class MathObject
    {
        public readonly Matrix Object;

        public MathObject(Matrix matrix) { this.Object = matrix; }
        public MathObject(Vector vector) { this.Object = DenseMatrix.OfArray(vector); }
        
        public static MathCollection operator +(MathObject o1, MathObject o2) => new MathCollection(new List<val> {o1, MathEnums.Operators.ADD, o2}, o1.Object + o2.Object);
        public static MathCollection operator *(MathObject o1, MathObject o2) => new MathCollection(new List<MathObject> {o1, MathEnums.Operators.MUL, o2}, o1.Object * o2.Object);
        public static MathCollection operator *(double d, MathObject o) => new MathCollection(new List<MathObject> {d, MathEnums.Operators.MUL, o}, d * o.Object);
        public static MathCollection operator *(MathObject o, double d) => new MathCollection(new List<MathObject> {o, MathEnums.Operators.MUL, d}, d * o.Object);
    }

    static public class MathEnums
    {
        public enum Operators {ADD, SUB, MUL, DIV, DOT, CROSS};
    }

    public class MathCollection
    {
        public readonly List<MathObject> Collection;
        public readonly object Value;

        public MathCollection(MathObject obj)
        {
            this.Collection = new List<MathObject> {obj};
            this.Value = this.Collection[0];
        }
        public MathCollection(List<MathObject> objs, object val)
        {
            this.Collection = objs;
            this.Value = val;
        }
    }
}