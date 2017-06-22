﻿using AFS_Visicon.Data;
using AFS_Visicon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AFS_Visicon.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// Home page of web application, display all users
        /// </summary>
        /// <param name="sortKey">user attribute name by we sorting users</param>
        /// <param name="lastSortKey">name of last sorting attribute </param>
        /// <param name="searchString">searching name of users</param>
        /// <returns>view with filtered users</returns>
        public ActionResult Index(string sortKey, string lastSortKey, string searchString)
        {
            using (UsersDbContext context = new UsersDbContext())
            {
                //List of all users
                List<User> users = context.Users.ToList();

                //Filtring users by their name
                if (!String.IsNullOrEmpty(searchString))
                {
                    users = users.Where(x => x.FirstName.Contains(searchString) || x.LastName.Contains(searchString)).ToList();
                }

                bool ascending = true;
                //first time set sorting by name of user
                if (sortKey == null)
                    sortKey = "FirstName";

                //choose type of sorting (asc, desc), switching value by choosing same attribute
                if (lastSortKey != null && sortKey == lastSortKey)
                {
                    ascending = false;
                    ViewBag.lastSortKey = null;
                }
                else
                {
                    ViewBag.lastSortKey = sortKey;
                }

                //sort users by specific attribute 
                switch (sortKey)
                {
                    case "FirstName":
                        users = ascending ? users.OrderBy(x => x.FirstName).ToList() : users.OrderByDescending(x => x.FirstName).ToList();
                        break;
                    case "LastName":
                        users = ascending ? users.OrderBy(x => x.LastName).ToList() : users.OrderByDescending(x => x.LastName).ToList();
                        break;
                    case "DateOfBirth":
                    case "Age":
                        users = ascending ? users.OrderBy(x => x.DateOfBirth).ToList() : users.OrderByDescending(x => x.DateOfBirth).ToList();
                        break;
                }

                return View(users);
            }
        }

        /// <summary>
        /// Show create page
        /// </summary>
        /// <returns>Empty view</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Add user to database
        /// </summary>
        /// <param name="user">info about creating user</param>
        /// <returns>Redirect to home page</returns>
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                using (UsersDbContext context = new UsersDbContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(user);
        }

        /// <summary>
        /// Show user info for editing
        /// </summary>
        /// <param name="id">id of user</param>
        /// <returns>Editing page</returns>
        public ActionResult Edit(int id)
        {
            using (UsersDbContext context = new UsersDbContext())
            {
                User user = context.Users.Single(x => x.UserID == id);

                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
        }

        /// <summary>
        /// Edit existing user
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="user">info of user</param>
        /// <returns>Show user with changed values</returns>
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            using (UsersDbContext context = new UsersDbContext())
            {
                User _user = context.Users.Single(x => x.UserID == id);

                if (ModelState.IsValid)
                {
                    //set user attributes
                    _user.FirstName = user.FirstName;
                    _user.LastName = user.LastName;
                    _user.DateOfBirth = user.DateOfBirth;
                    _user.Mobil = user.Mobil;
                    _user.Email = user.Email;

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(user);
            }
        }
        /// <summary>
        /// Show user info for deleting
        /// </summary>
        /// <param name="id">id of user</param>
        /// <returns>View info of user</returns>
        public ActionResult Delete(int id)
        {
            using (UsersDbContext context = new UsersDbContext())
            {
                User user = context.Users.Single(x => x.UserID == id);

                if (user == null)
                {
                    return HttpNotFound();
                }

                return View(user);
            }
        }
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <param name="user">User info</param>
        /// <returns>Show empty values of user</returns>
        [HttpPost]
        public ActionResult Delete(int id, User user)
        {
            using (UsersDbContext context = new UsersDbContext())
            {
                User _user = context.Users.Single(x => x.UserID == id);

                if (ModelState.IsValid)
                {
                    //remove user from database
                    context.Users.Remove(_user);
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(user);
            }
        }
    }
}