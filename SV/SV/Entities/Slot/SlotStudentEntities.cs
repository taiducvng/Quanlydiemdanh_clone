using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SV.Entities
{
    [Table("SlotStudent")]
    public class SlotStudentEntities : BaseEntities
    {
        public int SlotId { get; set; }
        public int StudentId { get; set; }
    }
}