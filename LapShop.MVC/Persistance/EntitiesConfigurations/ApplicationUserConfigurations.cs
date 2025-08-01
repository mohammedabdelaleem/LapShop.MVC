
using LapShop.MVC.Abstractions.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LapShop.MVC.Persistance.EntitiesConfigurations;

public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		builder.Property(x => x.FirstName)
			.HasMaxLength(100);

		builder.Property(x => x.LastName)
			.HasMaxLength(100);
	
	}

	
}
