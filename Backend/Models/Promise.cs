using System.ComponentModel.DataAnnotations;

namespace Checkpromise.Models
{
    public class Promise
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public bool Completed { get; set; }

        public string Source { get; set; }
    }
}
