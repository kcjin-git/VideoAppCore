using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace TeamWork.Models
{
    public class VideoDBContext : DbContext
    {
        public VideoDBContext()
        {

        }

        public VideoDBContext(DbContextOptions<VideoDBContext> options)
            :base(options)
        {
            //공식
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //.nset framework 기반에서 호출되는 코드 영역
            //App.config 또는 Web.config의 연결 문자열 사용
            
            if(!optionsBuilder.IsConfigured)
            {
                string connectString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                optionsBuilder.UseSqlServer(connectString);
            }
            
            //base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Video> Videos { get; set; }

       // public DbSet<Report> Reports { get; set; }
    }
}
