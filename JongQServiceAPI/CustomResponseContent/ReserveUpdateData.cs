﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JongQServiceAPI.CustomResponseContent
{
    public class ReserveUpdateData
    {
        public string ResName { get; set; }
        public string ResBranch { get; set; }
        public string QueueType { get; set; }
        public string Token { get; set; }
    }
}