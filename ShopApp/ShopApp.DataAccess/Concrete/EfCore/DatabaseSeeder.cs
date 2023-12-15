﻿using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public static class DatabaseSeeder
    {
        public static void Seed()
        {
            var context = new ShopDbContext();

            if (!context.Database.GetPendingMigrations().Any())
            {
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(Categories);
                }

                if(!context.Products.Any())
                {
                    context.Products.AddRange(Products);
                }

                context.SaveChanges();
            }
        }

        private static readonly Category[] Categories =
        [
            new() { Name = "Phone" },
            new() { Name = "Computer" },
        ];

        private static readonly Product[] Products =
        [
            new() { Name = "Samsung Galaxy S10", Price = 500, ImageUrl = "s10.png" },
            new() { Name = "Samsung Galaxy S11", Price = 600, ImageUrl = "s11.png" },
            new() { Name = "Iphone 12", Price = 700, ImageUrl = "i12.png" },
            new() { Name = "Iphone 14 Max", Price = 1000, ImageUrl = "i14max.png" },
        ];
    }
}
