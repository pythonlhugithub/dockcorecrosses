using Microsoft.EntityFrameworkCore;

namespace Dockcorecross.precipitation;
public class PrecipDbContext : DbContext{
public PrecipDbContext()
{
       
}

public PrecipDbContext(DbContextOptions opts):base(opts)
{
    
}


public DbSet<Precipitation> Precipitation{get;set;}

protected override void OnModelCreating(ModelBuilder modelbuilder){
    base.OnModelCreating(modelbuilder);
    SnakeCasen(modelbuilder);
}

protected static void SnakeCasen(ModelBuilder m){
m.Entity<Precipitation>(
    b=>{
    b.ToTable("precipitation");}
    );
}

}