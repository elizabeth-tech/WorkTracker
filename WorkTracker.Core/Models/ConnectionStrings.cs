using WorkTracker.Core.Interfaces;

namespace WorkTracker.Core.Models
{
    public class ConnectionStrings : IConnectionStrings
    {
        public string PostgreSQLConnection { get; set; }
    }
}
