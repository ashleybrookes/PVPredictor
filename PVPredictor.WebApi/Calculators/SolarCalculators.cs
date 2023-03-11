

namespace SolarCalculator
{
    public class SunElevation
    {

        //need to store the data of all the values
        private static double JulianNumDayOffset = 2415018.5;
        //
        private static double JulianCenturyUnit = 2451545;

        private static double CenturyNumberOfDays = 36525;
        //TODO accept time zone now its alwa
        public LocationSolarData InputData { get; set; }
        public double JulianDay { get; set; }

        public void SetJulianDay()
        {
            this.JulianDay = CalcJulianDay_Date(this.InputData.dt) - CalcJulianDay_TimeZone(this.InputData.TimeZoneOffset);
        }

        public double CalcJulianDay_Date(DateTime dt)
        {
            //excel number of days for the julian offset from normaldate
            //2415018.5
            return dt.ToOADate() + JulianNumDayOffset;
            
        }
        public double CalcJulianDay_TimeZone(double timeZoneOffset)
        {
            return timeZoneOffset / 24;
        }

        public double JulianCentury { get; set; }

        //todo make this none dependant on julianDay
        public void SetJulianCentury()
        {
            //TODO find out what this const is

            this.JulianCentury = CalcJulianCentury(this.JulianDay);
        }
        public double CalcJulianCentury(double JulianDay)
        {
            double juliandayoffset = 2451545;
            return (JulianDay - juliandayoffset) / CenturyNumberOfDays;
        }
        
        public void SetGeomMeanLon_degSun()
        {
            this.GeomMeanLongSun_deg = CalcGeomMeanLongSun_deg(this.JulianCentury);

        }
        //=MOD(280.46646+I2*(36000.76983+I2*0.0003032),360)
        public double CalcGeomMeanLongSun_deg(double JulianCentury)
        {
            return (280.46646+ JulianCentury * (36000.76983+ JulianCentury * 0.0003032)) % 360;
        }

        private double GeomMeanLongSun_deg;
        private double GeomMeanAnomSun;
        private double EccentEarthOrbit;
        private double SunEqOfCtr;
        private double SunTrueLong;
        private double SunTrueAnom;
        private double SunRadVector;
        private double SunAppLong_deg;
        private double MeanObliqEcliptic_deg;
        private double ObliqCorr_deg;
        private double SunRtAscen_deg;
        private double SunDeclin_deg;
        private double VarY;
        private double EqOfTime_minutes;
        private double HASunrise_deg;
        private DateTime SolarNoon_LST;
        private DateTime SunriseTime_LST;
        private DateTime SunsetTime_LST;
        private double SunlightDuration_minutes;
        private double TrueSolarTime_min;
        private double HourAngle_deg;
        private double SolarZenithAngle_deg;
        private double SolarElevationAngle_deg;
        private double ApproxAtmosphericRefraction_deg;
        private double SolarElevationCorrectedForAtmRefraction_deg;
        private double SolarAzimuthAngle_degCwFromN;

        public SunElevation(LocationSolarData locationData)
        {
            this.InputData = locationData;
        }


    }

    public class AngleSunCalculator
    {

    }

}