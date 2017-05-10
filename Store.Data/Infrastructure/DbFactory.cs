namespace Store.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private StoreDbContext dbContext;

        public StoreDbContext Init()
        {
            return dbContext ?? (dbContext = new StoreDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
