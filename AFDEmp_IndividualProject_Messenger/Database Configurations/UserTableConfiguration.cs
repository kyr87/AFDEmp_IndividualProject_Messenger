using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace AFDEmp_IndividualProject_Messenger
{
    //EntityTypeConfiguration<TEntityType> Class Allows configuration to be performed for an entity type in a model.
    public class UserTableConfiguration : EntityTypeConfiguration<User>
    {
        public UserTableConfiguration()
        {
            Property(user => user.UserName)
                .HasMaxLength(4096)
                .IsRequired();

            Property(user => user.Password)
                .IsRequired();

            Property(user => user.UserAccess)
                .IsRequired();

            HasKey(u => u.Id);
                                              
        }
    }
}
