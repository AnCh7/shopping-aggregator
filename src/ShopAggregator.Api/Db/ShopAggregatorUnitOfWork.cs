using ShopAggregator.Api.Models;
using System;

namespace ShopAggregator.Api.Db
{
    public class ShopAggregatorUnitOfWork : IDisposable
    {
        private readonly ShopAggregatorContext _ctx;
        private IRepository<Shop> _shopRepository;
        private IRepository<Product> _productRepository;
        private IRepository<ShopProduct> _stockRepository;

        public ShopAggregatorUnitOfWork(ShopAggregatorContext ctx)
        {
            _ctx = ctx;
        }

        public IRepository<Shop> ShopRepository
        {
            get
            {
                if (this._shopRepository == null)
                    this._shopRepository = new Repository<Shop>(_ctx);

                return _shopRepository;
            }
        }

        public IRepository<Product> ProductRepository
        {
            get
            {
                if (this._productRepository == null)
                    this._productRepository = new Repository<Product>(_ctx);

                return _productRepository;
            }
        }

        public IRepository<ShopProduct> StockRepository
        {
            get
            {
                if (this._stockRepository == null)
                    this._stockRepository = new Repository<ShopProduct>(_ctx);

                return _stockRepository;
            }
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}