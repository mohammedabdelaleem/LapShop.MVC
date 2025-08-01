using LapShop.MVC.Contracts.Auth;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LapShop.MVC.Controllers;
public class AuthController
	(UserManager<ApplicationUser> userManager,
	SignInManager<ApplicationUser> signInManager): Controller
{
	private readonly UserManager<ApplicationUser> _userManager = userManager;
	private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

	public IActionResult Register(string returnUrl)
	{
		ViewBag.ReturnUrl = returnUrl;
		return View(new RegisterRequest("", "", "",""));
	}


	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Register(RegisterRequest request, string returnUrl = null, CancellationToken cancellationToken = default)
	{
		if (!ModelState.IsValid)
			return View(nameof(Register), request);

		if (await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken))
		{
			ModelState.AddModelError("Email.Duplicate", "This Email Is Found");
			return View(nameof(Register), request);
		}

		var user = new ApplicationUser()
		{
			FirstName = request.FirstName,
			LastName = request.LastName,
			Email = request.Email,
			UserName = request.Email,
		};

		var result = await _userManager.CreateAsync(user, request.Password);

		if (result.Succeeded)
		{
			await _signInManager.SignInAsync(user, isPersistent: true);

			if (!string.IsNullOrEmpty(returnUrl))
				return Redirect(returnUrl);

			return RedirectToAction("Index", "Home");
		}

		foreach (var error in result.Errors)
		{
			ModelState.AddModelError("", error.Description);
		}

		return View(request);
	}



	public IActionResult Login(string returnUrl)
	{
		ViewBag.ReturnUrl = returnUrl;
		return View(new LoginRequest("",""));
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginRequest request, string returnUrl, CancellationToken cancellationToken=default)
	{
		if (!ModelState.IsValid)
			return View(request);

		var user = new ApplicationUser()
		{
			Email = request.Email,
			UserName = request.Email,
		};

		var loginResult = await _signInManager.PasswordSignInAsync(user.Email, request.Password, true, true);

		if (loginResult.Succeeded)
		{
			if(string.IsNullOrEmpty(returnUrl))
			{
				return Redirect("~/");
			}else
			{
				return Redirect(returnUrl);
			}
		}

		return View();
	}


	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		return Redirect("~/");
	}

	public IActionResult AccessDenied()
	{
			return View();
	}

	




}
