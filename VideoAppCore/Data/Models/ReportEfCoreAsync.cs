using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace VideoAppCore.Data.Models
{
    /// <summary>
    /// [4][3][2] EfCore를 이용한 ASync CRUD
    /// </summary>
    public class ReportEfCoreAsync : IReportAsync
    {
        private readonly ApplicationDbContext _context;

        public ReportEfCoreAsync(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<Report> AddReportAsync(Report model)
        {
            model.API_NAME = this.ToString() + "AddReportAsync";
            model.CREATION_DATE = DateTime.Now;


            _context.Add(model);

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<Report> GetReportByIdAsync(int id)
        {
            return await _context.Reports.FindAsync(id);
        }

        public async Task<List<Report>> GetReportListAsync()
        {
            //return await _context.Reports.FromSqlRaw<Report>("select * from Reports").ToListAsync(); // .Reports.FromSql<Report>("select * from Reports").ToListAsync();
            return await _context.Reports.ToListAsync();
        }

        public async Task RemoveReportAsync(int id)
        {
            var video = await _context.Reports.FindAsync(id);
            if(video != null)
            {
                _context.Reports.Remove(video);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Report> UpdateReportAsync(Report model)
        {
            model.API_NAME = this.ToString() + "UpdateReportAsync";

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return model;
        }
        public async Task<Report> GetReportByDate(string sEmail, DateTime workDate)
        {
            string query = "SELECT top(1) * FROM REPORTS WHERE {0} BETWEEN STRT_DATE AND END_DATE";

            //SqlParameter param = new SqlParameter("@WORK_DATE", workDate);


            return await _context.Reports.FromSqlRaw<Report>(query, workDate).FirstOrDefaultAsync();
        }
        public async Task<Report> GetReportsByModuleName(string orgn_name, DateTime report_date)
        {
            return null;
        }
        public async Task<List<Report>> GetReportsByOrgnName(string orgn_name, DateTime report_date)
        {
            return null;
        }
    }

}
