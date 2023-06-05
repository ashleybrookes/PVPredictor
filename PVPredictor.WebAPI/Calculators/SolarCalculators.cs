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
            this.JulianDay = GetJulianDay_Date(InputData.dt) - GetJulianDay_TimeZone(InputData.TimeZoneOffset);
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
        //=23+(26+((21.448-I2*(46.815+I2*(0.00059-I2*0.001813))))/60)/60
        private double MeanObliqEcliptic_deg;
        public void SetMeanObliqEcliptic_deg()
        {
            MeanObliqEcliptic_deg = GetMeanObliqEcliptic_deg(JulianCentury);
        }
        public double GetMeanObliqEcliptic_deg(double JulianCentury)
        {
            return 23 + (26 + ((21.448 - JulianCentury * (46.815 + JulianCentury * (0.00059 - JulianCentury * 0.001813)))) / 60) / 60;
        }

        private double ObliqCorr_deg;
        public void SetObliqCorr_deg()
        {
            ObliqCorr_deg = GetObliqCorr_deg(JulianCentury, MeanObliqEcliptic_deg);
        }
        public double GetObliqCorr_deg(double JulianCentury, double MeanObliqEcliptic_deg)
        {
            return MeanObliqEcliptic_deg + 0.00256 * Math.Cos(ConvertDegreesToRadians(125.04 - 1934.136 * JulianCentury));
        }

        private double SunRtAscen_deg;

        public void SetSunRtAscen_deg()
        {
            SunRtAscen_deg = GetSunRtAscen_deg(ObliqCorr_deg, SunAppLong_deg);
        }
        public double GetSunRtAscen_deg(double ObliqCorr_deg, double SunAppLong_deg)
        {
            return ConvertRadiansToDegrees(Math.Atan2(Math.Cos(ConvertDegreesToRadians(ObliqCorr_deg)) * Math.Sin(ConvertDegreesToRadians(SunAppLong_deg)), Math.Cos(ConvertDegreesToRadians(SunAppLong_deg))));
        }


        private double SunDeclin_deg;
        //=DEGREES(ASIN(SIN(RADIANS(T2))*SIN(RADIANS(R2))))
        public void SetSunDeclin_deg()
        {
            SunDeclin_deg = GetSunDeclin_deg(ObliqCorr_deg, SunAppLong_deg);
        }
        public double GetSunDeclin_deg(double ObliqCorr_deg, double SunAppLong_deg)
        {
            return ConvertRadiansToDegrees(Math.Asin(Math.Sin(ConvertDegreesToRadians(ObliqCorr_deg)) * Math.Sin(ConvertDegreesToRadians(SunAppLong_deg))));
        }
        private double VarY;
        //=TAN(RADIANS(T2/2))*TAN(RADIANS(T2/2))
        public void SetVarY()
        {
            VarY = GetVarY(ObliqCorr_deg);
        }
        public double GetVarY(double ObliqCorr_deg)
        {
            return Math.Tan(ConvertDegreesToRadians(ObliqCorr_deg / 2)) * Math.Tan(ConvertDegreesToRadians(ObliqCorr_deg / 2));
        }

        private double EqOfTime_minutes;
        //=4*DEGREES(W2*SIN(2*RADIANS(K2))-2*M2*SIN(RADIANS(L2))+4*M2*W2*SIN(RADIANS(L2))*COS(2*RADIANS(K2))-0.5*W2*W2*SIN(4*RADIANS(K2))-1.25*M2*M2*SIN(2*RADIANS(L2)))
        public void SetEqOfTime_minutes()
        {
            EqOfTime_minutes = GetEqOfTime_minutes(VarY, GeomMeanAnomSun_deg, GeomMeanLongSun_deg, EccentEarthOrbit);
        }
        public double GetEqOfTime_minutes( double VarY, double GeomMeanAnomSun_deg, double GeomMeanLongSun_deg, double EccentEarthOrbit)
        {
            return 4 * ConvertRadiansToDegrees(VarY * Math.Sin(2 * ConvertDegreesToRadians(GeomMeanLongSun_deg)) - 2 * EccentEarthOrbit * Math.Sin(ConvertDegreesToRadians(GeomMeanAnomSun_deg)) + 4 * EccentEarthOrbit * VarY * Math.Sin(ConvertDegreesToRadians(GeomMeanAnomSun_deg)) * Math.Cos(2 * ConvertDegreesToRadians(GeomMeanLongSun_deg)) - 0.5 * VarY * VarY * Math.Sin(4 * ConvertDegreesToRadians(GeomMeanLongSun_deg)) - 1.25 * EccentEarthOrbit * EccentEarthOrbit * Math.Sin(2 * ConvertDegreesToRadians(GeomMeanAnomSun_deg)));
        }

        private double HASunrise_deg;
        public void SetHASunrise_deg()
        {
            HASunrise_deg = GetHASunrise_deg(SunDeclin_deg, InputData.latitude);
        }
        public double GetHASunrise_deg(double SunDeclin_deg, double Latitude)
        {
            //=DEGREES(ACOS(COS(RADIANS(90.833))/(COS(RADIANS($B$3))*COS(RADIANS(V2)))-TAN(RADIANS($B$3))*TAN(RADIANS(V2))))
            return ConvertRadiansToDegrees(Math.Acos(Math.Cos( ConvertDegreesToRadians(90.833)) / (Math.Cos(ConvertDegreesToRadians(Latitude)) * Math.Cos(ConvertDegreesToRadians(SunDeclin_deg))) - Math.Tan(ConvertDegreesToRadians(Latitude)) * Math.Tan(ConvertDegreesToRadians(SunDeclin_deg))));
        }
        private double SolarNoon;
        public void SetSolarNoon()
        {
            SolarNoon = GetSolarNoon(EqOfTime_minutes, InputData.longitude, InputData.TimeZoneOffset );
        }
        public double GetSolarNoon(double EqOfTime_minutes, double Longitude, double TimezoneOffset)
        {
            return (720 - 4 * Longitude - EqOfTime_minutes + TimezoneOffset * 60)/ 1440;
        }

        private double SunriseTime;
        public void SetSunriseTime()
        {
            SunriseTime = GetSunriseTime(SolarNoon, HASunrise_deg);
        }
        public double GetSunriseTime(double SolarNoon, double HASunrise_deg)
        {
            //=Z2-Y2*4/1440
            return SolarNoon - HASunrise_deg * 4 / 1440;
        }
        private double SunsetTime;
        public void SetSunsetTime()
        {
            SunsetTime = GetSunsetTime(SolarNoon, HASunrise_deg);
        }
        public double GetSunsetTime(double SolarNoon, double HASunrise_deg)
        {
            //=Z2+Y2*4/1440
            return SolarNoon + HASunrise_deg * 4 / 1440;
        }

        private double SunlightDuration_minutes;
        public void SetSunlightDuration_minutes()
        {
            SunlightDuration_minutes = GetSunlightDuration_minutes(HASunrise_deg);
        }
        public double GetSunlightDuration_minutes(double HASunrise_deg)
        {
            //=Z2-Y2*4/1440
            return HASunrise_deg * 8;
        }
        private double TrueSolarTime_min;
        public void SetTrueSolarTime_min()
        {
            TrueSolarTime_min = GetTrueSolarTime_min(InputData.dt, InputData.longitude, InputData.TimeZoneOffset, EqOfTime_minutes);
        }

        public double GetTrueSolarTime_min(DateTime Time_pastmidnight, double Longitude, double TimeZoneOffset, double EqofTime_minutes)
        {
            //=MOD(E2*1440+X2+4*$B$4-60*$B$5,1440)
            return (Time_pastmidnight.TimeOfDay.TotalDays  * 1440 + EqofTime_minutes + 4 * Longitude - 60 * TimeZoneOffset) % 1440;
        }

        private double HourAngle_deg;
        public void SetHourAngle_deg()
        {
            HourAngle_deg = GetHourAngle_deg(TrueSolarTime_min);
        }

        public double GetHourAngle_deg(double TrueSolarTime_min)
        {
            //=IF(AD2/4<0,AD2/4+180,AD2/4-180)
            if ((TrueSolarTime_min / 4) < 0)
            {
                return TrueSolarTime_min / 4 + 180;
            } else
            {
                return TrueSolarTime_min / 4 - 180;
            }
        }


        private double SolarZenithAngle_deg;
        public void SetSolarZenithAngle_deg()
        {
            SolarZenithAngle_deg = GetSolarZenithAngle_deg(InputData.latitude, SunDeclin_deg,HourAngle_deg);
        }
        public double GetSolarZenithAngle_deg(double Latitude, double SunDeclin_deg, double HourAngle_deg)
        {
            //=DEGREES(ACOS(SIN(RADIANS($B$3))*SIN(RADIANS(V2))+COS(RADIANS($B$3))*COS(RADIANS(V2))*COS(RADIANS(AE2))))
            return ConvertRadiansToDegrees(Math.Acos(Math.Sin(ConvertDegreesToRadians(Latitude)) * Math.Sin(ConvertDegreesToRadians(SunDeclin_deg)) + Math.Cos(ConvertDegreesToRadians(Latitude)) * Math.Cos(ConvertDegreesToRadians(SunDeclin_deg)) * Math.Cos(ConvertDegreesToRadians(HourAngle_deg))));
        }

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