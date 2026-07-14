namespace NoviCode.Infrastructure
{
    public interface ICreatePlayerPersistence
    {
        Task Persist(Player player);
    }
}
