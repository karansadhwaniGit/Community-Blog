using CommunityBlog.Models;

namespace CommunityBlog.Services
{
    public class TokenHandlerService : ITokenHandlerService
    {
        private readonly DataContext _context;
        public TokenHandlerService(DataContext context)
        {
            _context = context;
        }
        public TokenModel? GetValidExistingToken(int userid, int is_remember)
        {
            long currenttime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            TokenModel? token = _context.Token.FirstOrDefault(x => x.User_Id == userid
            && x.Is_Remember == is_remember);
            if (token != null)
            {
                long created_token_date = DateTimeOffset.Parse(token.Expires_At).ToUnixTimeMilliseconds();
                if (created_token_date >= currenttime)
                    return token;
            }
            return null;
        }
        public bool IsTokenValid(string token, int is_remember)
        {
            long currenttime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            TokenModel _token = _context.Token.Where(x => x.Token == token && x.Is_Remember == is_remember).FirstOrDefault();
            if (_token != null)
            {
                long created_token_date = DateTimeOffset.Parse(_token.Expires_At).ToUnixTimeMilliseconds();
                if (created_token_date >= currenttime)
                    return true;
            }
            return false;
        }
        public TokenModel CreateToken(int user_id, int is_remembered)
        {
            var tokenExists = GetValidExistingToken(user_id, is_remembered);
            if (tokenExists != null)
                return tokenExists;
            var time = is_remembered == 1 ? DateTime.Now.AddHours(1) : DateTime.Now.AddMinutes(10);
            TokenModel token = new TokenModel();
            token.User_Id = user_id;
            token.Token = Hash.GenerateToken(user_id);
            token.Expires_At = time.ToString();
            token.Is_Remember = is_remembered;
            _context.Token.Add(token);
            _context.SaveChanges();
            return token;
        }
        public bool DeleteTokenByTokenValue(string token)
        {
            var _token = _context.Token.Where(x => x.Token == token).FirstOrDefault();
            if (_token != null)
            {
                _context.Token.Remove(_token);
                _context.SaveChanges(true);
                return true;
            }
            return false;
        }
        public bool DeleteToken(int userid, int is_remember)
        {
            var _token = _context.Token.Where(x => x.User_Id == userid && x.Is_Remember == is_remember).FirstOrDefault();
            if (_token != null)
            {
                _context.Token.Remove(_token);
                _context.SaveChanges(true);
                return true;
            }
            return false;
        }
        public int GetIDFromToken(string token, int is_remember)
        {
            var _token = _context.Token.Where(x => x.Token == token && x.Is_Remember == is_remember).FirstOrDefault();
            if (_token != null && IsTokenValid(token,is_remember))
            {
                return _token.User_Id;
            }
            return -1;
        }
    }
}
