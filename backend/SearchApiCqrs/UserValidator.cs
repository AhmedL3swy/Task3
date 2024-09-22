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
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync((user, email, cancellationToken) => IsUniqueField(user, "Email", cancellationToken)).WithMessage("Email already exists.");

            RuleFor(user => user.Mobile)
                .NotEmpty().WithMessage("Mobile is required.")
                .Matches(@"^05\d{8}$").WithMessage("Invalid mobile format. Must start with 05 and be 10 digits long.")
                .MustAsync((user, mobile, cancellationToken) => IsUniqueField(user, "Mobile", cancellationToken)).WithMessage("Mobile already exists.");

            RuleFor(user => user.MunicipalNo)
                .NotEmpty().WithMessage("Municipal number is required.")
                .Matches(@"^\d{6,10}$").WithMessage("Invalid municipal number format. Must be between 6 and 10 digits long.")
                .MustAsync((user, municipalNo, cancellationToken) => IsUniqueField(user, "MunicipalNo", cancellationToken)).WithMessage("Municipal number already exists.");

            RuleFor(user => user.NationalId)
                .NotEmpty().WithMessage("National ID is required.")
                .Matches(@"^\d{10}$").WithMessage("Invalid national ID format. Must be 10 digits long.")
                .MustAsync((user, nationalId, cancellationToken) => IsUniqueField(user, "NationalId", cancellationToken)).WithMessage("National ID already exists.");

            RuleFor(user => user.MaritalStatusId)
                .NotEmpty().WithMessage("Marital status is required.");
        }

        // Consolidated uniqueness check for email, mobile, municipal number, and national ID
        private async Task<bool> IsUniqueField(User user, string propertyName, CancellationToken cancellationToken)
        {
            return propertyName switch
            {
                "Email" => await _userRepository.IsUniqueAsync(user, "Email", "Id"),
                "Mobile" => await _userRepository.IsUniqueAsync(user, "Mobile", "Id"),
                "MunicipalNo" => await _userRepository.IsUniqueAsync(user, "MunicipalNo", "Id"),
                "NationalId" => await _userRepository.IsUniqueAsync(user, "NationalId", "Id"),
                _ => true
            };
        }


    }
}
