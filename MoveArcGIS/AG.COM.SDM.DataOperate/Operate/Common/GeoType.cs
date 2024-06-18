using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.COM.SDM.DataOperate
{
    public enum GeoType
    {
        /// <summary>
        /// The OGIS geometry type number for points.
        /// </summary>
        Point = 1,

        /// <summary>
        /// The OGIS geometry type number for lines.
        /// </summary>
        LineString = 2,

        /// <summary>
        /// The OGIS geometry type number for polygons.
        /// </summary>
        Polygon = 3,

        /// <summary>
        /// The OGIS geometry type number for aggregate points.
        /// </summary>
        MultiPoint = 4,

        /// <summary>
        /// The OGIS geometry type number for aggregate lines.
        /// </summary>
        MultiLineString = 5,

        /// <summary>
        /// The OGIS geometry type number for aggregate polygons.
        /// </summary>
        MultiPolygon = 6,

        /// <summary>
        /// The OGIS geometry type number for feature collections.
        /// </summary>
        GeometryCollection = 7,


    }


}
