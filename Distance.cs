using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Xml.XPath;

namespace Carpool
{
    //http://www.mapquestapi.com/directions/v2/route?key=Fmjtd%7Cluurnu6r2u%2Cbw%3Do5-9wbg54&from=177 Williams Ct, Fremont, CA&to=38623 Cherry Ln, Fremont, CA&outFormat=xml
    class Distance
    {
        string strSourceAddress;
        string strSourceCity;
        string strSourceState;
        string strDestinationAddress;
        string strDestinationCity;
        string strDestinationState;

        string strKey;
        XmlDocument xmlDoc;
        double dDistance;


        public Distance(string _sSourceAddress, string _sSourceCity, string _sSourceState, string _sDestinationAddress, string _sDestinationCity, string _sDestinationState)
        {
            this.strSourceAddress = _sSourceAddress;
            this.strSourceCity = _sSourceCity;
            this.strSourceState = _sSourceState;
            this.strDestinationAddress = _sDestinationAddress;
            this.strDestinationCity = _sDestinationCity;
            this.strDestinationState = _sDestinationState;

            this.strKey = "Fmjtd%7Cluurnu6r2u%2Cbw%3Do5-9wbg54";
            this.xmlDoc = new XmlDocument();
        }

        public double NumMiles
        {
            get
            {
                return this.dDistance;
            }
        }

        public string URL
        {
            get
            {
                string sURL = "http://www.mapquestapi.com/directions/v2/route?key=" + this.strKey + "&from=" + this.strSourceAddress + "," + this.strSourceCity + "," + this.strSourceState + "&to=" + this.strDestinationAddress + "," + this.strDestinationCity + "," + this.strDestinationState + "&outFormat=xml";
                return sURL;
            }
        }

        public int MakeRequest()
        {
            int iReturnStatus = 0;

            try
            {

                HttpWebRequest request = WebRequest.Create(this.URL) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                this.xmlDoc.Load(response.GetResponseStream());

                iReturnStatus = 0;

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return iReturnStatus;
        }

        public int ParseXml()
        {
            int iReturnStatus = 0;

            try
            {
                //XmlNode nodeDistance = this.xmlDoc.SelectSingleNode("//response/route/boundingBox/distance");
                XmlNode nodeDistance = this.xmlDoc.SelectSingleNode("//response/route/distance");
                this.dDistance = Convert.ToDouble(nodeDistance.InnerText);

                iReturnStatus = 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return iReturnStatus;
        }

        public int Run()
        {
            int iReturnStatus = 0;

            try
            {
                if (MakeRequest() == 0)
                {
                    if (ParseXml() == 0)
                    {
                        iReturnStatus = 0;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return iReturnStatus;
        }

    }
}
