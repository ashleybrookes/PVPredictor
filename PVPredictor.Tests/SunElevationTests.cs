using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolarCalculator;
namespace solarcalculatorapi.test;

[TestClass]
public class SunElevationTests
{
    [TestMethod]
    public void TestJulianDay()
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
    public void TestJulianDay_Date()
    {

        LocationSolarData locationSolarData = new LocationSolarData();
        locationSolarData.dt = DateTime.Parse("21/6/2010 00:06");
        
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expected_julianDay_Date = 2455368.50416667;

        Assert.AreEqual(expected_julianDay_Date, Math.Round(sunElevation.GetJulianDay_Date(locationSolarData.dt),8, MidpointRounding.AwayFromZero));

    }
    [TestMethod]
    public void TestJulianDay_TimeZoneOffset()
    {

        LocationSolarData locationSolarData = new LocationSolarData();
        locationSolarData.TimeZoneOffset = -7;

        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expected_julianDay_TimeZoneOffset = -0.2916666667;

        Assert.AreEqual(expected_julianDay_TimeZoneOffset, Math.Round(sunElevation.GetJulianDay_TimeZone(locationSolarData.TimeZoneOffset), 10, MidpointRounding.AwayFromZero));

    }

    [TestMethod]
    public void TestJulianCentury()
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
    public void TestGeomMeanLongSun_deg()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double testJulianCentury = 0.1046898243212430;

        double expectedGeomMeanLongSun = 89.38073226;
        //= 89.38073226;


        Assert.AreEqual(expectedGeomMeanLongSun, Math.Round(sunElevation.GetGeomMeanLongSun_deg(testJulianCentury) ,8, MidpointRounding.AwayFromZero));
    }

    [TestMethod]
    public void TestGeomMeanAnomSun_deg()
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
}