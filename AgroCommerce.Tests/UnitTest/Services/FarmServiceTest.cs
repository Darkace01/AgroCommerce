using AgroCommerce.Data;
using AgroCommerce.Data.Implementations;
using AgroCommerce.Services.Contracts;
using AgroCommerce.Services.Implementations;
using AgroCommerce.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AgroCommerce.Tests.UnitTest.Services
{
    public class FarmServiceTest : TestBase
    {

        [Fact]
        public void GetAll_ShouldReturnAllFarms()
        {
            using (var context = GetSampleData(nameof(GetAll_ShouldReturnAllFarms)))
            {
                //Arrange
                var farmService = MockFarmService(context);

                //Act
                var allFarms = farmService.GetAll();

                //Assert
                Assert.NotNull(allFarms);
                Assert.Equal(3, allFarms.Count());
            }
        }

        [Fact]
        public void CreateNewFarm_ShouldCreateNewFarmData()
        {
            using (var context = GetSampleData(nameof(CreateNewFarm_ShouldCreateNewFarmData)))
            {
                //Arrange 
                var farmService = MockFarmService(context);
                var userService = MockUserService(context);
                var dataFactory = new DataFactory();
                var farm = dataFactory.GetFarm(9, "Hello Farm", "imagePath", "2fhu-556abc-43frs-Gdh4", "Hello Location", true);
                var owner = dataFactory.GetSellerUser("2fhu-5456abc-43frs-Gdh4", "seller3@gmail.com", "seller3", "seller3First", "seller3Last", "Ade123dfja;sdfhad=asdfhad;fjdadf", "seller", "imagepath3");

                //Act
                context.Users.Add(owner);
                context.SaveChanges();
                farmService.SetupFarm(farm, owner);
                var allFarms = farmService.GetAll();
                var allUsers = userService.GetTotalNumberOfUsersInDb();

                //Assert
                Assert.Equal(10, allUsers);
                Assert.NotNull(allFarms);
                Assert.Equal(4, allFarms.Count());
                
            }
        }


        #region helpers
        private IFarmService MockFarmService(ApplicationDbContext context)
        {
            var _unitOfWork = new UnitOfWork(context);

            var farmService = new FarmService(_unitOfWork);

            return farmService;
        }

        private IUserAccountService MockUserService(ApplicationDbContext context)
        {
            var _unitOfWork = new UnitOfWork(context);

            var userService = new UserAccountService(_unitOfWork);

            return userService;
        }
        #endregion
    }
}
