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
        public async Task addSupeAsync(Supe supe)
        {
           _context.Supes.Add(supe);
            await _context.SaveChangesAsync();
        }

        public async Task deleteSupeAsync(Supe supe)
        {
            _context.Supes.Remove(supe);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Supe>> getAllSupesAsync()
        {
           return await _context.Supes.ToListAsync();
        }

        public async Task<Supe> getSupeByIdAsync(int id)
        {
            return await _context.Supes.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
