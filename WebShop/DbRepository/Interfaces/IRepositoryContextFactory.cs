namespace WebShop.DbRepository.Interfaces
{
    public interface IRepositoryContextFactory
    {
        public RepositoryContext CreateDbContext(string connectionString);
    }
}
