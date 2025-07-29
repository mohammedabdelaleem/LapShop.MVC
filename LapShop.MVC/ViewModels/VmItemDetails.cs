namespace LapShop.MVC.ViewModels;

public class VmItemDetails
{
	public ItemResponse Item { get; set; } = null!;
	public List<TbItemImage> ItemImages { get; set; } = [];
	public List<ItemResponse> RelatedItems { get; set; } = [];
}
