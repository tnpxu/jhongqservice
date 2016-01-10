using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JongQServiceAPI.CustomResponseContent
{
    public class ResBranchPickingResponse
    {
        public string ResBranch { get; set; }
        public string Region { get; set; }
        public bool QueueStatus { get; set; }
    }
}