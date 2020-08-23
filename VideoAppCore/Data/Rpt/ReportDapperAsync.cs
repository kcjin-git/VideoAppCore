using Blazored.SessionStorage;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TeamWork.Data.Rpt
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
                                       API_NAME, 
                                       STRT_DATE, 
                                       END_DATE, 
                                       REPORT_TYPE,
                                       ORGN_NAME, 
                                       MODULE_NAME, 
                                       USER_NAME, 
                                       REPORT_TITLE, 
                                       REPORT_CONTENT,
                                       REPORT_CONTENT_EX) 
                               VALUES (@CREATOR, 
                                       @API_NAME, 
                                       @STRT_DATE, 
                                       @END_DATE,
                                       @REPORT_TYPE,
                                       @ORGN_NAME,
                                       @MODULE_NAME,
                                       @USER_NAME,
                                       @REPORT_TITLE,
                                       @REPORT_CONTENT,
                                       @REPORT_CONTENT_EX);" +
                "SELECT Cast(SCOPE_IDENTITY() AS Int);";

            model.API_NAME = "ReportDapperAsync::AddReportAsync";
            int reportId = await db.ExecuteScalarAsync<int>(query, model);

            model.REPORT_ID = reportId;

            return model;
        }

        public async Task<Report> GetReportByIdAsync(int reprotId)
        {
            const string query = "SELECT * FROM REPORTS WHERE REPORT_ID = @REPORT_ID AND EXPR_DATE > GetDate()";

            Report report = new Report();
            report.REPORT_ID = reprotId;

      
            report = await db.QueryFirstOrDefaultAsync<Report>(query, new { REPORT_ID = report.REPORT_ID }, commandType: CommandType.Text);

            return report;
        }

        public async Task<List<Report>> GetReportListAsync()
        {
            const string query = "SELECT * FROM REPORTS ;";

            var reports = await db.QueryAsync<Report>(query);

            return reports.ToList();
        }

        public async Task<int> RemoveReportAsync(int report_id, string email)
        {
            const string query = @"UPDATE REPORTS
                                      SET API_NAME       = 'ReportDapperAsync::RemoveReportAsync', 
                                          MODIFIER       = @EMAIL,
                                          MODIFICATION_DATE = GetDate(),
                                          EXPR_DATE = GetDate()
                                    WHERE REPORT_ID = @REPORT_ID
                                      AND EXPR_DATE > GetDate() ";

            int nAffected = await db.ExecuteAsync(query, new {report_id, email});
            return nAffected;
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
                                          REPORT_CONTENT = @REPORT_CONTENT,
                                          REPORT_CONTENT_EX = @REPORT_CONTENT_EX
                                    WHERE REPORT_ID = @REPORT_ID";

            await db.ExecuteAsync(query, model);

            return model;
        }

        public async Task<Report> GetReportByDate(string eMail, DateTime report_date)
        {
            const string query = @"SELECT * 
                                     FROM REPORTS 
                                    WHERE CREATOR = @EMAIL 
                                      AND @REPORT_DATE BETWEEN STRT_DATE AND END_DATE 
                                      AND REPORT_TYPE = 'P' 
                                      AND EXPR_DATE > GetDate() ";

            var report = await db.QueryFirstOrDefaultAsync<Report>(query, new {eMail, report_date }, commandType: CommandType.Text);

            return report;
        }

        public async Task<Report> GetReportByOwner(string report_type, string report_owner, DateTime report_date)
        {
            string query = @"SELECT * 
                               FROM REPORTS 
                              WHERE CREATOR = @REPORT_OWNER 
                                AND @REPORT_DATE BETWEEN STRT_DATE AND END_DATE 
                                AND REPORT_TYPE = 'P' 
                                AND EXPR_DATE > GetDate() ";

            if(report_type == "M")
            {
                query = @"SELECT * 
                            FROM REPORTS 
                           WHERE MODULE_NAME = @REPORT_OWNER 
                             AND @REPORT_DATE BETWEEN STRT_DATE AND END_DATE 
                             AND REPORT_TYPE = 'M'  
                             AND EXPR_DATE > GetDate() ";
            }

            if (report_type == "T")
            {
                query = @"SELECT * 
                            FROM REPORTS 
                           WHERE ORGN_NAME = @REPORT_OWNER 
                             AND @REPORT_DATE BETWEEN STRT_DATE AND END_DATE 
                             AND REPORT_TYPE = 'T'  
                             AND EXPR_DATE > GetDate() ";
            }

            var report = await db.QueryFirstOrDefaultAsync<Report>(query, new { report_owner, report_date }, commandType: CommandType.Text);

            return report;
        }

        /// <summary>
        /// MODULE_NAME에 해당하는 개인('P')의 REPORT 조회
        /// ORGN_NAME에 해당하는 모듈('M')의 REPORT조회
        /// </summary>
        /// <param name="report_type">M : 모듈 OR T :팀</param>
        /// <param name="report_name">모듈명 OR 팀명</param>
        /// <param name="report_date">소속일자</param>
        /// <returns></returns>
        public async Task<List<Report>> GetReportListByOwner(string report_type, string report_name, DateTime report_date)
        {
            /*
            const string query = @"SELECT DISTINCT STRT_DATE, END_DATE, ORGN_NAME, MODULE_NAME, REPORT_TITLE
                                        , STUFF((SELECT '&lt;p&gt;' + USER_NAME + '&lt;/p&gt;' + REPORT_CONTENT
                                                   FROM REPORTS T1
                                                  WHERE T1.MODULE_NAME = T2.MODULE_NAME
                                                    AND T1.STRT_DATE = T2.STRT_DATE
                                                    FOR XML PATH(''), TYPE).Value('.', 'NVARCHAR(MAX)'
                                                ), 1, 1, '') AS REPORT_CONTENT
                                        , STUFF((SELECT '&lt;p&gt;' + USER_NAME + '&lt;/p&gt;' + REPORT_CONTENT_EX
                                                   FROM REPORTS TX
                                                  WHERE TX.MODULE_NAME = T2.MODULE_NAME
                                                    AND TX.STRT_DATE = T2.STRT_DATE
                                                    FOR XML PATH(''), TYPE).Value('.', 'NVARCHAR(MAX)'
                                                ), 1, 1, '') AS REPORT_CONTENT_EX
                                     FROM REPORTS T2
                                    WHERE MODULE_NAME = @MODULE_NAME AND @REPORT_DATE BETWEEN STRT_DATE AND END_DATE ";
            */
            //개인 주간보고
            string query = @"SELECT * 
                               FROM REPORTS 
                              WHERE MODULE_NAME = @REPORT_NAME 
                                AND @REPORT_DATE BETWEEN STRT_DATE AND END_DATE 
                                AND REPORT_TYPE = 'P' 
                                AND EXPR_DATE > GetDate() ";

            if (report_type == "T") //Module
            {
                query = "SELECT * " +
                        "  FROM REPORTS " +
                        " WHERE ORGN_NAME = @REPORT_NAME " +
                        "   AND @REPORT_DATE BETWEEN STRT_DATE AND END_DATE " +
                        "   AND REPORT_TYPE = 'M' " +
                        "   AND EXPR_DATE > GetDate() ";
            }

            var reports = await db.QueryAsync<Report>(query, new { report_name, report_date}, commandType: CommandType.Text);

            return reports.ToList();
        }


        public async Task<List<Report>> GetReportsByOrgnName(string orgn_name, DateTime report_date)
        {
            const string query = @"SELECT * 
                                     FROM REPORTS 
                                    WHERE ORGN_NAME = @ORGN_NAME 
                                      AND @REPORT_DATE BETWEEN STRT_DATE AND END_DATE 
                                      AND EXPR_DATE > GetDate()
                                 ORDER BY PART_NAME";

            var reports = await db.QueryAsync<Report>(query, new { orgn_name, report_date }, commandType: CommandType.Text);

            return reports.ToList();
        }
    }
}
