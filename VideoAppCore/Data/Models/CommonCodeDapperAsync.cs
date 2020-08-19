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
    public class CommonCodeDapperAsync : ICommonCodeAsync
    {
        public SqlConnection db { get; }

        public CommonCodeDapperAsync(IConfiguration config)
        {
            db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<List<CommonCode>> GetCommonCodeListAsync(string group_code)
        {
            const string query = "SELECT * FROM COMMON_CODES WHERE GROUP_CODE = @GROUP_CODE AND EXPR_DATE >= GetDate()";

            var common_codes = await db.QueryAsync<CommonCode>(query, new { group_code });

            return common_codes.ToList();
        }

    }
}
