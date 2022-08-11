using System;
namespace Dockcorecross.Reports.Models{

public class PrecipitModel{
       public Guid id {get;set;}
      public DateTime CreatedOn {get;set;}
        public decimal Amountinches {get;set;}  
        public string? WeatherType {get;set;}  
        public string? ZipCode {get;set;}  
}
}