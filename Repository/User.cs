using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Restul_Web_Assessment.Interfaces;
using Restul_Web_Assessment.Repository.Models;
using Restul_Web_Assessment.Repository.PostModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restul_Web_Assessment.Repository
{
    public class User: IUser
    {
        private readonly IConfiguration _config;
        private readonly BankingDbContext _db;
        public User(IConfiguration config, BankingDbContext db)
        {
            _config = config;
            _db = db;
        }

        public string LoginAuthorize(string idNumber, string password)
        {
            var results = GetUser(idNumber, password);
            try
            {


                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("IDNumber", results.Idnumber),
                    new Claim("Password", results.Password)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(5),
                    signingCredentials: signIn);
                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                return tokenValue;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }

        public UserModel PostUser(UserDTO userData)
        {
            try
            {
                var user = new UserModel
                {
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                    DateOfBirth = userData.DateOfBirth,
                    Idnumber = userData.Idnumber,
                    ResidentialAddress = userData.ResidentialAddress,
                    MobileNumber = userData.MobileNumber,                   //I changed this value to varchar cos an intiger with
                                                                            //a zero at the beggining fails, also sql ignores the zero at the start
                    EmailAddress = (userData.EmailAddress != null) ? userData.EmailAddress : null,
                    Password = userData.Password
                };
                _db.Users.Add(user);
                _db.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + " Ensure that a user with the same IDNumber does not exist");
            }
            return null;
        }

        private UserModel? GetUser(string idNumber, string password)
        {
            try
            {
                return _db.Users.SingleOrDefault(usr => usr.Idnumber == idNumber && usr.Password == password);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + " Wrong credentials, no user with IDNumber " + idNumber + " and " + password);
                return null;
            }
        }
    }
}
