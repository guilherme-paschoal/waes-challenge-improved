using System;
using System.ComponentModel.DataAnnotations;

namespace WaesApi.Data
{
    public class Diff
    {
        [Key]
        public int Id { get; set; }
        public int DiffId { get; set; }
        public string Value { get; set; }
        public string Direction { get; set; }
    }
}
