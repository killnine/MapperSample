using System.Collections.Generic;

namespace Model.Domain.Mappers
{
    public class MapResult
    {
        public bool Success { get; set; }
        public IList<string> Errors { get; set; } = new List<string>();
        public IList<string> Warnings { get; set; } = new List<string>();
    }
}
