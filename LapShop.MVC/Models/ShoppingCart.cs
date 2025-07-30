namespace LapShop.MVC.Models;

public class ShoppingCart
{
	public List<ShoppingCartItem> Items { get; set; } = [];
	public decimal Total { get; set; }
}
