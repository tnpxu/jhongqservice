using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JongQServiceAPI.CustomResponseContent
{
    public class AcceptQueueData
    {
        //UserId of standby status
        public int UserId { get; set; }
        public string ResName { get; set; }
        public string ResBranch { get; set; }
        public string QueueType { get; set; }
    }
}