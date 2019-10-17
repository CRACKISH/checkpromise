namespace CheckPromise.Data.Models
{
	public enum PromiseStatus
	{
		Nothing = 0,
		Done = 1,
		NotPerformed = 2
	}

	public class Promise
    {
		public int Id { get; set; }
		public string Value { get; set; }
		public PromiseStatus Status { get; set; }
		public string Source { get; set; }
    }
}
