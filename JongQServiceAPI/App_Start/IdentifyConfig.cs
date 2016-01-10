using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(JongQServiceAPI.IdentifyConfig))] 
namespace JongQServiceAPI
{
    public class IdentifyConfig
    {
        public void Configuration(IAppBuilder app) { }
    }
}