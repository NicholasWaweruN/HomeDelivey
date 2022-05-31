
//using DataAccessLayer.ViewModels;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace Payroll.Authentication
//{
//    public class AuthenticationServices
//    {
//        private readonly IConfiguration _configuration;
//        private readonly IHttpContextAccessor _contextAccessor;

//        public AuthenticationServices(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
//        {
//            _configuration = configuration;
//            _contextAccessor = httpContextAccessor;
//        }
//        public Task<Response> Login(bool status)
//        {

//            if (status == true)
//            {

//                var authClaims = new List<Claim>
//                {
//                    new Claim(ClaimTypes.Name, user.UserName),
//                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                };
//                var token = GetToken(authClaims);

//                var tokendetails = (new
//                {
//                    token = new JwtSecurityTokenHandler().WriteToken(token),
//                    expiration = token.ValidTo
//                });

//                return new Response { Status = "Success", Expiration = tokendetails.expiration, Token = tokendetails.token };
//            }

//            return new Response { Status = "Failed", StatusMessage = "PassWord Or UserName Is InCorrect" };
//        }
//        private JwtSecurityToken GetToken(List<Claim> authClaims)
//        {
//            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

//            var token = new JwtSecurityToken(
//                issuer: _configuration["JWT:ValidIssuer"],
//                audience: _configuration["JWT:ValidAudience"],
//                expires: DateTime.Now.AddHours(3),
//                claims: authClaims,
//                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
//                );
//            return token;
//        }
//    }
//}