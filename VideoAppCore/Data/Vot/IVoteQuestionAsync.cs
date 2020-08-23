using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeamWork.Data.Vot
{
    public interface IVoteQuestionAsync
    {
        Task<VoteQuestion> AddVoteQuestionAsync(VoteQuestion model);

        /// <summary>
        /// 삭제 or 완료가 아닌, 진행중이거나 투표 참여를 한 레코드 리스트
        /// </summary>
        /// <returns></returns>
        Task<List<VoteQuestion>> GetVoteQuestionListAsync(string email);  

        /*
        Task<VoteQuestion> GetVoteQuestionByIdAsync(int reprotId);
        Task<VoteQuestion> UpdateVoteQuestionAsync(VoteQuestion model);
        Task<int> RemoveVoteQuestionAsync(int reportId, string email);

        Task<VoteQuestion> GetVoteQuestionByDate(string sUser, DateTime workDate);
        Task<List<VoteQuestion>> GetVoteQuestionListByOwner(string report_type, string report_owner, DateTime report_date);

        Task<VoteQuestion> GetVoteQuestionByOwner(string report_type, string report_owner, DateTime report_date);
        */
    }

}
