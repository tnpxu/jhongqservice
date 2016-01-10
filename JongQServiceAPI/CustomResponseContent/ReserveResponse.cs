using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JongQServiceAPI.CustomResponseContent
{
    public class ReserveResponse
    {
        public bool Error { get; set; }
        public string MsgEng { get; set; }
        public string MsgThai { get; set; }
    }
}