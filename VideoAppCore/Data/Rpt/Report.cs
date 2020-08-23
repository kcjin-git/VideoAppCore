using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TeamWork.Data.Comm;

namespace TeamWork.Data.Rpt
{
    public class Report : ControlField
    {
        [Key]
        public int REPORT_ID { get; set; }

        public DateTime STRT_DATE { get; set; }
        public DateTime END_DATE { get; set; }

        public string REPORT_TYPE { get; set; }
        public string ORGN_NAME { get; set; }
        public string MODULE_NAME { get; set; }
        public string USER_NAME { get; set; }
     
        public string REPORT_TITLE { get; set; }
        public string REPORT_CONTENT { get; set; }
        public string REPORT_CONTENT_EX { get; set; }

        public Report()
        {

        }

        public Report(DateTime dt)
        {
            STRT_DATE = getMondayDate(dt);
            END_DATE = STRT_DATE.AddDays(6);

            REPORT_TITLE = STRT_DATE.Month.ToString() + "월 " + GetWeekNumberOfMonth(STRT_DATE).ToString() + "주 주간보고";
        }

        public DateTime getMondayDate(DateTime dt)
        {
            return dt.AddDays(DayOfWeek.Monday - dt.DayOfWeek);
        }
        private int GetWeekNumberOfMonth(DateTime date)
        {
            date = date.Date;
            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            if (firstMonthMonday > date)
            {
                firstMonthDay = firstMonthDay.AddMonths(-1);
                firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            }
            return (date - firstMonthMonday).Days / 7 + 1;
        }
    }

 
}
