using System.Linq;
using Checkpromise.Models;
using Checkpromise.Persistence;

namespace Checkpromise.Promises
{

    public class PromiseRepository : IPromiseRepository
    {

        private readonly ApplicationDbContext _context;

        public PromiseRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public void Add(Promise entity)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Promise> GetAll() => _context.Promise;
    }
}
