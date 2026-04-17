using System;
using System.Xml;

namespace BenjaminSchroeter.GeoNames
{
    public class GeonameDataBase
    {
        protected XmlElement xml;

        protected decimal ToDecimal(string str)
        {
            if (str == "")
                return 0;

            try
            {
                return XmlConvert.ToDecimal(str);
            }
            catch
            {
                return 0;
            }
        }

        protected int ToInt(string str)
        {
            if (str == "")
                return 0;

            try
            {
                return XmlConvert.ToInt32(str);
            }
            catch
            {
                return 0;
            }
        }

        protected DateTime ToDataTime(string str)
        {
            if (str == "")
                return DateTime.MinValue;

            try
            {
                return Convert.ToDateTime(str);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        protected string GetElement(XmlElement doc, string name)
        {
            XmlNodeList elements = doc.GetElementsByTagName(name);

            if (elements.Count == 0)
                return "";

            return elements[0].InnerXml;
        }
    }
}