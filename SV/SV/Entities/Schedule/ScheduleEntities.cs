using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SV.Entities.Schedule
{
    [Table("Schedule")]
    public class ScheduleEntities : BaseEntities
    {
        public int StudentId { get; set; }
        public int SlotId { get; set; }
    }
}