using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TeamWork.Data.Comm;

namespace TeamWork.Data.Vot
{

    public class VoteQuestion : ControlField
    {
        [Key]
        public int VOTE_QUESTION_ID { get; set; }

        public string QUESTION { get; set; }
        public string VOTE_STAT { get; set; }   //--01 : 진행중, 02 : 삭제, 03 : 완료
        public DateTime EFCT_DATE { get; set; }
        public DateTime EXPR_DATE { get; set; }

        public int CANDIDATE_ID { get; set; }

    }

}
