using System.Collections.Generic;
using System.Globalization;
using AweSamNet.Common.Models;

namespace AweSamNet.Common.Services
{
    public interface IGeoLookupService
    {
        IList<Country> GetCountryList(CultureInfo culture, int page = 0, int pageSize = 0);
        IList<Country> GetCountryList(string culture = null, int page = 0, int pageSize = 0);
        IList<AdministrativeRegion> GetAdministrativeRegions1(string countryCode, CultureInfo culture, int page = 0, int pageSize = 0);
        IList<AdministrativeRegion> GetAdministrativeRegions1(string countryCode, string culture = null, int page = 0, int pageSize = 0);
        IList<AdministrativeRegion> Search(GeoLookupSearchOptions options);
        IList<AdministrativeRegion> Search(GeoLookupSearchOptions options, string culture);

    }
}