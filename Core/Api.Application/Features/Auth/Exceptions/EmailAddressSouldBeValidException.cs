using Api.Application.Bases;

namespace Api.Application.Features.Auth.Exceptions
{
    public class EmailAddressSouldBeValidException : BaseException
    {
        public EmailAddressSouldBeValidException() : base("Böyle bir email adresi bulunmamaktadır!") { }

    }

}
