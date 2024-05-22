using ExchangeRates.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;

namespace ExchangeRates.Controllers
{
    public class ExchangeRateController : ApiController
    {
        private const string TcmbUrl = "https://www.tcmb.gov.tr/kurlar/today.xml";
        [HttpGet]
        [Route("api/exchangerates")]
        public async Task<IHttpActionResult> UpdateCurrencyRates()
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {

                    string xmlData = await client.GetStringAsync(TcmbUrl);

    
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmlData);
                    XmlNodeList currencyNodes = xmlDoc.SelectNodes("/Tarih_Date/Currency");
                    var exchangeRates = new List<ExchangeRate>();


                    foreach (XmlNode currencyNode in currencyNodes)
                    {
                        string currencyName = currencyNode.SelectSingleNode("CurrencyName").InnerText;
                        string forexBuying = currencyNode.SelectSingleNode("ForexBuying").InnerText;

                        if (currencyName == "US DOLLAR" || currencyName == "EURO")
                        {
                            var exchangeRate = new ExchangeRate
                            {
                                Currency = currencyName,
                                BuyingRate = forexBuying,
                                Date = DateTime.Now
                            };
                            exchangeRates.Add(exchangeRate);
                        }

                    }


                    return Ok(exchangeRates);
                }
                }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
