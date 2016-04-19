using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AweSamNet.Common.Models
{
    public enum AdministrativeRegionStyle
    {
        Short,
        Medium,
        Long,
        Full
    }

    public class GeoLookupSearchOptions
    {
        public CultureInfo Culture{get; set;}
        public string Query{get; set;}
        public string CountryCode{get; set;}
        public string AdminCode1{get; set;}
        public string AdminCode2{get; set;}
        public string AdminCode3{get; set;}
        public int Page{get; set;}
        public int PageSize{get; set;}
        public string ContinentCode {get; set;}
        public AdministrativeRegionStyle? Style {get; set;}
        public CountryStateRegionFeatureCodes FeatureCodes { get; set; }
    }
}
