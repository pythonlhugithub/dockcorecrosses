using Dockcorecross.temperature.Models;
using Dockcorecross.precipitation.Models;
using  Dockcorecross.reports.DataAccess;
using  Dockcorecross.temperature;
using  Dockcorecross.precipitation; 


namespace Dockcorecross.reports.businesslogic;
 
public interface IReportsAggr{
public Task<Reprot> buildReprot(string zipcode, int? days);
}
//cache this report in this report proj, so client AIP comes here to calll it
public class ReportsAggr:IReportsAggr{
    private readonly IHttpClientFactory _http;
   private readonly  ILogger<ReportsAggr> _logger;
     private readonly  reportdataconfig _reportconfig;

   private readonly  PrecipDbContext _db;

   public ReportsAggr(    
     IHttpClientFactory http,
      ILogger<ReportsAggr> logger,
IOptions<reportdataconfig> reportconfig,
PrecipDbContext db
)
  {
_http=http;
_logger=logger;
_reportconfig=reportconfig.Value;
_db=db;
  }
    
public async Task<Reprot> buildReprot(string zipcode, int? days){

    var httpCLit=_http.CreateClient();
    var precipData=await FetchprecipData(httpCLit, zipcode);
    var temperData=await FetchtemperData(httpCLit, zipcode);
}


private async Task<List<PrecipitationModel>> FetchprecipData(HttpClient htpclt, string zip){

 var endpoint=BuildPrecipServiceendPoint(zip);

    var preciprecords=await htpclt.GetAsync(endpoint); //from program.cs app.MapGet()

    var precipData=await preciprecords.Content.ReadFromJsonAsync<List<PreciptationModel>>();

    return precipData ?? new List<PreciptationModel>();


}

private async Task<List<TemperatureModel>> FetchtemperData(HttpClient htpclt, string zip){
    
    var endpoint=BuildTempServiceendPoint(zip);

    var temprecords=await htpclt.GetAsync(endpoint); //from program.cs app.MapGet()

    var temperData=await temprecords.Content.ReadFromJsonAsync<List<TemperatureModel>>();

    return temperData ?? new List<TemperatureModel>();

}

private string BuildTempServiceendPoint(string zip){

var temperServiceProtocol=_reportconfig.TempDataProtocol;

var temperServiceHost=_reportconfig.TempDataHost;

var temperServicePort=_reportconfig.TempDataPort;

   return $"{temperServiceProtocol}://{temperServiceHost}:{temperServicePort}/okkv/{zip}";

}


private string BuildPrecipServiceendPoint(string zip){

var precipServiceProtocol=_reportconfig.PrecipDataProtocol;

var precipServiceHost=_reportconfig.PrecipDataHost;

var precipServicePort=_reportconfig.PrecipDataPort;

   return $"{precipServiceProtocol}://{precipServiceHost}:{precipServicePort}/okkv/{zip}";

}


   }
