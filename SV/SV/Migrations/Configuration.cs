namespace SV.Migrations
{
    using SV.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SV.Entities.CoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SV.Entities.CoreContext context)
        {
            context.Employees.AddOrUpdate(z => z.Code, new EmployeeEntities
            {
                Email = "admin@gmail.com",
                Password = "123456",
                Name = "Admin",
                Code = "0001",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Gender = 1,
                IsActive = true,
                Role = SV.Enum.RoleEnum.ADMIN,
            });

            context.Lecturers.AddOrUpdate(z => z.Code, new LecturerEntities
            {
                Email = "giaovien2@gmail.com",
                Password = "123456",
                Name = "Giáo Viên A",
                Code = "0002",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Gender = 1,
                IsActive = true,
                Role = SV.Enum.RoleEnum.TEACHER,
            });

            context.Lecturers.AddOrUpdate(z => z.Code, new LecturerEntities
            {
                Email = "giaovien3@gmail.com",
                Password = "123456",
                Name = "Giáo Viên B",
                Code = "0003",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Gender = 1,
                IsActive = true,
                Role = SV.Enum.RoleEnum.TEACHER,
            });

            context.Lecturers.AddOrUpdate(z => z.Code, new LecturerEntities
            {
                Email = "giaovien4@gmail.com",
                Password = "123456",
                Name = "Giáo Viên C",
                Code = "0004",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Gender = 1,
                IsActive = true,
                Role = SV.Enum.RoleEnum.TEACHER,
            });

            context.Lecturers.AddOrUpdate(z => z.Code, new LecturerEntities
            {
                Email = "giaovien5@gmail.com",
                Password = "123456",
                Name = "Giáo Viên D",
                Code = "0005",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Gender = 1,
                IsActive = true,
                Role = SV.Enum.RoleEnum.TEACHER,
            });

            context.Courses.AddOrUpdate(z => z.Name, new CourseEntities
            {
                Code = "K21",
                Name = "Khóa K21",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            });

            context.Courses.AddOrUpdate(z => z.Name, new CourseEntities
            {
                Code = "K22",
                Name = "Khóa K22",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            });

            context.Courses.AddOrUpdate(z => z.Name, new CourseEntities
            {
                Code = "K23",
                Name = "Khóa K23",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            });
        }
    }
}
