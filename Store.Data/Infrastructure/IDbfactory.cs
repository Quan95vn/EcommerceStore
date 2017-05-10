using System;

namespace Store.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        StoreDbContext Init();
    }
}
