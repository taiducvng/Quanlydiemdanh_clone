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
    public class LecturerController : Controller
    {
        private readonly CoreContext coreContext;
        public LecturerController()
        {
            this.coreContext = new CoreContext();
        }
        public ActionResult Index()
        {
            var models = coreContext.Lecturers.Where(z => z.IsActive && !z.IsDelete && z.Role == RoleEnum.TEACHER)
                                    .Select(z => new UserModels
                                    {
                                        Id = z.Id,
                                        Code = z.Code,
                                        Name = z.Name,
                                        Email = z.Email,
                                        Phone = z.Phone,
                                        Address = z.Address,
                                        Gender = z.Gender,
                                    }).ToList();
            return View(models);
        }

        public ActionResult New()
        {
            var model = new UserModels();
            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> New(UserModels model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Xác nhận mật khẩu không đúng");
            }
            
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            var sinhVienEntities = new LecturerEntities();
            sinhVienEntities.Code = model.Code;
            sinhVienEntities.Name = model.Name;
            sinhVienEntities.Email = model.Email;
            sinhVienEntities.Password = model.Password;
            sinhVienEntities.Phone = model.Phone;
            sinhVienEntities.Address = model.Address;
            sinhVienEntities.IsActive = model.IsActive;
            sinhVienEntities.CreatedAt = model.CreatedAt;
            sinhVienEntities.UpdatedAt = model.UpdatedAt;
            sinhVienEntities.Role = RoleEnum.TEACHER;
            coreContext.Lecturers.Add(sinhVienEntities);
            var result = await coreContext.SaveChangesAsync();
            if (result >= 1)
            {
                TempData["Successful"] = "Thêm giảng viên thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IntervalServer"] = "Lỗi thêm mới giảng viên";
            }
            return View(model);
        }


        public async Task<ActionResult> Edit(int Id)
        {
            var model = await coreContext.Lecturers.Where(z => z.Id == Id && z.IsActive)
                                                .Select(z => new UserModels
                                                {
                                                    Code = z.Code,
                                                    Name = z.Name,
                                                    Email = z.Email,
                                                    Phone = z.Phone,
                                                    Password = z.Password,
                                                    ConfirmPassword = z.Password,
                                                    Address = z.Address,
                                                    IsActive = z.IsActive,
                                                    Id = z.Id
                                                }).FirstOrDefaultAsync();
            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(UserModels model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Xác nhận mật khẩu không đúng");
            }
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            var sinhVienEntities = coreContext.Lecturers.FirstOrDefault(z => z.Id == model.Id);
            sinhVienEntities.Code = model.Code;
            sinhVienEntities.Name = model.Name;
            sinhVienEntities.Email = model.Email;
            sinhVienEntities.Password = model.Password;
            sinhVienEntities.Phone = model.Phone;
            sinhVienEntities.Address = model.Address;
            sinhVienEntities.IsActive = model.IsActive;
            sinhVienEntities.UpdatedAt = model.UpdatedAt;
            var Response = await coreContext.SaveChangesAsync();
            if (Response >= 0)
            {
                TempData["Successful"] = "Cập nhật giảng viên học thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IntervalServer"] = "Lỗi chỉnh sửa giảng viên";
            }
            return View(model);
        }

        public async Task<ActionResult> Destroy(int Id)
        {
            var entity = await coreContext.Lecturers.FirstOrDefaultAsync(z => z.Id == Id);
            if (entity != null)
            {
                entity.IsDelete = true;
                entity.IsActive = false;
                entity.UpdatedAt = DateTime.Now;
                var Response = await coreContext.SaveChangesAsync();
                if (Response >= 1)
                {
                    TempData["Successful"] = "Xóa giảng viên thành công";
                }
                else
                {
                    TempData["IntervalServer"] = "Lỗi xóa giảng viên";
                }
            }
            return RedirectToAction("Index");
        }
    }
}