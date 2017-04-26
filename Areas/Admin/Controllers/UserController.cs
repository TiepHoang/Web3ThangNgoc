using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WCF.BussinessController.BCL;
using WCF.BussinessObject.EntityObject;

namespace Web3ThangNgoc.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index()
        {
            return View(new UserBCL().GetAll());
        }

        // GET: Admin/User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/User/Create
        [HttpPost]
        public ActionResult Create(UserObject obj)
        {
            try
            {
                // TODO: Add insert logic here
                string s = Web3ThangNgoc.Models.Core.getLinkImage("Avatar", this);
                if (s != null) obj.Avatar = s;
                if (ModelState.IsValid)
                {
                    obj.Password = Web3ThangNgoc.Models.iMD5.GetMD5Hash(obj.Password);
                    new UserBCL().Add(obj);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
            }
            return View();
        }

        // GET: Admin/User/Edit/5
        public ActionResult Edit(string Username)
        {
            return View(new UserBCL().Get(Username));
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        public ActionResult Edit(UserObject obj)
        {
            try
            {
                // TODO: Add insert logic here
                string s = Web3ThangNgoc.Models.Core.getLinkImage("Avatar", this);
                if (s != null) obj.Avatar = s;
                if (ModelState.IsValid)
                {
                    obj.Password = Web3ThangNgoc.Models.iMD5.GetMD5Hash(obj.Password);
                    new UserBCL().Update(obj);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
            }
            return View();
        }

        // GET: Admin/User/Delete/5
        public ActionResult Delete(string username)
        {
            return View(new UserBCL().Get(username));
        }

        // POST: Admin/User/Delete/5
        [HttpPost]
        public ActionResult Delete(UserObject obj)
        {
            try
            {
                // TODO: Add delete logic here
                new UserBCL().Remove(obj.Username);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ChangePassword()
        {
            var ss = new Models.UserModel().GetSession();
            if (ss == null)
            {
                return RedirectToAction("Logout", "Login", new { area = "" });
            }
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection form)
        {
            var ss = new Models.UserModel().GetSession();
            var newPass = form["newPass"];
            var oldPass = form["oldPass"];
            var reNewPass = form["reNewPass"];
            if (reNewPass == null || newPass == null || oldPass == null || !newPass.Equals(reNewPass) || newPass.ToString().Length < 8)
            {
                ModelState.AddModelError("Result", "Không bỏ trống, hai mật khẩu phải trùng nhau, Mật khẩu mới phải >=8 kí tự");
                return View();
            }
            var ob = new UserBCL().Get(ss.Username);
            if (!Models.iMD5.Vietify(oldPass, ob.Password))
            {
                ModelState.AddModelError("Result", "Mật khẩu không đúng");
                return View();
            }
            else
            {
                ob.Password = Models.iMD5.GetMD5Hash(newPass);
                new UserBCL().Update(ob);
                ModelState.AddModelError("Result", "Thành công");
                return RedirectToAction("Index");
            }
        }
    }
}
