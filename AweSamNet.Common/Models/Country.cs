namespace AweSamNet.Common.Models
{
    public class Country
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public int IsoNumeric { get; set; }
        public string IsoAlpha3 { get; set; }
        public string FipsCode { get; set; }
        public string Continent { get; set; }
        public string ContinentName { get; set; }
        public string CurrencyCode { get; set; }
        public string Languages { get; set; }
        public int GeonameId { get; set; }
        public string PostalCodeFormat { get; set; }
        public double West { get; set; }
        public double North { get; set; }
        public double East { get; set; }
        public double South { get; set; }
    }
}
