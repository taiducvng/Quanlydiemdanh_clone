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
    public class EmployeeController : Controller
    {
        private readonly CoreContext coreContext;
        public EmployeeController()
        {
            this.coreContext = new CoreContext();
        }
        public ActionResult Index()
        {
            var models = coreContext.Employees.Where(z => z.IsActive && !z.IsDelete && z.Role == RoleEnum.ADMIN)
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
            var employeeEntities = new EmployeeEntities();
            employeeEntities.Code = model.Code;
            employeeEntities.Name = model.Name;
            employeeEntities.Email = model.Email;
            employeeEntities.Password = model.Password;
            employeeEntities.Phone = model.Phone;
            employeeEntities.Address = model.Address;
            employeeEntities.IsActive = model.IsActive;
            employeeEntities.CreatedAt = model.CreatedAt;
            employeeEntities.UpdatedAt = model.UpdatedAt;
            employeeEntities.Role = RoleEnum.ADMIN;
            coreContext.Employees.Add(employeeEntities);
            var result = await coreContext.SaveChangesAsync();
            if (result >= 1)
            {
                TempData["Successful"] = "Thêm nhân viên thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IntervalServer"] = "Lỗi thêm mới nhân viên";
            }
            return View(model);
        }


        public async Task<ActionResult> Edit(int Id)
        {
            var model = await coreContext.Employees.Where(z => z.Id == Id && z.IsActive)
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
            var sinhVienEntities = coreContext.Employees.FirstOrDefault(z => z.Id == model.Id);
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
                TempData["Successful"] = "Cập nhật nhân viên học thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IntervalServer"] = "Lỗi chỉnh sửa nhân viên";
            }
            return View(model);
        }

        public async Task<ActionResult> Destroy(int Id)
        {
            var entity = await coreContext.Employees.FirstOrDefaultAsync(z => z.Id == Id);
            if (entity != null)
            {
                entity.IsDelete = true;
                entity.IsActive = false;
                entity.UpdatedAt = DateTime.Now;
                var Response = await coreContext.SaveChangesAsync();
                if (Response >= 1)
                {
                    TempData["Successful"] = "Xóa nhân viên thành công";
                }
                else
                {
                    TempData["IntervalServer"] = "Lỗi xóa nhân viên";
                }
            }
            return RedirectToAction("Index");
        }
    }
}