using DataGrid.Domain;
using DataGrid.Persistence.Repositories;
using FluentValidation;
using System.Threading.Tasks;

namespace DataGrid.Application.Features.Users.Command.AddUser
{
    public class UserValidator : AbstractValidator<User>
    {
        private readonly UserRepository _userRepository;

        public UserValidator(UserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(user => user.Mobile)
                .NotEmpty().WithMessage("Mobile is required.")
                .Matches(@"^05\d{8}$").WithMessage("Invalid mobile format. Must start with 05 and be 10 digits long.");

            RuleFor(user => user.MunicipalNo)
                .NotEmpty().WithMessage("Municipal number is required.");

            RuleFor(user => user.NationalId)
                .NotEmpty().WithMessage("National ID is required.");

            RuleFor(user => user.MaritalStatusId)
                .MustAsync(IsValidMaritalStatus).WithMessage("Marital status is not valid.");
            RuleFor(user => user)
                .MustAsync(BeUniqueEmail).WithMessage("Email already exists.").WithName("Email")
                .MustAsync(BeUniqueMobile).WithMessage("Mobile already exists.").WithName("Mobile")
                .MustAsync(BeUniqueMunicipalNo).WithMessage("Municipal number already exists.").WithName("MunicipalNo")
                .MustAsync(BeUniqueNationalId).WithMessage("National ID already exists.").WithName("NationalId");
        }

        private async Task<bool> BeUniqueEmail(User user, CancellationToken cancellationToken)
        {
            return await _userRepository.IsEmailUniqueAsync(user);
        }

        private async Task<bool> BeUniqueMobile(User user, CancellationToken cancellationToken)
        {
            return await _userRepository.IsMobileUniqueAsync(user);
        }

        private async Task<bool> BeUniqueMunicipalNo(User user, CancellationToken cancellationToken)
        {
            return await _userRepository.IsMunicipalNoUniqueAsync(user);
        }

        private async Task<bool> BeUniqueNationalId(User user, CancellationToken cancellationToken)
        {
            return await _userRepository.IsNationalIdUniqueAsync(user);
        }

        private async Task<bool> IsValidMaritalStatus(int maritalStatusId, CancellationToken cancellationToken)
        {
            return await _userRepository.IsMaritalStatusValidAsync(maritalStatusId);
        }
    }
}
