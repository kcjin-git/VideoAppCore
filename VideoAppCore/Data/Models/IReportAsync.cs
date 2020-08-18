﻿using System;
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
        Task RemoveReportAsync(int reportId);

        Task<Report> GetReportByDate(string sUser, DateTime workDate);
        Task<Report> GetReportsByModuleName(string part_name, DateTime report_date);
    }
}
