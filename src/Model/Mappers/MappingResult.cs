using System.Collections.Generic;

namespace Model
{
    public class MappingResult
    {
        public bool Success { get; set; }
        public IList<string> Errors { get; set; } = new List<string>();
        public IList<string> Warnings { get; set; } = new List<string>();
    }
}
