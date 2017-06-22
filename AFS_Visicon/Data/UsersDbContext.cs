using AFS_Visicon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AFS_Visicon.Data
{
    public class UsersDbContext : DbContext
    {
        /// <summary>
        /// Initialize the database
        /// </summary>
        public UsersDbContext()
        {
            Database.SetInitializer<UsersDbContext>(new UserInitializer());
        }

        //Representation of table
        public DbSet<User> Users { get; set; }
    }
}