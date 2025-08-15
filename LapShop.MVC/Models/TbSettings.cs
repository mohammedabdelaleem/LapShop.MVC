namespace LapShop.MVC.Models;

// website setting to make your website more dynamic
public class TbSettings
{
	[Key]
	public int Id { get; set; }
	public  string WebsiteName { get; set; }
	public string WebsiteDescription { get; set; }
	public string FacebookLink { get; set; }
	public string TwitterLink { get; set; }
	public string YoutubeLink { get; set; }
	public string GoogleLink { get; set; }
	public string InstgramLink { get; set; }


	[DataType(DataType.PhoneNumber)]
	public string ContactNumber { get; set; }

	// photoes
	public string Logo { get; set; }
	public string MiddleBanner { get; set; }
	public string LastBanner { get; set; }
}
