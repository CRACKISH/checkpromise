using System.ComponentModel.DataAnnotations;

namespace Checkpromise.Models
{
    public class Chart
    {
        [Key]
        public int Id { get; set; }

        public string Source { get; set; }
    }
}
