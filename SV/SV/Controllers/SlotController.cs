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
    public class SlotController : Controller
    {
        private readonly CoreContext coreContext;
        public SlotController()
        {
            this.coreContext = new CoreContext();
        }
        public ActionResult Index()
        {
            var models = coreContext.Slots.Where(z => z.IsActive && !z.IsDelete)
                                    .Select(z => new SlotModels
                                    {
                                        Id = z.Id,
                                        Code = z.Code,
                                        SubjectId = z.SubjectId,
                                        SubjectName = coreContext.Subjects.FirstOrDefault(f=>f.Id == z.SubjectId).Name,
                                        LecturerId = z.LecturerId,
                                        LecturerName = coreContext.Lecturers.FirstOrDefault(f => f.Id == z.LecturerId).Name,
                                        CourseId = z.CourseId,
                                        CourseName = coreContext.Courses.FirstOrDefault(f => f.Id == z.CourseId).Name,
                                        StartTime = z.StartDate,
                                        EndTime = z.EndDate
                                    }).ToList();
            return View(models);
        }

        public ActionResult New()
        {
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
            ViewBag.Lecturer = coreContext.Lecturers.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            ViewBag.Student = coreContext.Students.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            var model = new SlotModels();
            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> New(SlotModels model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            var slotEntities = new SlotEntities();
            slotEntities.Code = model.Code;
            slotEntities.SubjectId = model.SubjectId;
            slotEntities.CourseId = model.CourseId;
            slotEntities.LecturerId = model.LecturerId;
            slotEntities.StartDate = model.StartTime;
            slotEntities.EndDate = model.EndTime;
            slotEntities.IsActive = model.IsActive;
            slotEntities.CreatedAt = model.CreatedAt;
            slotEntities.UpdatedAt = model.UpdatedAt;
            coreContext.Slots.Add(slotEntities);
            var trans = coreContext.Database.BeginTransaction();
            try
            {
                var Response = await coreContext.SaveChangesAsync();
                if (Response >= 0)
                {
                    List<SlotStudentEntities> lSlotStudentEntities = new List<SlotStudentEntities>();
                    if (model.StudentIds != null && model.StudentIds.Any())
                    {
                        foreach (var item in model.StudentIds)
                        {
                            lSlotStudentEntities.Add(new SlotStudentEntities()
                            {
                                SlotId = slotEntities.Id,
                                StudentId = item,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now
                            });
                        }
                        coreContext.SlotStudents.AddRange(lSlotStudentEntities);
                    }
                    Response = await coreContext.SaveChangesAsync();
                    trans.Commit();
                }
                TempData["Successful"] = "Sửa thông tin slot thành công";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
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
                ViewBag.Lecturer = coreContext.Lecturers.Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();
                ViewBag.Student = coreContext.Students.Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();
                TempData["IntervalServer"] = "Lỗi sửa thông tin Slot";
                trans.Rollback();
                return View(model);
            }
        }


        public async Task<ActionResult> Edit(int Id)
        {
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
            ViewBag.Lecturer = coreContext.Lecturers.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            ViewBag.Student = coreContext.Students.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            var model = await coreContext.Slots.Where(z => z.Id == Id && z.IsActive)
                                                .Select(z => new SlotModels
                                                {
                                                    Code = z.Code,
                                                    IsActive = z.IsActive,
                                                    Id = z.Id,
                                                    LecturerId = z.LecturerId,
                                                    SubjectId = z.SubjectId,
                                                    CourseId = z.CourseId,
                                                    StudentIds = coreContext.SlotStudents.Where(w => w.SlotId == z.Id).Select(s => s.StudentId).ToList(),
                                                    StartTime = z.StartDate,
                                                    EndTime = z.EndDate
                                                }).FirstOrDefaultAsync();   
            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(SlotModels model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            var slotEntities = coreContext.Slots.FirstOrDefault(z => z.Id == model.Id);
            slotEntities.Code = model.Code;
            slotEntities.SubjectId = model.SubjectId;
            slotEntities.CourseId = model.CourseId;
            slotEntities.LecturerId = model.LecturerId;
            slotEntities.StartDate = model.StartTime;
            slotEntities.EndDate = model.EndTime;
            slotEntities.IsActive = model.IsActive;
            slotEntities.UpdatedAt = model.UpdatedAt;
            var trans = coreContext.Database.BeginTransaction();
            try
            {
                var Response = await coreContext.SaveChangesAsync();
                if (Response >= 0)
                {
                    //lấy thông tin thời khóa biểu
                    var deleteSlotStident = coreContext.SlotStudents.Where(w => w.SlotId == slotEntities.Id);
                    coreContext.SlotStudents.RemoveRange(deleteSlotStident);
                    await coreContext.SaveChangesAsync();
                    List<SlotStudentEntities> lSlotStudentEntities = new List<SlotStudentEntities>();
                    if (model.StudentIds != null && model.StudentIds.Any())
                    {
                        foreach (var item in model.StudentIds)
                        {
                            lSlotStudentEntities.Add(new SlotStudentEntities()
                            {
                                SlotId = slotEntities.Id,
                                StudentId = item,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now
                            });
                        }
                        coreContext.SlotStudents.AddRange(lSlotStudentEntities);
                    }
                    Response = await coreContext.SaveChangesAsync();
                    trans.Commit();
                }
                TempData["Successful"] = "Sửa thông tin slot thành công";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
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
                ViewBag.Lecturer = coreContext.Lecturers.Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();
                ViewBag.Student = coreContext.Students.Select(s => new SelectListItem()
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();
                TempData["IntervalServer"] = "Lỗi sửa thông tin lớp học";
                trans.Rollback();
                return View(model);
            }
        }

        public async Task<ActionResult> Destroy(int Id)
        {
            var deleteSlotStident = coreContext.SlotStudents.Where(w => w.SlotId == Id);
            coreContext.SlotStudents.RemoveRange(deleteSlotStident);
            await coreContext.SaveChangesAsync();
            var entity = await coreContext.SlotStudents.FirstOrDefaultAsync(z => z.Id == Id);
            if (entity != null)
            {
                entity.IsDelete = true;
                entity.IsActive = false;
                entity.UpdatedAt = DateTime.Now;
                var Response = await coreContext.SaveChangesAsync();
                if (Response >= 1)
                {
                    TempData["Successful"] = "Xóa slot thành công";
                }
                else
                {
                    TempData["IntervalServer"] = "Lỗi xóa slot";
                }
            }
            return RedirectToAction("Index");
        }
    }
}