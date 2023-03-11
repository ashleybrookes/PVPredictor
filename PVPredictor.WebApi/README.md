# SolarCalculatorApi - In development

The api will provide an estimate of how much electricity your solar PV system will produce at any minute of a given day. It will only need to know your city/town, PV Max, Roof inclination and date.

## How will it do this?
It will first look up the city latitude and longitude data from the city name.

It will then estimate the percentage of PV max from the elevation of the sun in the sky.

To find the elevation of the sun it will use astronomical algorthms. The alogrithms were given to made available by the Earth System Research Labitories Global Monitoring Laboratory (GML) of the National Oceanic and Atmospheric Administration.
https://gml.noaa.gov/grad/solcalc/calcdetails.html

Originally these algorthims were published by Jean Meeus.

## What will the api require as input ?
- Day
- city
- PV System Max Power
- Roof inclination / Angle of PV Panels

