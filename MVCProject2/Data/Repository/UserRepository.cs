using MVCProject2.Data.Interfaces;
using MVCProject2.Data.Models;
using System.Data.SqlClient;
using MVCProject2.Data.Models;
using MVCProject2.Services;

namespace MVCProject2.Data.Repository
{
    public class UserRepository : IUsers
    {
        private readonly AppDBContent appDBContent;
        public UserRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public void AddUser(User user)
        {
            user.ActivationCode = Guid.NewGuid();
            appDBContent.Users.Add(user);
            appDBContent.SaveChanges();
            
            var item = new RoleUser()
            {
                roleID = 3,
                userID = user.Id
            };

            appDBContent.RoleUsers.Add(item);
            //var roleID = new SqlParameter("@RoleID", 3);
            //var userID = new SqlParameter("@UserID", user.Id);
            appDBContent.SaveChanges();
        }
        public void AddAppUser(AppUser appUser)
        {
            appUser.NameIdentifier = appUser.Username;
            appDBContent.AppUsers.Add(appUser);
            appDBContent.SaveChanges();
        }
    }
}
