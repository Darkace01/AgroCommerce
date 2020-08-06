using AgroCommerce.Data.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AgroCommerce.Tests
{
    public class CoreRepository : TestBase
    {
        [Fact]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            using (var context = GetSampleData(nameof(GetAllUsers_ShouldReturnAllUsers)))
            {
                //Arrange
                var unitOfWork = new UnitOfWork(context);

                //Act
                var users = unitOfWork.UserAccountRepo.GetAll();


                //Assert
                Assert.Equal(9, users.Count());

            }
        }

        [Fact]
        public void GetAllAdmins_ShouldReturnAllAdmins()
        {
            using (var context = GetSampleData(nameof(GetAllAdmins_ShouldReturnAllAdmins)))
            {
                //Arrange
                var unitOfWork = new UnitOfWork(context);

                //Act
                var admins = unitOfWork.UserAccountRepo.GetAll().Where(a => a.role == "admin");

                //Assert
                Assert.Equal(3, admins.Count());
            }
        }

        [Fact]
        public void GetAllSellers_ShouldReturnAllSellers()
        {
            using (var context = GetSampleData(nameof(GetAllSellers_ShouldReturnAllSellers)))
            {
                //Arrange
                var unitOfWork = new UnitOfWork(context);

                //Act
                var sellers = unitOfWork.UserAccountRepo.GetAll().Where(a => a.role == "seller");

                //Assert
                Assert.Equal(3, sellers.Count());
            }
        }

        [Fact]
        public void GetAllBuyers_ShouldReturnAllBuyers()
        {
            using (var context = GetSampleData(nameof(GetAllBuyers_ShouldReturnAllBuyers)))
            {
                //Arrange
                var unitOfWork = new UnitOfWork(context);

                //Act
                var buyers = unitOfWork.UserAccountRepo.GetAll().Where(a => a.role == "buyer");

                //Assert
                Assert.Equal(3, buyers.Count());
            }
        }

        [Fact]
        public void GetAllFarms_ShouldReturnFarms()
        {
            using(var context = GetSampleData(nameof(GetAllFarms_ShouldReturnFarms)))
            {
                //Arrange
                var unitOfWork = new UnitOfWork(context);

                //Act
                var farms = unitOfWork.FarmRepo.GetAll();

                //Assert
                Assert.Equal(3, farms.Count());
            }
        }

        [Fact]
        public void GetAllAnimalTypes_ShouldReturnAnimalTypes()
        {
            using(var context = GetSampleData(nameof(GetAllAnimalTypes_ShouldReturnAnimalTypes)))
            {
                //Arrange
                var unitOfWork = new UnitOfWork(context);

                //Arrange
                var animalTypes = unitOfWork.AnimalTypeRepo.GetAll();

                //Assert
                Assert.Equal(3, animalTypes.Count());
            }
        }
    }
}
