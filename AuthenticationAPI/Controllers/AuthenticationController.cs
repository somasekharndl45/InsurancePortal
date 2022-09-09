using AuthenticationAPI.DataEntity;
using AuthenticationAPI.Model;
using AuthenticationAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private InsuranceContext dbContext { get; set; }
        private readonly ITokenService tokenService;

        public AuthenticationController(IConfiguration configuration, InsuranceContext dbContext, ITokenService tokenService)
        {
            _configuration = configuration;
            this.dbContext = dbContext;
            this.tokenService = tokenService;
        }


        [HttpPost]
        public ActionResult Authentication(UserCredentials userCredential)
        {
            try
            {
                IEnumerable<string> audience = new[]
                        {
                        _configuration["Jwt:InsuranceAudience"],
                        };
                bool isValidUser = ValidateUserCredentials(userCredential.UserName, userCredential.Password);
                if (isValidUser)
                {
                    var token = tokenService.BuildToken(_configuration["Jwt:Key"],
                        _configuration["Jwt:Issuer"],
                        audience,
                        userCredential.UserName);
                    string userRole = dbContext.UserRegistrations.Where(user => user.UserName == userCredential.UserName).Select(user => user.UserRole).FirstOrDefault();
                    return Ok(new
                    {
                        Token = token,
                        Role = userRole
                    });
                }
                return Ok(new
                {
                    Token = string.Empty,
                    Role = false
                });
            }
            catch (Exception ex)
            {
                return Ok("Error occurred while generating  authentication token");
            }
        }


        private bool ValidateUserCredentials(string userName, string password)
        {
            bool isValidUser = !(dbContext.UserRegistrations.Where(u => u.UserName == userName && u.Password == password).FirstOrDefault() is null) ? true : false;
            return isValidUser;
        }
    }

   


}
