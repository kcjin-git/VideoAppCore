using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoAppCore.Models
{
    public interface IVideoRepositoryASync
    {
        Task<Video> Add(Video model);
        Task<List<Video>> GetVideos();
        Task<Video> GetVideoById(int id);
        Task<Video> UpdateVideo(Video model);
        Task  RemoveVideo(int id);

    }

}
