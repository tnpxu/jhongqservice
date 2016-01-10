using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JongQServiceAPI.CustomResponseContent
{
    public class CurrentQueueDetailResponse
    {
        public int TotalQueue { get; set; }
        public int QueueNum { get; set; }
        public string QueueType { get; set; }
    }
}