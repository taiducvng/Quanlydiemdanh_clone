using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SV.Entities
{
    [Table("Slot")]
    public class SlotEntities : BaseEntities
    {
        public string Code { get; set; }
        public DateTime StartDate {get; set; }
        public DateTime EndDate { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int LecturerId { get; set; }
    }
}