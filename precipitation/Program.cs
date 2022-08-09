using Dockcorecross.precipitation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PrecipDbContext>(
    opts=>{
        opts.EnableDetailedErrors();
        opts.EnableSensitiveDataLogging();
        opts.UseSqlServer(builder.Configuration.GetConnectionString("AppDb"));
    }, ServiceLifetime.Transient
);  //link program.cs to dbcontext database

var app = builder.Build();



app.MapGet("/precipfirstservice/{zip}", async(string? zip, [FromQuery] int? days, PrecipDbContext db)=>{

// if(zip==null)
// return Results.BadRequest("please enter a zip code");
// else
// //return Results.Ok(zip); //this is the service in this microservice (as webapi way)

var results=await db.Precipitation.Where(p=>p.ZipCode==zip).ToListAsync();


return Results.Ok(results); 

});

app.Run();
