using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JongQServiceAPI.Models
{
    //clear database every time cuz DropCreateDatabaseAlways
    public class UserDbInitializer
        : DropCreateDatabaseIfModelChanges<UserDbContext>
    {
        protected override void Seed(UserDbContext context)
        {
            new List<User> {

            new User(){ Username = "1@hotmail.com", Password = "12345", Nickname = "nick", 
                Img = "haha", Tel = "088-128-9292", Gender = "Male", BirthDate = DateTime.Now, RegisTime = DateTime.Now, Point = 0,ReservePoint = 3,PanaltyTime = null,KeyNoti = "none",IsReserve = false,Token = "" },
            new User(){ Username = "2@hotmail.com", Password = "12345", Nickname = "nick", 
                Img = "haha", Tel = "088-128-9292", Gender = "Male", BirthDate = DateTime.Now, RegisTime = DateTime.Now, Point = 0,ReservePoint = 3,PanaltyTime = null,KeyNoti = "none",IsReserve = false,Token = "" },
            new User(){ Username = "3@hotmail.com", Password = "12345", Nickname = "nick", 
                Img = "haha", Tel = "088-128-9292", Gender = "Male", BirthDate = DateTime.Now, RegisTime = DateTime.Now, Point = 0,ReservePoint = 3,PanaltyTime = null,KeyNoti = "none",IsReserve = false,Token = "" },
            new User(){ Username = "4@hotmail.com", Password = "12345", Nickname = "nick", 
                Img = "haha", Tel = "088-128-9292", Gender = "Male", BirthDate = DateTime.Now, RegisTime = DateTime.Now, Point = 0,ReservePoint = 3,PanaltyTime = null,KeyNoti = "none",IsReserve = false,Token = "" },
            new User(){ Username = "5@hotmail.com", Password = "12345", Nickname = "nick", 
                Img = "haha", Tel = "088-128-9292", Gender = "Male", BirthDate = DateTime.Now, RegisTime = DateTime.Now, Point = 0,ReservePoint = 3,PanaltyTime = null,KeyNoti = "none",IsReserve = false,Token = "" }
        }.ForEach(user => context.Users.Add(user));

        context.SaveChanges();

        }
    }
}