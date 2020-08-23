using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeamWork.Data.Comm
{
    public interface ICommonCodeAsync 
    {
        Task<List<CommonCode>> GetCommonCodeListAsync(string group_code);
    }
}
