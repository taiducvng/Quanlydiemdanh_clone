using SV.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV.Models
{
    public class StudentSlotModels : BaseDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Slot { get; set; }
        public string SlotName { get; set; }
        public int LecturerId { get; set; }
        public string LecturerName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}