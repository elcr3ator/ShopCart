using MVCProject2.Data.Models;

namespace MVCProject2.Data.Interfaces
{
    public interface IUsers
    {

        void AddUser(User user);

        public void AddAppUser(AppUser appUser);
    }
}
