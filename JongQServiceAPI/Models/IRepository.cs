using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JongQServiceAPI.Models
{
    interface IRepository
    {
        IEnumerable<User> Users { get; }
        Task<int> SaveUserAsync(User user);
        Task<User> DeleteUserAsync(int Id);
    }
}
