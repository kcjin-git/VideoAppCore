using Dapper;
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
        private readonly string _connectionString;
        public SqlConnection db { get; }
        
        public ReportDapperAsync(string connectionString)
        {
            this._connectionString = connectionString;
            db = new SqlConnection(connectionString);
        }

        public async Task<Report> AddReportAsync(Report model)
        {
            const string query =
                @"INSERT INTO REPORTS (CREATOR, 
                                       CREATION_DATE, 
                                       API_NAME, 
                                       STRT_DATE, 
                                       END_DATE, 
                                       PART_NAME, 
                                       USER_NAME, 
                                       REPORT_TITLE, 
                                       REPORT_CONTENT) 
                               VALUES (@CREATOR, 
                                       GetDate(), 
                                       @API_NAME, 
                                       @STRT_DATE, 
                                       @END_DATE,
                                       @PART_NAME,
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
            const string query = "SELECT * FROM REPORTS WHERE REPORT_ID = @reportId";

            var report = await db.QueryFirstOrDefaultAsync<Report>(query, new { reprotId }, commandType: CommandType.Text);

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
            throw new System.NotImplementedException();
        }

        public async Task<Report> GetReportByDate(DateTime workDate)
        {
            throw new System.NotImplementedException();
        }
    }
}
