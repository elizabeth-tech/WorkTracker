using WorkTracker.Data.Context;

namespace WorkTracker.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly WorkTrackerContext _context;

        protected BaseRepository(WorkTrackerContext context) => _context = context;
    }
}
