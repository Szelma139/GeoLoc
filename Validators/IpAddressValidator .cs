using FluentValidation;
using System.Net;

namespace Backend.Validators
{
    public class IpAddressValidator : AbstractValidator<string>
    {
        public IpAddressValidator()
        {
            RuleFor(ip => ip)
                .Must(BeValidIpAddress)
                .WithMessage("IP address is not valid or it is not ip adress.");
        }

        private bool BeValidIpAddress(string ip)
        {
            return IPAddress.TryParse(ip, out IPAddress ipAddress);
        }
    }
}
