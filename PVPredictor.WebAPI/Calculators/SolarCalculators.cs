using PVPredictor.WebAPI.Calculators;
using System.Runtime.CompilerServices;
using static PVPredictor.WebAPI.Calculators.Trigonometry;

namespace SolarCalculator
{
    public class SunElevation
    {
        //need to store the data of all the values
        private static double JulianNumDayOffset = 2415018.5;
        private static double JulianCenturyUnit = 2451545;
        private static double CenturyNumberOfDays = 36525;
        //TODO accept time zone now its alwa
        public LocationSolarData InputData { get; set; }
        public double JulianDay { get; set; }

        public void SetJulianDay()
        {
            this.JulianDay = GetJulianDay_Date(this.InputData.dt) - GetJulianDay_TimeZone(this.InputData.TimeZoneOffset);
        }

        public double GetJulianDay_Date(DateTime dt)
        {
            //excel number of days for the julian offset from normaldate
            //2415018.5
            return dt.ToOADate() + JulianNumDayOffset;
            
        }
        public double GetJulianDay_TimeZone(double timeZoneOffset)
        {
            return timeZoneOffset / 24;
        }

        public double JulianCentury { get; set; }

        //todo make this none dependant on julianDay
        public void SetJulianCentury()
        {
            //TODO find out what this const is

            this.JulianCentury = GetJulianCentury(this.JulianDay);
        }
        public double GetJulianCentury(double JulianDay)
        {
            double juliandayoffset = 2451545;
            return (JulianDay - juliandayoffset) / CenturyNumberOfDays;
        }

        private double GeomMeanLongSun_deg;
        public void SetGeomMeanLon_degSun()
        {
            this.GeomMeanLongSun_deg = GetGeomMeanLongSun_deg(this.JulianCentury);

        }
        //=MOD(280.46646+I2*(36000.76983+I2*0.0003032),360)
        public double GetGeomMeanLongSun_deg(double JulianCentury)
        {
            return (280.46646 + JulianCentury * (36000.76983 + JulianCentury * 0.0003032))% 360;
        }
        
        private double GeomMeanAnomSun_deg;

        public void SetGeomMeanLongSun_deg()
        {
            this.GeomMeanAnomSun_deg = GetGeomMeanAnomSun_deg(this.JulianCentury);
        }
        //=357.52911+I2*(35999.05029-0.0001537*I2)
        public double GetGeomMeanAnomSun_deg(double JulianCentury)
        {
            return 357.52911 + JulianCentury * (35999.05029 - 0.0001537 * JulianCentury);
        }

        private double EccentEarthOrbit;
        public void SetEccentEartOrbit()
        {
            EccentEarthOrbit = GetEccentEartOrbit(this.JulianCentury);
        }
        //=0.016708634-I2*(0.000042037+0.0000001267*I2)
        public double GetEccentEartOrbit(double JulianCentury)
        {
            return 0.016708634 - JulianCentury * (0.000042037 + 0.0000001267 * JulianCentury);
        }

        private double SunEqOfCtr;

        //=SIN(RADIANS(L2))*(1.914602-I2*(0.004817+0.000014*I2))+SIN(RADIANS(2*L2))*(0.019993-0.000101*I2)+SIN(RADIANS(3*L2))*0.000289
        public void SetSunEqOfCtr()
        {
            SunEqOfCtr = GetSunEqOfCtr(this.JulianCentury, this.GeomMeanAnomSun_deg);
        }
        public double GetSunEqOfCtr(double JulianCentury, double GeomMeanAnomSun_deg)
        {
            return Math.Sin(ConvertDegreesToRadians(GeomMeanAnomSun_deg)) * (1.914602 - JulianCentury * (0.004817 + 0.000014 * JulianCentury)) + Math.Sin(ConvertDegreesToRadians(2 * GeomMeanAnomSun_deg)) * (0.019993 - 0.000101 * JulianCentury) + Math.Sin(ConvertDegreesToRadians(3 * GeomMeanAnomSun_deg)) * 0.000289;
        }

        private double SunTrueLong;
        public void SetSunTrueLong()
        {
            SunTrueLong = GetSunTrueLong(this.SunEqOfCtr, this.GeomMeanLongSun_deg);
        }
        public double GetSunTrueLong(double SunEqOfCtr, double GeomMeanLongSun_deg)
        {
            return SunEqOfCtr + GeomMeanLongSun_deg;
        }

        private double SunTrueAnom;
        public void SetSunTrueAnom()
        {
            SunTrueAnom = GetSunTrueAnom(this.SunEqOfCtr, this.GeomMeanAnomSun_deg);
        }
        public double GetSunTrueAnom(double SunEqOfCtr, double GeomMeanAnomSun_deg)
        {
            return SunEqOfCtr + GeomMeanAnomSun_deg;
        }

        private double SunRadVector;
        //=(1.000001018*(1-M2*M2))/(1+M2*COS(RADIANS(P2)))
        public void SetSunRadVector()
        {
            SunRadVector = GetSunRadVector(this.EccentEarthOrbit, this.SunTrueAnom);
        }
        public double GetSunRadVector(double EccentEarthOrbit, double SunTrueAnom)
        {
            return (1.000001018 * (1 - EccentEarthOrbit * EccentEarthOrbit)) / (1 + EccentEarthOrbit * Math.Cos(ConvertDegreesToRadians(SunTrueAnom)));
        }
        private double SunAppLong_deg;
        //=O2-0.00569-0.00478*SIN(RADIANS(125.04-1934.136*I2))
        public void SetSunAppLong_deg()
        {
            SunAppLong_deg = GetSunAppLong_deg(this.SunTrueLong, this.JulianCentury);
        }
        public double GetSunAppLong_deg(double SunTrueLong, double JulianCentury)
        {
            return SunTrueLong - 0.00569 - 0.00478 * Math.Sin(ConvertDegreesToRadians(125.04 - 1934.136 * JulianCentury));
        }
        
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