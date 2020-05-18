using AgroCommerce.Core;
using AgroCommerce.Data.Contracts;
using AgroCommerce.Data.Implementations;
using AgroCommerce.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroCommerce.Services.Implementations
{
    public class UserAccountService : IUserAccountService
    {
        private readonly UnitOfWork _uow;
        public UserAccountService(IUnitOfWork uow)
        {
            this._uow = uow as UnitOfWork;
        }

        public int GetTotalNumberOfUsersInDb()
        {
            return _uow.UserAccountRepo.Count();
        }
        public int GetTotalNumberOfBuyers()
        {
            return _uow.UserAccountRepo.GetAllUsersWithRelationships().Where(u => u.role == "buyer").Count();
        }
        public int GetTotalNumberOfSellers()
        {
            return _uow.UserAccountRepo.GetAllUsersWithRelationships().Where(u => u.role == "seller").Count();
        }

        public ApplicationUser GetById(string Id)
        {
            return _uow.UserAccountRepo.GetUserWithRelationships(Id);
        }
        public async Task Update(ApplicationUser user)
        {
            _uow.UserAccountRepo.Update(user);
            await _uow.Save();
        }

        public bool IsAccountComplete(string userId)
        {
            return _uow.UserAccountRepo.Get(userId).IsAccountComplete;
        }

        public ApplicationUser GetByUsername(string username)
        {
            return _uow.UserAccountRepo.Find(u => String.Compare(u.UserName, username, true) == 0).FirstOrDefault();
        }

        public ApplicationUser GetByFarmID(long id)
        {
            return _uow.UserAccountRepo.Find(u => u.Farm.ID == id).FirstOrDefault();
        }

        public ApplicationUser GetUSerByPhoneNumber(string phoneNumber)
        {
            return _uow.UserAccountRepo.Find(u => u.PhoneNumber == phoneNumber).FirstOrDefault();
        }
    }
}
