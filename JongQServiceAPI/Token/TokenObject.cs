using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using System.Security.Cryptography;
using System.Text;



namespace JongQServiceAPI.Token
{
    public class TokenObject
    {
          public string GetUniqueKey()
          {
              //RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
              //byte[] randomBytes = new byte[62];
              //random.GetBytes(randomBytes);
              //string Key = "";
              //foreach (var b in randomBytes)
              //{
              //    Key = Key + b;
              //}

              //return Key;

              int maxSize  = 25 ;
              int minSize = 5 ;
              char[] chars = new char[62];
              string a;
              a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
              chars = a.ToCharArray();
              int size  = maxSize ;
              byte[] data = new byte[1];
              RNGCryptoServiceProvider  crypto = new RNGCryptoServiceProvider();
              crypto.GetNonZeroBytes(data) ;
              size =  maxSize ;
              data = new byte[size];
              crypto.GetNonZeroBytes(data);
              StringBuilder result = new StringBuilder(size) ;
              foreach(byte b in data )
              { 
                  result.Append(chars[b % (chars.Length - 1)]);             
              }

                return result.ToString();            
          }
    }
}