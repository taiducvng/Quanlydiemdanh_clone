using SV.Entities;
using SV.Enum;
using SV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV.Controllers
{
    public class HomeController : Controller
    {
        private readonly CoreContext coreContext;
        public HomeController()
        {
            this.coreContext = new CoreContext();
        }
        public ActionResult Index()
        {
            List<UserPointModels> models = new List<UserPointModels>();
            ViewBag.SV = coreContext.Students.Where(w => w.Role == Enum.RoleEnum.STUDENT).Count();
            ViewBag.GV = coreContext.Lecturers.Where(w => w.Role == Enum.RoleEnum.TEACHER).Count();
            ViewBag.ND = coreContext.Employees.Where(w => w.Role == Enum.RoleEnum.ADMIN).Count();

            var user = (UserSession)Session["User"];
            if (user != null && user.Id > 0 && user.Role == Enum.RoleEnum.STUDENT)
            {
                models = coreContext.Students.Join(coreContext.StudentMarks, u => u.Id, up => up.StudentId, (u, up) => new { u = u, up = up })
                    .Where(z => z.u.Id == user.Id)
                                        .Select(z => new UserPointModels
                                        {
                                            Id = z.u.Id,
                                            Code = z.u.Code,
                                            Name = z.u.Name,
                                            Email = z.u.Email,
                                            PointStudy = z.up.PointStudy,
                                            PointGK = z.up.PointGK,
                                            PointCK = z.up.PointCK,
                                            SubjectName = coreContext.Subjects.FirstOrDefault(f => f.Id == z.up.SubjectId).Name
                                        }).ToList();

                var schedule = coreContext.Slots.Join(coreContext.SlotStudents, s => s.Id, s => s.SlotId, (s, st) => new { s = s, st = st })
                    .Where(z => z.st.StudentId == user.Id)
                                        .Select(z => new StudentSlotModels
                                        {
                                            Id = z.s.Id,
                                            Code = z.s.Code,
                                            CourseName = coreContext.Courses.FirstOrDefault(f => f.Id == z.s.CourseId).Name,
                                            LecturerName = coreContext.Lecturers.FirstOrDefault(f => f.Id == z.s.LecturerId).Name,
                                            SubjectName = coreContext.Subjects.FirstOrDefault(f => f.Id == z.s.SubjectId).Name,
                                            StartTime = z.s.StartDate,
                                            EndTime = z.s.StartDate,
                                        }).ToList();
                ViewData["Schedule"] = schedule;
            }
            return View(models);
        }

    }
}