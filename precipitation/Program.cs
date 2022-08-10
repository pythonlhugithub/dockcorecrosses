using Dockcorecross.precipitation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddDbContext<PrecipDbContext>(
    opts=>{
        opts.EnableDetailedErrors();
        opts.EnableSensitiveDataLogging();
        opts.UseSqlServer(builder.Configuration.GetConnectionString("AppDb"));
    }, ServiceLifetime.Transient
);  //link program.cs to dbcontext database


app.MapGet("/precipfirstservice/{zip}", async(string? zip, [FromQuery] int? days, PrecipDbContext db)=>{

// if(zip==null)
// return Results.BadRequest("please enter a zip code");
// else
// //return Results.Ok(zip); //this is the service in this microservice (as webapi way)

var results=await db.Precipitation.Where(p=>p.ZipCode==zip).ToListAsync();


return Results.Ok(results); 

});



app.MapPost("/precipfirstservice", async(Precipitation precip, PrecipDbContext db)=>{

precip.CreatedOn=precip.CreatedOn.ToUniversalTime();
await db.AddAsync(precip);
await db.SaveChangesAsync(); //commit

});

app.Run();
