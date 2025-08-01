namespace LapShop.MVC.Contracts.Auth;

public record LoginRequest
(
	string Email, 
	string Password
	);