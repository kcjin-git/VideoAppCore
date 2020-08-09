using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideoAppCore.Data.Models;

namespace VideoAppCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //.nset framework 기반에서 호출되는 코드 영역
            //App.config 또는 Web.config의 연결 문자열 사용

            if (!optionsBuilder.IsConfigured)
            {
                string connectString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                optionsBuilder.UseSqlServer(connectString);
            }

            //base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Report> Reports { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
