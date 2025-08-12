using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace LapShop.MVC.Models;

public partial class TbSlider
{
	[ValidateNever]
    public int SliderId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

	[ValidateNever]
    public string? ImageName { get; set; } = null!;


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

}
