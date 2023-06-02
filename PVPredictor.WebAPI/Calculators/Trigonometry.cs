namespace PVPredictor.WebAPI.Calculators
{
    public class Trigonometry
    {
        public static double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }
    }
}
