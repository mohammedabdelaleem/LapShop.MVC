﻿namespace LapShop.MVC.Abstractions.Consts;

public static class RegexPatterns
{
	public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$";

}
