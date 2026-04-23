namespace CheckPromise.Uploader;

public class UploaderOptions
{
    public const string SectionName = "Uploader";

    /// <summary>Time of day (UTC) when the uploader should run.</summary>
    public TimeOnly RunAt { get; set; } = new(1, 0);

    /// <summary>Run once immediately on startup before scheduling the daily cadence.</summary>
    public bool RunOnStartup { get; set; } = true;
}
