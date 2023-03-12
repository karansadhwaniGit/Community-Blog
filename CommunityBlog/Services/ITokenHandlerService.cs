using CommunityBlog.Models;

namespace CommunityBlog.Services
{
    public interface ITokenHandlerService
    {
        TokenModel? GetValidExistingToken(int userid, int is_remember);
        bool IsTokenValid(string token, int is_remember);
        TokenModel CreateToken(int user_id, int is_remembered);
        int GetIDFromToken(string token, int is_remember);
        bool DeleteTokenByTokenValue(string token);
        bool DeleteToken(int userid, int is_remember);
    }
}
