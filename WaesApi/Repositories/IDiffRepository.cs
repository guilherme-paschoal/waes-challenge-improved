using System;
using WaesApi.Data;

namespace WaesApi.Repositories
{
    public interface IDiffRepository
    {
        void Add(Diff model);
        Diff Get(int id, string direction);

        bool DiffExists(int id);
    }
}
