using App.Application.ModelView;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace App.Persistence.Services;

public class TokenProvider
{
    public class AccessTokenGenerator
    {
        private readonly IConfiguration _configuration;
        public AppIdentityDbContext _context { get; set; }
        public ApplicationUser _applicationUser { get; set; }

        /// <summary>
        /// Class'ın oluşturulması.
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_config"></param>
        /// <param name="_applicationUser"></param>
        /// <returns></returns>
        public AccessTokenGenerator(AppIdentityDbContext context,
                                    IConfiguration config,
                                    ApplicationUser applicationUser)
        {
            _configuration = config;
            _context = context;
            _applicationUser = applicationUser;
        }

        /// <summary>
        /// Kullanıcı üzerinde tanımlı tokenı döner;Token yoksa oluşturur. Expire olmuşsa update eder.
        /// </summary>
        /// <returns></returns>
        public ApplicationUserTokens GetToken()
        {
            ApplicationUserTokens userTokens = null;
            TokenView tokenInfo = null;

            //Kullanıcıya ait önceden oluşturulmuş bir token var mı kontrol edilir.
            if (_context.ApplicationUserTokens.Count(x => x.UserId == _applicationUser.Id) > 0)
            {
                //İlgili token bilgileri bulunur.
                userTokens = _context.ApplicationUserTokens.FirstOrDefault(x => x.UserId == _applicationUser.Id);

                //Expire olmuş ise yeni token oluşturup günceller.
                if (userTokens.ExpireDate <= DateTime.Now)
                {
                    //Create new token
                    tokenInfo = GenerateToken();

                    userTokens.ExpireDate = tokenInfo.Expiration.AddHours(1);
                    userTokens.Value = tokenInfo.Token;

                    _context.ApplicationUserTokens.Update(userTokens);
                }
            }
            else
            {
                //Create new token
                tokenInfo = GenerateToken();

                userTokens = new ApplicationUserTokens();

                userTokens.UserId = _applicationUser.Id;
                userTokens.LoginProvider = "SystemAPI";
                userTokens.Name = _applicationUser.Name;
                userTokens.ExpireDate = tokenInfo.Expiration;
                userTokens.Value = tokenInfo.Token;

                _context.ApplicationUserTokens.Add(userTokens);
            }

            _context.SaveChangesAsync();

            return userTokens;
        }

        /// <summary>
        /// Kullanıcıya ait tokenı siler.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteToken()
        {
            bool ret = true;

            try
            {
                //Kullanıcıya ait önceden oluşturulmuş bir token var mı kontrol edilir.
                if (_context.ApplicationUserTokens.Count(x => x.UserId == _applicationUser.Id) > 0)
                {
                    ApplicationUserTokens userTokens = userTokens = _context.ApplicationUserTokens.FirstOrDefault(x => x.UserId == _applicationUser.Id);

                    _context.ApplicationUserTokens.Remove(userTokens);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// Yeni token oluşturur.
        /// </summary>
        /// <returns></returns>
        private TokenView GenerateToken()
        {
            DateTime expireDate = DateTime.Now.AddSeconds(50);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Application:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration["Application:Audience"],
                Issuer = _configuration["Application:Issuer"],
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //Claim tanımları yapılır. Burada en önemlisi Id ve emaildir.
                    //Id üzerinden, aktif kullanıcıyı buluyor olacağız.
                    new Claim(ClaimTypes.NameIdentifier, _applicationUser.Id.ToString()),
                    new Claim("userName", _applicationUser.UserName),
                    new Claim("name", _applicationUser.Name),
                    new Claim("surname", _applicationUser.Surname),
                    new Claim("email", _applicationUser.Email),
                    new Claim("role", RoleEnum.Member.Name)
                }),

                //ExpireDate
                Expires = expireDate,

                //Şifreleme türünü belirtiyoruz: HmacSha256Signature
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            TokenView tokenInfo = new TokenView();

            tokenInfo.Token = tokenString;
            tokenInfo.Expiration = expireDate;

            return tokenInfo;
        }
    }
}

