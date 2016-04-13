using System.Collections.Generic;
using System.Globalization;
using AweSamNet.Common.Models;

namespace AweSamNet.Common.Services
{
    public interface IGeoLookupService
    {
        IList<Country> GetCountryList(CultureInfo culture, int page = 0, int pageSize = 0);
        IList<Country> GetCountryList(string culture = null, int page = 0, int pageSize = 0);
        IList<AdministrativeRegion> GetAdministrativeRegions(int parentGeonameId, CultureInfo culture, int page = 0, int pageSize = 0);
        IList<AdministrativeRegion> GetAdministrativeRegions(int parentGeonameId, string culture = null, int page = 0, int pageSize = 0);
    }
}