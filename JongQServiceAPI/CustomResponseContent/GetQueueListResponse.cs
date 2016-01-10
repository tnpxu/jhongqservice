using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JongQServiceAPI.CustomResponseContent
{
    public class GetQueueListResponse
    {
        public int TotalQueueWait { get; set; }
        public List<QueueList> QueueList { get; set; }
    }

    public class QueueList
    {
        public int UserId { get; set; }
        public int QueueNum { get; set; }
        public string QueueType { get; set; }
        public string Nickname { get; set; }
        public bool QueueCheck { get; set; }
        public string Status { get; set; }
    }
}