using SV.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SV.Entities
{
    [Table("StudentMark")]
    public class StudentMarkEntities : BaseEntities
    {
        public int StudentId  { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public decimal PointStudy { get; set; }
        public decimal PointGK { get; set; }
        public decimal PointCK { get; set; }
    }
}