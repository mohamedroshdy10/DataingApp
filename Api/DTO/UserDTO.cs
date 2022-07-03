using System.ComponentModel.DataAnnotations;

namespace Api.DTO
{
    public class UserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        
    }

    public class RegisterDTO
    :UserDTO{

    }
    public class LoginDTO
    :UserDTO{

    }
    public class UserTokenDTO{
        public string UserName { get; set; }
    public string Token { get; set; }
    }

}