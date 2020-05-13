using AgroCommerce.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroCommerce.Services.Contracts
{
    public interface ITransactionService
    {
        Task Create(Transaction transaction);
        IEnumerable<Transaction> GetAll();
        IEnumerable<Transaction> GetAllForSeller(string id);
        IEnumerable<Transaction> GetAllForBuyer(string id);
        Transaction GetByID(long id);
        int GetNumberForSeller(string id);
        int GetNumberForBuyer(string id);
    }
}
