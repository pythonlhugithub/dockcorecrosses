using Microsoft.EntityFrameworkCore;

namespace Dockcorecross.Reports.DataAccess;

public class ReprotDbContext : DbContext {
public ReprotDbContext()
{
       
}

public ReprotDbContext(DbContextOptions opts):base(opts)
{
    
}


public DbSet<Reprot> Reprot{get;set;}

protected override void OnModelCreating(ModelBuilder modelbuilder){
    base.OnModelCreating(modelbuilder);
    SnakeCasen(modelbuilder);
}

protected static void SnakeCasen(ModelBuilder m){
m.Entity<Reprot>(
    b=>{
    b.ToTable("reprot");}
    );
}



protected override void OnConfiguring(DbContextOptionsBuilder options)=>
    options.UseSqlServer($"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=reprotDb;Data Source=.\\sqlexpress");

 
}


