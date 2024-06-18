using OSGeo.OGR;
using OSGeo.OSR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTopologySuite.GdalEx
{
    public interface IFeatureLayer
    {
        Layer Layer { get; set; }
        
        string Name { get; set; }

        SpatialReference SR { get; set; }

        wkbGeometryType GeometryType { get; set; }

        List<FieldDefn> FieldDefns { get; set; }

    }
}
