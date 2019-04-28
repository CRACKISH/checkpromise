using System.Linq;
using Checkpromise.Models;
using Checkpromise.Persistence;

namespace Checkpromise.Charts
{

    public class ChartRepository : IChartRepository
    {

        private readonly ApplicationDbContext _context;

        public ChartRepository(ApplicationDbContext ctx)
        {
            _context = ctx;
        }

        public void Add(Chart entity)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Chart> GetAll() => _context.Chart;
    }
}