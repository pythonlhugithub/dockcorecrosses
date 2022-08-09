namespace Dockcorecross.report;
public class Report{
    public Guid id {get;set;}
      public DateTime CreatedOn {get;set;}
        public decimal? TempH {get;set;}  
        public decimal? TempLow {get;set;}  
         public decimal? RainfallTotalInches {get;set;}  
        public decimal? snowTotalInches {get;set;}  
        public string? ZipCode {get;set;}  
       
}