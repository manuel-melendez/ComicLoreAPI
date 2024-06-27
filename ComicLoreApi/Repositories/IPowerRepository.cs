using ComicLoreApi.Entities;

namespace ComicLoreApi.Repositories
{
    public interface IPowerRepository
    {
        Task<Power> getPowerByIdAsync(int id);
        Task<IEnumerable<Power>> getAllPowersAsync();
        Task addPowerAsync(Power power);
        Task deletePowerAsync(Power power);
    }
}
