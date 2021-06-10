using Domain.Constants;
using Domain.Entities;
using Infrastructure.EntityFramework;
using Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public static class SeedData
    {
        public static async Task InitSysData(this ShopDbContext ctx)
        {
            try
            {
                if (ctx == null)
                    throw new ArgumentNullException(nameof(ctx));
                if (!await ctx.Users.AnyAsync())
                {
                    ctx.Users.Add(new User()
                    {
                        Id = CommonConstantsUser.UserAdminId,
                        Username = CommonConstantsUser.UsernameAdmin,
                        Password = CommonConstantsUser.PasswordAdmin,
                        Type = CommonConstantsUser.TypeAdmin,
                        CreateByDate = DateTime.Now,
                        CreatedByName = CommonConstantsUser.UsernameAdmin,
                        IsActive = true,
                        ObjectState = Infrastructure.EntityFramework.ObjectState.Added
                    });
                    ctx.SaveChanges();
                }
                if (!await ctx.PageContents.AnyAsync())
                {
                    foreach (var item in PageContentConstants.ListPageContents)
                    {
                        item.Value.ObjectState = ObjectState.Added;
                        item.Value.Id = item.Key;
                        ctx.PageContents.Add(item.Value);
                        ctx.SaveChanges();
                    }
                }
                if (!await ctx.InformationWebsites.AnyAsync())
                {
                    ctx.InformationWebsites.Add(new InformationWebsite()
                    {
                        Id = CommonConstants.WebSiteInformationId,
                        Address = CommonConstants.Address,
                        Email = CommonConstants.Email,
                        Fax = CommonConstants.Fax,
                        Logo = CommonConstants.Logo,
                        Phone = CommonConstants.Phone,
                        CreateByDate = DateTime.Now,
                        Title = CommonConstants.Title,
                        Description = CommonConstants.Description,
                        IsActive = true,
                        ObjectState = Infrastructure.EntityFramework.ObjectState.Added
                    });
                    ctx.SaveChanges();
                }
                if (!await ctx.Categories.AnyAsync())
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        ctx.Categories.Add(new Category()
                        {
                            Name = CommonConstantsCategory.Name + i,
                            Description = CommonConstantsCategory.Description + i,
                            ImageUrl = CommonConstantsCategory.ImageUrl,
                            CreateByDate = DateTime.Now,
                            IsActive = true,
                            ObjectState = Infrastructure.EntityFramework.ObjectState.Added
                        });
                        ctx.SaveChanges();
                    }

                    
                }
                if (!await ctx.Products.AnyAsync())
                {
                    for (int i = 1; i <= 50; i++)
                    {
                        var category = ctx.Categories.Skip(new Random().Next(0, ctx.Categories.Count())).First();
                        ctx.Products.Add(new Product()
                        {
                            Name = CommonConstantsProduct.Name + i,
                            Description = CommonConstantsProduct.Description + i,
                            ContentHTML = CommonConstantsProduct.ContentHTML + i,
                            CategoryId = category.Id,
                            Price = CommonConstantsProduct.Price,
                            DisplayOrder = CommonConstantsProduct.DisplayOrder,
                            IsFeatured = false,
                            IsDeleted = false,
                            CreateByDate = DateTime.Now,
                            IsActive = true,
                            HasDisplayHomePage = false,
                            ImageUrl = CommonConstantsProduct.ImageUrl,
                            ObjectState = Infrastructure.EntityFramework.ObjectState.Added
                        });;
                        ctx.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }

        public static async void Run(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ShopDbContext>();
                    if (context != null)
                    {
                        await context.InitSysData();
                    }
                }
                catch (Exception ex)
                {
                    throw;

                }
            }
        }
    }
}
