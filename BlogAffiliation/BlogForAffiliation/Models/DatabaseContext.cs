using System;
using Microsoft.EntityFrameworkCore;
using Sxylo_Stock.Model;
using Sxylo_Stock.Model.Entities;

namespace Sxylo_Stock.Model
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Article> articles { get; set; }

        public DbSet<AssocColorArticle> assocColorArticles { get; set; }

        public DbSet<AssocSizeArticle> assocSizeArticles { get; set; }

        public DbSet<Category> category { get; set; }

        public DbSet<ColorArticle> colorArticles { get; set; }
        public DbSet<PictureArticle> pictureArticles { get; set; }

        public DbSet<SizeArticle> sizeArticles { get; set; }

        public DbSet<SubCategory> subCategory { get; set; }



    }
}

