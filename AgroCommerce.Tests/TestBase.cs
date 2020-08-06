using AgroCommerce.Core;
using AgroCommerce.Data;
using AgroCommerce.Tests.TestData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Tests
{
    public class TestBase
    {
        protected ApplicationDbContext GetSampleData(string db)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(databaseName: db);

            var options = builder.Options;

            var context = new ApplicationDbContext(options);

            //Get sample data and save them for testing

            var admin = new List<ApplicationUser>();
            var seller = new List<ApplicationUser>();
            var buyer = new List<ApplicationUser>();
            var farm = new List<Farm>();
            var listing = new List<Listing>();
            var animalType = new List<AnimalType>();
            var review = new List<Review>();
            var transaction = new List<Transaction>();
            var locationMgt = new List<LocationMgt>();
            var dataFactory = new DataFactory();


            //admin 3
            admin.Add(dataFactory.GetAdminUser("34fg5-56abc-43frs-Gdh4", "admin@gmail.com", "admin", "adminFirst", "adminLast", "Ade123dfja;sdfhad=asdfhad;fjdadf", "admin", "imagepath1"));
            admin.Add(dataFactory.GetAdminUser("34fg5-5fgubc-43frs-Gdh4", "admin2@gmail.com", "admin2", "admin2First", "admin2Last", "Ade123dfja;sdfhad=asdfhad;fjdadf", "admin", "imagepath1"));
            admin.Add(dataFactory.GetAdminUser("34fg5-556abc-43frs-Gdh4", "admin3@gmail.com", "admin3", "admin3First", "admin3Last", "Ade123dfja;sdfhad=asdfhad;fjdadf", "admin", "imagepath1"));

            context.Users.AddRange(admin);
            context.SaveChanges();

            //seller 3
            seller.Add(dataFactory.GetSellerUser("2efhi-556abc-43frs-Gdh4", "seller@gmail.com", "seller", "sellerFirst", "sellerLast", "Ade123dfja;sdfhad=asdfhad;fjdadf", "seller", "imagepath"));
            seller.Add(dataFactory.GetSellerUser("2fir-556abc-43frs-Gdh4", "seller1@gmail.com", "seller1", "seller1First", "seller1Last", "Ade123dfja;sdfhad=asdfhad;fjdadf", "seller", "imagepath1"));
            seller.Add(dataFactory.GetSellerUser("2fhu-556abc-43frs-Gdh4", "seller2@gmail.com", "seller2", "seller2First", "seller2Last", "Ade123dfja;sdfhad=asdfhad;fjdadf", "seller", "imagepath2"));

            context.Users.AddRange(seller);
            context.SaveChanges();

            //buyer 3
            buyer.Add(dataFactory.GetBuyerUser("1fbi-556abc-43frs-Gdh4", "buyer@gmail.com", "buyer", "buyerFirst", "buyerLast", "Ade123dfja;sdfhad=asdfhad;fjdadf", "buyer", "imagepath"));
            buyer.Add(dataFactory.GetBuyerUser("1gfiu-556abc-43frs-Gdh4", "buyer1@gmail.com", "buyer1", "buyer1First", "buyer1Last", "Ade123dfja;sdfhad=asdfhad;fjdadf", "buyer", "imagepath1"));
            buyer.Add(dataFactory.GetBuyerUser("1uifh-556abc-43frs-Gdh4", "buyer2@gmail.com", "buyer2", "buyer2First", "buyer2Last", "Ade123dfja;sdfhad=asdfhad;fjdadf", "buyer", "imagepath2"));

            context.Users.AddRange(buyer);
            context.SaveChanges();

            //farm 3
            farm.Add(dataFactory.GetFarm(654700467436, "Farm", "imagepath", "2efhi-556abc-43frs-Gdh4", "location", true));
            farm.Add(dataFactory.GetFarm(676464647647, "Farm1", "imagepath1", "2fir-556abc-43frs-Gdh4", "location1", true));
            farm.Add(dataFactory.GetFarm(864476576576, "Farm2", "imagepath2", "2fhu-556abc-43frs-Gdh4", "location2", true));

            context.Farm.AddRange(farm);
            context.SaveChanges();

            //animalType 3
            animalType.Add(dataFactory.GetAnimalType(749474998, "new class, bro class", "new breed, sis breed", "Goat"));
            animalType.Add(dataFactory.GetAnimalType(784058372, "new class2, bro class2", "new breed2, sis breed2", "Pig"));
            animalType.Add(dataFactory.GetAnimalType(735477575, "new class3, bro class3", "new breed3, sis breed3", "Cow"));

            context.AnimalType.AddRange(animalType);
            context.SaveChanges();

            return context;
        }
    }
}
