using System;
using WaesApi.Data;
using WaesApi.Data.Repositories;
using System.Linq;
using System.Collections.Generic;
using WaesApi.Utils;

namespace WaesApi.Services
{
    public class DiffService : IDiffService
    {
        readonly IDiffRepository repository;

        public DiffService(IDiffRepository repository)
        {
            this.repository = repository;
        }

        public void Input(int diffId, string direction, string value)
        {
            repository.Add(new Diff { DiffId = diffId, Direction = direction, Value = value });
        }

        private IList<Diff> QueryDiffs(int diffId)
        {
            var diffs = repository.GetByDiffId(diffId);
            if (diffs.Count() < 2) { throw new IncompleteInputException(); }
            return diffs;
        }

        public DiffResult GetDiff(int diffId) {
            var diffs = QueryDiffs(diffId);
            return new DiffResult(diffs.First(x=>x.Direction == "left").Value, diffs.First(x => x.Direction == "left").Value);
         }
    }
}
