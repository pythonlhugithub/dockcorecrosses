using System.Linq;
using Dockcorecross.Precipitation.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<PrecipDbContext>(
    opts=>{
        opts.EnableDetailedErrors();
        opts.EnableSensitiveDataLogging();
        opts.UseSqlServer(builder.Configuration.GetConnectionString("AppDb"));
    }, ServiceLifetime.Transient
);  //link program.cs to dbcontext database

var app = builder.Build();
app.MapGet("/okkv/{zip}", async(string? zip, [FromQuery] int? days, PrecipDbContext db)=>{

// if(zip==null)
// return Results.BadRequest("please enter a zip code");
// else
// //return Results.Ok(zip); //this is the service in this microservice (as webapi way)

var results=await db.Precipitation.Where(p=>p.ZipCode==zip).ToListAsync();


return Results.Ok(results); 

});



app.MapPost("/okkv", async(Precipitation precip, PrecipDbContext db)=>{

precip.CreatedOn=precip.CreatedOn.ToUniversalTime();
await db.AddAsync(precip);
await db.SaveChangesAsync(); //commit

});

app.Run();
