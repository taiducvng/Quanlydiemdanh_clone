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
    public class StudentMarkController : Controller
    {
        private readonly CoreContext coreContext;
        public StudentMarkController()
        {
            this.coreContext = new CoreContext();
        }
        public ActionResult Index()
        {
            var models = coreContext.Students.Join(coreContext.StudentMarks, u => u.Id, up => up.StudentId, (u, sm) => new { u = u, sm = sm })
                .Where(z => z.u.IsActive && !z.u.IsDelete)
                                    .Select(z => new StudentMarkModels
                                    {
                                        Code = z.u.Code,
                                        Name = z.u.Name,
                                        Email = z.u.Email,
                                        SubjectId = z.sm.SubjectId,
                                        SubjectName = coreContext.Subjects.FirstOrDefault(f=>f.Id == z.sm.SubjectId).Name,
                                        CourseId = z.sm.CourseId,
                                        CourseName = coreContext.Courses.FirstOrDefault(f => f.Id == z.sm.CourseId).Name,
                                        PointStudy = z.sm.PointStudy,
                                        PointGK = z.sm.PointGK,
                                        PointCK = z.sm.PointCK
                                    }).ToList();
            ViewBag.Course = coreContext.Courses.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            ViewBag.Subject = coreContext.Subjects.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            return View(models);
        }

        [HttpPost]
        public ActionResult Search(int course, int subject)
        {
            var _userMarks = coreContext.StudentMarks.AsQueryable();
            if (course > 0)
            {
                _userMarks = _userMarks.Where(w => w.CourseId == course);
            }
            if (subject > 0)
            {
                _userMarks = _userMarks.Where(w => w.SubjectId == subject);
            }
            var models = coreContext.Students.Join(_userMarks, u => u.Id, up => up.StudentId, (u, sm) => new { u = u, sm = sm })
                .Where(z => z.u.IsActive && !z.u.IsDelete)
                                    .Select(z => new StudentMarkModels
                                    {
                                        Code = z.u.Code,
                                        Name = z.u.Name,
                                        Email = z.u.Email,
                                        SubjectId = z.sm.SubjectId,
                                        SubjectName = coreContext.Subjects.FirstOrDefault(f => f.Id == z.sm.SubjectId).Name,
                                        CourseId = z.sm.CourseId,
                                        CourseName = coreContext.Courses.FirstOrDefault(f => f.Id == z.sm.CourseId).Name,
                                        PointStudy = z.sm.PointStudy,
                                        PointGK = z.sm.PointGK,
                                        PointCK = z.sm.PointCK
                                    }).ToList();

            ViewBag.Course = coreContext.Courses.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            ViewBag.Subject = coreContext.Subjects.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            return View(models);
        }

        public ActionResult New()
        {
            ViewBag.Student = coreContext.Students.Where(z => z.IsActive && !z.IsDelete).Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            ViewBag.Course = coreContext.Courses.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            ViewBag.Subject = coreContext.Subjects.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            var model = new StudentMarkModels();
            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> New(StudentMarkModels model)
        {
            var pointEntities = new StudentMarkEntities();

            pointEntities.StudentId = model.StudentId;
            pointEntities.SubjectId = model.SubjectId;
            pointEntities.CourseId = model.CourseId;
            pointEntities.PointStudy = model.PointStudy;
            pointEntities.PointGK = model.PointGK;
            pointEntities.PointCK = model.PointCK;
            pointEntities.CreatedAt = DateTime.Now;
            pointEntities.UpdatedAt = DateTime.Now;
            coreContext.StudentMarks.Add(pointEntities);
            var result = await coreContext.SaveChangesAsync();
            if (result >= 1)
            {
                TempData["Successful"] = "Thêm điểm sinh viên thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IntervalServer"] = "Lỗi thêm điểm sinh viên";
            }
            return View(model);
        }


        public async Task<ActionResult> Edit(int Id)
        {
            var model = await coreContext.Students.Where(z => z.Id == Id && z.IsActive)
                                                .Select(z => new UserModels
                                                {
                                                    Name = z.Name,
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
            var sinhVienEntities = coreContext.Students.FirstOrDefault(z => z.Id == model.Id);
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
                TempData["Successful"] = "Cập nhật sinh viên học thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IntervalServer"] = "Lỗi chỉnh sửa sinh viên";
            }
            return View(model);
        }
    }
}