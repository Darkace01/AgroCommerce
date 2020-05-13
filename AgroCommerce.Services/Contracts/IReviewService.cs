using AgroCommerce.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroCommerce.Services.Contracts
{
    public interface IReviewService
    {
        Task Create(Review review);
        IEnumerable<Review> GetAll();
        IEnumerable<Review> GetAllForProduct(int id);
        int GetTotalNumberForSeller(string sellerId);
        int GetTotalNumberForBuyer(string buyerId);
    }
}
