using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;

namespace BenjaminSchroeter.GeoNames
{
    public struct AlternateName
    {
        public string Language { get; set;}
        public string Name { get; set;}

        public override string ToString()
        {
            return Language + ": " + Name;
        }
    }

    [DebuggerDisplay("{Name} [{Id}] ({Latitude} / {Longitude})")]
    public class Geoname : GeonameDataBase
    {
        #region properties
        [Category("Names")]
        public string Name { get; private set; }
        [Category("Names")]
        public string CountryName { get; private set; }
        [Category("Names")]
        public string AdminName1 { get; private set; }
        [Category("Names")]
        public string AdminName2 { get; private set; }

        [Category("Codes")]
        public string Fcl { get; private set; }
        [Category("Codes")]
        public string Fcode { get; private set; }
        [Category("Codes")]
        public string FclName { get; private set; }
        [Category("Codes")]
        public string FcodeName { get; private set; }
        [Category("Codes")]
        public string AdminCode1 { get; private set; }
        [Category("Codes")]
        public string AdminCode2 { get; private set; }
        [Category("Codes")]
        public string ContinentCode { get; private set; }
        [Category("Codes")]
        public string CountryCode { get; private set; }

        [Category("Position")]
        public decimal Latitude { get; private set; }
        [Category("Position")]
        public decimal Longitude { get; private set; }
        [Category("Position")]
        public decimal Distance { get; private set; }
       
        [Category("Data")]
        public int Population { get; private set; }
        [Category("Data")]
        public string Timezone { get; private set; }
        [Category("Data")]
        public decimal TimezoneOffsetDst { get; private set; }
        [Category("Data")]
        public decimal TimezoneOffsetGmt { get; private set; }

        public int Id { get; private set; }
        #endregion

        public Geoname(string genonameXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(genonameXml);

            if (doc.DocumentElement == null)
                return;

            XmlNodeList root = doc.GetElementsByTagName("geoname");
            if ( root.Count == 0 )
                return;

            xml = (XmlElement) root[0];

            Init();
        }

        public Geoname(XmlElement geoname)
        {
            xml = geoname;
            Init();
        }

        private void Init()
        {
            this.Name = GetElement(xml, "name");
            this.CountryCode = GetElement(xml, "countryCode");
            this.CountryName = GetElement(xml, "countryName");
            this.Fcl = GetElement(xml, "fcl");
            this.Fcode = GetElement(xml, "fcode");
            this.FclName = GetElement(xml, "fclName");
            this.FcodeName = GetElement(xml, "fcodeName");
            this.ContinentCode = GetElement(xml, "continentCode");

            this.AdminCode1 = GetElement(xml, "adminCode1");
            this.AdminCode2 = GetElement(xml, "adminCode2");
            this.AdminName1 = GetElement(xml, "adminName1");
            this.AdminName2 = GetElement(xml, "adminName2");

            this.Latitude = ToDecimal(GetElement(xml, "lat"));
            this.Longitude = ToDecimal(GetElement(xml, "lng"));
            this.Distance = ToDecimal(GetElement(xml, "distance"));

            this.Id = ToInt(GetElement(xml, "geonameId"));
            this.Population = ToInt(GetElement(xml, "population"));

            this.Timezone = GetElement(xml, "timezone");
            XmlNode n = xml.SelectSingleNode("timezone/@dstOffset");
            if (n != null) this.TimezoneOffsetDst = XmlConvert.ToDecimal(n.Value);
            n = xml.SelectSingleNode("timezone/@gmtOffset");
            if (n != null) this.TimezoneOffsetGmt = XmlConvert.ToDecimal(n.Value);
        }

        public IEnumerable<AlternateName> AlternateNames()
        {
            if ( xml == null )
                yield break;

            XmlNodeList nodes = xml.SelectNodes("alternateName");
            if ( nodes == null )
                yield break;

            foreach (XmlElement node in nodes)
            {
                AlternateName a = new AlternateName();
                a.Language = node.GetAttribute("lang");
                a.Name = node.InnerText;
                yield return a;
            }
        }

        public IEnumerable<Geoname> Hierarchy()
        {
            return GeoNamesOrgWebservice.Hierarchy(this);
        }
        public IEnumerable<Geoname> Hierarchy(GeoNamesDataStyle style)
        {
            return GeoNamesOrgWebservice.Hierarchy(this, style);
        }

        public IEnumerable<Geoname> Children()
        {
            return GeoNamesOrgWebservice.Children(this);
        }
        public IEnumerable<Geoname> Children(GeoNamesDataStyle style)
        {
            return GeoNamesOrgWebservice.Children(this, style);
        }
    }
}