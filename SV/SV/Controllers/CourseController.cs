using Newtonsoft.Json;
using SV.Entities;
using SV.Enum;
using SV.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SV.Controllers
{
    public class CourseController : Controller
    {
        private readonly CoreContext coreContext;
        public CourseController()
        {
            this.coreContext = new CoreContext();
        }
        public ActionResult Index()
        {
            var models = coreContext.Courses.Where(z => z.IsActive && !z.IsDelete)
                                    .Select(z => new CourseModels
                                    {
                                        Id = z.Id,
                                        Code = z.Code,
                                        Name = z.Name,
                                    }).ToList();
            return View(models);
        }

        public ActionResult New()
        {
            var model = new CourseModels();
            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> New(CourseModels model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            var lopEntities = new CourseEntities();
            lopEntities.Code = model.Code;
            lopEntities.Name = model.Name;
            lopEntities.IsActive = model.IsActive;
            lopEntities.CreatedAt = model.CreatedAt;
            lopEntities.UpdatedAt = model.UpdatedAt;
            coreContext.Courses.Add(lopEntities);
            var result = await coreContext.SaveChangesAsync();
            if (result >= 1)
            {
                TempData["Successful"] = "Thêm mới khóa học thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IntervalServer"] = "Lỗi thêm mới khóa học";
            }
            return View(model);
        }


        public async Task<ActionResult> Edit(int Id)
        {
            var model = await coreContext.Courses.Where(z => z.Id == Id && z.IsActive)
                                                .Select(z => new CourseModels
                                                {
                                                    Code = z.Code,
                                                    Name = z.Name,
                                                    IsActive = z.IsActive,
                                                    Id = z.Id
                                                }).FirstOrDefaultAsync();           
            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(CourseModels model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            var sinhVienEntities = coreContext.Courses.FirstOrDefault(z => z.Id == model.Id);
            sinhVienEntities.Code = model.Code;
            sinhVienEntities.Name = model.Name;
            sinhVienEntities.IsActive = model.IsActive;
            sinhVienEntities.UpdatedAt = model.UpdatedAt;
            var Response = await coreContext.SaveChangesAsync();
            if (Response >= 0)
            {
                TempData["Successful"] = "Cập nhật khóa học thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IntervalServer"] = "Lỗi chỉnh sửa khóa học";
            }
            return View(model);
        }

        public async Task<ActionResult> Destroy(int Id)
        {
            var entity = await coreContext.Courses.FirstOrDefaultAsync(z => z.Id == Id);
            if (entity != null)
            {
                entity.IsDelete = true;
                entity.IsActive = false;
                entity.UpdatedAt = DateTime.Now;
                var Response = await coreContext.SaveChangesAsync();
                if (Response >= 1)
                {
                    TempData["Successful"] = "Xóa khóa học thành công";
                }
                else
                {
                    TempData["IntervalServer"] = "Lỗi xóa khóa học";
                }
            }
            return RedirectToAction("Index");
        }
    }
}