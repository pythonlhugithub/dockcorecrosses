var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.MapGet("/precipfirstservice/{zip}", (string? zip)=>{

if(zip==null)
return Results.BadRequest("please enter a zip code");
else
return Results.Ok(zip); //this is the service in this microservice (as webapi way)

});

app.Run();
