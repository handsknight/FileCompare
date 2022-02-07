using FileCompare.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCompare.Infastructure.Persistence.DBContext
{
    public class ApplicationDBContext: IdentityDbContext<IdentityUser>
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        { }

       
        public DbSet<FileCompareHistoryEntity> FileCompareHistory { get; set; }
       
    }
}
