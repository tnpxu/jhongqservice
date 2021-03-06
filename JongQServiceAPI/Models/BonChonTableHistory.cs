﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JongQServiceAPI.Models
{
    public class BonChonTableHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string QueueNum { get; set; }
        public DateTime ReserveTime { get; set; }
        public DateTime ArriveTime { get; set; }
        public string Status { get; set; }
        public string QueueType { get; set; }
        public bool QueueCheck { get; set; }
        public string ResBranch { get; set; }
        public string ReservLocation { get; set; }
    }
}