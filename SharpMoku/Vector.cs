using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMoku
{
    public class Vector
    {
        public float X;
        public float Y;
        public System.Drawing.PointF ToPoint()
        {
            return new System.Drawing.PointF(this.X, this.Y);
        }
        public static Vector Zero()
        {
            return new Vector(0, 0);
        }
        public Vector attract(Movable cM, Vector grafity, float mass1, float mass2)
        {
            Vector force = new Vector(X, Y);
            force = force - cM.Location;

            float distance = force.Mag();
            force.Normalize();
            float strength = (grafity.Mag() * mass1 * mass2) / (distance * distance);
            force = force * strength;
            return force;

        }
        public bool IsOutOfControl(float Width, float Height)
        {
            if (X < 0) return true;
            if (X > Width) return true;
            if (Y < 0) return true;
            if (Y > Height) return true;

            return false;
        }
        public Vector Clone()
        {
            return new Vector(this);
        }

        public Vector(Vector p)
        {
            X = p.X;
            Y = p.Y;
        }
        public Vector(float pX, float pY)
        {
            X = pX;
            Y = pY;
        }
        public void Add(Vector v2)
        {
            X += v2.X;
            Y += v2.Y;
        }
        public string ToString()
        {
            return "X:" + X.ToString() + "  Y:" + Y.ToString();
        }
        public float Mag()
        {
            return Convert.ToSingle(Math.Sqrt(Convert.ToDouble(X * X + Y * Y)));
        }
        public void Normalize()
        {
            float M = Mag();
            Divide(M);

        }
        private void Divide(float iValue)
        {
            this.X = this.X / iValue;
            this.Y = this.Y / iValue;
        }
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector operator *(Vector v1, float iValue)
        {
            return new Vector(v1.X * iValue, v1.Y * iValue);
        }

        public static Vector operator /(Vector v1, float iValue)
        {
            return new Vector(v1.X / iValue, v1.Y / iValue);
        }
    }
}
