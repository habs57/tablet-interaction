using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Media;

namespace TablectionSketch
{
    /// <summary>
    /// 입력받은 경로를 모아서 영역을 만들어 반환합니다.
    /// </summary>
    public class PathGenerator
    {
        public event Action<PathGeometry> PathGenerated;

        private FrameworkElement _source;
        
        PathSegmentCollection _psCollection = new PathSegmentCollection();

        public PathGenerator(FrameworkElement source)
        {
            _source = source;
        }
        
        public void BeginCollect()
        {
            _psCollection.Clear();
        }

        public void Collect(System.Windows.Input.TouchEventArgs e)
        {
            Point pt = e.GetTouchPoint(_source).Position;
            PathSegment seg = new LineSegment(new Point(pt.X, pt.Y), false);
            _psCollection.Add(seg);
        }

        public void EndCollect()
        {
            if (this.PathGenerated != null && _psCollection.Count > 0)
            {
                PathGeometry pg = new PathGeometry();
                pg.FillRule = FillRule.Nonzero;

                PathFigureCollection figs = new PathFigureCollection();
                pg.Figures = figs;

                //닫힌 Path를 형성함
                PathSegmentCollection pscol2 = _psCollection.Clone();
                PathSegment last = pscol2.Last();
                pscol2.Insert(0, last);

                PathFigure fig = new PathFigure();
                fig.Segments = pscol2;
                fig.IsClosed = true;
                figs.Add(fig);

                this.PathGenerated(pg);
            }
        }

    }

}
