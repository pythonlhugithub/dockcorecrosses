using Microsoft.EntityFrameworkCore;

namespace Dockcorecross.temperature;
public class TemperDbContext : DbContext{

public TemperDbContext()
{
    
}

public TemperDbContext(DbContextOptions opts):base(opts)
{
    
}


    public DbSet<Temperature>? Temperature{get;set;}

protected override void OnModelCreating(ModelBuilder modelbuilder){
    base.OnModelCreating(modelbuilder);
    SnakeCasen(modelbuilder);
}

protected static void SnakeCasen(ModelBuilder m){
m.Entity<Temperature>(
    b=>{
    b.ToTable("temperature");}
    );
}


protected override void OnConfiguring(DbContextOptionsBuilder options)=>
    options.UseSqlServer($"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=TemperDb;Data Source=.\\sqlexpress");

 
 



}