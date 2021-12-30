using System;
using System.Collections.Generic;
using System.Text;

namespace neuronprog
{
    class davarPoneVeholek
    {
        public double x = 0;
        public double y = 0;
        public double direction = 0;
        private double radianDirection;
        public davarPoneVeholek()
        {

        }
        public void step(double steps)
        {
            radianDirection = Math.PI * (direction / 180);
            x += steps * Math.Cos(radiansOf(direction));
            y += steps * Math.Sin(radiansOf(direction));
            
            
            /*
            double newX = x + (steps / (Math.Pow(Math.Tan(radianDirection), 2) + 1));
            y = y + Math.Tan(radianDirection) * (newX - x);
            x = newX;
            */
        }
        public void turnTo(double newDirection)
        {
            direction = newDirection % 360;
        }
        public void turn(double additionalDegrees)
        {
            turnTo(direction + additionalDegrees);
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
            Console.Write("X= " + x + ", Y= " + y + ". direction: " + direction);
        }
        public static Double radiansOf(double degrees)
        {
            return (Math.PI * (degrees / 180));
        }
        public void reset()
        {
            direction = 0;
            x = 0;
            y = 0;
        }
        
        /*
         * public double directionOf(targetX, targetY){}
         */
    }
}
