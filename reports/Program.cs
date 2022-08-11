using Dockcorecross.Reports.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dockcorecross.Reports.Businesslogic;
using Dockcorecross.Reports.Config;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddHttpClient();
builder.Services.AddTransient<IReportSum,ReportSum>();
builder.Services.AddOptions();
builder.Services.Configure<Reportdataconfig>(builder.Configuration.GetSection("Reportdataconfig"));
 
builder.Services.AddDbContext<ReprotDbContext>(
    opts=>{
        opts.EnableDetailedErrors();
        opts.EnableSensitiveDataLogging();
        opts.UseSqlServer(builder.Configuration.GetConnectionString("AppDb"));
    }, ServiceLifetime.Transient
);  //link program.cs to dbcontext database

var app = builder.Build();

 app.MapGet("/GetReport/{zip}",(string zip, IReportSum rpts)=>{

var alldata=rpts.buildReprot(zip); //inject contains two http.get();

return alldata;
 

 });







app.Run();
