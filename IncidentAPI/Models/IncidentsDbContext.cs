using Microsoft.EntityFrameworkCore;

namespace IncidentAPI.Models
{
    public class IncidentsDbContext : DbContext
    {
        public IncidentsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected IncidentsDbContext()
        {
        }
        public virtual DbSet<Incident> Incidents { get; set; }
    }
}
