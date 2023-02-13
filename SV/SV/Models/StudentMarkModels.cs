using SV.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV.Models
{
    public class StudentMarkModels
    {
        public int StudentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public decimal PointStudy { get; set; }
        public decimal PointGK { get; set; }
        public decimal PointCK { get; set; }
    }
}