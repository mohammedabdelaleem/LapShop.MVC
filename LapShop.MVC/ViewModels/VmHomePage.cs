namespace LapShop.MVC.ViewModels;

public class VmHomePage
{
	public List<ItemResponse> AllItems { get; set; } = [];
	public List<ItemResponse> RecommendedItems { get; set; } = [];
	public List<ItemResponse> NewItems { get; set; } = [];
	public List<ItemResponse> FreeDelivery { get; set; } = [];
	public List<ItemResponse> FeautureItems { get; set; } = [];
	public List<TbCategory> Categories { get; set; } = [];


}
