using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolarCalculator;
namespace solarcalculatorapi.test;

[TestClass]
public class SunElevationTests
{
    [TestMethod]
    public void TestGetJulianDay()
    {

        LocationSolarData locationSolarData = new LocationSolarData();
        locationSolarData.dt = DateTime.Parse("21/6/2010 00:06:00.000");
        locationSolarData.TimeZoneOffset = -7;
        Console.WriteLine("locationSolarData.dt = " + locationSolarData.dt.ToShortDateString() + " " + locationSolarData.dt.ToShortTimeString());
        SunElevation sunElevation = new SunElevation(locationSolarData);

        sunElevation.SetJulianDay();

        double expected_julianDay = 2455368.795833;

        Assert.AreEqual(expected_julianDay , Math.Round(sunElevation.JulianDay, 6, MidpointRounding.AwayFromZero) );
                       
    }

    [TestMethod]
    public void TestGetJulianDay_Date()
    {

        LocationSolarData locationSolarData = new LocationSolarData();
        locationSolarData.dt = DateTime.Parse("21/6/2010 00:06");
        
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expected_julianDay_Date = 2455368.50416667;

        Assert.AreEqual(expected_julianDay_Date, Math.Round(sunElevation.GetJulianDay_Date(locationSolarData.dt),8, MidpointRounding.AwayFromZero));

    }
    [TestMethod]
    public void TestGetJulianDay_TimeZoneOffset()
    {

        LocationSolarData locationSolarData = new LocationSolarData();
        locationSolarData.TimeZoneOffset = -7;

        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expected_julianDay_TimeZoneOffset = -0.2916666667;

        Assert.AreEqual(expected_julianDay_TimeZoneOffset, Math.Round(sunElevation.GetJulianDay_TimeZone(locationSolarData.TimeZoneOffset), 10, MidpointRounding.AwayFromZero));

    }

    [TestMethod]
    public void TestGetJulianCentury()
    {
        // I should be able to create a SunEvaluation without supplying the data for testing
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        //based on 21/06/2010 0:06:0.0 with -7 timezone -7
        double test_julianDay = 2455368.795833;
        double expectedJulianCentury = 0.10468982;


        Assert.AreEqual(expectedJulianCentury, Math.Round(sunElevation.GetJulianCentury(test_julianDay), 8, MidpointRounding.AwayFromZero));
    }

    [TestMethod]
    public void TestGetGeomMeanLongSun_deg()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double testJulianCentury = 0.1046898243212430;

        double expectedGeomMeanLongSun = 89.38073226;
        //= 89.38073226;


        Assert.AreEqual(expectedGeomMeanLongSun, Math.Round(sunElevation.GetGeomMeanLongSun_deg(testJulianCentury) ,8, MidpointRounding.AwayFromZero));
    }

    [TestMethod]
    public void TestGetGeomMeanAnomSun_deg()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedGeomMeanAnomSun_deg = 4126.263359;

        double testJulianCentury = 0.1046898243212430;
;

        Assert.AreEqual(expectedGeomMeanAnomSun_deg, Math.Round( sunElevation.GetGeomMeanAnomSun_deg(testJulianCentury), 6, MidpointRounding.AwayFromZero));
    }

    [TestMethod]
    public void TestGetEccentEartOrbit()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedEccentEartOrbit = 0.016704;

        double testJulianCentury = 0.1046898243212430;
;

        Assert.AreEqual(expectedEccentEartOrbit, Math.Round(sunElevation.GetEccentEartOrbit(testJulianCentury), 6, MidpointRounding.AwayFromZero));
    }

    //only accurate to 13 decimal places, should be ok
    [TestMethod]
    public void TestGetSunEqOfCtr()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedSunEqOfCtr = 0.4454922851937;

        double testJulianCentury = 0.1046898243212430;
        double testGeomMeanAnomSun_deg = 4126.26335890714;


        Assert.AreEqual(expectedSunEqOfCtr, Math.Round(sunElevation.GetSunEqOfCtr(testJulianCentury, testGeomMeanAnomSun_deg), 13, MidpointRounding.AwayFromZero));
    }

    [TestMethod]
    public void TestGetSunTrueLong()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedSunTrueLong = 89.826224540453;

        double testSunEqOfCtr = 0.4454922851936810;
        double testGeomMeanLongSun_deg = 89.3807322552593
;

        Assert.AreEqual(expectedSunTrueLong, Math.Round(sunElevation.GetSunTrueLong(testSunEqOfCtr, testGeomMeanLongSun_deg), 12, MidpointRounding.AwayFromZero));
    }
    [TestMethod]
    public void TestGetSunTrueAnom()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedSunTrueAnom = 4126.70885119233;

        double testSunEqOfCtr = 0.4454922851936810;
        double testGeomMeanAnomSun_deg = 4126.26335890714;

        Assert.AreEqual(expectedSunTrueAnom, Math.Round(sunElevation.GetSunTrueAnom(testSunEqOfCtr, testGeomMeanAnomSun_deg), 11, MidpointRounding.AwayFromZero));
    }

    [TestMethod]
    public void TestGetSunRadVector()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedSunRadVector = 1.016242841862370;

        double testEarthEccentOrbit = 0.0167042317652282;
        double testSunTrueAnom_deg= 4126.708851192330000;

        Assert.AreEqual(expectedSunRadVector, Math.Round(sunElevation.GetSunRadVector(testEarthEccentOrbit, testSunTrueAnom_deg), 14, MidpointRounding.AwayFromZero));
    }

   
    [TestMethod]
    public void TestGetSunAppLong_deg()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedSunAppLong_deg = 89.825200228448300;

        double testJulianCentury = 0.104689824321243;
        double testSunTrueLong_deg = 89.826224540453000;

        Assert.AreEqual(expectedSunAppLong_deg  , Math.Round(sunElevation.GetSunAppLong_deg(testSunTrueLong_deg, testJulianCentury), 13, MidpointRounding.AwayFromZero));
    }

    [TestMethod]
    public void TestGetMeanObliqEcliptic_deg()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedMeanObliqEcliptic_deg = 23.437929705969;
        double testJulianCentury = 0.104689824321243;
        Assert.AreEqual(expectedMeanObliqEcliptic_deg, Math.Round(sunElevation.GetMeanObliqEcliptic_deg(testJulianCentury), 12, MidpointRounding.AwayFromZero));

    }

    [TestMethod]
    public void TestGetObliqCorr_deg()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedObliqCorr_deg = 23.4384862182944;
        double testJulianCentury = 0.104689824321243;
        double testMeanObliqEcliptic_deg = 23.437929705969;
        Assert.AreEqual(expectedObliqCorr_deg, Math.Round(sunElevation.GetObliqCorr_deg(testJulianCentury, testMeanObliqEcliptic_deg), 13, MidpointRounding.AwayFromZero));

    }

    [TestMethod]
    public void TestGetSunRtAscen_deg()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedSunRtAscen_deg = 89.809480084416;
        double testObliqCorr_deg = 23.4384862182944;
        double testSunAppLong_deg = 89.8252002284483;

        Assert.AreEqual(expectedSunRtAscen_deg, Math.Round(sunElevation.GetSunRtAscen_deg(testObliqCorr_deg, testSunAppLong_deg), 12, MidpointRounding.AwayFromZero));
    }

    [TestMethod]
    public void TestGetSunDeclin_deg()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedSunDeclin_deg = 23.438370619287;
        double testObliqCorr_deg = 23.4384862182944;
        double testSunAppLong_deg = 89.8252002284483;

        Assert.AreEqual(expectedSunDeclin_deg, Math.Round(sunElevation.GetSunDeclin_deg(testObliqCorr_deg, testSunAppLong_deg), 12, MidpointRounding.AwayFromZero));
    }


    
    [TestMethod]
    public void TestGetVarY()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedVarY = 0.043031489687857;
        double testObliqCorr_deg = 23.4384862182944;

        Assert.AreEqual(expectedVarY, Math.Round(sunElevation.GetVarY(testObliqCorr_deg), 15, MidpointRounding.AwayFromZero));  
    }

    //Accurate to 12 decimal places comparing to excel expected value
    [TestMethod]
    public void TestGetEqOfTime_minutes()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedEqOfTime_minutes = -1.715367578468;
        double testVarY= 0.043031489687857;
        double testGeomMeanAnomSun_deg = 4126.26335890714;
        double testGeomMeanLongSun_deg = 89.3807322552593;
        double testEccentEarthOrbit = 0.0167042317652282;

        Assert.AreEqual(expectedEqOfTime_minutes, Math.Round(sunElevation.GetEqOfTime_minutes(testVarY, testGeomMeanAnomSun_deg, testGeomMeanLongSun_deg, testEccentEarthOrbit), 12, MidpointRounding.AwayFromZero));
    }

    [TestMethod]
    public void TestGetHASunrise_deg()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        locationSolarData.latitude = 40;
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedHASunrise_deg = 112.610410069884;
        double testSunDeclin_deg = 23.438370619287;

        Assert.AreEqual(expectedHASunrise_deg, Math.Round(sunElevation.GetHASunrise_deg(testSunDeclin_deg, locationSolarData.latitude), 12, MidpointRounding.AwayFromZero));
    }
    [TestMethod]
    public void TestGetSolarNoon()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        locationSolarData.longitude = -105;
        locationSolarData.TimeZoneOffset = -7;
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedSolarNoon = 0.501191227485;
        double testEqOfTime_minutes = -1.715367578468;

        Assert.AreEqual(expectedSolarNoon, Math.Round(sunElevation.GetSolarNoon(testEqOfTime_minutes, locationSolarData.longitude, locationSolarData.TimeZoneOffset), 12, MidpointRounding.AwayFromZero));
    }
    [TestMethod]
    public void TestGetSunriseTime()
    {
        LocationSolarData locationSolarData = new LocationSolarData();

        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedSunriseTime = 0.188384532846;
        double testSolarNoon = 0.501191227485;
        double testHASunrise_deg = 112.610410069884;

        Assert.AreEqual(expectedSunriseTime, Math.Round(sunElevation.GetSunriseTime(testSolarNoon, testHASunrise_deg), 12, MidpointRounding.AwayFromZero));
    }

    [TestMethod]
    public void TestGetSunSetTime()
    {
        LocationSolarData locationSolarData = new LocationSolarData();

        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedSunsetTime = 0.813997922124;
        double testSolarNoon = 0.501191227485;
        double testHASunrise_deg = 112.610410069884;

        Assert.AreEqual(expectedSunsetTime, Math.Round(sunElevation.GetSunsetTime(testSolarNoon, testHASunrise_deg), 12, MidpointRounding.AwayFromZero));
    }
    [TestMethod]
    public void TestGetSunlightDuration_minutes()
    {
        LocationSolarData locationSolarData = new LocationSolarData();

        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedSunlightDuration_minutes = 900.88328055907;
        double testHASunrise_deg = 112.610410069884;

        Assert.AreEqual(expectedSunlightDuration_minutes, Math.Round(sunElevation.GetSunlightDuration_minutes(testHASunrise_deg), 11, MidpointRounding.AwayFromZero));
    }

    [TestMethod]
    public void TestGetTrueSolarTime_min()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        locationSolarData.longitude = -105;
        locationSolarData.TimeZoneOffset = -7;
        locationSolarData.dt = new DateTime(2010,6,21,0,6,0);
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedTrueSolarTime_min = 4.284632421532;
        double testEqOfTime_minutes = -1.715367578468;

        Assert.AreEqual(expectedTrueSolarTime_min, Math.Round(sunElevation.GetTrueSolarTime_min(locationSolarData.dt, locationSolarData.longitude, locationSolarData.TimeZoneOffset, testEqOfTime_minutes), 12, MidpointRounding.AwayFromZero));
    }
    [TestMethod]
    public void TestGetHourAngle_deg()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedHourAngle_deg = -178.928841894617;
        double testTrueSolarTime_min = 4.284632421532;

        Assert.AreEqual(expectedHourAngle_deg, Math.Round(sunElevation.GetHourAngle_deg(testTrueSolarTime_min), 12, MidpointRounding.AwayFromZero));
    }

    [TestMethod]
    public void TestGetSolarZenithAngle_deg()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        locationSolarData.latitude = 40;
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expectedSolarZenithAngle_deg = 116.553762119181;
        double testHourAngle_deg = -178.928841894617;
        double testSunDeclin_deg = 23.438370619287;

        Assert.AreEqual(expectedSolarZenithAngle_deg, Math.Round(sunElevation.GetSolarZenithAngle_deg(locationSolarData.latitude, testSunDeclin_deg, testHourAngle_deg), 12, MidpointRounding.AwayFromZero));
    }
}