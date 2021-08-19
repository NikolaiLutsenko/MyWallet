using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWallet.Data.Entities;

namespace MyWallet.Data.Configurations
{
	class HistoryLineEntityConfiguration : IEntityTypeConfiguration<HistoryLineEntity>
	{
		public void Configure(EntityTypeBuilder<HistoryLineEntity> builder)
		{
			builder.ToTable("history_lines");

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id);

			builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
			builder.Property(x => x.Date).IsRequired();
			builder.Property(x => x.CategoryId).IsRequired();
			builder.Property(x => x.Type).IsRequired();
			builder.Property(x => x.Amount).HasColumnType("decimal(38,2)").IsRequired();

			builder.HasIndex(x => x.Date);

			builder.HasOne(x => x.Category).WithMany(x => x.HistoryLines).HasForeignKey(x => x.CategoryId);
		}
	}
}
