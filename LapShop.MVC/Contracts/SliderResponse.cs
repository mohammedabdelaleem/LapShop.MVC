namespace LapShop.MVC.Contracts;

public record SliderResponse(
	 int SliderId ,
	 string? Title,
	 string? Description ,
	 string ImageName
	);
