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
    public class TransactionService : ITransactionService
    {
        private readonly UnitOfWork _uow;
        public TransactionService(IUnitOfWork uow)
        {
            this._uow = uow as UnitOfWork;
        }

        public async Task Create(Transaction transaction)
        {
            _uow.TransactionRepo.Add(transaction);
            await _uow.Save();
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _uow.TransactionRepo.GetAll();
        }

        public IEnumerable<Transaction> GetAllForSeller(string id)
        {
            return _uow.TransactionRepo.GetAll().Where(t => String.Compare(id, t.SellerId, true) == 0);
        }

        public IEnumerable<Transaction> GetAllForBuyer(string id)
        {
            return _uow.TransactionRepo.GetAll().Where(t => String.Compare(id, t.BuyerId, true) == 0);
        }

        public Transaction GetByID(long id)
        {
            return _uow.TransactionRepo.Get(id);
        }

        public int GetNumberForSeller(string id)
        {
            return _uow.TransactionRepo.GetAll().Where(t => t.SellerId == id).Count();
        }

        public int GetNumberForBuyer(string id)
        {
            return _uow.TransactionRepo.GetAll().Where(t => t.BuyerId == id).Count();
        }
    }
}
