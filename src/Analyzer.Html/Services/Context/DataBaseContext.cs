using Microsoft.EntityFrameworkCore;

namespace Analyzer.Html.Services.Context;

public class DataBaseContext : DbContext 
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
