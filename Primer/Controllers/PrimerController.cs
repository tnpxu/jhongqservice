using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Primer.Controllers
{
    public class PrimerController : ApiController
    {
        private static string TargetUrl = " http://apress.com";

        public long GetPageSize()
        {
            WebClient wc = new WebClient();
            Stopwatch sw = Stopwatch.StartNew();
            byte[] apressData = wc.DownloadData(TargetUrl);
            Debug.WriteLine("Elapse ms:{0}", sw.ElapsedMilliseconds;
            return apressData.LongLength;
        }
    }
}
