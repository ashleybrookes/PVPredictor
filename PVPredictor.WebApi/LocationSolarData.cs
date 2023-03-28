namespace SolarCalculator
{
    public class LocationSolarData
    {
        public System.DateTime dt { get; set; }

        public double TimeZoneOffset { get; set;}

        public string? City { get; set; }

        public double? latitude { get; set; }
        public double? longitude { get; set; }

        public double solarPowerWatts { get; set; }

        public double solarPercentage { get; set; }

        public double solarMaximum { get; set; }


    }
}
