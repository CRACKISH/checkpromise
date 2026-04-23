namespace CheckPromise.Data.Models;

public class News
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public required string Value { get; set; }
}
