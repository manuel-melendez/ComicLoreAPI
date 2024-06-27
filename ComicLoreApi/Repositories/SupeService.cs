
using System.Linq.Expressions;

namespace ComicLoreApi.Repositories
{
    public class SupeService : ISupeService
    {
        private readonly ISupeRepository _supeRepository;
        private readonly IPowerRepository _powerRepository;

        public SupeService(ISupeRepository supeRepository, IPowerRepository powerRepository)
        {
            _supeRepository = supeRepository ?? throw new ArgumentNullException(nameof(supeRepository));
            _powerRepository = powerRepository ?? throw new ArgumentNullException(nameof(powerRepository));
        }
        public async Task AddPowerToSupeAsync(int supeId, int powerId)
        {
            var Supe = await _supeRepository.getSupeByIdAsync(supeId);
            if (Supe == null)
            {
                throw new ArgumentException($"Supe with id {supeId} not found");
            }

            var power = await _powerRepository.getPowerByIdAsync(powerId);
            if(power == null)
            {
                throw new ArgumentException($"Power with id {powerId} not found");
            }

            Supe.Powers.Add(power);
            await _supeRepository.SaveChangesAsync();
        }
    }
}
