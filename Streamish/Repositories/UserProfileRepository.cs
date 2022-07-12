using Microsoft.Extensions.Configuration;
using Streamish.Models;
using System.Collections.Generic;
using Streamish.Utils;

namespace Streamish.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM UserProfile WHERE Id=@id";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UserProfile Get(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id,
                                               [Name],
                                               Email,
                                               ImageUrl,
                                               DateCreated
                                          FROM UserProfile
                                         WHERE Id=@id";

                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();

                    var profile = new UserProfile();

                    if (reader.Read())
                    {
                        profile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
                        };    
                    }
                    return profile;
                }
            }
        }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id,
                                               [Name], 
                                               Email, 
                                               ImageUrl, 
                                               DateCreated 
                                          FROM UserProfile";

                    using (var reader = cmd.ExecuteReader())
                    {
                        var users = new List<UserProfile>();

                        while (reader.Read())
                        {
                            var user = new UserProfile()
                            {
                                Id= DbUtils.GetInt(reader, "Id"),
                                Name= DbUtils.GetString(reader, "Name"),
                                Email=DbUtils.GetString(reader, "Email"),
                                ImageUrl= DbUtils.GetString(reader, "ImageUrl"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated")

                            };

                            users.Add(user);
                        }
                        return users;  
                    }
                }
            }
        }

        public Video GetWithAuthoredVideos(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id,
                                        [Name], Email, ImageUrl, DateCreated,

                                        v.Id AS videoId, Title, Description, Url, v.DateCreated AS videoDate, UserProfileId,

                                        c.Id AS commentId, Message, VideoId, c.UserProfileId AS commentProfileId 
                                   FROM UserProfile
                                   JOIN Video v ON v.Id = Id 
                              LEFT JOIN Comment c ON c.Id = v.Id
                                  WHERE Id=@id AND UserProfileId=@id AND VideoId=videoId";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var video = new Video();
                        var comments = new List<Comment>();
                        while (reader.Read())
                        {
                            
                            video = new Video()
                            {
                                Id= DbUtils.GetInt(reader, "videoId"),
                                Title= DbUtils.GetString(reader, "Title"),
                                Description= DbUtils.GetString(reader, "Description"),
                                Url= DbUtils.GetString(reader, "Url"),
                                DateCreated = DbUtils.GetDateTime(reader, "videoDate"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                UserProfile = new UserProfile() 
                                {
                                    Id=DbUtils.GetInt(reader, "Id"),
                                    Name= DbUtils.GetString(reader, "Name"),
                                    Email=DbUtils.GetString(reader, "Email"),
                                    ImageUrl= DbUtils.GetString(reader, "ImageUrl"),
                                    DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
                                }
                            };

                            var comment = new Comment() 
                            {
                                Id = DbUtils.GetInt(reader, "commentId"),
                                Message= DbUtils.GetString(reader, "Message"),
                                VideoId = DbUtils.GetInt(reader, "VideoId"),
                                UserProfileId = DbUtils.GetInt(reader, "commentProfileId")
                            };
                            comments.Add(comment);


                        }

                        foreach (var comment in comments)
                        {
                            video.Comments.Add(comment);
                        }

                        return video;
                    }


                }
            }
        }

        public void Update(UserProfile profile)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE UserProfile
                                           SET [Name], Email, ImageUrl, DateCreated
                                         WHERE Id=@id";

                    cmd.Parameters.AddWithValue("@id", profile.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
