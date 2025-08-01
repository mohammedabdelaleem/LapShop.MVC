using FluentValidation;
using LapShop.MVC.Abstractions.Consts;

namespace LapShop.MVC.Contracts.Auth;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
	public LoginRequestValidator()
	{
		RuleFor(x => x.Email)
			.NotEmpty()
			.EmailAddress();

		RuleFor(x => x.Password)
			.NotEmpty()
			.Matches(RegexPatterns.Password)
			.WithMessage("Password Should Be At Least 8 Digits And Should Contains Lowercase , Uppercase And NonAlphanumeric");

	
	}
}
