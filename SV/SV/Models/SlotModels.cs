using SV.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SV.Models
{
    public class SlotModels : BaseDTO
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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
        public List<int> StudentIds { get; set; }
    }
}