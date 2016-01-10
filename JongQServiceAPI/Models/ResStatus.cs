using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JongQServiceAPI.Models
{
    public class ResStatus
    {
        public int Id { get; set; }
        public string ResName { get; set; }
        public string ResBranch { get; set; }
        public bool QueueStatus { get; set; }
        public string Region { get; set; }
        public string ResType { get; set; }

    }
}