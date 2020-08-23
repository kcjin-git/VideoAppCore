using System.Collections.Generic;
using System.Text;

namespace TeamWork.Models
{
    public interface IVideoRepository
    {
        Video Add(Video model);
        List<Video> GetVideos();
        Video GetVideoById(int id);
        Video UpdateVideo(Video model);
        void RemoveVideo(int id);

    }

}
