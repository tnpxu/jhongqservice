using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace JongQServiceAPI.Models
{
    public class UserDbContext 
        : DbContext
    {
        public UserDbContext()
            : base("JongQDB")
        {
            Database.SetInitializer<UserDbContext>(new UserDbInitializer()); 
        }

        public DbSet<User> Users { get; set; } 
    }
}