using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoAppCore.Models
{
    /// <summary>
    /// [4][1][2] Ado.net을 이용한 ASync CRUD
    /// </summary>
    public class VideoRepositoryAdoNetASync : IVideoRepositoryASync
    {
        public Task<Video> Add(Video model)
        {
            throw new NotImplementedException();
        }

        public Task<Video> GetVideoById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Video>> GetVideos()
        {
            throw new NotImplementedException();
        }

        public Task RemoveVideo(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Video> UpdateVideo(Video model)
        {
            throw new NotImplementedException();
        }
    }
}
