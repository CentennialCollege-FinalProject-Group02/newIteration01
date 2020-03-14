using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using HappySitter.CustomExceptions;

namespace HappySitter.Utils
{
    public class GeocodeValue
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class GeocodingUtil
    {
        public static GeocodeValue ConvertAddressToGeocode(string address)
        {

            string apiKey = ConfigurationManager.AppSettings["GoogleApiKey"];
            string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false&key={1}"
                , Uri.EscapeDataString(address), apiKey);

            WebRequest request = WebRequest.Create(requestUri);
            WebResponse response = request.GetResponse();
            XDocument xdoc = XDocument.Load(response.GetResponseStream());

            //This will take only one result on the top.
            XElement result = xdoc.Element("GeocodeResponse")?.Element("result");
            if (result != null)
            {
                XElement locationElement = result.Element("geometry")?.Element("location");
                //InvalidGeocodeInfoException
                if (locationElement != null)
                {
                    GeocodeValue rtnGeocodeValue = new GeocodeValue
                    {
                        Latitude = Convert.ToDouble(locationElement.Element("lat").Value),
                        Longitude = Convert.ToDouble(locationElement.Element("lng").Value)
                    };

                    return rtnGeocodeValue;
                }
                else
                {
                    throw new InvalidGeocodeInfoException("location element not exits. Address: " + address + " \nresult:" + result.ToString());
                }
            }
            else
            {
                throw new InvalidGeocodeInfoException("GeocodeResponse result not found. Address: " + address);
            }
        }
    }
}