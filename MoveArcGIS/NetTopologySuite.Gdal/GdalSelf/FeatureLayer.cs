using OSGeo.OGR;
using OSGeo.OSR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTopologySuite.GdalEx
{
    public class FeatureLayer : IFeatureLayer
    {
        public Layer Layer { get; set; }

        public string Name { get; set; }

        public SpatialReference SR { get; set; }

        public wkbGeometryType GeometryType { get; set; }

        public List<FieldDefn> FieldDefns { get; set; }

    }
}
