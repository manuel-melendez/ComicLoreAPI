namespace ComicLoreApi.Repositories
{
    public interface ISupeService
    {
        Task AddPowerToSupeAsync(int supeId, int powerId);
    }
}
