using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AFDEmp_IndividualProject_Messenger
{
    public class DatabaseEntities : DbContext
    {
        public DatabaseEntities() : base("Messenger")
        {
            //Entity Framework Fluent API is used to configure domain classes to override conventions. 
            //EF Fluent API is based on a Fluent API design pattern (a.k.a Fluent Interface) where the result is formulated by method chaining.
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UserTableConfiguration());
            modelBuilder.Configurations.Add(new MessageTableConfiguration());

        }
    }
}
