using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoAppCore.Data.Models
{
    public interface IReportAsync
    {
        Task<Report> AddReportAsync(Report model);
        Task<List<Report>> GetReportListAsync();
        Task<Report> GetReportByIdAsync(int reprotId);
        Task<Report> UpdateReportAsync(Report model);
        Task<int> RemoveReportAsync(int reportId, string email);

        Task<Report> GetReportByDate(string sUser, DateTime workDate);
        Task<List<Report>> GetReportListByOwner(string report_type, string report_owner, DateTime report_date);

        Task<Report> GetReportByOwner(string report_type, string report_owner, DateTime report_date);

    }
}
