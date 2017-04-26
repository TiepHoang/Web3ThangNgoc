using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WCF.BussinessController.BCL;

namespace Web3ThangNgoc.Models
{
    public class UserModel
    {
        string nameSessionLogin = "SessionLogin";
        string nameCookiesLogin = "ckLogin";

        public bool CheckLogin(string Username, string inputPassword)
        {
            var ob = new UserBCL().Get(Username);
            return ob != null && ob.Password != null && Web3ThangNgoc.Models.iMD5.Vietify(inputPassword, ob.Password);
        }

        public void SetSession(UserSession uSession)
        {
            HttpContext.Current.Session[nameSessionLogin] = uSession;
        }

        public UserSession GetSession()
        {
            var s = HttpContext.Current.Session[nameSessionLogin];
            return s as UserSession;
        }

        public void clearSession()
        {
            if (GetSession() != null) HttpContext.Current.Session[nameSessionLogin] = null;
        }


        public UserSession GetUserObjectInCookie()
        {
            var ck = HttpContext.Current.Request.Cookies[nameCookiesLogin];
            if (ck == null) return null;
            string valueCookie = HttpContext.Current.Server.UrlDecode(ck.Value);
            return JsonConvert.DeserializeObject<UserSession>(valueCookie);
        }

        public void SetCookieUserObject(UserSession obj)
        {
            var jsonUser = JsonConvert.SerializeObject(obj);
            string cookieValue = HttpContext.Current.Server.UrlEncode(jsonUser);
            HttpCookie cookie = new HttpCookie(nameCookiesLogin, cookieValue)
            {
                Expires = DateTime.Now.AddDays(10)
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}