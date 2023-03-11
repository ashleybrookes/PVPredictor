

namespace SolarCalculator
{
    public class SunElevation
    {

        //need to store the data of all the values
        private static double JulianNumDayOffset = 2415018.5;
        //TODO accept time zone now its alwa
        public LocationSolarData InputData { get; set; }
        public double JulianDay { get; set; }

        public void CalcJulianDay()
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

        private double JulianCentury;
        private double GeomMeanLongSun;
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