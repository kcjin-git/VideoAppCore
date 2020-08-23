using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TeamWork.Data.Vot
{
    public class VoteQuestionDapperAsync : IVoteQuestionAsync
    {
        public SqlConnection db { get; }

        public VoteQuestionDapperAsync(IConfiguration config)
        {
            db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<VoteQuestion> AddVoteQuestionAsync(VoteQuestion model)
        {
            const string query = @"INSERT INTO VOTE_QUESTION 
                                              (CREATOR, 
                                               API_NAME, 
                                               QUESTION, 
                                               VOTE_STAT, 
                                               EFCT_DATE,
                                               EXPR_DATE, 
                                               CANDIDATE_ID) 
                                       VALUES (@CREATOR, 
                                               @API_NAME, 
                                               @QUESTION, 
                                               @VOTE_STAT,
                                               @EFCT_DATE,
                                               @CANDIDATE_ID);" +
                                  "SELECT Cast(SCOPE_IDENTITY() AS Int);";

            model.API_NAME = "VoteQuestionDapperAsync::AddVoteQuestionAsync";
            int voteId = await db.ExecuteScalarAsync<int>(query, model);

            model.VOTE_QUESTION_ID = voteId;

            return model;
        }

        public async Task<List<VoteQuestion>> GetVoteQuestionListAsync(string email)
        {
            const string query = @"SELECT A.*
                                     FROM VOTE_QUESTION A,
                                     VOTE_PARTICIPANT C
                                    WHERE A.VOTE_ID = C.VOTE_ID
                                      AND A.EXPR_DATE >= GetDate()
                                      AND VOTE_STAT != '02'      
                                      AND C.EMAIL = @EMAIL ";

            var votes = await db.QueryAsync<VoteQuestion>(query,new { email });

            return votes.ToList();
        }
    }
}
