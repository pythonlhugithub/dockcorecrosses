using System.Linq;
using Dockcorecross.Temperature.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TemperDbContext>(
    opts=>{
        opts.EnableDetailedErrors();
        opts.EnableSensitiveDataLogging();
        opts.UseSqlServer(builder.Configuration.GetConnectionString("AppDb"));
    }, ServiceLifetime.Transient
);  //link program.cs to dbcontext database

var app = builder.Build();

app.MapGet("/okkv/{zip}", async(string? zip, [FromQuery] int? days, TemperDbContext db)=>{

// if(zip==null)
// return Results.BadRequest("please enter a zip code");
// else
// //return Results.Ok(zip); //this is the service in this microservice (as webapi way)

var results=await db.Temperature.Where(p=>p.ZipCode==zip).ToListAsync();


return Results.Ok(results); 

});



app.MapPost("/okkv", async(Temperature temper, TemperDbContext db)=>{

temper.CreatedOn=temper.CreatedOn.ToUniversalTime();
await db.AddAsync(temper);
await db.SaveChangesAsync(); //commit

});

app.Run();
