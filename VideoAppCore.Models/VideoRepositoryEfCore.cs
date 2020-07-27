using System;
using System.Collections.Generic;

namespace VideoAppCore.Models
{
    /// <summary>
    /// [4][3] EfCore를 이용한 CRUD
    /// </summary>
    public class VideoRepositoryEfCore : IVideoRepository
    {
        public Video Add(Video model)
        {
            throw new NotImplementedException();
        }

        public Video GetVideoById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Video> GetVideos()
        {
            throw new NotImplementedException();
        }

        public void RemoveVideo(int id)
        {
            throw new NotImplementedException();
        }

        public Video UpdateVideo(Video model)
        {
            throw new NotImplementedException();
        }
    }

}
