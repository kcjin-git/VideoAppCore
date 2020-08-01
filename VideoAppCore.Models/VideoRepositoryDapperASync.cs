using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace VideoAppCore.Models
{
    /// <summary>
    /// [4][2][2] Dapper를 이용한 ASync CRUD
    /// </summary> 
    public class VideoRepositoryDapperASync : IVideoRepositoryASync
    {
        private readonly string _connectionString;
        public SqlConnection db { get; }

        public VideoRepositoryDapperASync(String connectionString)
        {
            this._connectionString = connectionString;

            db = new SqlConnection(connectionString);
        }

        
        public async Task<Video> Add(Video model)
        {
            const string query =
                "Insert Into Videos(Title, Url, Name, Company, CreatedBy) Values(@Title, @Url, @Name, @Company, @CreatedBy);" +
                "Select Cast(SCOPE_IDENTITY() As Int);";

            int id = await db.ExecuteScalarAsync<int>(query, model);

            model.Id = id;

            return model;
        }

        public async Task<Video> GetVideoById(int id)
        {
            const string query = "Select * from Videos Where Id = @Id";

            var video = await db.QueryFirstOrDefaultAsync<Video>(query, new { id }, commandType: CommandType.Text);

            return video;
        }

        public async Task<List<Video>> GetVideos()
        {
            const string query = "Select * From Videos;";

            var videos = await db.QueryAsync<Video>(query);

            return videos.ToList();
        }

        public async Task RemoveVideo(int id)
        {
            const string query = "Delete Videos Where Id = @Id";

            await db.ExecuteAsync(query, new { id }, commandType: CommandType.Text);

        }

        public async Task<Video> UpdateVideo(Video model)
        {
            const string query = @"Update Videos
                                          set Title = @Title,
                                              Url = @Url,
                                              Name = @Name,
                                              Company = @Company,
                                              ModifiedBy = @ModifiedBy
                                        Where Id = @Id";

            await db.ExecuteAsync(query, model);

            return model;
        }
    }
}
