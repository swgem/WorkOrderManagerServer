using Microsoft.EntityFrameworkCore;
using WorkOrderManagerServer.Data;

namespace WorkOrderManagerServer.Repo
{
    public class WorkOrderDbContext : DbContext
    {
        public WorkOrderDbContext(DbContextOptions<WorkOrderDbContext> options) : base(options) { }
        public DbSet<WorkOrder> WorkOrders { get; set; }
    }
}