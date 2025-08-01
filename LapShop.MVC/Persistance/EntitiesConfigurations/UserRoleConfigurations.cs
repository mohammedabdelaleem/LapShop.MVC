﻿namespace LapShop.MVC.Persistance.EntitiesConfigurations;

public class UserRoleConfigurations : IEntityTypeConfiguration<IdentityUserRole<string>>
{
	public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
	{

		builder.HasData(
		new IdentityUserRole<string>
		{
			UserId = DefaultUsers.AdminId,
			RoleId = DefaultRoles.AdminRoleId
		}
			);
	}
}