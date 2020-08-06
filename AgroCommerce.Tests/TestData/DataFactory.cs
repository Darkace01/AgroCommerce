using AgroCommerce.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Tests.TestData
{
    public class DataFactory
    {
        public ApplicationUser GetSellerUser(string id, string email, string username, string firstName,string lastName, string passwordHash, string role, string imagePath)
        {
            var user = new ApplicationUser
            {
                Id = id,
                Email = email,
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = passwordHash,
                role = role,
                ImagePath = imagePath,
            };
            return user;
        }

        public ApplicationUser GetBuyerUser(string id, string email, string username, string firstName, string lastName, string passwordHash, string role, string imagePath)
        {
            var user = new ApplicationUser
            {
                Id = id,
                Email = email,
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = passwordHash,
                role = role,
                ImagePath = imagePath,
            };
            return user;
        }

        public ApplicationUser GetAdminUser(string id, string email, string username, string firstName, string lastName, string passwordHash, string role, string imagePath)
        {
            var admin = new ApplicationUser
            {
                Id = id,
                Email = email,
                UserName = username,
                FirstName = firstName,
                LastName = lastName,
                PasswordHash = passwordHash,
                role = role,
                ImagePath = imagePath,
            };
            return admin;
        }

        public Farm GetFarm(long id, string name, string imagePath, string applicationUserId, string location, bool isActive)
        {
            var farm = new Farm
            {
                ID = id,
                Name = name,
                ImagePath = imagePath,
                ApplicationUserId = applicationUserId,
                Location = location,
                IsActive = isActive
            };
            return farm;
        }


        public Listing GetListing(long id, string title, string description, decimal price, string location, string animalType, string classes, string breed, string age, int ageYear, int ageMonth, int ageWeek, int ageConvertedToWeeks, string gender, Farm farm, long farmId, string imagePath)
        {
            var listing = new Listing
            {
                ID = id,
                Title = title,
                Description = description,
                Price = price,
                Location = location,
                AnimalType = animalType,
                Class = classes,
                Breed = breed,
                Age = age,
                AgeYear = ageYear,
                AgeMonth = ageMonth,
                AgeWeek = ageWeek,
                AgeConvertedToWeeks = ageConvertedToWeeks,
                Gender = gender,
                FarmId = farmId,
                Farm = farm,
                ImagePath = imagePath
            };

            return listing;
        }

        public AnimalType GetAnimalType(long id, string classes, string breed, string name)
        {
            var animalType = new AnimalType
            {
                ID = id,
                Class = classes,
                Breed = breed,
                Name = name
            };

            return animalType;
        }

        
    }
}
