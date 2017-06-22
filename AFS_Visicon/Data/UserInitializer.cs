using AFS_Visicon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AFS_Visicon.Data
{
    public class UserInitializer : DropCreateDatabaseIfModelChanges<UsersDbContext>
    {
        /// <summary>
        /// Fill database with init values
        /// </summary>
        /// <param name="context">database</param>
        protected override void Seed(UsersDbContext context)
        {
            var users = new List<User>
            {
                new User { FirstName = "John", LastName = "Black", Email = "john.black@gmail.com", Mobil = "0911853654", DateOfBirth= new DateTime(2000,4,12) },
                new User { FirstName = "Alice", LastName = "White", Email = "alicewhite@gmail.com", Mobil = "0918963654", DateOfBirth = new DateTime(2007, 1, 17) },
                new User { FirstName = "Arthur", LastName = "Green", Email = "arthur.green@gmail.com", Mobil = "0911853123", DateOfBirth = new DateTime(1957, 9, 1) },
                new User { FirstName = "Anna", LastName = "Purple", Email = "annapurple_1999@gmail.com", Mobil = "0925853984", DateOfBirth = new DateTime(1999, 9, 9) },
                new User { FirstName = "Fred", LastName = "Yellow", Email = "freddy@gmail.com", Mobil = "0911123454", DateOfBirth = new DateTime(1968, 1, 1) },
                new User { FirstName = "Laurence", LastName = "Black", Email = "laurence.black@gmail.com", Mobil = "0911203456", DateOfBirth = new DateTime(2002, 12, 12) }
            };

            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
        }
    }
}