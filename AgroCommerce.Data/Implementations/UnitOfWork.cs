using AgroCommerce.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroCommerce.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
            this.UserAccountRepo = new UserAccountRepo(this._context);
            this.FarmRepo = new FarmRepo(this._context);
            this.ListingRepo = new ListingRepo(this._context);
            this.AnimalTypeRepo = new AnimalTypeRepo(this._context);
            this.LocationMgtRepo = new LocationMgtRepo(this._context);
            this.TransactionRepo = new TransactionRepo(this._context);
            this.ReviewRepo = new ReviewRepo(this._context);
        }

        public UserAccountRepo UserAccountRepo { get; set; }
        public FarmRepo FarmRepo { get; set; }
        public ListingRepo ListingRepo { get; set; }
        public AnimalTypeRepo AnimalTypeRepo { get; set; }
        public LocationMgtRepo LocationMgtRepo { get; set; }
        public TransactionRepo TransactionRepo { get; set; }
        public ReviewRepo ReviewRepo { get; set; }
        public async Task Save()
        {
           await this._context.SaveChangesAsync();
        }



        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

