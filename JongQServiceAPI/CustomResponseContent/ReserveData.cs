using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JongQServiceAPI.CustomResponseContent
{
    public class ReserveData
    {
        public string ResName { get; set; }
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public string QueueType { get; set; }
        public string ResBranch { get; set; }
        public string Token { get; set; }
        public int CurrentUserQueue { get; set; }
    }
}