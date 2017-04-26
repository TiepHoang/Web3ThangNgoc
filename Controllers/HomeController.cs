using WCF.BussinessController.BCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WCF.BussinessObject.EntityObject;

namespace Web3ThangNgoc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new ChiTieuBCL().GetAll().Take(30));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ChiTieuObject obj, FormCollection form)
        {
            string s = Models.Core.getLinkImage("Image", this);
            if (s != null) obj.Image = s;
            obj.ID = Guid.NewGuid();
            obj.ThoiGian = DateTime.Now;
            if (form["_Username"] != null) obj.Username = form["Username"];
            if (ModelState.IsValid)
            {
                new ChiTieuBCL().Add(obj);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(Guid id)
        {
            return View(new ChiTieuBCL().Get(id));
        }

        [HttpPost]
        public ActionResult Edit(ChiTieuObject obj)
        {
            try
            {
                string s = Web3ThangNgoc.Models.Core.getLinkImage("Image", this);
                if (s != null) obj.Image = s;
                obj.ThoiGian = DateTime.Now;
                new ChiTieuBCL().Update(obj);
                return RedirectToAction("Index");
            }
            catch
            {
            }
            return View();
        }

        public ActionResult Delete(Guid id)
        {
            return View(new ChiTieuBCL().Get(id));
        }

        [HttpPost]
        public ActionResult Delete(ChiTieuObject obj)
        {
            try
            {
                new ChiTieuBCL().Remove(obj.ID);
                return RedirectToAction("Index");
            }
            catch
            {
            }
            ModelState.AddModelError("Result", "Lỗi");
            return View();
        }
    }
}