using ComicLoreApi.Entities;

namespace ComicLoreApi.Repositories
{
    public interface ISupeRepository
    {
        Task<Supe> getSupeByIdAsync(int id);
        Task<IEnumerable<Supe>> getAllSupesAsync();
        Task addSupeAsync(Supe supe);
        Task deleteSupeAsync(Supe supe);
    }
}
