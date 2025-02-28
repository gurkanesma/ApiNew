using Api.Application.Bases;
using Api.Application.Features.Auth.Rules;
using Api.Application.Interfaces.AutoMapper;
using Api.Application.Interfaces.Tokens;
using Api.Application.Interfaces.UnitOfWorks;
using Api.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Application.Features.Auth.Command.Login
{
    public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly AuthRules _authRules;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginCommandHandler(
            UserManager<User> userManager,
            ITokenService tokenService,
            AuthRules authRules,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) 
            : base(mapper, unitOfWork, httpContextAccessor)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _authRules = authRules;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
             User user= await _userManager.FindByEmailAsync(request.Email);
            bool checkPassword= await _userManager.CheckPasswordAsync(user, request.Password);

            await _authRules.EmailOrPasswordShouldNotBeInvalid(user, checkPassword);

            IList<string> roles = await _userManager.GetRolesAsync(user);

            JwtSecurityToken token = await _tokenService.CreateToken(user,roles);
            string refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays); // dönüş değerine ihtiyaç yok

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime=DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);

            string _token =new JwtSecurityTokenHandler().WriteToken(token);

            await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", _token);

            return new()
            {
                Token = _token,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };

        }
    }
}
