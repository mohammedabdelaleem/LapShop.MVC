using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LapShop.MVC.Models;

public partial class TbItem
{
	[ValidateNever]
	public int ItemId { get; set; }


	[Required(ErrorMessage = "Item name is required.")]
	[StringLength(100, MinimumLength = 2, ErrorMessage = "Item name must be between 2 and 100 characters.")]
	public string ItemName { get; set; } = null!;


	[Required(ErrorMessage = "Sales price is required.")]
	[Range(50, 9999999, ErrorMessage = "Sales price must betweem 50 and 9999999")]
	[Display(Name = "Sales Price")]
	[DataType(DataType.Currency)]
	public decimal SalesPrice { get; set; }


	[Required(ErrorMessage = "Purchase price is required.")]
	[Range(50, 9999999, ErrorMessage = "Sales price must betweem 50 and 9999999")]
	[DataType(DataType.Currency)]
	[Display(Name = "Purchase Price")]
	public decimal PurchasePrice { get; set; }


	[Required(ErrorMessage = "Category is required.")]
	[Display(Name = "Category")]
	public int CategoryId { get; set; }


	[StringLength(255, ErrorMessage = "Image name can't exceed 255 characters.")]
	public string? ImageName { get; set; }


	[Required(ErrorMessage = "Created date is required.")]
	[DataType(DataType.Date)]
	[Display(Name = "Created Date")]
	public DateTime CreatedDate { get; set; }


	[ValidateNever]
	public string? CreatedBy { get; set; } = null!;


	[Required(ErrorMessage = "Current state is required.")]
	public int CurrentState { get; set; } = 1;


	[StringLength(100, ErrorMessage = "Updated by can't exceed 100 characters.")]
	[Display(Name = "Updated By")]
	public string? UpdatedBy { get; set; }


	[DataType(DataType.Date)]
	[Display(Name = "Updated Date")]
	public DateTime? UpdatedDate { get; set; }


	[StringLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
	public string? Description { get; set; }

	[StringLength(100, ErrorMessage = "GPU name can't exceed 100 characters.")]
	public string? Gpu { get; set; }


	[StringLength(100, ErrorMessage = "Hard disk description can't exceed 100 characters.")]
	[Display(Name = "Hard Disk")]
	public string? HardDisk { get; set; }


	[Required(ErrorMessage = "Item type is required.")]
	[Display(Name = "Item Type")]
	public int ItemTypeId { get; set; }


	[StringLength(100, ErrorMessage = "Processor name can't exceed 100 characters.")]
	public string? Processor { get; set; }


	[Range(1, 512, ErrorMessage = "RAM size must be between 1 and 512 GB.")]
	[Display(Name = "RAM Size (GB)")]
	public int RamSize { get; set; }


	[StringLength(100, ErrorMessage = "Screen resolution can't exceed 100 characters.")]
	[Display(Name = "Screen Resolution")]
	public string? ScreenReslution { get; set; }


	[StringLength(50, ErrorMessage = "Screen size can't exceed 50 characters.")]
	[Display(Name = "Screen Size")]
	public string? ScreenSize { get; set; }


	[StringLength(50, ErrorMessage = "Weight can't exceed 50 characters.")]
	public string? Weight { get; set; }


	[Required(ErrorMessage = "Operating system is required.")]
	[Display(Name = "Operating System")]
	public int OsId { get; set; }


	// Navigation Properties
	[ValidateNever]
	public virtual TbCategory? Category { get; set; }

	[ValidateNever]
	public virtual TbItemType? ItemType { get; set; }

	[ValidateNever]
	public virtual TbO? Os { get; set; }

	[ValidateNever]
	public virtual ICollection<TbItemDiscount> TbItemDiscounts { get; set; } = new List<TbItemDiscount>();

	[ValidateNever]
	public virtual ICollection<TbItemImage> TbItemImages { get; set; } = new List<TbItemImage>();

	[ValidateNever]
	public virtual ICollection<TbPurchaseInvoiceItem> TbPurchaseInvoiceItems { get; set; } = new List<TbPurchaseInvoiceItem>();

	[ValidateNever]
	public virtual ICollection<TbSalesInvoiceItem> TbSalesInvoiceItems { get; set; } = new List<TbSalesInvoiceItem>();

	[ValidateNever]
	public virtual ICollection<TbCustomer> Customers { get; set; } = new List<TbCustomer>();
}
