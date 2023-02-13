﻿using Newtonsoft.Json;
using SV.Entities;
using SV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var _context = new CoreContext())
            {
                var User = _context.Employees.Where(z => z.IsActive && z.Email == model.Email && z.Password == model.Password).Select(s =>
                new UserSession()
                {
                    FullName = s.Name,
                    Email = s.Email,
                    Role = s.Role,
                    Id = s.Id
                }).FirstOrDefault();
                if (User == null)
                {
                    User = _context.Lecturers.Where(z => z.IsActive && z.Email == model.Email && z.Password == model.Password).Select(s =>
                        new UserSession()
                        {
                            FullName = s.Name,
                            Email = s.Email,
                            Role = s.Role,
                            Id = s.Id
                        }).FirstOrDefault();
                }
                if (User == null)
                {
                    User = _context.Students.Where(z => z.IsActive && z.Email == model.Email && z.Password == model.Password).Select(s =>
                       new UserSession()
                       {
                           FullName = s.Name,
                           Email = s.Email,
                           Role = s.Role,
                           Id = s.Id
                       }).FirstOrDefault();
                }
                if (User != null)
                {
                    TempData["MessagesSuccessful"] = "Đăng nhập thành công.";
                    UserSession userSession = new UserSession();
                    userSession.FullName = User.FullName;
                    userSession.Email = model.Email;
                    userSession.Role = User.Role;
                    userSession.Id = User.Id;
                    userSession.IsAuthenticated = true;
                    Session.Add("User", userSession);
                    string myObjectJson = JsonConvert.SerializeObject(userSession);
                    HttpCookie userCookie = new HttpCookie("UserCookie");
                    userCookie.Expires = DateTime.Now.AddDays(360);
                    userCookie.Value = Server.UrlEncode(myObjectJson);
                    HttpContext.Response.Cookies.Add(userCookie);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["MessagesError"] = "Thông tin đăng nhập không chính xác.";
                    return View(model);
                }
            }
        }

        [HttpGet]
        public ActionResult Profile()
        {
            var Users = Session["User"] as UserSession;
            if(Users == null)
            {
                return RedirectToAction("Index", "Account");
            }
            var DbContext = new CoreContext();
            var UsersModel = DbContext.Employees.Where(m => m.Id == Users.Id).Select(m => new UserModels
            {
                Id = m.Id,
                Address = m.Address,
                Name = m.Name,
                Phone = m.Phone,
                Password = m.Password,
                ConfirmPassword = m.Password,
                Email = m.Email
            }).FirstOrDefault();
            if (UsersModel == null)
            {
                UsersModel = DbContext.Lecturers.Where(m => m.Id == Users.Id).Select(m => new UserModels
                {
                    Id = m.Id,
                    Address = m.Address,
                    Name = m.Name,
                    Phone = m.Phone,
                    Password = m.Password,
                    ConfirmPassword = m.Password,
                    Email = m.Email
                }).FirstOrDefault();
            }
            if (UsersModel == null)
            {
                UsersModel = DbContext.Students.Where(m => m.Id == Users.Id).Select(m => new UserModels
                {
                    Id = m.Id,
                    Address = m.Address,
                    Name = m.Name,
                    Phone = m.Phone,
                    Password = m.Password,
                    ConfirmPassword = m.Password,
                    Email = m.Email
                }).FirstOrDefault();
            }
            return View(UsersModel);
        }

        [HttpPost]
        public ActionResult Profile(UserModels model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Xác nhận mật khẩu không đúng");
            }
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            var Users = Session["User"] as UserSession;
            if (Users == null)
            {
                return RedirectToAction("Index", "Account");
            }
            var DbContext = new CoreContext();
            var UserE = DbContext.Employees.FirstOrDefault(m => m.Id == model.Id);
            if (UserE != null)
            {
                UserE.Name = model.Name;
                UserE.Address = model.Address;
                UserE.Phone = model.Phone;
                UserE.Email = model.Email;
                UserE.Password = model.Password;
                UserE.UpdatedAt = DateTime.Now;
            }
            else
            {
                var UserL = DbContext.Lecturers.FirstOrDefault(m => m.Id == model.Id);
                if (UserL != null)
                {
                    UserL.Name = model.Name;
                    UserL.Address = model.Address;
                    UserL.Phone = model.Phone;
                    UserL.Email = model.Email;
                    UserL.Password = model.Password;
                    UserL.UpdatedAt = DateTime.Now;
                }
                else
                {
                    var UserS = DbContext.Lecturers.FirstOrDefault(m => m.Id == model.Id);
                    UserS.Name = model.Name;
                    UserS.Address = model.Address;
                    UserS.Phone = model.Phone;
                    UserS.Email = model.Email;
                    UserS.Password = model.Password;
                    UserS.UpdatedAt = DateTime.Now;
                }
            }
            
            var Result = DbContext.SaveChanges() > 0;
            if(Result)
            {
                TempData["MessagesSuccessful"] = "Cập nhật thông tin profile thành công.";
                return RedirectToAction("Index", "Home");
            }
            TempData["MessagesError"] = "Cập nhật thông tin profile thất bại.";
            return View(model);
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Account");
        }
    }
}