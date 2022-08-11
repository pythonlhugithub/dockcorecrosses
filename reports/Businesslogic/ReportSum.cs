using  Dockcorecross.Reports.DataAccess;
using Dockcorecross.Reports.Config;
using Dockcorecross.Reports.Models;
using Microsoft.Extensions.Options;
 
// using Dockcorecross.Precipitation.Models;
// using Dockcorecross.Temperature.Models;

// using Dockcorecross.Precipitation.DataAccess;
// using Dockcorecross.Temperature.DataAccess;
 

namespace Dockcorecross.Reports.Businesslogic{

    public interface IReportSum{
        public Task<List<Reports.DataAccess.Reprot>> buildReprot(string zipcode);

    }

public class ReportSum:IReportSum{

 private readonly IHttpClientFactory _http;
private readonly  ILogger<ReportSum> _logger;
private readonly  IOptions<Reportdataconfig> _reportconfig;
private readonly  ReprotDbContext _db;

public ReportSum(
        IHttpClientFactory http,
        ILogger<ReportSum> logger,
       // IOptions<Reportdataconfig> reportconfig,
        ReprotDbContext db
)
{
        _http=http;
        _logger=logger;
      //  _reportconfig=reportconfig;
        _db=db;
    
}

public async Task<List<Reports.DataAccess.Reprot>> buildReprot(string zipcode){

    var httpCLit=_http.CreateClient();

    var precipData=await FetchprecipData(httpCLit, zipcode);

    var temperData=await FetchtemperData(httpCLit, zipcode);

var pre = precipData
    .Select(i => new { i.CreatedOn , i.ZipCode})
    .Distinct()
    .OrderByDescending(i => i.CreatedOn)
    .ToArray();

 var tep = temperData
    .Select(i => new {i.CreatedOn , i.ZipCode})
    .Distinct()
    .OrderByDescending(i => i.CreatedOn)
    .ToArray();

var rlt=new List<Reprot>();
var rlt1=new Reprot();

 foreach(var rt in pre){
 
rlt1.CreatedOn=rt.CreatedOn;
rlt1.ZipCode=rt.ZipCode;
rlt.Add(rlt1);

 }

 foreach(var rt in tep){
 
rlt1.CreatedOn=rt.CreatedOn;
rlt1.ZipCode=rt.ZipCode;
rlt.Add(rlt1);

 }


return rlt;
 
}



private async Task<List<PrecipitModel>> FetchprecipData(HttpClient htpclt, string zip){

 var endpoint="http://localhost:5204/okkv/2065";
 
 //BuildPrecipServiceendPoint(zip);
    var preciprecords=await htpclt.GetAsync(endpoint); //from program.cs app.MapGet()

// var jsonSerOption=new jsonSerializerOptions{
//     propertyNameCaseInsensitive=true,
//     PropertyNamingPolicy=JsonNamingPolicy.CamelCase
// };

    var precipData=await preciprecords.Content.ReadFromJsonAsync<List<PrecipitModel>>();

    return precipData ?? new List<PrecipitModel>();


}

private async Task<List<TemperatureModel>> FetchtemperData(HttpClient htpclt, string zip){
    
    var endpoint="http://localhost:5164/okkv/2065"; //can be put in appsettings.json as getsection, then 
    //BuildTempServiceendPoint(zip);

    var temprecords=await htpclt.GetAsync(endpoint); //from program.cs app.MapGet()

        // var jsonSerOption=new JsonSerializerOptions{
        //     propertyNameCaseInsensitive=true,
        //     PropertyNamingPolicy=JsonNamingPolicy.CamelCase
        // };

    var temperData=await temprecords.Content.ReadFromJsonAsync<List<TemperatureModel>>();

    return temperData ?? new List<TemperatureModel>();

}

// private string BuildTempServiceendPoint(string zip){

// var temperServiceProtocol=_reportconfig.TempDataProtocol;

// var temperServiceHost=_reportconfig.TempDataHost;

// var temperServicePort=_reportconfig.TempDataPort;

//    return $"{temperServiceProtocol}://{temperServiceHost}:{temperServicePort}/okkv/{zip}";

// }


// private string BuildPrecipServiceendPoint(string zip){

// var precipServiceProtocol=_reportconfig.PrecipDataProtocol;

// var precipServiceHost=_reportconfig.PrecipDataHost;

// var precipServicePort=_reportconfig.PrecipDataPort;

//    return $"{precipServiceProtocol}://{precipServiceHost}:{precipServicePort}/okkv/{zip}";

// }




}
}