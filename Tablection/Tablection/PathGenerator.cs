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

        private bool _isCollecting = false;
        public bool IsCollecting
        {
            get
            {
                return _isCollecting;
            }

            set
            {
                _isCollecting = value;
            }
        }

        public PathGenerator(FrameworkElement source)
        {
            _source = source;
        }
        
        public void BeginCollect()
        {
            if (_isCollecting == false)
            {
                this.Clear();

                //_source.PreviewTouchDown += new EventHandler<System.Windows.Input.TouchEventArgs>(_source_PreviewTouchDown);
                //_source.PreviewTouchMove += new EventHandler<System.Windows.Input.TouchEventArgs>(_source_PreviewTouchMove);
                //_source.PreviewTouchUp += new EventHandler<System.Windows.Input.TouchEventArgs>(_source_PreviewTouchUp);

                _source.PreviewStylusButtonDown +=new System.Windows.Input.StylusButtonEventHandler(_source_PreviewStylusButtonDown); 
                _source.PreviewStylusMove +=new System.Windows.Input.StylusEventHandler(_source_PreviewStylusMove); 
                _source.PreviewStylusUp +=new System.Windows.Input.StylusEventHandler(_source_PreviewStylusUp);  
            }

            this._isCollecting = true;
        }

        void _source_PreviewStylusUp(object sender, System.Windows.Input.StylusEventArgs e)
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

        void _source_PreviewStylusMove(object sender, System.Windows.Input.StylusEventArgs e)
        {
            Point pt = e.StylusDevice.GetPosition(_source);
            PathSegment seg = new LineSegment(new Point(pt.X, pt.Y), false);
            _psCollection.Add(seg);
        }

        void _source_PreviewStylusButtonDown(object sender, System.Windows.Input.StylusButtonEventArgs e)
        {
            e.StylusDevice.Capture(_source);
            this.Clear();
        }

        void _source_PreviewTouchMove(object sender, System.Windows.Input.TouchEventArgs e)
        {
            Point pt = e.GetTouchPoint(_source).Position;
            PathSegment seg = new LineSegment(new Point(pt.X, pt.Y), false);
            _psCollection.Add(seg);            
        }

        void _source_PreviewTouchUp(object sender, System.Windows.Input.TouchEventArgs e)
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

        void _source_PreviewTouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            e.TouchDevice.Capture(_source);
            this.Clear();      
        }
        
        public void EndCollect()
        {
            if (this._isCollecting == true)
            {
                //_source.PreviewTouchUp -= new EventHandler<System.Windows.Input.TouchEventArgs>(_source_PreviewTouchUp);
                //_source.PreviewTouchMove -= new EventHandler<System.Windows.Input.TouchEventArgs>(_source_PreviewTouchMove);
                //_source.PreviewTouchDown -= new EventHandler<System.Windows.Input.TouchEventArgs>(_source_PreviewTouchDown);

                _source.PreviewStylusButtonDown -= new System.Windows.Input.StylusButtonEventHandler(_source_PreviewStylusButtonDown);
                _source.PreviewStylusMove -= new System.Windows.Input.StylusEventHandler(_source_PreviewStylusMove);
                _source.PreviewStylusUp -= new System.Windows.Input.StylusEventHandler(_source_PreviewStylusUp); 

                this.Clear();
            }

            this._isCollecting = false;
        }

        public void Clear()
        {
            _psCollection.Clear();
        }

    }

}
