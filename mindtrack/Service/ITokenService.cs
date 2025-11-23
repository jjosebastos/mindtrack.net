using mindtrack.Model;

namespace mindtrack.Service
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }

}
