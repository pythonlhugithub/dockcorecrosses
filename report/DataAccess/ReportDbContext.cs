
using Microsoft.EntityFrameworkCore;
namespace Dockcorecross.report;
public class ReportsDbContext : DbContext {
public ReportsDbContext()
{
       
}

public ReportsDbContext(DbContextOptions opts):base(opts)
{
    
}


public DbSet<Report> Report{get;set;}

protected override void OnModelCreating(ModelBuilder modelbuilder){
    base.OnModelCreating(modelbuilder);
    SnakeCasen(modelbuilder);
}

protected static void SnakeCasen(ModelBuilder m){
m.Entity<Report>(
    b=>{
    b.ToTable("report");}
    );
}

}