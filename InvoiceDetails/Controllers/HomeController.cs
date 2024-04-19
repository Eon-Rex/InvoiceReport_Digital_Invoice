using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using InvoiceDetails.Models;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Sockets;
using System.Web.UI.WebControls;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using InvoiceDetails.AppCode;
using System.Net;

namespace InvoiceDetails.Controllers
{
    public class ReaponseData
    {
        public int status { get; set; }
        public string url { get; set; }
    }
   
    public class HomeController : Controller
    {
       

        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection"]);
        string APIKEY = ConfigurationManager.AppSettings["APIKEY"].ToString();
        string KEYVALUE = ConfigurationManager.AppSettings["KEYVALUE"].ToString();
        string APIURL = ConfigurationManager.AppSettings["APIURL"].ToString();
        public ActionResult ViewInvoice(string ID)
        { 
            AcxInvoice ObjInvoice = new AcxInvoice();
            DataSet ds = new DataSet();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                using (SqlCommand cmd = new SqlCommand("USP_GET_INVOICE", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SECRETKEY", ID);
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToDateTime(ds.Tables[0].Rows[0]["EXPIRYDATE"].ToString()) >= DateTime.Today)
                        {
                            using (var client = new HttpClient())
                            {
                                string PDFUrl = ds.Tables[0].Rows[0]["BLOBPATH"].ToString();

                                string filePath = PDFUrl.Replace("%5C", "\\");

                                client.DefaultRequestHeaders.Add("FilePath", filePath);

                                client.DefaultRequestHeaders.Add(APIKEY, KEYVALUE);
                                HttpResponseMessage response = client.PostAsync(APIURL, null).Result;

                                if (response.IsSuccessStatusCode)
                                {
                                    string result = response.Content.ReadAsStringAsync().Result;

                                    var info = JsonConvert.DeserializeObject<ReaponseData>(result);
                                    byte[] file = Convert.FromBase64String(info.url);
                                    return File(file, "application/pdf", "invoice.pdf");
                                }
                                else
                                {
                                    return Content("<script language='javascript' type='text/javascript'> alert('Invoice url expired');</script>");
                                }

                            }
                            // byte[] fileContents = default;
                            // return File(fileContents, "application/pdf", "invoice"); 
                        }
                        else
                        {
                            return Content("<script language='javascript' type='text/javascript'> alert('Invoice url expired');</script>");
                        }
                    }
                    else
                    {
                        return Content("<script language='javascript' type='text/javascript'> alert('Invalid URL');</script>");

                    }
                }
                // byte[] fileContents = default;
                //return File(fileContents, "application/pdf", "invoice");
                //string filePathnew = "~Content/files/" + "invoice.pdf";
                //Response.AddHeader("Content-Disposition",  "invoice.pdf");
                //return File(filePathnew, "application/pdf");
            }
            catch (Exception ex){
                LogFile.WriteErrorLog(ex,"Error Info");
            }
            return null;
        }

        public ContentResult ShowMessage(string strMessage)
        { 
        return Content("<script language='javascript' type='text/javascript'> alert('"+ strMessage + "');</script>");
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}