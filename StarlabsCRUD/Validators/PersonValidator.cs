using FluentValidation;
using StarlabsCRUD.Models;

namespace StarlabsCRUD.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");
            RuleFor(x => x.Age).InclusiveBetween(0,150).WithMessage("Age must be between 0 and 150");
        }
    }
}
