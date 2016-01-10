using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JongQServiceAPI.Models
{
    public class BonChonTable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public int QueueNum { get; set; }
        public DateTime ReserveTime { get; set; }
        public DateTime? ArriveTime { get; set; }
        public string Status { get; set; }
        public string QueueType { get; set; }
        //check for remove to history table ( accept,skip,cancel = true | waiting = false)
        public bool QueueCheck { get; set; }
        public string ResBranch { get; set; }
        public string ReserveLocation { get; set; }
    }
}