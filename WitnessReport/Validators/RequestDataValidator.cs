using FluentValidation;
using WitnessReports.Models;

namespace WitnessReports.Validators
{
    public class RequestDataValidator : AbstractValidator<RequestData>
    {
        public RequestDataValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required!");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required!");
        }
    }
}
