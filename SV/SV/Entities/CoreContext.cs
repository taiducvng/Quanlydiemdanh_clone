using SV.Entities.Schedule;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SV.Entities
{
    public class CoreContext : DbContext
    {
        public CoreContext() : base("name=Core_Context")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                                            .Where(type => !String.IsNullOrEmpty(type.Namespace))
                                            .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                                            type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            Database.SetInitializer<CoreContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<EmployeeEntities> Employees { get; set; }
        public DbSet<LecturerEntities> Lecturers { get; set; }
        public DbSet<StudentEntities> Students { get; set; }
        public DbSet<CourseEntities> Courses { get; set; }
        public DbSet<SlotEntities> Slots { get; set; }
        public DbSet<SlotStudentEntities> SlotStudents { get; set; }
        public DbSet<SubjectEntities> Subjects { get; set; }
        public DbSet<StudentMarkEntities> StudentMarks { get; set; }
        public DbSet<ScheduleEntities> Schedules { get; set; }

    }
}