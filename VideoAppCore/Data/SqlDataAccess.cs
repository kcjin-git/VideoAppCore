using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamWork.Data
{
    public class SqlDataAccess
    {
        private readonly IConfiguration _config;

        public string ConnectionStringNae { get; set; } = "Default";

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }
    }
}
