using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWallet.Data.Configurations;
using MyWallet.Data.Entities;

namespace MyWallet.Data
{
	public class MyWalletContext : IdentityDbContext<IdentityUser>
	{
		public DbSet<CategoryEntity> Categories { get; set; }

		public DbSet<HistoryLineEntity> HistoryLines { get; set; }

		public MyWalletContext(DbContextOptions<MyWalletContext> options)
			: base(options)
		{
			//Database.EnsureCreated();
			Database.Migrate();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
			modelBuilder.ApplyConfiguration(new HistoryLineEntityConfiguration());
			base.OnModelCreating(modelBuilder);
		}
	}
}
