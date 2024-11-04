using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using MaxMind.GeoIP2.Responses;
using MaxMind.GeoIP2;
using System.Web.Helpers;
using System.Web.Services.Description;
using System.Xml.Linq;

namespace WebTechsolution.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string ipAddressr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string ipAddress = Request.UserHostAddress;
            string browser = Request.Browser.Type;
                      
            string userAgent = Request.UserAgent;
            string referrer = Request.UrlReferrer?.ToString();

            // Use the IP from HTTP_X_FORWARDED_FOR if available
            string clientIp = string.IsNullOrEmpty(ipAddressr) ? ipAddress : ipAddressr.Split(',')[0].Trim();

            string country = GetCountryName(clientIp);

            SqlConnection con = new SqlConnection("Data Source=AKASH\\SQLEXPRESS01;Initial Catalog=NEXUSTECHTECHNOLIES;Integrated Security=True;Encrypt=False");
            SqlCommand cmd = new SqlCommand("Insert into VisitorDetails (browser,ipAddress,AddedOn,WebSiteId) values ('" + browser + "','" + ipAddress + "',getdate(),'1')", con);
            con.Open();
            int n = cmd.ExecuteNonQuery();
            if (n > 0)
            {
                ViewBag.Message = "OK";
                ModelState.Clear();
            }




            string filePath = @"C:\Projects 2024\ErrorLog.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("-------  https://www.nexustechnogies.site/  ----------");
                writer.WriteLine("Date : " + DateTime.Now.ToString());
                writer.WriteLine();

               
                   
                    writer.WriteLine("ipAddress : " + ipAddress);
                    writer.WriteLine("browser : " + browser);
                    writer.WriteLine("browser : " + Request);
                    writer.WriteLine("userAgent : " + userAgent);
                    writer.WriteLine("referrer : " + referrer);
                    writer.WriteLine("country : " + country);

                   
                
            }

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            

            return View();
        }
        private string GetCountryName(string ipAddress)
        {
            string country = "Unknown";
            try
            {
                using (var reader = new DatabaseReader("/path/to/GeoLite2-Country.mmdb"))
                {
                    CountryResponse response = reader.Country(ipAddress);
                    country = response.Country.Name;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GeoIP2 Exception: {ex.Message}");
            }
            return country;
        }


        public ActionResult Contact()
        {
           

            return View();
        }
        [HttpPost]
        public ActionResult Contact( string name ,string email, string subject,string message)
        {
            SqlConnection con = new SqlConnection("Data Source=AKASH\\SQLEXPRESS01;Initial Catalog=NEXUSTECHTECHNOLIES;Integrated Security=True;Encrypt=False");
            SqlCommand cmd = new SqlCommand("Insert into ContactMessages values ('" + name+"','"+email+"','"+subject+"','"+message+"')",con);
            con.Open();

          int n=  cmd.ExecuteNonQuery();
            if(n>0)
            {
                ViewBag.Message = "OK";
                ModelState.Clear();
            }
            

            return View();
        }
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        public ActionResult Portfolio()
        {


            return View();
        }

        public ActionResult Team()
        {


            return View();
        }

        public ActionResult Blog()
        {


            return View();
        }
        public ActionResult Services()
        {


            return View();
        }



       

    }
}