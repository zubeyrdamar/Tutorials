using Microsoft.EntityFrameworkCore;
using Walks.API.Models;

namespace Walks.API.Data
{
    public class DatabaseSeeder
    {
        public static void Seed(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<WalksDbContext>();
            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }


                if (!context.Difficulties.Any())
                {
                    context.Difficulties.AddRange(
                        new Difficulty()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Easy"
                        },
                        new Difficulty()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Medium"
                        },
                        new Difficulty()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Hard"
                        }
                    );

                    context.SaveChanges();
                }

                if(!context.Regions.Any())
                {
                    context.Regions.AddRange(
                        new Region()
                        {
                            Id = Guid.NewGuid(),
                            Name = "İstanbul",
                            Code = "34",
                            ImageUrl = "ist.png"
                        },
                        new Region()
                        {
                            Id = Guid.NewGuid(),
                            Name = "İzmir",
                            Code = "35",
                            ImageUrl = "izm.png"
                        },
                        new Region()
                        {
                            Id = Guid.NewGuid(),
                            Name = "Balıkesir",
                            Code = "10",
                            ImageUrl = "bal.png"
                        }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
