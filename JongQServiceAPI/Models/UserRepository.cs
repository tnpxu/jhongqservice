using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using JongQServiceAPI.Token;
using FluentDate;
using FluentDateTime;
using FluentDateTimeOffset;

namespace JongQServiceAPI.Models
{
    public class UserRepository
        : IRepository
    {
        private UserDbContext context = new UserDbContext();
        public IEnumerable<User> Users { get { return context.Users; } }

        public async Task<int> SaveUserAsync(User user)
        {
            /* i didn't validate register input yet */
            if (user.Id == 0)
            {
                context.Users.Add(user);
            }
            
            return await context.SaveChangesAsync();
        }

        public async Task<int> EditUserAsync(User user)
        {
            User dbEntry = context.Users.Find(user.Id);
            if (dbEntry != null)
            {
                dbEntry.Username = user.Username;
                dbEntry.Password = user.Password;
                dbEntry.Nickname = user.Nickname;
                dbEntry.Img = "blank";
                dbEntry.Tel = user.Tel;
                dbEntry.Gender = user.Gender;
                dbEntry.BirthDate = user.BirthDate;
                               
            }

            return await context.SaveChangesAsync();
        }

        public async Task<User> DeleteUserAsync(int Id)
        {
            User dbEntry = context.Users.Find(Id);
            if (dbEntry != null)
            {
                context.Users.Remove(dbEntry);
            }
            await context.SaveChangesAsync();
            return dbEntry;
        }

        public async Task<string> SaveTokenAsync(int Id)
        {
            User dbEntry = context.Users.Find(Id);

            TokenObject temp = new TokenObject();
            string tk = temp.GetUniqueKey();

            if (dbEntry != null)
            {
                dbEntry.Token = tk;
            }

            await context.SaveChangesAsync();
            return tk; 
        }

        public async Task<int> SaveRegisPushNotiIdAsync(int Id, String KeyNoti)
        {
            User dbEntry = context.Users.Find(Id);

            if (dbEntry != null)
            {
                dbEntry.KeyNoti = KeyNoti;
            }

            return await context.SaveChangesAsync();
        }

        public async Task<int> ChangeIsReserveAsync(int Id)
        {
            User dbEntry = context.Users.Find(Id);

            if (dbEntry != null)
            {
                if (dbEntry.IsReserve)
                {
                    dbEntry.IsReserve = false;
                }
                else
                {
                    dbEntry.IsReserve = true;
                }
            }

            return await context.SaveChangesAsync();            
        }

        /******* update reserve point *******/

        public async Task<int> UpdateReservePoint(int Id,int reservepoint)
        {
            User dbEntry = context.Users.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.ReservePoint = reservepoint;
            }

            return await context.SaveChangesAsync(); 
        }

        /******* end update reserve point *******/

        public async Task<int> SetPanaltyTime(int Id)
        {
            User dbEntry = context.Users.Find(Id);
            if (dbEntry != null)
            {
                dbEntry.PanaltyTime = DateTime.Now + 30.Minutes();
            }

            return await context.SaveChangesAsync();
        }
    }


}