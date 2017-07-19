using AFS_Visicon.Data;
using AFS_Visicon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AFS_Visicon.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// Home page of web application, display all users
        /// </summary>
        /// <param name="sortKey">user attribute for sorting</param>
        /// <param name="lastSortKey">name of last sorting attribute </param>
        /// <param name="searchString">name of user to search</param>
        /// <returns>view with filtered users</returns>
        public ActionResult Index(string sortKey, string lastSortKey, string searchString)
        {
            using (UsersDbContext context = new UsersDbContext())
            {
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
            using(UsersDbContext context = new UsersDbContext())
            {
                List<User> users = context.Users.ToList();

                var today = DateTime.Today;

                var result = from u in users select new[] { u.UserID.ToString(), u.FirstName, u.LastName, (DateTime.Today.Year - u.DateOfBirth.Year).ToString(), u.DateOfBirth.ToShortDateString(), u.Mobil,  u.Email};

                JsonResult jsonR = Json(new
                {
                    aaData = result
                },
                JsonRequestBehavior.AllowGet);

                return jsonR;
            }
        }

        /// <summary>
        /// Create new user form
        /// </summary>
        /// <returns>Empty view</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Add user to database
        /// </summary>
        /// <param name="user">user model</param>
        /// <returns>Redirect to home page if insert was successfull, otherwise show model errors</returns>
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
        /// <returns>Editing page, otherwise 404</returns>
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
        /// <param name="user">changed user model</param>
        /// <returns>Show user with changed values</returns>
        public ActionResult Edit(int id, User user)
        {
            using (UsersDbContext context = new UsersDbContext())
            {
                User dbUser = context.Users.Single(x => x.UserID == id);

                if (ModelState.IsValid)
                {
                    // set user attributes
                    dbUser.FirstName = user.FirstName;
                    dbUser.LastName = user.LastName;
                    dbUser.DateOfBirth = user.DateOfBirth;
                    dbUser.Mobil = user.Mobil;
                    dbUser.Email = user.Email;

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(user);
            }
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">id of user</param>
        /// <returns>Result message</returns>
        public string Delete(int id)
        {
            using (UsersDbContext context = new UsersDbContext())
            {
                User user = context.Users.Single(x => x.UserID == id);

                if (user == null)
                {
                    return "User doesn't exist!"; 
                }
                else
                {
                    context.Users.Remove(user);
                    context.SaveChanges();

                    return "User was deleted.";
                }

            }
        }


    }
}