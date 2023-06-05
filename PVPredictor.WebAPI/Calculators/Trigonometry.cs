namespace PVPredictor.WebAPI.Calculators
{
    public class Trigonometry
    {
        public static double ConvertDegreesToRadians(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }
        public static double ConvertRadiansToDegrees(double radians)
        {
            return (180 / Math.PI) * radians;
        }
    }
}
