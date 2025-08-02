namespace LapShop.MVC.Controllers;

public class AuthController
	(UserManager<ApplicationUser> userManager,
	SignInManager<ApplicationUser> signInManager, 
	RoleManager<ApplicationRole> roleManager): Controller
{
	private readonly UserManager<ApplicationUser> _userManager = userManager;
	private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
	private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

	public IActionResult Register(string returnUrl=null)
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
			// auto login 
			await _signInManager.SignInAsync(user, isPersistent: true);

			// add to role 
			if (!await _roleManager.RoleExistsAsync(DefaultRoles.Member))
			{
				await _roleManager.CreateAsync(new ApplicationRole { Name = DefaultRoles.Member});
			}

			var roleResult = await _userManager.AddToRoleAsync(user, DefaultRoles.Member);
			if (!roleResult.Succeeded)
			{
				foreach (var error in roleResult.Errors)
					ModelState.AddModelError("", error.Description);
			}

			// redirect to the place where you came form
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



	public IActionResult Login(string returnUrl=null)
	{
		ViewBag.ReturnUrl = returnUrl;
		return View(new LoginRequest("",""));
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginRequest request, string returnUrl=null, CancellationToken cancellationToken=default)
	{
		if (!ModelState.IsValid)
			return View(request);

		if(await _userManager.FindByEmailAsync(request.Email) is not { } user)
		{
			ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			return View(request);
		}

		var loginResult = await _signInManager.PasswordSignInAsync(user.UserName!, request.Password, true, true);

		if (loginResult.Succeeded)
		{
			return string.IsNullOrEmpty(returnUrl) ? Redirect("~/") : Redirect(returnUrl);
		}

		ModelState.AddModelError(string.Empty, "Invalid login attempt.");
		return View(request);
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
