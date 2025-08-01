using FluentValidation;
using LapShop.MVC.Abstractions.Consts;

namespace LapShop.MVC.Contracts.Auth;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
	public RegisterRequestValidator()
	{
		RuleFor(x=>x.Email)
			.NotEmpty()
			.EmailAddress();

		RuleFor(x => x.Password)
			.NotEmpty()
			.Matches(RegexPatterns.Password)
			.WithMessage("Password Should Be At Least 8 Digits And Should Contains Lowercase , Uppercase And NonAlphanumeric");

		RuleFor(x => x.FirstName)
			.NotEmpty()
			.Length(3, 225); // look at DB to prevent the trancation 

		RuleFor(x => x.LastName)
			.NotEmpty()
			.Length(3, 225);
	}
}
