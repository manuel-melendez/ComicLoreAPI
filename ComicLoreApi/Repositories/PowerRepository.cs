using ComicLoreApi.DbContexts;
using ComicLoreApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComicLoreApi.Repositories
{
    public class PowerRepository : IPowerRepository
    {
        private readonly SupeInfoDbContext _context;

        public PowerRepository(SupeInfoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task addPowerAsync(Power power)
        {
            _context.Powers.Add(power);
            await _context.SaveChangesAsync();
        }

        public async Task deletePowerAsync(Power power)
        {
            _context.Powers.Remove(power);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Power>> getAllPowersAsync()
        {
           return await _context.Powers.ToListAsync();
        }

        public async Task<Power> getPowerByIdAsync(int id)
        {
            return await _context.Powers.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
