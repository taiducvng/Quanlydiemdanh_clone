using SV.Entities;
using SV.Entities.Schedule;
using SV.Enum;
using SV.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV.Controllers
{
    public class ScheduleController : Controller
    {
        public ActionResult Index()
        {
            var DbContext = new CoreContext();
            var Course = DbContext.Courses.Where(m => !m.IsDelete)
                                    .Select(m => new CourseModels
                                    {
                                        Id = m.Id,
                                        Code = m.Code,
                                        Name = m.Name,
                                    }).ToList();
            return View(Course);
        }

        public ActionResult DsSlot(int courseId)
        {
            var DbContext = new CoreContext();
            var Slots = DbContext.Slots.Where(m => m.CourseId == courseId && !m.IsDelete)
                                    .Select(m => new SlotModels
                                    {
                                        Id = m.Id,
                                        Code = m.Code,
                                        StartTime = m.StartDate,
                                        EndTime = m.EndDate
                                    }).ToList();
            return View(Slots);
        }

        public ActionResult DsStudent(int slotId)
        {
            var DbContext = new CoreContext();
            var StudentIds = DbContext.SlotStudents.Where(m => m.SlotId == slotId).Select(m => m.StudentId);
            var Students = DbContext.Students.Where(m => StudentIds.Contains(m.Id))
                .Select(m => new StudentModels
                {
                    Id = m.Id,
                    Name = m.Name,
                    Email = m.Email,
                    Address = m.Address,
                    Phone = m.Phone,
                    SlotId = slotId
                }).ToList();
            var Schedules = DbContext.Schedules.Where(m => m.SlotId == slotId)
                .Select(m => m.StudentId).ToList();
            if(Students != null && Students.Any() && Schedules != null && Schedules.Any())
            {
                Students.ForEach(m =>
                {
                    m.IsChecked = Schedules.Any(y => y == m.Id);
                });
            }
            return View(Students);
        }

        [HttpPost]
        public JsonResult DiemDanh(ScheduleModels model)
        {
            var DbContext = new CoreContext();
            if (model.IsDiemDanh == false)
            {
                var Schedule = DbContext.Schedules.Where(m => m.SlotId == model.SlotId && 
                            m.StudentId == model.StudentId
                            && DbFunctions.TruncateTime(DateTime.Now) == DbFunctions.TruncateTime(m.CreatedAt)).FirstOrDefault();
                DbContext.Schedules.Remove(Schedule);
            }
            else
            {
                DbContext.Schedules.Add(new ScheduleEntities
                {
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                    SlotId = model.SlotId,
                    StudentId = model.StudentId,
                    UpdatedAt = DateTime.Now
                });
            }
            var Result = DbContext.SaveChanges() > 0;
            return Json(new { Status = 200, Success = Result });
        }
    }
}