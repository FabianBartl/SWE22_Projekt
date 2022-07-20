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
        public enum Structures {
            ADD, SUB, MUL, DIV,
            DOT, CROSS,
            MATRIX, VECTOR, DOUBLE
        };

        public readonly object Object;
        public readonly Structures Type;

        public MathObject(Matrix m) { this.Object = m; this.Type = Structures.MATRIX; }
        public MathObject(Vector v) { this.Object = v; this.Type = Structures.VECTOR; }
        public MathObject(double d) { this.Object = d; this.Type = Structures.DOUBLE; }
        
        public MathObject(Structures s) { this.Type = s; }
        
        public static MathCollection operator +(MathObject o1, MathObject o2) => new MathCollection(
            new List<MathObject> {o1, new MathObject((Structures)Structures.ADD), o2},
            new MathObject((Vector)o1.Object + (Vector)o2.Object)
        );
        public static MathCollection operator *(MathObject o1, MathObject o2) => new MathCollection(
            new List<MathObject> {o1, new MathObject((Structures)Structures.MUL), o2},
            new MathObject((Matrix)o1.Object * (Vector)o2.Object)
        );
        public static MathCollection operator *(double d, MathObject o) => new MathCollection(
            new List<MathObject> {d, new MathObject((Structures)Structures.MUL), o},
            new MathObject((double)d * (Vector)o.Object)
        );
        public static MathCollection operator *(MathObject o, double d) => new MathCollection(
            new List<MathObject> {o, new MathObject((Structures)Structures.MUL), d},
            new MathObject((double)d * (Vector)o.Object)
        );

        public String Render() { return "latex"; }
    }

    public class MathCollection
    {
        public readonly List<MathObject> Collection;
        public readonly object Object;
        public readonly MathObject.Structures Type;

        public MathCollection(MathObject obj)
        {
            this.Collection = new List<MathObject> {obj};
            this.Object = obj;
            this.Type = obj.Type;
        }
        public MathCollection(List<MathObject> objs, MathObject obj)
        {
            this.Collection = objs;
            this.Object = obj;
            this.Type = obj.Type;
        }

        public String Render()
        {
            String latex = "";
            for (int i=0; i<this.Collection.Count; i++) { latex += this.Collection[i].Render(); }
            return latex;
        }
    }
}