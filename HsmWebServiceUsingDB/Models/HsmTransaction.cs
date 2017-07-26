using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HsmWebServiceUsingDB.Models
{
    public class HsmTransaction
    {
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        [Key]
        public Guid TransactionSeq { get; set; }
        public string ApplicationID { get; set; }
        public string ApplicationData { get; set; }
        public string rtnCode { get; set; }
        public string MethodName { get; set; }
    }
}