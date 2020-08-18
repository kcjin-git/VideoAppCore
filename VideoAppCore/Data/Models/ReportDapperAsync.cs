using Blazored.SessionStorage;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace VideoAppCore.Data.Models
{
 

    public class ReportDapperAsync : IReportAsync
    {

        public SqlConnection db { get; }

        public ReportDapperAsync(IConfiguration config)
        {
            db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<Report> AddReportAsync(Report model)
        {
            const string query =
                @"INSERT INTO REPORTS (CREATOR, 
                                       CREATION_DATE, 
                                       API_NAME, 
                                       STRT_DATE, 
                                       END_DATE, 
                                       ORGN_NAME, 
                                       MODULE_NAME, 
                                       USER_NAME, 
                                       REPORT_TITLE, 
                                       REPORT_CONTENT) 
                               VALUES (@CREATOR, 
                                       GetDate(), 
                                       @API_NAME, 
                                       @STRT_DATE, 
                                       @END_DATE,
                                       @ORGN_NAME,
                                       @MODULE_NAME,
                                       @USER_NAME,
                                       @REPORT_TITLE,
                                       @REPORT_CONTENT);" +
                "SELECT Cast(SCOPE_IDENTITY() AS Int);";

            int reportId = await db.ExecuteScalarAsync<int>(query, model);

            model.REPORT_ID = reportId;

            return model;
        }

        public async Task<Report> GetReportByIdAsync(int reprotId)
        {
            const string query = "SELECT * FROM REPORTS WHERE REPORT_ID = @REPORT_ID";

            Report report = new Report();
            report.REPORT_ID = reprotId;

      
            report = await db.QueryFirstOrDefaultAsync<Report>(query, new { REPORT_ID = report.REPORT_ID }, commandType: CommandType.Text);

            return report;
        }

        public async Task<List<Report>> GetReportListAsync()
        {
            const string query = "SELECT * FROM REPORTS;";

            var reports = await db.QueryAsync<Report>(query);

            return reports.ToList();
        }

        public async Task RemoveReportAsync(int reportId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Report> UpdateReportAsync(Report model)
        {            
           

            const string query = @"UPDATE REPORTS
                                      SET API_NAME       = 'ReportDapperAsync::UpdateReportAsync', 
                                          MODIFIER       = @MODIFIER,
                                          MODIFICATION_DATE = GetDate(),
                                          STRT_DATE      = @STRT_DATE, 
                                          END_DATE       = @END_DATE, 
                                          ORGN_NAME      = @ORGN_NAME, 
                                          MODULE_NAME    = @MODULE_NAME, 
                                          USER_NAME      = @USER_NAME, 
                                          REPORT_TITLE   = @REPORT_TITLE, 
                                          REPORT_CONTENT = @REPORT_CONTENT
                                    WHERE REPORT_ID = @REPORT_ID";

            await db.ExecuteAsync(query, model);

            return model;
        }

        public async Task<Report> GetReportByDate(string eMail, DateTime report_date)
        {
            const string query = "SELECT * FROM REPORTS WHERE CREATOR = @EMAIL AND @REPORT_DATE BETWEEN STRT_DATE AND END_DATE";

            var report = await db.QueryFirstOrDefaultAsync<Report>(query, new {eMail, report_date }, commandType: CommandType.Text);

            return report;
        }

        public async Task<Report> GetReportsByModuleName(string part_name, DateTime report_date)
        {
            const string query = @"SELECT DISTINCT STRT_DATE, END_DATE, ORGN_NAME, MODULE_NAME, REPORT_TITLE
                                        , STUFF((SELECT '<br>' + USER_NAME + REPORT_CONTENT
                                                   FROM REPORTS T1
                                                  WHERE T1.MODULE_NAME = T2.MODULE_NAME
                                                    AND T1.STRT_DATE = T2.STRT_DATE
                                                    FOR XML PATH('')
                                                ), 1, 1, '') AS REPORT_CONTENT
                                     FROM REPORTS T2
                                    WHERE MODULE_NAME = @MODULE_NAME AND @REPORT_DATE BETWEEN STRT_DATE AND END_DATE ";

            var report = await db.QueryFirstOrDefaultAsync<Report>(query, new { part_name, report_date }, commandType: CommandType.Text);

            return report;
        }


        public async Task<List<Report>> GetReportsByOrgnName(string orgn_name, DateTime report_date)
        {
            const string query = "SELECT * FROM REPORTS WHERE ORGN_NAME = @ORGN_NAME AND @REPORT_DATE BETWEEN STRT_DATE AND END_DATE ORDER BY PART_NAME";

            var reports = await db.QueryAsync<Report>(query, new { orgn_name, report_date }, commandType: CommandType.Text);

            return reports.ToList();
        }
    }
}
