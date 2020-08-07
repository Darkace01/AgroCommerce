using AgroCommerce.Core;
using AgroCommerce.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroCommerce
{
    public static class DataBaseSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            // add some initial/default data here
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (await UserManager.FindByEmailAsync("seller@email.com") == null && await UserManager.FindByEmailAsync("buyer@email.com") == null)
                {
                    var animalTypes = new List<AnimalType>
            {
                new AnimalType{Name="Pig",Class="Korean Native Pig",Breed="Cerole Pig,Duroc,Forest Mountain,Gascon"},
                new AnimalType{Name="Goat", Class="Nubian,Saanen,Toggenburg", Breed="Alpine,Nigerian Dwarfs,Boer"},
                new AnimalType{Name="Cow", Class="Hereford,Jersey,Charolais", Breed="Brown Swiss,Limousin,Highland,Brangus"},
                new AnimalType{Name="Chicken", Class="Plymouth,Rhode Island",Breed="Silkie,Wyandotte,Brahma,Coachin"},
                new AnimalType{Name="Horse", Class="American Paint Horse,Shetland,Arabian,Morgan",Breed="Mustang,Shire,Belgian"},
                new AnimalType{Name="Fish",Class="Cyprinids,Characins,Anabantoids",Breed="CatFish,GoldFish,Killifish"}
            };
                    animalTypes.ForEach(s => context.AnimalType.Add(s));


                    ApplicationUser seller = new ApplicationUser()
                    {
                        UserName = "seller",
                        PhoneNumber = "08117930419",
                        Email = "seller@email.com",
                        IsBasicProfileDetailsSet = true,
                        FirstName = "Adebakin",
                        LastName = "Olofinjana",
                        Address = "54 Presidential Road",
                        LocalGovtArea = "Eti osa",
                        City = "Lagos",
                        State = "Lagos",
                        Country = "Nigeria",
                        ZipCode = "200987",
                        role = "seller"
                    };
                    string sellerPassword = "seller123";
                    if (!await roleManager.RoleExistsAsync("Seller"))
                    {
                        seller.role = "Seller";

                        await roleManager.CreateAsync(new IdentityRole("Seller"));
                    }
                    var chkUser = UserManager.CreateAsync(seller, sellerPassword);
                    if (chkUser.IsCompletedSuccessfully)
                    {
                        var result1 = UserManager.AddToRoleAsync(seller, "seller");

                        //setup Farm
                        Farm farm = new Farm()
                        {
                            ImagePath = "https://cdn.pixabay.com/photo/2017/05/19/15/16/countryside-2326787__340.jpg",
                            Name = "Segun Tokede Farm",
                            Location = "Iyana Ipaja",
                            ApplicationUserId = seller.Id,
                            //FarmOwner = seller,
                            IsActive = true,
                        };

                        seller.IsAccountComplete = true;
                        context.Farm.Add(farm);

                        //add listings
                        var listing = new List<Listing>
                    {
                        new Listing{Title="Chicken",Description="Well grown chicken with natural feed",Price=5000,Location=farm.Location,Age="2 months 3 weeks old",AgeYear=0,
                          AgeMonth=2,AgeWeek=3,AgeConvertedToWeeks=11,  Gender ="Male",
                        AnimalType="Chicken",Class="Plymouth",Breed="Silkie",ImagePath="https://cdn.pixabay.com/photo/2017/07/20/15/21/cock-2522623__340.jpg", Farm=farm},

                        new Listing{Title="Cows",Description="Cows with nurished milks",Price=50000,Location=farm.Location,Age="1 years 2 weeks old",AgeYear=1,
                          AgeMonth=0,AgeWeek=2,AgeConvertedToWeeks=54,Gender="Male",
                        AnimalType="Cow",Class="Jersey",Breed="Brown Swiss",ImagePath="https://cdn.pixabay.com/photo/2019/07/21/12/10/cow-4352623__340.jpg", Farm=farm},

                        new Listing{Title="Cat Fish",Description="Big catfishes for sale",Price=5000,Location=farm.Location,Age="5 weeks old",AgeYear=0,
                          AgeMonth=0,AgeWeek=5,AgeConvertedToWeeks=5,Gender="Female",
                        AnimalType="Fish",Class="Cyprinids",Breed="Catfish",ImagePath="https://cdn.pixabay.com/photo/2016/11/23/15/40/animal-1853610__340.jpg", Farm=farm},

                        new Listing{Title="Horse",Description="fast and strength filled horses, ",Price=200000,Location=farm.Location,Age="2 years old",AgeYear=2,
                          AgeMonth=0,AgeWeek=0,AgeConvertedToWeeks=104,Gender="Male",
                        AnimalType="Horse",Class="Shetland",Breed="Mustang",ImagePath="https://cdn.pixabay.com/photo/2017/01/16/19/17/horses-1984977__340.jpg", Farm=farm},

                        new Listing{Title="Fat Chicken",Description="Well grown chicken with natural feed",Price=50000,Location=farm.Location,Age="3 monts old",AgeYear=0,
                          AgeMonth=3,AgeWeek=0,AgeConvertedToWeeks=12,Gender="Female",
                        AnimalType="Chicken",Class="Plymouth",Breed="Silkie",ImagePath="https://toppng.com/public/uploads/preview/broiler-chicken-png-11552936097lsgs8etzsj.png", Farm=farm},

                        new Listing{Title="Horse for sale",Description="fast and strength filled horses, ",Price=200000,Location=farm.Location,Age="3 years old",AgeYear=3,
                          AgeMonth=0,AgeWeek=0,AgeConvertedToWeeks=156,Gender="Female",
                        AnimalType="Horse",Class="Shetland",Breed="Mustang",ImagePath="https://thehorse.com/wp-content/uploads/2017/01/iStock-510488648.jpg", Farm=farm},

                        new Listing{Title="Tilapia Fish",Description="Big Tilapia fish for sale",Price=4000,Location=farm.Location,Age="5 weeks old",AgeYear=0,
                          AgeMonth=0,AgeWeek=5,AgeConvertedToWeeks=5,Gender="Female",
                        AnimalType="Fish",Class="Cyprinids",Breed="Catfish",ImagePath="https://4.imimg.com/data4/MF/FF/MY-26293601/blue-tilapia-fish-500x500.jpg", Farm=farm},
                    };
                        listing.ForEach(l => context.Listing.Add(l));

                    }

                    ApplicationUser buyer = new ApplicationUser()
                    {
                        UserName = "buyer",
                        PhoneNumber = "0903456735",
                        Email = "buyer@email.com",
                        IsBasicProfileDetailsSet = true,
                        FirstName = "John",
                        LastName = "Doe",
                        Address = "56 Candos Road",
                        LocalGovtArea = "Alimosho",
                        City = "Lagos",
                        State = "Lagos",
                        Country = "Nigeria",
                        ZipCode = "100456",
                        role = "buyer"
                    };
                    string buyerPassword = "buyer123";
                    if (!await roleManager.RoleExistsAsync("Buyer"))
                    {
                        seller.role = "Buyer";

                        await roleManager.CreateAsync(new IdentityRole("Buyer"));
                    }
                    var chkUser2 = UserManager.CreateAsync(buyer, buyerPassword);
                    if (chkUser2.IsCompletedSuccessfully)
                    {
                        var result1 = UserManager.AddToRoleAsync(buyer, "buyer");

                    }

                    Transaction transaction = new Transaction()
                    {
                        ListingId = 3,
                        Listing = context.Listing.Where(p => p.ID == 3).First(),
                        Seller = context.Users.Where(s => string.Compare(s.UserName, "seller", true) == 0).First(),
                        SellerId = context.Users.Where(s => string.Compare(s.UserName, "seller", true) == 0).First().Id,
                        Buyer = context.Users.Where(s => string.Compare(s.UserName, "buyer", true) == 0).First(),
                        BuyerId = context.Users.Where(s => string.Compare(s.UserName, "buyer", true) == 0).First().Id,
                        Quantity = 3,
                        Price = 3000,
                        progress = Transaction.Progress.Successful,
                    };
                    context.Transaction.Add(transaction);
                    
                    Transaction transaction2 = new Transaction()
                    {
                        ListingId = 1,
                        Listing = context.Listing.Where(p => p.ID == 1).First(),
                        Seller = context.Users.Where(s => string.Compare(s.UserName, "seller", true) == 0).First(),
                        SellerId = context.Users.Where(s => string.Compare(s.UserName, "seller", true) == 0).First().Id,
                        Buyer = context.Users.Where(s => string.Compare(s.UserName, "buyer", true) == 0).First(),
                        BuyerId = context.Users.Where(s => string.Compare(s.UserName, "buyer", true) == 0).First().Id,
                        Quantity = 2,
                        Price = 8000,
                        progress = Transaction.Progress.Processing,
                    };
                    context.Transaction.Add(transaction2);
                    
                    Review review1 = new Review()
                    {
                        ReviewComment = "I really enjoyed the value I got from the product",
                        Transaction = context.Transaction.Where(t => t.ID == 1).FirstOrDefault(),
                        TransactionId = 1,
                    };
                    context.Review.Add(review1);

                    Review review2 = new Review()
                    {
                        ReviewComment = "If I could rate this product over 100 percent, I would",
                        Transaction = context.Transaction.Where(t => t.ID == 2).FirstOrDefault(),
                        TransactionId = 2,
                    };
                    context.Review.Add(review2);
                    context.SaveChanges();
                }
            }
        }
    }
}
