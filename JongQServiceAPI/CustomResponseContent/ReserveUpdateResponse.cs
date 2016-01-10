using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JongQServiceAPI.CustomResponseContent
{
    public class ReserveUpdateResponse
    {
        public int CurrentQueue { get; set; }
        public int YourQueue { get; set; }
        public string QueueType { get; set; }
        public bool Error { get; set; }
    }
}