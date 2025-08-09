namespace LapShop.MVC.Contracts;


public record ItemResponse
    (
		int ItemId,
		string ItemName,
		decimal PurchasePrice,
		decimal SalesPrice,
		string? ImageName,
		string? Description,
		string? Processor,
		int? RamSize,
		string CategoryName,
	    string ItemTypeName,
	    string OsName,
		string? HardDisk,
		DateTime CreatedDate

	);
