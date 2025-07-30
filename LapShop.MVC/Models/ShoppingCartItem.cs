namespace LapShop.MVC.Models;

public class ShoppingCartItem
{
	public int ItemId { get; set; }
	public string ItemName { get; set; }
	public string IamgeName { get; set; }
	public int Quantity { get; set; } = 1;
	public decimal Price { get; set; }
	public decimal Total { get; set; }


}
