using System;

namespace Utils
{
    // Class containing math functions that are needed
    public static class MathUtils
    {
         public static double ToDegrees(double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }

        public static double ToRadians(double degrees)
        {
            double radians = degrees / (180 / Math.PI);
            return (radians);
        }
        
    }
}