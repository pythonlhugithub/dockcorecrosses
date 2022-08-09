using Dockcorecross.reports.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
 

 
builder.Services.AddDbContext<ReprotDbContext>(
    opts=>{
        opts.EnableDetailedErrors();
        opts.EnableSensitiveDataLogging();
        opts.UseSqlServer(builder.Configuration.GetConnectionString("AppDb"));
    }, ServiceLifetime.Transient
);  //link program.cs to dbcontext database


var app = builder.Build();
app.Run();
