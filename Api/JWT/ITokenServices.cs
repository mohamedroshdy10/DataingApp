using Api.Entitis;

namespace Api.JWT
{
    public interface ITokenServices
    {
        public string CreateWebToken(User user);
    }
}