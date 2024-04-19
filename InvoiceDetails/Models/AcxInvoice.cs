using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceDetails.Models
{
    public class AcxInvoice
    {
        public string ID { get; set; }
        public string SECRETKEY { get; set; }
        public string BLOBPATH { get; set; }
       public DateTime EXPIRYDATE { get; set; }
    }
}