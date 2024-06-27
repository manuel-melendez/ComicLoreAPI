using ComicLoreApi.DbContexts;
using ComicLoreApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComicLoreApi.Repositories
{
    public class SupeRepository : ISupeRepository
    {
        private readonly SupeInfoDbContext _context;

        public SupeRepository(SupeInfoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Supe>> getAllSupesAsync()
        {
            return await _context.Supes.Include(s => s.Powers).ToListAsync();
        }

        public async Task<Supe> getSupeByIdAsync(int id)
        {
            return await _context.Supes.Include(s => s.Powers).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task addSupeAsync(Supe supe)
        {
           _context.Supes.Add(supe);
            await _context.SaveChangesAsync();
        }

        public void deleteSupe(Supe supe)
        {
            _context.Supes.Remove(supe);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
