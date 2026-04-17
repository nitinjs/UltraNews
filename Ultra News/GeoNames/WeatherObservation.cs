using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace BenjaminSchroeter.GeoNames
{

    //<observation>
    //<observation>
    //LESO 251630Z 01003KT 330V050 9999 FEW028 SCT100 16/12 Q1030
    //</observation>
    //<observationTime>2008-10-25 16:30:00</observationTime>
    //<stationName>San Sebastian / Fuenterrabia</stationName>
    //<ICAO>LESO</ICAO>
    //<countryCode>ES</countryCode>
    //<elevation>8</elevation>
    //<lat>43.35</lat>
    //<lng>-1.8</lng>
    //<temperature>16</temperature>
    //<dewPoint>12</dewPoint>
    //<humidity>77</humidity>
    //<clouds>few clouds</clouds>
    //<weatherCondition>n/a</weatherCondition>
    //<hectoPascAltimeter>1030.0</hectoPascAltimeter>
    //<windDirection>10.0</windDirection>
    //<windSpeed>03</windSpeed>
    //</observation>
    [DebuggerDisplay("{StationName}: {Temperature}°, {Clouds}, {WeatherCondition}")]
    public class WeatherObservation : GeonameDataBase
    {
        #region properties
        [Category("Names")]
        public string Observation { get; private set; }
        [Category("Names")]
        public string StationName { get; private set; }
        [Category("Codes")]
        public string Icao { get; private set; }
        [Category("Codes")]
        public string CountryCode { get; private set; }

        [Category("Data")]
        public string Clouds { get; private set; }
        [Category("Data")]
        public string WeatherCondition { get; private set; }

        [Category("Position")]
        public decimal Elevation { get; private set; }
        [Category("Position")]
        public decimal Latitude { get; private set; }
        [Category("Position")]
        public decimal Longitude { get; private set; }
        
        [Category("Data")]
        public decimal Temperature { get; private set; }
        [Category("Data")]
        public decimal DewPoint { get; private set; }
        [Category("Data")]
        public decimal Humidity { get; private set; }
        [Category("Data")]
        public decimal HectoPascAltimeter { get; private set; }
        [Category("Data")]
        public decimal WindDirection { get; private set; }
        [Category("Data")]
        public decimal WindSpeed { get; private set; }

        [Category("Data")]
        public DateTime ObservationTime { get; private set; }
        #endregion

        public WeatherObservation(string genonameXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(genonameXml);

            if (doc.DocumentElement == null)
                return;

            XmlNodeList root = doc.GetElementsByTagName("observation");
            if ( root.Count == 0 )
                return;

            xml = (XmlElement) root[0];

            Init();
        }
        public WeatherObservation(XmlElement geoname)
        {
            xml = geoname;
            Init();
        }

        private void Init()
        {
            this.Observation = GetElement(xml, "observation");
            this.StationName = GetElement(xml, "stationName");
            this.Icao = GetElement(xml, "ICAO");
            this.CountryCode = GetElement(xml, "countryCode");
            this.Clouds = GetElement(xml, "clouds");
            this.WeatherCondition = GetElement(xml, "weatherCondition");


            this.Latitude = ToDecimal(GetElement(xml, "lat"));
            this.Longitude = ToDecimal(GetElement(xml, "lng"));
            this.Elevation = ToDecimal(GetElement(xml, "elevation"));
            this.Temperature = ToDecimal(GetElement(xml, "temperature"));
            this.DewPoint = ToDecimal(GetElement(xml, "dewPoint"));
            this.Humidity = ToDecimal(GetElement(xml, "humidity"));
            this.HectoPascAltimeter = ToDecimal(GetElement(xml, "hectoPascAltimeter"));
            this.WindDirection = ToDecimal(GetElement(xml, "windDirection"));
            this.WindSpeed = ToDecimal(GetElement(xml, "windSpeed"));

            this.ObservationTime = ToDataTime(GetElement(xml, "observationTime"));
        }
    }
}
