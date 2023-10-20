using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknolojidenizi.DAL.Entities;

namespace Teknolojidenizi.DAL.Context
{
	public class SQLContext:DbContext
	{

        public SQLContext(DbContextOptions<SQLContext> opt):base(opt) 
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasOne(x => x.ParentCategory).WithMany(x => x.SubCategories).HasForeignKey(x => x.ParentID);
			modelBuilder.Entity<Product>().HasOne(x => x.Category).WithMany(x => x.Products).OnDelete(DeleteBehavior.SetNull);
			modelBuilder.Entity<Product>().HasOne(x => x.Brand).WithMany(x => x.Products).OnDelete(DeleteBehavior.SetNull);
			modelBuilder.Entity<ProductPicture>().HasOne(x => x.Product).WithMany(x => x.ProductPictures);
			

			modelBuilder.Entity<Admin>().HasData(new Admin
			{
				ID = 1,
				NameSurname="Ertan Özgür Çetin",
				LastLoginDate = DateTime.Now,
				LastLoginIP = "",
				UserName="matata340",
				Password= "37eb4119fe1540a79331b7aa00f3d163"
			});
		}
		public DbSet<Admin> Admin { get; set; }

        public DbSet<Slide> Slide { get; set; }
		public DbSet<Category> Category { get; set; }

		public DbSet<Product> Product { get; set; }

		public DbSet<Brand> Brand { get; set; }

		public DbSet<ProductPicture> ProductPicture { get; set; }

		public DbSet<Order> Order { get; set; }
		public DbSet<OrderDetail> OrderDetail { get; set; }


	}
}
