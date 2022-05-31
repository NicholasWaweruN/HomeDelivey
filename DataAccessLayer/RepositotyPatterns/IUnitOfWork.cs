namespace DataAccessLayer.RepositotyPatterns
{
    public interface IUnitOfWork
    {
        Task<int> Complete();
    }
}