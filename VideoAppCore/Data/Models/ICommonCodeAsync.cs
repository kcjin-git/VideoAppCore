using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoAppCore.Data.Models
{
    public interface ICommonCodeAsync 
    {
        Task<List<CommonCode>> GetCommonCodeListAsync(string group_code);
    }
}
