namespace WaesApi.Repositories
{
    public interface IDiffRepository
    {
        void Add(T model);
        T Get(int id, string direction);
        bool Exists(int identifier);
    }
}
