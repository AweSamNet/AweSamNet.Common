using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AweSamNet.Common.Models
{
    // http://www.geonames.org/export/codes.html
    [Flags]
    public enum CountryStateRegionFeatureCodes
    {
        /// <summary>
        /// first-order administrative division	a primary administrative division of a country, such as a state in the United States
        /// </summary>
        ADM1 = 1,

        /// <summary>
        /// historical first-order administrative division	a former first-order administrative division
        /// </summary>
        ADM1H = 1 << 1,

        /// <summary>
        /// second-order administrative division	a subdivision of a first-order administrative division
        /// </summary>
        ADM2 = 1 << 2,

        /// <summary>
        /// historical second-order administrative division	a former second-order administrative division
        /// </summary>
        ADM2H = 1 << 3,

        /// <summary>
        /// third-order administrative division	a subdivision of a second-order administrative division
        /// </summary>
        ADM3 = 1 << 4,

        /// <summary>
        /// historical third-order administrative division	a former third-order administrative division
        /// </summary>
        ADM3H = 1 << 5,

        /// <summary>
        /// fourth-order administrative division	a subdivision of a third-order administrative division
        /// </summary>
        ADM4 = 1 << 6,

        /// <summary>
        /// historical fourth-order administrative division	a former fourth-order administrative division
        /// </summary>
        ADM4H = 1 << 7,

        /// <summary>
        /// fifth-order administrative division	a subdivision of a fourth-order administrative division
        /// </summary>
        ADM5 = 1 << 8,

        /// <summary>
        /// administrative division	an administrative division of a country, undifferentiated as to administrative level
        /// </summary>
        ADMD = 1 << 9,

        /// <summary>
        /// historical administrative division	a former administrative division of a political entity, undifferentiated as to administrative level
        /// </summary>
        ADMDH = 1 << 10,

        /// <summary>
        /// leased area	a tract of land leased to another country, usually for military installations
        /// </summary>
        LTER = 1 << 11,

        /// <summary>
        /// political entity
        /// </summary>
        PCL = 1 << 12,

        /// <summary>
        /// dependent political entity
        /// </summary>
        PCLD = 1 << 13,

        /// <summary>
        /// freely associated state
        /// </summary>
        PCLF = 1 << 14,

        /// <summary>
        /// historical political entity	a former political entity
        /// </summary>
        PCLH = 1 << 15,

        /// <summary>
        /// independent political entity
        /// </summary>
        PCLI = 1 << 16,

        /// <summary>
        /// section of independent political entity
        /// </summary>
        PCLIX = 1 << 17,

        /// <summary>
        /// semi-independent political entity
        /// </summary>
        PCLS = 1 << 18,

        /// <summary>
        /// parish	an ecclesiastical district
        /// </summary>
        PRSH = 1 << 19,

        /// <summary>
        /// territory
        /// </summary>
        TERR = 1 << 20,

        /// <summary>
        /// zone
        /// </summary>
        ZN = 1 << 21,

        /// <summary>
        /// buffer zone	a zone recognized as a buffer between two nations in which military presence is minimal or absent
        /// </summary>
        ZNB = 1 << 22
    }
}
