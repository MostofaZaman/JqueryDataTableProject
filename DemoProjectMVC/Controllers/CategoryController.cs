using DemoProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DemoProjectMVC.Controllers
{
    public class CategoryController : Controller
    {
        DemoContext context = new DemoContext();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult IndexAjax()
        {
            var allCategory = (from
                c in context.Category
                               select new { ID = c.ID, Name=c.Name,Photo= c.PhotoFileName  }).ToList();
            return Json(new { data = allCategory }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                if (category.ID > 0)
                {
                    Category existCategory = (from c in context.Category where c.ID == category.ID select c).FirstOrDefault();
                    existCategory.Name = category.Name;
                    existCategory.PhotoFileName = category.PhotoFileName;
                    context.Entry(existCategory).State = EntityState.Modified;
                    context.SaveChanges();
                    //UploadFile();
                    return Json(new { IsSuccess = true, data = "Category Update Successfully!" });
                }
                else
                {
                    context.Category.Add(category);
                    context.SaveChanges();
                    return Json(new { IsSuccess = true, data = "Category Save Successfully!" });
                }
            }
            catch (Exception e)
            {

                throw e;
            }


        }

        public JsonResult UploadFile()
        {
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _imgname = Guid.NewGuid().ToString();
                    var _comPath = Server.MapPath("/Upload/") + pic.FileName;
                    _imgname = pic.FileName;

                    ViewBag.Msg = _comPath;
                    var path = _comPath;
                    //if (!Directory.Exists(path))
                    //{
                    //    Directory.CreateDirectory(path,FileAccess.ReadWrite);
                    //}
                    // Saving Image in Original Mode
                    pic.SaveAs(path);

                    // resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);

                    if (img.Width > 200)
                        img.Resize(200, 200);
                    img.Save(_comPath);
                    // end resize
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditorAjax(int id)
        {
            Category category = null;
            if (id > 0)
            {
                category = (from c in context.Category where c.ID == id select c).FirstOrDefault();
                if (category == null)
                {
                    throw new HttpException(404, "HTTP/1.1 404 Not Found");
                }
            }
            else
            {
                category = new Category();
            }

            return PartialView("_CreateEdit", category);
        }

        public ActionResult DeleteAjax(int id)
        {
            Category category = null;
            if (id > 0)
            {
                category = (from c in context.Category where c.ID == id select c).FirstOrDefault();
                context.Entry(category).State = EntityState.Deleted;
                context.SaveChanges();
                return Json(new { IsSuccess = true, data = "Category deleted successfully!" });
                if (category == null)
                {
                    return Json(new { IsSuccess = true, data = "Category not found!" });
                }
            }

            return Json(new { IsSuccess = true, data = "Category not found!" });
        }

    }


}
