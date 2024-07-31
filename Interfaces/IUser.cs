using Restul_Web_Assessment.Repository.Models;
using Restul_Web_Assessment.Repository.PostModels;

namespace Restul_Web_Assessment.Interfaces
{
    public interface IUser
    {
        string LoginAuthorize(string idNumber, string password);
        UserModel PostUser(UserDTO userData);
    }
}
