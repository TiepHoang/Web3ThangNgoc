using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web3ThangNgoc.Models
{
    public class Core
    {
        public static string getLinkImage(string nameFile, Controller controller, string folderSave = "/Content/Images")
        {
            HttpPostedFileBase file = controller.Request.Files[nameFile];
            if (file != null && file.ContentLength > 0 && file.ContentType.Contains("image"))
            {
                string fileName = DateTime.Now.ToString("ddMMyyyyhhMMss") + Path.GetFileName(file.FileName);
                string path = Path.Combine(controller.Server.MapPath(folderSave), fileName);
                file.SaveAs(path);
                return folderSave + "/" + fileName;
            }
            else
            {
                return null;
            }
        }
    }
}