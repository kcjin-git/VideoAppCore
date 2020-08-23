using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamWork.Models
{
    /// <summary>
    /// [4][3][2] EfCore를 이용한 ASync CRUD
    /// </summary>
    public class VideoRepositoryEfCoreASync : IVideoRepositoryASync
    {
        private readonly VideoDBContext _context;

        public VideoRepositoryEfCoreASync(VideoDBContext context)
        {
            this._context = context;
        }
        public async Task<Video> Add(Video model)
        {
            _context.Add(model);

            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<Video> GetVideoById(int id)
        {
            return await _context.Videos.FindAsync(id);
        }

        public async Task<List<Video>> GetVideos()
        {
            //return await _context.Videos.FromSql<Video>("select * from videos").ToListAsync();
            return await _context.Videos.ToListAsync();
        }

        public async Task RemoveVideo(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            if(video != null)
            {
                _context.Videos.Remove(video);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Video> UpdateVideo(Video model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChangesAsync();

            return model;
        }
    }

}
