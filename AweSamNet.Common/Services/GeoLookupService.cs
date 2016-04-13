using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AweSamNet.Common.Models;
using Newtonsoft.Json;

namespace AweSamNet.Common.Services
{
    public class GeonamesOrgLookupService 
        : IGeoLookupService
    {
        private readonly IRegistry _registry;

        public GeonamesOrgLookupService(IRegistry registry)
        {
            _registry = registry;
        }

        public IList<Country> GetCountryList(string culture = null, int page = 0, int pageSize = 0)
        {
            CultureInfo cultureInfo = null;
            if (!string.IsNullOrWhiteSpace(culture))
            {
                cultureInfo = new CultureInfo(culture);
            }

            return GetCountryList(cultureInfo, page, pageSize);
        }

        public IList<Country> GetCountryList(CultureInfo culture, int page = 0, int pageSize = 0)
        {
            var cacheKey = "GeonamesOrgLookupService.GetCountryList";
            if (culture != null)
            {
                cacheKey += ", culture=" + culture;
            }

            var countries = _registry.Cache.GetOrAdd(cacheKey, () =>
            {
                string url = "http://api.geonames.org/countryInfoJSON?username=" + _registry.Config.GeoLookupGeoNamesUser;

                url = AppendCulture(culture, url);

                return HttpGetJson<GeonamesCollection<Country>>(url).Geonames;

            }, _registry.Config.GeoLookupCacheExpiration);

            return countries
                .Page(page, pageSize)
                .ToList();
        }

        public IList<AdministrativeRegion> GetAdministrativeRegions(int parentGeonameId, string culture = null, int page = 0,
            int pageSize = 0)
        {
            CultureInfo cultureInfo = null;
            if (!string.IsNullOrWhiteSpace(culture))
            {
                cultureInfo = new CultureInfo(culture);
            }

            return GetAdministrativeRegions(parentGeonameId, cultureInfo, page, pageSize);
        }

        public IList<AdministrativeRegion> GetAdministrativeRegions(int parentGeonameId, CultureInfo culture, int page = 0, int pageSize = 0)
        {
            var cacheKey = "GeonamesOrgLookupService.GetStateProvinceList, parentGeonameId=" + parentGeonameId;
            if (culture != null)
            {
                cacheKey += ", culture=" + culture;
            }
            var stateProvinces = _registry.Cache.GetOrAdd(cacheKey, () =>
            {
                string url = string.Format("http://api.geonames.org/childrenJSON?geonameId={0}&username={1}",
                    parentGeonameId, _registry.Config.GeoLookupGeoNamesUser);

                url = AppendCulture(culture, url);
                var regions = HttpGetJson<GeonamesCollection<AdministrativeRegion>>(url);
                return regions == null || regions.Geonames == null ? new List<AdministrativeRegion>() : regions.Geonames;

            }, _registry.Config.GeoLookupCacheExpiration);

            return stateProvinces
                .Page(page, pageSize)
                .ToList();
        }

        private static string AppendCulture(CultureInfo culture, string url)
        {
            if (culture != null && !String.IsNullOrWhiteSpace(culture.TwoLetterISOLanguageName))
            {
                url += "&lang=" + culture.TwoLetterISOLanguageName;
            }
            return url;
        }

        private T HttpGetJson<T>(Uri uri)
        {
            return HttpGetJson<T>(uri.ToString());
        }

        private T HttpGetJson<T>(string url)
        {
            using (var client = new WebClient())
            {
                var json = client.DownloadString(url);
                return JsonConvert.DeserializeObject<T>(json);
            }
        }
    }
}
