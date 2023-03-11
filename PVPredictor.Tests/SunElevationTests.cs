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

        sunElevation.CalcJulianDay();

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
}