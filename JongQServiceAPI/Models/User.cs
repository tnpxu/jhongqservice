using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;
using JongQServiceAPI.Infrastructure;

namespace JongQServiceAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string Nickname { get; set; }
        public string Img { get; set; }
        public string Tel { get; set; }
        
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegisTime { get; set; }
        public long Point { get; set; }
        public int ReservePoint { get; set; }
        public DateTime? PanaltyTime { get; set; }
        public String KeyNoti { get; set; }
        //public List<UserFav> FavRes { get; set; }
        public bool IsReserve { get; set; }
        public string Token { get; set; }

    }
}