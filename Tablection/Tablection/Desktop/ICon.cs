using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tablection.Desktop
{
    /// <summary>
    /// XAML 파일에서 이 사용자 지정 컨트롤을 사용하려면 1a 또는 1b단계를 수행한 다음 2단계를 수행하십시오.
    ///
    /// 1a단계) 현재 프로젝트에 있는 XAML 파일에서 이 사용자 지정 컨트롤 사용.
    /// 이 XmlNamespace 특성을 사용할 마크업 파일의 루트 요소에 이 특성을 
    /// 추가합니다.
    ///
    ///     xmlns:MyNamespace="clr-namespace:Tablection.Desktop"
    ///
    ///
    /// 1b단계) 다른 프로젝트에 있는 XAML 파일에서 이 사용자 지정 컨트롤 사용.
    /// 이 XmlNamespace 특성을 사용할 마크업 파일의 루트 요소에 이 특성을 
    /// 추가합니다.
    ///
    ///     xmlns:MyNamespace="clr-namespace:Tablection.Desktop;assembly=Tablection.Desktop"
    ///
    /// 또한 XAML 파일이 있는 프로젝트의 프로젝트 참조를 이 프로젝트에 추가하고
    /// 다시 빌드하여 컴파일 오류를 방지해야 합니다.
    ///
    ///     솔루션 탐색기에서 대상 프로젝트를 마우스 오른쪽 단추로 클릭하고
    ///     [참조 추가]->[프로젝트]를 차례로 클릭한 다음 이 프로젝트를 찾아서 선택합니다.
    ///
    ///
    /// 2단계)
    /// 계속 진행하여 XAML 파일에서 컨트롤을 사용합니다.
    ///
    ///     <MyNamespace:ICon/>
    ///
    /// </summary>
    public class ICon : ToggleButton
    {
        Window _parent;

        static ICon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ICon), new FrameworkPropertyMetadata(typeof(ICon)));
        }

#region Field

        /// <summary>
        /// Saved old point for support drag
        /// </summary>
        private Point _prevPoint;

        private Brush _prevBackground; 

        /// <summary>
        /// Icon Information Browser.
        /// 아이콘 하나를 누른 상태에서 그것을 중심으로 드래그 하면 이 오브젝트가 생성되어 아이콘의 정보를 보여준다.
        /// </summary>
        IconInfoBrowser iib;

#endregion //Field
        
#region Private Helpers

        private Point FitInRange(Point pt)
        {
            Point newPoint = pt;

            newPoint.X = (newPoint.X < 0 ? 0 : newPoint.X);
            newPoint.Y = (newPoint.Y < 0 ? 0 : newPoint.Y);

            double width = (this.Parent as FrameworkElement).ActualWidth;
            double height = (this.Parent as FrameworkElement).ActualHeight;

            newPoint.X = (newPoint.X + this.ActualWidth > width ? width - this.Width : newPoint.X);
            newPoint.Y = (newPoint.Y + this.ActualHeight > height ? height - this.Height : newPoint.Y);

            System.Diagnostics.Debug.WriteLine(string.Format("X {0}, Y {1}", width, height));

            return newPoint;
        }

#endregion //Private Helpers

#region Public Methods

        public void BeginDrag(Point currentPoint)
        {
            this._prevPoint = currentPoint;
        }

        public void DoDrag(Point currentPoint)
        {
            double deltaX = currentPoint.X - this._prevPoint.X;
            double deltaY = currentPoint.Y - this._prevPoint.Y;

            double curPositionX = Canvas.GetLeft(this);
            double curPositionY = Canvas.GetTop(this);

            double nextPositionX = curPositionX + deltaX;
            double nextPositionY = curPositionY + deltaY;

            Point nextPosition = FitInRange(new Point(nextPositionX, nextPositionY));

            Canvas.SetLeft(this, nextPosition.X);
            Canvas.SetTop(this, nextPosition.Y);

            _prevPoint = currentPoint;
        }

        public void BringToFront()
        {
            Canvas parent = (this.Parent as Canvas);

            int totalICons = (this.Parent as Canvas).Children.Count;
            parent.Children.Remove(this);
            parent.Children.Insert(totalICons - 1, this);
        }

        /// <summary>
        /// 소속된 윈도우를 지정한다.
        /// </summary>
        /// <param name="ui">부모 윈도우</param>
        public void SetParent(Window ui)
        {
            _parent = ui;
        }

#endregion //Public Methods

#region Touch Handlers

        protected override void OnTouchUp(TouchEventArgs e)
        {
            this.Background = _prevBackground;
            base.OnTouchUp(e);
        }

        protected override void OnTouchMove(TouchEventArgs e)
        {
            this.DoDrag(e.GetTouchPoint(Application.Current.MainWindow).Position);

            //System.Diagnostics.Debug.WriteLine(string.Format("IsPressed {0}, IsChecked {1}, IsMouseOver {2}, IsStylusOver {3}", this.IsPressed, this.IsChecked, this.IsMouseOver, this.IsStylusOver));

            if (this.Background !=  Brushes.HotPink)
            {
                this._prevBackground = this.Background;
                this.Background = Brushes.HotPink;
            }

            e.Handled = true;
            base.OnTouchMove(e);           
        }

        protected override void OnTouchDown(TouchEventArgs e)
        {
            this.BeginDrag(e.GetTouchPoint(Application.Current.MainWindow).Position);
            this.BringToFront();            

            e.Handled = true;
            base.OnTouchDown(e);
        }

       

#endregion //Touch Handlers

#region Mouse Handlers

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (_parent == null)
                throw new Exception("부모가 없어요! SetParent() 사용!");

            Point pt = new Point(_parent.Left, _parent.Top);

            ////// Show window
            //iib = new IconInfoBrowser();
            //iib.Top = pt.Y + Canvas.GetTop(this);
            //iib.Left = pt.X + Canvas.GetLeft(this);
            //iib.Show();

            //// Begin Drag and put this top
            this.BeginDrag(e.GetPosition(Application.Current.MainWindow));
            this.BringToFront();
            
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {   
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DoDrag(e.GetPosition(Application.Current.MainWindow));
            }           

            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            // Dispose icon information browser.
            //iib.Close();
         
            base.OnMouseUp(e);
        }
        
#endregion //Mouse Handlers

    }
}
