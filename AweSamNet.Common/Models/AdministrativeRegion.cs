using System.Linq;

namespace AweSamNet.Common.Models
{
    /// <summary>
    /// Represents a Province or State or Regional District
    /// </summary>
    public class AdministrativeRegion
    {
        public string ToponymName { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int GeonameId { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }

        public string Abbreviation
        {
            get
            {
                if (AlternateNames == null)
                {
                    return Name;
                }

                var existingName = AlternateNames.FirstOrDefault(x => x.Lang == "abbr");
                return existingName != null ? existingName.Name : Name;
            }
        }

        public AlternateName[] AlternateNames { get; set; }
    }

    public class AlternateName
    {
        public string Name { get; set; }
        public string Lang { get; set; }
    }
}
