using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWallet.Data.Entities;

namespace MyWallet.Data.Configurations
{
	class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
	{
		public void Configure(EntityTypeBuilder<CategoryEntity> builder)
		{
			builder.ToTable("categories");

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).IsRequired();

			builder.Property(x => x.Label).IsRequired().IsUnicode().HasMaxLength(30);
			builder.Property(x => x.Color).IsRequired().IsUnicode().HasMaxLength(10);

			builder.HasOne(x => x.Parrent).WithMany(x => x.Child);

		}
	}
}
