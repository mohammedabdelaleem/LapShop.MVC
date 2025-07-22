using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LapShop.MVC.Models;

public partial class TbCategory
{

    [ValidateNever]
    public int CategoryId { get; set; }

	[Required ,Length(3,50,ErrorMessage = "Required Category Name With Length Between 3 and 50")] 
    public string CategoryName { get; set; } = null!;

	[ValidateNever]
	public string CreatedBy { get; set; } = null!;

	[ValidateNever]
	public DateTime CreatedDate { get; set; }

    public int CurrentState { get; set; }

	[ValidateNever]
	public string ImageName { get; set; } = string.Empty;

    public bool ShowInHomePage { get; set; }

	[ValidateNever]
	public string? UpdatedBy { get; set; }

	[ValidateNever]
	public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
