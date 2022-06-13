using System;
using System.Collections.Generic;
using System.Text;

namespace neuronprog
{
    class davarPoneVeholek
    {
        public double x = 0;
        public double y = 0;
        public double myDirection = 0;
        public davarPoneVeholek()
        {

        }
        public double direction()
        {
            return myDirection;
        }
        public void direct(double newDirection)
        {
            myDirection = inPositiveAngel(newDirection);
        }
        public void step(double steps)
        {
            x += steps * Math.Cos(radiansOf(myDirection));
            y += steps * Math.Sin(radiansOf(myDirection));
        }
        public void turnTo(double newDirection)
        {
            myDirection = newDirection % 360;
        }
        public void turn(double additionalDegrees)
        {
            turnTo(myDirection + additionalDegrees);
        }
        public void goTo(Double newX, double newY)
        {
            x = newX;
            y = newY;
        }
        public bool isBetween(double x1, double y1, double x2, double y2)
        {
            return (((x > x1 && x < x2)|| (x < x1 && x > x2)) && ((y > y1 && y < y2) || (y < y1 && y > y2)));
        }
        public double distanceFrom(double x1, double y1)
        {
            return(Math.Sqrt(Math.Pow(x1 - x, 2) + Math.Pow(y1 - y, 2)));
        }
        public void print()
        {
            Console.Write("X= " + x + ", Y= " + y + ". direction: " + myDirection);
        }
        public static Double radiansOf(double degrees)
        {
            return (Math.PI * (degrees / 180));
        }
        public void reset()
        {
            myDirection = 0;
            x = 0;
            y = 0;
        }
        public double directionTo(double targetX, double targetY)
        {
            double theDirection = 180 * Math.Atan((targetY - y) / (targetX - x)) / Math.PI;
            if(x > targetX)
            {
                theDirection += 180;
            }
            return (inPositiveAngel(theDirection));
        }
        public static double inPositiveAngel(double drctn)
        {
            drctn = drctn % 360;
            if(drctn >= 0)
            {
                return drctn;
            }
            return (inPositiveAngel(drctn + 360));
        }
    }
}
