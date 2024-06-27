using ComicLoreApi.Entities;

namespace ComicLoreApi.Repositories
{
    public interface ISupeRepository
    {
        Task<Supe> getSupeByIdAsync(int id);
        Task<IEnumerable<Supe>> getAllSupesAsync();
        Task addSupeAsync(Supe supe);
        void deleteSupe(Supe supe);
        Task<bool> SaveChangesAsync();
    }
}
