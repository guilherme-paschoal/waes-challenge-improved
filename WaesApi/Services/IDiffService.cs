using System;
using WaesApi.Data;

namespace WaesApi.Services
{
    public interface IDiffService
    {
        void Input(int diffId, string direction, string value);
        DiffResult GetDiff(int diffId);
    }
}
