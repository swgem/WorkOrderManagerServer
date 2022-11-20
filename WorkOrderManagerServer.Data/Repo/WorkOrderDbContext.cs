using Microsoft.EntityFrameworkCore;
using WorkOrderManagerServer.Data.Entities;

namespace WorkOrderManagerServer.Data.Repo
{
    public class WorkOrderDbContext : DbContext
    {
        public WorkOrderDbContext(DbContextOptions<WorkOrderDbContext> options) : base(options) { }
        public DbSet<WorkOrderEntity> WorkOrders { get; set; }
    }
}