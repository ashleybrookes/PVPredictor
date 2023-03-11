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

        Assert.AreEqual(expected_julianDay_Date, Math.Round(sunElevation.CalcJulianDay_Date(locationSolarData.dt),8, MidpointRounding.AwayFromZero));

    }
    [TestMethod]
    public void TestJulianDay_TimeZoneOffset()
    {

        LocationSolarData locationSolarData = new LocationSolarData();
        locationSolarData.TimeZoneOffset = -7;

        SunElevation sunElevation = new SunElevation(locationSolarData);

        double expected_julianDay_TimeZoneOffset = -0.2916666667;

        Assert.AreEqual(expected_julianDay_TimeZoneOffset, Math.Round(sunElevation.CalcJulianDay_TimeZone(locationSolarData.TimeZoneOffset), 10, MidpointRounding.AwayFromZero));

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


        Assert.AreEqual(expectedJulianCentury, Math.Round(sunElevation.CalcJulianCentury(test_julianDay), 8, MidpointRounding.AwayFromZero));
    }

    [TestMethod]
    public void TestGeomMeanLongSun_deg()
    {
        LocationSolarData locationSolarData = new LocationSolarData();
        SunElevation sunElevation = new SunElevation(locationSolarData);

        double test_julianCentury = 0.10468982;

        double expectedGeomMeanLongSun = 89.38073226;

        Assert.AreEqual(expectedGeomMeanLongSun, sunElevation.CalcGeomMeanLongSun_deg(test_julianCentury));
    }

}