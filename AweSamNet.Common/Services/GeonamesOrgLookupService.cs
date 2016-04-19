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

        //http://www.geonames.org/export/geonames-search.html
        public IList<AdministrativeRegion> GetAdministrativeRegions1(string countryCode, string culture = null, int page = 0,
            int pageSize = 0)
        {
            CultureInfo cultureInfo = null;
            if (!string.IsNullOrWhiteSpace(culture))
            {
                cultureInfo = new CultureInfo(culture);
            }

            return GetAdministrativeRegions1(countryCode, cultureInfo, page: page, pageSize: pageSize);
        }

        public IList<AdministrativeRegion> GetAdministrativeRegions1(string countryCode, CultureInfo culture, int page = 0, int pageSize = 0)
        {
            var cacheKey = "GeonamesOrgLookupService.GetAdministrativeRegions1, countryCode=" + countryCode;
            if (culture != null)
            {
                cacheKey += ", culture=" + culture;
            }

            var stateProvinces = _registry.Cache.GetOrAdd(cacheKey, () =>
             Search(new GeoLookupSearchOptions
                {
                    Culture = culture,
                    CountryCode = countryCode,
                    FeatureCodes = CountryStateRegionFeatureCodes.ADM1,
                    Style = AdministrativeRegionStyle.Full,
                })

            , _registry.Config.GeoLookupCacheExpiration);

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

        public IList<AdministrativeRegion> Search(GeoLookupSearchOptions options)
        {
            string url = string.Format("http://api.geonames.org/searchJSON?username={0}", _registry.Config.GeoLookupGeoNamesUser);

            url = AppendCulture(options.Culture, url);

            if (!string.IsNullOrWhiteSpace(options.AdminCode1))
            {
                url += "&adminRegion1=" + options.AdminCode1;
            }

            if (!string.IsNullOrWhiteSpace(options.AdminCode2))
            {
                url += "&adminRegion2=" + options.AdminCode2;
            }

            if (!string.IsNullOrWhiteSpace(options.AdminCode3))
            {
                url += "&adminRegion3=" + options.AdminCode3;
            }

            if (!string.IsNullOrWhiteSpace(options.ContinentCode))
            {
                url += "&continentCode=" + options.ContinentCode;
            }

            if (!string.IsNullOrWhiteSpace(options.CountryCode))
            {
                url += "&country=" + options.CountryCode;
            }

            var featureCodes = options.FeatureCodes.GetUniqueFlags();
            foreach (var code in featureCodes)
            {
                url += "&fcode=" + code;
            }

            if (options.Style != null)
            {
                url += "&style=" + options.Style.Value.ToString().ToUpper();
            }

            if (options.Page > 0 && options.PageSize > 0)
            {
                url += string.Format("&maxRows={0}&startRow={1}"
                    , options.PageSize
                    , (options.Page - 1) * options.PageSize + 1);
            }

            var regions = HttpGetJson<GeonamesCollection<AdministrativeRegion>>(url);
            return regions == null || regions.Geonames == null ? new List<AdministrativeRegion>() : regions.Geonames.OrderBy(x => x.Name).ToList();
        }

        public IList<AdministrativeRegion> Search(GeoLookupSearchOptions options, string culture)
        {
            if (!string.IsNullOrWhiteSpace(culture))
            {
                options.Culture = new CultureInfo(culture);
            }

            return Search(options);
        }

    }
}
