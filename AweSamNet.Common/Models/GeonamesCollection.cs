using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AweSamNet.Common.Models
{
    public class GeonamesCollection<T>
    {
        public int TotalResultsCount { get; set; }
        public List<T> Geonames { get; set; }
    }
}
