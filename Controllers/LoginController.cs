using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WCF.BussinessController.BCL;
using WCF.BussinessObject.EntityObject;
using Web3ThangNgoc.Models;

namespace Web3ThangNgoc.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserSession obj)
        {
            try
            {
                var ob = new UserBCL().Get(obj.Username);
                if (ob != null && new Web3ThangNgoc.Models.UserModel().CheckLogin(obj.Username, obj.Password))
                {
                    new Web3ThangNgoc.Models.UserModel().SetSession(obj);
                    if (obj.Remenber)
                    {
                        new UserModel().SetSession(obj);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Result", "Tên đăng nhập hoặc mật khẩu không chính xác");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }
            return View();
        }

        public ActionResult Logout()
        {
            new Models.UserModel().clearSession();
            return RedirectToAction("Login", "Login");
        }
    }
}