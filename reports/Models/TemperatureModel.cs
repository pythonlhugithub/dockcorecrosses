using System;
namespace Dockcorecross.Reports.Models{

public class TemperatureModel{
    

 public Guid id {get;set;}
      public DateTime CreatedOn {get;set;}
        public decimal? TempH {get;set;}  
        public decimal? TempLow {get;set;}  
        public string? ZipCode {get;set;}  

}}