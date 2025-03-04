using Api.Application.Bases;

namespace Api.Application.Features.Auth.Exceptions
{
    public class EmailOrPasswordShouldNotBeInvalidException : BaseException
    {
        public EmailOrPasswordShouldNotBeInvalidException() : base("Kullanıcı adı veya Şifre Yanlış!") { }
        

    }
    
}
