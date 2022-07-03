using System.Security.Cryptography;
using System.Text;
using Api.DataContext;
using Api.DTO;
using Api.Entitis;
using Api.JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDBContext _dataContext;
        private readonly ITokenServices _tokenServies;
        public AccountController(AppDBContext dataContext, ITokenServices tokenServies)
        {
            _tokenServies = tokenServies;
            _dataContext = dataContext;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserTokenDTO>> Register(RegisterDTO mdl)
        {
            if (await CheckUserName(mdl.UserName.ToLower())) return BadRequest("User Name exites");
            using var hmac = new HMACSHA512();
            var user = new User
            {
                UserName = mdl.UserName.ToLower(),
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(mdl.Password)),
                passwordSalt = hmac.Key
            };
            _dataContext.users.Add(user);
            await _dataContext.SaveChangesAsync();
            return new UserTokenDTO
            {
                UserName = user.UserName,
                Token =_tokenServies.CreateWebToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO mdl)
            {

                var user = await _dataContext.users.SingleOrDefaultAsync(x => x.UserName == mdl.UserName.ToLower());
                if (user == null) return Unauthorized("this user not exites");
                using var hmac = new HMACSHA512(user.passwordSalt);
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(mdl.Password));
                for (var i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != user.passwordHash[i]) return BadRequest("Invalid Password");
                }
                return Ok(user);

            }
            private async Task<bool> CheckUserName(string userName)
            {
                return await _dataContext.users.AnyAsync(x => x.UserName == userName.ToLower());
            }

        }
    }