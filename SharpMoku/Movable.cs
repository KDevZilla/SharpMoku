using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku
{
    public class Movable
    {
        public void SetCenterLocation(Vector v)
        {
            _location = new Vector(v.X - (this._size.X / 2), v.Y - (this._size.Y / 2));
        }

        public void SetLocation(Vector v)
        {
            _location = new Vector(v);
        }
        public void Log()
        {
            StringBuilder strB = new StringBuilder();

            strB.Append(this.GetType().ToString()).Append(Environment.NewLine)
                .Append("Center Location:" + CenterLocation.ToString()).Append(Environment.NewLine)
                .Append("Location:" + Location.ToString()).Append(Environment.NewLine)
                .Append("Velocity:" + Velocity.ToString()).Append(Environment.NewLine)
                .Append("Acceleration:" + Acceleration.ToString()).Append(Environment.NewLine)
                .Append("==============================================");

           // cUtil.WriteLog(strB.ToString());
        }
        public virtual void Draw(System.Drawing.Graphics g)
        {
            throw new Exception("Not Implemented yet");
        }
        /*
        public bool IsInSide(cLiquid L)
        {
            Rectangle R1 = this.ToRectangle();
            Rectangle R2 = L.ToRectangle();
            if (R1.IntersectsWith(R2))
            {
                return true;
            }

            return false;
        }
        */

        public virtual void Update()
        {
            // cUtil.WriteLog("cMovable_Update()");
            _velocity.Add(_Acceleration);
            //_velocity.Normalize();
            LimitVelocity();
            _location.Add(_velocity);

            _Acceleration = new Vector(0, 0);

        }

        public void SetAcc(Vector v)
        {
            _Acceleration = new Vector(v);
        }
        public void setLimitVelocity(Single s)
        {
        }
        private Single _LimitVelocity = 10f;
        private void LimitVelocity()
        {

            if (_velocity.X > _LimitVelocity)
            {
                _velocity.X = _LimitVelocity;
            }
            if (_velocity.Y > _LimitVelocity)
            {
                _velocity.Y = _LimitVelocity;
            }

        }
        public void ClearEveryForce()
        {
            this._Acceleration = new Vector(0, 0);
            this._velocity = new Vector(0, 0);
        }
        public void ApplyForce(Vector vForce)
        {
            Vector vTemp = vForce.Clone();
            int Size = (int)((this.Size.X + this.Size.Y) / 2);
            vTemp = vTemp / Size;

            this.Acceleration.Add(vTemp);

        }

        private Vector _location = null;
        private Vector _velocity = new Vector(0, 0);
        private Vector _Acceleration = null;
        public Vector Acceleration
        {
            get { return _Acceleration; }
        }
        //private int _size = 0;
        private Vector _size = new Vector(0, 0);
        protected bool _IsDie = false;
        public bool IsDie
        {
            get
            {
                return _IsDie;
            }
        }
        public void CheckEdge(int width, int height)
        {
            if (Location.X > width)
            {
                Location.X = width;
            }
            if (Location.X < 0)
            {
                Location.X = 0;
            }
            if (Location.Y > height)
            {
                Location.Y = height;
            }

            if (Location.Y < 0)
            {
                Location.Y = 0;
            }
        }
        public Vector Size
        {
            get { return _size; }
        }
        public Vector CenterLocation
        {
            get
            {
                return new Vector(_location.X + (_size.X / 2), _location.Y + (_size.Y / 2));
            }
        }

        public Vector Location
        {
            get { return _location; }
        }
        public Vector Velocity
        {
            get { return _velocity; }
        }
        public Movable(Vector pLocation, Vector pAccelation, int pSquareSize)
        {
            ExplicitConstructor(pLocation, pAccelation, pSquareSize, pSquareSize);
        }

        private void ExplicitConstructor(Vector pLocation, Vector pAccelation, int Width, int Height)
        {
            _location = new Vector(pLocation.X, pLocation.Y);
            _Acceleration = new Vector(pAccelation);
            _size = new Vector(Width, Height);

        }

        public static System.Drawing.Rectangle ToRectangle( Movable M)
        {
            System.Drawing.Rectangle R = new System.Drawing.Rectangle((int)M.Location.X,
                (int) M.Location.Y,
                (int) M.Size.X,
                 (int)M.Size.Y);


            return R;
        }
        public bool IsInTersectWith(Movable M)
        {
            Rectangle R1 = ToRectangle(this);
            Rectangle R2 = ToRectangle(M);
            if (R1.IntersectsWith(R2))
            {
                return true;
            }

            return false;
        }
        public void SetDie()
        {
            _IsDie = true;
        }
        public Movable(Vector pLocation, Vector pAccelation, int Width, int Height)
        {
            ExplicitConstructor(pLocation, pAccelation, Width, Height);
        }
    }
}
