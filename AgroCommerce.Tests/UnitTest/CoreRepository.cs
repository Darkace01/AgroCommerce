using AgroCommerce.Data;
using AgroCommerce.Data.Implementations;
using AgroCommerce.Services.Contracts;
using AgroCommerce.Services.Implementations;
using AgroCommerce.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AgroCommerce.Tests.UnitTest
{
    public class CoreRepository : TestBase
    {
        [Fact]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            using (var context = GetSampleData(nameof(GetAllUsers_ShouldReturnAllUsers)))
            {
                //Arrange
                var _unitOfWork = new UnitOfWork(context);

                //Act
                var users = _unitOfWork.UserAccountRepo.GetAll();
               

                //Assert
                Assert.Equal(9, users.Count());
               
            }
        }

        [Fact]
        public async Task CreateNewFarm_ShouldCreateNewFarm()
        {
            using (var context = GetSampleData(nameof(CreateNewFarm_ShouldCreateNewFarm)))
            {
                //Arrange
                var uow = new UnitOfWork(context);

                var dataFactory = new DataFactory();

                //Act
                //add a new user to make it 4 farm
                var farm = dataFactory.GetFarm(9, "Hello Farm", "imagePath", "2fhu-556abc-43frs-Gdh4", "Hello Location", true);
                var owner = dataFactory.GetSellerUser("2fhu-5456abc-43frs-Gdh4", "seller3@gmail.com", "seller3", "seller3First", "seller3Last", "Ade123dfja;sdfhad=asdfhad;fjdadf", "seller", "imagepath3");
                uow.FarmRepo.Add(farm);
                uow.UserAccountRepo.Add(owner);
                await uow.Save();
                var allFarm = uow.FarmRepo.GetAll();
               
                //Assert
                Assert.Equal("Hello Farm", farm.Name);
                Assert.Equal(4, allFarm.Count());
            }
        }
    }
}