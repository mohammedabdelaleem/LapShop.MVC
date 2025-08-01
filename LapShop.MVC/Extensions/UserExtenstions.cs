using System.Security.Claims;

namespace LapShop.MVC.Extensions;

public static class UserExtenstions
{
	public static string? GetUserId(this ClaimsPrincipal user) =>
		user.FindFirstValue(ClaimTypes.NameIdentifier);
}
