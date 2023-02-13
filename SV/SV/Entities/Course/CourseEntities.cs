using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SV.Entities
{
    [Table("Course")]
    public class CourseEntities : BaseEntities
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}