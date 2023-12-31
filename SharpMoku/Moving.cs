using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace SharpMoku
{
    public class Moving
    {


        public Boolean Move(Movable MouseObject, Point Destination )
        {

            //Point MouseLocation

            // Vector dir = new Vector(MouseObject.CenterLocation);
            Vector dir = new Vector(MouseObject.Location);
            //  Vector dir = new Vector(Ball.CenterLocation);
            Vector vDestination = new Vector(Destination.X, Destination.Y);
           // Vector vMouseLocation = new Vector(MouseLocation.X, MouseLocation.Y);
            //  Vector Distanct = vMouseLocation - Ball.CenterLocation;
            Vector Distanct = vDestination  - MouseObject.Location ;
           
           // dir = vMouseLocation - dir;
            //cUtil.WriteLog("MouseLocation:" + vMouseLocation.ToString());

            //cUtil.WriteLog("MouseLocation:" + vMouseLocation.ToString());

            if (Math.Abs(Distanct.X) <= 10 &&
                Math.Abs(Distanct.Y) <= 10)
            {
                
                MouseObject.SetCenterLocation(vDestination);
                MouseObject.ClearEveryForce();
                return true;
            }
            else
            {
                Distanct.Normalize();
                //  cUtil.WriteLog("Dir:" + dir.ToString());
                //  cUtil.WriteLog("Before Update");
                //   Ball.Log();
                MouseObject.ApplyForce(Distanct);
                MouseObject.Update();
                return false;
               
            }
        }
    }
}
