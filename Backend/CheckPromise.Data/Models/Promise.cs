namespace CheckPromise.Data.Models
{
	public class Promise
    {
		public int Id { get; set; }
		public string Value { get; set; }
		public bool IsCompleted { get; set; }
		public string Source { get; set; }
    }
}
