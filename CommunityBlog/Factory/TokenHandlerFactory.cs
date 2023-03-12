using CommunityBlog.Models;
using CommunityBlog.Services;

namespace CommunityBlog.Factory
{
    public class TokenHandlerFactory:ITokenHandlerFactory
    {
        private readonly ITokenHandlerService _token;
        public TokenHandlerFactory(ITokenHandlerService token)
        {
            _token = token;
        }
        public TokenModel? GetValidExistingToken(int userid, int is_remember)
        {
            return _token.GetValidExistingToken(userid, is_remember);
        }
        public bool IsTokenValid(string token, int is_remember)
        {
            return _token.IsTokenValid(token, is_remember);
        }
        public TokenModel CreateToken(int user_id, int is_remembered)
        {
            return _token.CreateToken(user_id, is_remembered);
        }
        public int GetIDFromToken(string token, int is_remember)
        {
            return _token.GetIDFromToken(token, is_remember);
        }
        public bool DeleteTokenByTokenValue(string token)
        {
            return _token.DeleteTokenByTokenValue(token);
        }
        public bool DeleteToken(int userid, int is_remember)
        {
            return _token.DeleteToken(userid, is_remember);
        }



    }
}
