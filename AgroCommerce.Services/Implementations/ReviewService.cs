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
    public class ReviewService : IReviewService
    {
        private readonly UnitOfWork _uow;
        public ReviewService(IUnitOfWork uow)
        {
            this._uow = uow as UnitOfWork;
        }

        public async Task Create(Review review)
        {
            _uow.ReviewRepo.Add(review);
            await _uow.Save();
        }

        public IEnumerable<Review> GetAll()
        {
            return _uow.ReviewRepo.GetAll();
        }

        public IEnumerable<Review> GetAllForProduct(int id)
        {
            return _uow.ReviewRepo.GetAll().Where(r => r.Transaction.ID == id).ToList();
        }

        public int GetTotalNumberForBuyer(string buyerId)
        {
            return _uow.ReviewRepo.GetAll().Where(r => r.Transaction.BuyerId == buyerId).Count();
        }

        public int GetTotalNumberForSeller(string sellerId)
        {
            return _uow.ReviewRepo.GetAll().Where(r => r.Transaction.SellerId == sellerId).Count();
        }
    }
}
