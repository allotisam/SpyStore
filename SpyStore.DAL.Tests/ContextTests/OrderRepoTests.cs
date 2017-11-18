using SpyStore.DAL.Initializers;
using SpyStore.DAL.Repos;
using System;

namespace SpyStore.DAL.Tests.ContextTests
{
    public class OrderRepoTests : IDisposable
    {
        private readonly OrderRepo _repo;

        public OrderRepoTests()
        {
            _repo = new OrderRepo(new OrderDetailRepo());
            StoreDataInitializer.ClearData(_repo.Context);
            StoreDataInitializer.InitializeData(_repo.Context);
        }

        public void Dispose()
        {
            StoreDataInitializer.ClearData(_repo.Context);
            _repo.Dispose();
        }
    }
}
