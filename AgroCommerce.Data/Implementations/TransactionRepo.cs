using AgroCommerce.Core;
using AgroCommerce.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroCommerce.Data.Implementations
{
    public class TransactionRepo : CoreRepo<Transaction>, ITransactionRepo
    {
        public TransactionRepo(ApplicationDbContext ctx) : base(ctx)
        {

        }
    }
}
