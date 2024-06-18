using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Utility.Display
{
    public interface INewGeometryFeedBack
    {
        IActiveView ActiveView { get; set; }
        ISymbol Symbol { get; }
        void Start(IPoint point);
        void AddPoint(IPoint point);
        void MoveTo(IPoint point);
        void FinishPart();
        IGeometry Stop();
        void Refresh();
        IGeometry Geometry { get; }
    }
}
