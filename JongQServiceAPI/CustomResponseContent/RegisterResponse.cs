using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace JongQServiceAPI.CustomResponseContent
{
    public class RegisterResponse
    {
        public bool Error { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }

        public List<Error> ErrorMsg;
                
   }

    public class Error
    {
        public Error(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }
}