using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace VideoAppCore.Models
{
    /// <summary>
    /// [4][1][2] Ado.net을 이용한 ASync CRUD
    /// </summary>
    public class VideoRepositoryAdoNetASync : IVideoRepositoryASync
    {
        private readonly string _connectString;

        public VideoRepositoryAdoNetASync(String connectString)
        {
            this._connectString = connectString;
        }

        public async Task<Video> Add(Video model)
        {
            using(SqlConnection con = new SqlConnection(_connectString))
            {
                const string query =
                    "Insert Into Videos(Title, Url, Name, Company, CreatedBy) Values(@Title, @Url, @Name, @Company, @CreatedBy);" +
                    "Select Cast(SCOPE_IDENTITY() As Int);";

                SqlCommand cmd = new SqlCommand(query, con) { CommandType = CommandType.Text };

                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@Url", model.Url);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Company", model.Company);
                cmd.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);

                con.Open();

                object result = await cmd.ExecuteScalarAsync();
                if(int.TryParse(result.ToString(), out int id))
                {
                    model.Id = id;
                }
                con.Close();
            }

            return model;
        }

        public async Task<Video> GetVideoById(int id)
        {
            Video video = new Video();
            
            using (SqlConnection con = new SqlConnection(_connectString))
            {
                const string query = "Select * from Videos where Id = @Id";

                SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader dr = await cmd.ExecuteReaderAsync();
                if(dr.Read())
                {
                    video.Id = dr.GetInt32(0);
                    video.Title = dr["Title"].ToString();
                    video.Url = dr["Url"].ToString();
                    video.Name = dr["Name"].ToString();
                    video.Company = dr["Company"].ToString();
                    video.CreatedBy = dr["CreatedBy"].ToString();
                    video.Created = Convert.ToDateTime(dr["Created"]);

                }

                con.Close();

                return video;
            }
        }

        public async Task<List<Video>> GetVideos()
        {
            List<Video> videos = new List<Video>();

            using (SqlConnection con = new SqlConnection(_connectString))
            {
                const string query = "Select * from Videos;";
                SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };

                con.Open();
                SqlDataReader dr = await cmd.ExecuteReaderAsync();
                while(dr.Read())
                {
                    Video video = new Video
                    {
                        Id = dr.GetInt32(0),
                        Title = dr["Title"].ToString(),
                        Url = dr["Url"].ToString(),
                        Name = dr["Name"].ToString(),
                        Company = dr["Company"].ToString(),
                        CreatedBy = dr["CreatedBy"].ToString(),
                        Created = Convert.ToDateTime(dr["Created"])
                    };

                    videos.Add(video);
                }
                con.Close();
            }

            return videos;
        }

        public async Task RemoveVideo(int id)
        {
            using(SqlConnection con = new SqlConnection(_connectString))
            {
                const string query = "Delete Videos Where Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
        }

        public async Task<Video> UpdateVideo(Video model)
        {
             using (SqlConnection con = new SqlConnection(_connectString))
            {
                const string query = @"Update Videos
                                          set Title = @Title,
                                              Url = @Url,
                                              Name = @Name,
                                              Company = @Company,
                                              ModifiedBy = @ModifiedBy
                                        Where Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con) { CommandType = CommandType.Text };

                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@Title", model.Title);
                cmd.Parameters.AddWithValue("@Url", model.Url);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Company", model.Company);
                cmd.Parameters.AddWithValue("@ModifiedBy", model.ModifiedBy);

                con.Open();

                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }

            return model;
        }
    }
}
