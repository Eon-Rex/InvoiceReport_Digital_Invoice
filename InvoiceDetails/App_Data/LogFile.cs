using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace WeighBridgeIntigration.AppCode
{
    internal class LogFile
    {

        public static void WriteErrorLog(Exception ex, string webMethodName)
        {
            string FileName = "\\LogFiles\\ErrorLog\\" + "ErrorLog_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles"))
                { Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles"); }
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles\\ErrorLog"))
                { Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles\\ErrorLog"); }



                using (StreamWriter stwriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + FileName, true))
                {
                    stwriter.WriteLine("-------------------Error Log Start-----------as on " + DateTime.Now.ToString("hh:mm tt"));
                    stwriter.WriteLine("WebMethod Name :" + webMethodName);
                    stwriter.WriteLine("Message:" + ex.ToString());
                    stwriter.WriteLine("-------------------End----------------------------");
                }



            }
            catch (Exception)
            {
                throw;
            }



        }

        public static void WriteResponseLog(Object oObject, string webMethodName)

        {

            string FileName = "\\LogFiles\\ResponseLog\\" + "ResponseLog_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles"))
                { 
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles"); 
                }

                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles\\ResponseLog"))

                { 
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles\\ResponseLog");
                }

                using (StreamWriter stwriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + FileName, true))
                {
                    stwriter.WriteLine("-------------------Response Log Start-----------as on " + DateTime.Now.ToString("hh:mm tt"));

                    stwriter.WriteLine("WebMethod Name :" + webMethodName);

                    stwriter.WriteLine("Response:" + JsonConvert.SerializeObject(oObject));

                    stwriter.WriteLine("-------------------End----------------------------");

                }
            }
            catch (Exception)

            {
                throw;
            }
        }

        //log file for API response 
        public static void APIResponseLog(Object oObject, string webMethodName)

        {

            string FileName = "\\LogFiles\\APIResponseLog\\" + "APIResponseLog_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles");
                }

                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles\\APIResponseLog"))

                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles\\APIResponseLog");
                }

                using (StreamWriter stwriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + FileName, true))
                {
                    stwriter.WriteLine("-------------------Response Log Start-----------as on " + DateTime.Now.ToString("hh:mm tt"));

                    stwriter.WriteLine("API:" + webMethodName);

                    stwriter.WriteLine("Response:" + JsonConvert.SerializeObject(oObject));

                    stwriter.WriteLine("-------------------End----------------------------");

                }
            }
            catch (Exception)

            {
                throw;
            }
        }
        //log file for database response 
        public static void DatabaseStored(Object oObject, string webMethodName)

        {

            string FileName = "\\LogFiles\\DatabaseStored\\" + "DatabaseStored_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles");
                }

                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles\\DatabaseStored"))

                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\LogFiles\\DatabaseStored");
                }

                using (StreamWriter stwriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + FileName, true))
                {
                    stwriter.WriteLine("-------------------Response Log Start-----------as on " + DateTime.Now.ToString("hh:mm tt"));

                    stwriter.WriteLine("Stored Data :" + webMethodName);

                    stwriter.WriteLine("Transaction Number:" + JsonConvert.SerializeObject(oObject));

                    stwriter.WriteLine("-------------------End----------------------------");

                }
            }
            catch (Exception)

            {
                throw;
            }
        }



    }
}
