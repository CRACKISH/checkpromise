using Checkpromise.Models;

namespace Checkpromise.UIData
{
    public class PromiseData
    {

        public string Description { get; }

        public bool Completed { get; }

        public string Source { get; }

        public PromiseData(Promise promise)
        {
            Description = promise.Description;
            Completed = promise.Completed;
            Source = promise.Source;
        }
    }
}
