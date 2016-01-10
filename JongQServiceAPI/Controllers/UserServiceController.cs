using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using JongQServiceAPI.Models;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using System.Web.Http.ModelBinding;
using JongQServiceAPI.CustomResponseContent;
using System.Text.RegularExpressions;

namespace JongQServiceAPI.Controllers
{
    public class UserServiceController : ApiController
    {
        private UserRepository Repository { get; set; }
        // GET: Users
        public UserServiceController()
        {
            Repository = new UserRepository();
        }

        //public IEnumerable<User> GetUsers()
        //{
        //    return Repository.Users;
        //}

        [HttpPost]
        [ActionName("Login")]
        public async Task<RegisterResponse> Login(LoginData data)
        {
            RegisterResponse resp = new RegisterResponse();
            User getUser = (from p in Repository.Users
                            where p.Username == data.username && p.Password == data.password
                            select p).FirstOrDefault();
            if (!(getUser == null))
            {
                resp.Error = false;
                resp.UserId = getUser.Id;
                resp.Nickname = getUser.Nickname;
                resp.Username = getUser.Username;
                resp.Token = await Repository.SaveTokenAsync(getUser.Id);
            }
            else
            {
                resp.Error = true;
                var result = new List<Error>();
                Error errorChunk = new Error("login.Fail", "Invalid User or Password");
                result.Add(errorChunk);
                resp.ErrorMsg = result;
            }

            return resp;
        }

        //[HttpPost]
        //[ActionName("Test")]
        //public IEnumerable<User> GetUsers()
        //{
        //    return Repository.Users;
        //}

        [HttpPost]
        [ActionName("Register")]
        public async Task<RegisterResponse> Register(RegisterData data)
        {
            //check e-mail must not be duplicate
            User checkUsername = (from p in Repository.Users
                                  where p.Username == data.Username
                                  select p).FirstOrDefault();

            if (!(checkUsername == null))
            {
                // can't register e-mail duplicated
                RegisterResponse temp = new RegisterResponse();
                temp.Error = true;
                //temp.Username = "This Username Has Been Used";
                var result = new List<Error>();
                Error errorChunk = new Error("Username", "This Username Has Been Used");
                result.Add(errorChunk);
                temp.ErrorMsg = result;
                return temp;
            }
            else
            {
                bool isvalid = true;

                RegisterResponse temp = new RegisterResponse();
                var result = new List<Error>();
                
                //temp.ErrorMsg = AllErrors(this.ModelState);

                if (string.IsNullOrEmpty(data.Username))
                {
                    Error errorChunk = new Error("Username", "Required Username");
                    result.Add(errorChunk);
                    isvalid = false;
                }
                if (!string.IsNullOrEmpty(data.Username))
                {
                    string emailRegex = @"^[A-Za-z0-9](([_.-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)*).([A-Za-z]{2,})$";
                    Regex re = new Regex(emailRegex);
                    if (!re.IsMatch(data.Username))
                    {
                        Error errorChunk = new Error("Username", "Username must be Email");
                        result.Add(errorChunk);
                        isvalid = false;
                    }
                }

                if (string.IsNullOrEmpty(data.Password))
                {
                    Error errorChunk = new Error("Password", "Required Password");
                    result.Add(errorChunk);
                    isvalid = false;
                }
                if (!string.IsNullOrEmpty(data.Password))
                {
                    if ((data.Password.Length > 8 || data.Password.Length < 4))
                    {
                        Error errorChunk = new Error("Password", "Password must be 4-8 Characters");
                        result.Add(errorChunk);
                        isvalid = false;
                    }
                }

                if (string.IsNullOrEmpty(data.Gender))
                {
                    Error errorChunk = new Error("Gender", "Required Gender");
                    result.Add(errorChunk);
                    isvalid = false;
                }

                if (string.IsNullOrEmpty(data.Nickname))
                {
                    Error errorChunk = new Error("Nickname", "Required Name");
                    result.Add(errorChunk);
                    isvalid = false;
                }
                if (!string.IsNullOrEmpty(data.Nickname))
                {
                    if (!(data.Nickname.Length <= 25))
                    {
                        Error errorChunk = new Error("Password", "Name must less than 25 Characters");
                        result.Add(errorChunk);
                        isvalid = false;
                    }
                }

                temp.ErrorMsg = result;

                if (isvalid)
                {
                    User user = new User();
                    user.Nickname = data.Nickname;
                    user.Username = data.Username;
                    user.Password = data.Password;
                    user.Tel = data.Tel;
                    user.Gender = data.Gender;
                    user.BirthDate = DateTime.Now;
                    user.RegisTime = DateTime.Now;
                    user.Point = 0;
                    user.ReservePoint = 3;
                    user.PanaltyTime = null;
                    user.KeyNoti = "none";

                    //List<UserFav> Fav = new List<UserFav>();
                    //UserFav userFav = new UserFav();
                    //userFav.ResBranch = "none";
                    //userFav.ResName = "none";
                    //Fav.Add(userFav);

                    //user.FavRes = null; 
                    user.IsReserve = false;
                    await Repository.SaveUserAsync(user);
                    RegisterResponse tempresp = new RegisterResponse();
                    tempresp.Error = false;
                    tempresp.ErrorMsg = null;
                    //then login
                    User getUser = (from p in Repository.Users
                                    where p.Username == user.Username && p.Password == user.Password
                                    select p).FirstOrDefault();
                    tempresp.UserId = getUser.Id;
                    tempresp.Nickname = getUser.Nickname;
                    tempresp.Username = getUser.Username;
                    tempresp.Token = await Repository.SaveTokenAsync(getUser.Id);

                    //return that success registered
                    return tempresp;

                }
                else
                {
                    temp.Error = true;
                    return temp;

                }

            }

            }

        }

        //public static List<Error> AllErrors(ModelStateDictionary modelState)
        //{
        //    var result = new List<Error>();
        //    var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
        //                                    .Select(x => new { x.Key, x.Value.Errors });

        //    foreach (var erroneousField in erroneousFields)
        //    {
        //        var fieldKey = erroneousField.Key;
        //        var fieldErrors = erroneousField.Errors
        //                           .Select(error => new Error(fieldKey, error.ErrorMessage));
        //        result.AddRange(fieldErrors);
        //    }

        //    return result;
        //}

    

}
