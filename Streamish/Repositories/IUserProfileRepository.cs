using Streamish.Models;
using System.Collections.Generic;

namespace Streamish.Repositories
{
    public interface IUserProfileRepository
    {
        public List<UserProfile> GetAll();
        public void Delete(int id);
        public void Update(UserProfile profile);
        public UserProfile Get(int id);

        public Video GetWithAuthoredVideos(int id);
    }
}
