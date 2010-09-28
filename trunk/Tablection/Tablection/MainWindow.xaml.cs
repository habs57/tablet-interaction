using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Shapes;
using TablectionSketch.Controls;
using TablectionSketch.Tool;

namespace TablectionSketch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PathGenerator _pathGenerator;
        ImageFreeCropHelper _freeCropHelper;
        private bool _isCutStarted;
        TouchRecognizeAutomata _recognier;
        private int gSensor_val;
        private Liner _liner;
        private Point _penStartPoint;
        /// <summary>
        /// Line 모드일 때 가상으로 그려주는 선
        /// </summary>
        private Line _floatingLine { get; set; }

        private TouchRecognizeAutomata.InputMode CurrentMode { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitializeUdpSocket();

            this.DrawingCanvas.Gesture += new RecongnitionGestrueHandler(DrawingCanvas_Gesture);

            _pathGenerator = new PathGenerator(this.DrawingCanvas);
            _pathGenerator.PathGenerated += new Action<PathGeometry>(_pathGenerator_PathGenerated);
            
            _freeCropHelper = new ImageFreeCropHelper(this.DrawingCanvas);
            _freeCropHelper.ImageCropped += new EventHandler<ImageCropEventArgs>(_freeCropHelper_ImageCropped);

            _recognier = new TouchRecognizeAutomata(this.DrawingCanvas);
            _recognier.ModeChanged += new Action<TouchRecognizeAutomata.InputMode>(_recognier_ModeChanged);

            if (Directory.Exists(".\\Saved") == false)
                Directory.CreateDirectory(".\\Saved");

            // Liner 등록
            AddLiner();

            // Line 모드의 이벤트 등록
            _recognier.OnLineStarted += new TouchRecognizeAutomata.EventHandler(_recognier_OnLineStarted);
            _recognier.OnLineMove += new TouchRecognizeAutomata.EventHandler(_recognier_OnLineMove);
            _recognier.OnLineEnded += new TouchRecognizeAutomata.EventHandler(_recognier_OnLineEnded);

            this.DrawingCanvas.PreviewTouchDown += new EventHandler<TouchEventArgs>(DrawingCanvas_PreviewTouchDown);
            this.DrawingCanvas.PreviewTouchMove += new EventHandler<TouchEventArgs>(DrawingCanvas_PreviewTouchMove);
            this.DrawingCanvas.PreviewTouchUp +=new EventHandler<TouchEventArgs>(DrawingCanvas_PreviewTouchUp);

            _isCutStarted = false;

            //<control:Liner x:Name="ui_lineRuler" Visibility="Collapsed">
            //    <control:Liner.Triggers>                    
            //        <EventTrigger RoutedEvent="TouchUp">
            //            <BeginStoryboard Storyboard="{StaticResource linerHideAnimation}"/>
            //        </EventTrigger>
            //    </control:Liner.Triggers>
            //</control:Liner>
        }

        void DrawingCanvas_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            if (this.CurrentMode == TouchRecognizeAutomata.InputMode.Cut)
            {
                if (e.TouchDevice.Id == this._recognier.PenDevID && _isCutStarted)
                {
                    _freeCropHelper.CollectCropArea(e);
                } 
                else if (e.TouchDevice.Id == this._recognier.PenDevID)
                {
                    _isCutStarted = true;
                    _freeCropHelper.StartCropArea(e);
                }                                  
            }
        }

        void DrawingCanvas_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (this.CurrentMode == TouchRecognizeAutomata.InputMode.Cut)
            {
                if (e.TouchDevice.Id == this._recognier.PenDevID)
                {                    
                    _freeCropHelper.StartCropArea(e);
                }
            }
            else if (this.CurrentMode == TouchRecognizeAutomata.InputMode.SelMovImg)
            {
                if (e.TouchDevice.Id != this._recognier.PenDevID)
                {
                    _freeCropHelper.SetCropTarget(e);
                }
            }
        }
        
        private void DrawingCanvas_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            //PreviewRefresh
            this.RefreshCurrentPreview();

            if (this.CurrentMode == TouchRecognizeAutomata.InputMode.Cut)
            {
                if (e.TouchDevice.Id == this._recognier.PenDevID)
                {
                    _isCutStarted = false;
                    _freeCropHelper.EndCropArea(e);
                }
            }
        }

        void _recognier_OnLineEnded(object sender, EventArgs e)
        {
            // 가상 선 등록
            //Line straightLine = new Line();
            //straightLine = new Line();
            //straightLine.StrokeThickness = this.DrawingCanvas.DefaultDrawingAttributes.Width;
            //straightLine.Stroke = this.DrawingCanvas.DefaultDrawingAttributes.Color;
            //straightLine.X1 = _floatingLine.X1;
            //straightLine.Y1 = _floatingLine.Y1;
            //straightLine.X2 = _floatingLine.X2;
            //straightLine.Y2 = _floatingLine.Y2;

            StylusPointCollection stcol = new StylusPointCollection();
            
            stcol.Add(new StylusPoint(_floatingLine.X1, _floatingLine.Y1));
            stcol.Add(new StylusPoint(_floatingLine.X2, _floatingLine.Y2));
            StrokeCollection col = new StrokeCollection();
            Stroke st = new Stroke(stcol);
            col.Add(st);

            this.DrawingCanvas.Strokes.Add(col);

            //gridMain.Children.Add(straightLine);
            //gridMain.RegisterName("ui_straightLine" + DateTime.Now.ToFileTime().ToString(), straightLine);

            _floatingLine.Visibility = System.Windows.Visibility.Collapsed;
        }

        void _recognier_OnLineMove(object sender, EventArgs e)
        {
            double angle = this._liner.rotation.Angle;
            double xLength = _recognier.PenEndPoint.Position.X - _floatingLine.X1;

            _floatingLine.X2 = _recognier.PenEndPoint.Position.X;
            _floatingLine.Y2 = _floatingLine.Y1 + (xLength * Math.Tan(angle * (Math.PI / 180)));
        }

        void _recognier_OnLineStarted(object sender, EventArgs e)
        {
            _floatingLine.Visibility = System.Windows.Visibility.Visible;
            _floatingLine.Opacity = 0.7;
            _floatingLine.X1 = _recognier.PenStartPoint.Position.X;
            _floatingLine.Y1 = _recognier.PenStartPoint.Position.Y;
            _floatingLine.X2 = _recognier.PenEndPoint.Position.X;
            _floatingLine.Y2 = _recognier.PenEndPoint.Position.Y;
        }

        private void AddLiner()
        {
            // Line(자) 등록
            _liner = new Liner();
            _liner.Name = "ui_lineRuler";
            _liner.Visibility = System.Windows.Visibility.Collapsed;
            _liner.OnRequestClose += new Liner.EventHandler(_liner_OnRequestClose);
            gridMain.Children.Add(_liner);
            gridMain.RegisterName("ui_lineRuler", _liner);

            // 가상 선 등록
            _floatingLine = new Line();
            _floatingLine.StrokeThickness = 2.0d;
            _floatingLine.Stroke = Brushes.Red;
            _floatingLine.Visibility = System.Windows.Visibility.Collapsed;

            gridMain.Children.Add(_floatingLine);
            gridMain.RegisterName("ui_floadingLine", _floatingLine);
        }

        void _liner_OnRequestClose(object sender, EventArgs e)
        {
            this._recognier.IsRuler = false;
            _liner.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void InitializeUdpSocket()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9985);
            UdpClient newsock = new UdpClient(ipep);

            UdpState s = new UdpState();

            s.e = ipep;
            s.u = newsock;

            newsock.BeginReceive(new AsyncCallback(OnReceive), s);
        }

        public void OnReceive(IAsyncResult ar)
        {
            UdpState s = new UdpState();

            UdpClient u = (UdpClient)((UdpState)(ar.AsyncState)).u;
            IPEndPoint e = (IPEndPoint)((UdpState)(ar.AsyncState)).e;

            s.u = u;
            s.e = e;

            Byte[] receiveBytes = u.EndReceive(ar, ref e);
            Debug.WriteLine(string.Format("Pen Signal Data : {0}", Encoding.ASCII.GetString(receiveBytes, 0, receiveBytes.Length)));

            if (Encoding.ASCII.GetString(receiveBytes, 0, receiveBytes.Length) == "NONE")
            {
                gSensor_val = 0;
                this._recognier.IsPen = false;
                //this.Dispatcher.Invoke(new myDelegate(DrawingCanvas_PreviewStylusDownBySensor),1);
                //this.llbTools.SelectedIndex = 1;
            }
            else if (Encoding.ASCII.GetString(receiveBytes, 0, receiveBytes.Length) == "WEAK")
            {
                gSensor_val = 1;
                this._recognier.IsPen = true;
                //this.Dispatcher.Invoke(new myDelegate(DrawingCanvas_PreviewStylusDownBySensor), 2);
            }
            else if (Encoding.ASCII.GetString(receiveBytes, 0, receiveBytes.Length) == "STNG")
            {
                gSensor_val = 2;
                this._recognier.IsPen = true;
                //this.Dispatcher.Invoke(new myDelegate(DrawingCanvas_PreviewStylusDownBySensor), 3);
            }
            else
            {
                gSensor_val = 3;
                this._recognier.IsPen = true;
                //this.Dispatcher.Invoke(new myDelegate(DrawingCanvas_PreviewStylusDownBySensor), 4);
            }

            u.BeginReceive(new AsyncCallback(OnReceive), s);
        }

        void _recognier_ModeChanged(TouchRecognizeAutomata.InputMode obj)
        {
            this.CurrentMode = obj;

            switch (obj)
            {
                case TouchRecognizeAutomata.InputMode.None:                    
                    this.SelectedIndex(0);
                    break;
                case TouchRecognizeAutomata.InputMode.Pen:                    
                    this.SelectedIndex(1);
                    break;
                case TouchRecognizeAutomata.InputMode.Erase:                    
                    this.SelectedIndex(6);
                    break;
                case TouchRecognizeAutomata.InputMode.SelMovImg:
                    this.SelectedIndex(0);
                    break;
                case TouchRecognizeAutomata.InputMode.TransImg:
                    this.SelectedIndex(0);
                    break;
                case TouchRecognizeAutomata.InputMode.Cut:                    
                    this.SelectedIndex(3);

                    break;
                case TouchRecognizeAutomata.InputMode.Ruler:
                    Debug.WriteLine("Ruler selected");
                    this.llbToolHeaders.SelectedIndex = 3;      // line ruler
                    break;
                case TouchRecognizeAutomata.InputMode.Line:
                    Polyline line = new Polyline();
                    // TODO
                    double angle = _liner.rotation.Angle + 90;
                    //Point linerFirstPoint = TouchDevice;
                    break;
                default:
                    break;
            }

            System.Diagnostics.Debug.WriteLine(string.Format("현재 툴 모드: {0}", this.DrawingCanvas.EditingMode));
                        
        }

        private void SelectedIndex(int newMode)
        {
            int index = this.llbTools.SelectedIndex;
            if (index != newMode)
            {
                this.llbTools.SelectedIndex = newMode;
            }
        }
               
        //이미지 가위질 되고 직후 동작
        void _freeCropHelper_ImageCropped(object sender, object e)
        {
            //this.llbTools.SelectedIndex = 0;
        }

        //스트로크 인식 
        void _pathGenerator_PathGenerated(PathGeometry obj)
        {  
            //this._freeCropHelper.BeginCrop(obj);            
        }

        void DrawingCanvas_Gesture(ApplicationGesture Gestrue)
        {            
            System.Diagnostics.Debug.WriteLine(Gestrue.ToString());
        }

        private void RefreshCurrentPreview()
        {
            System.Diagnostics.Debug.WriteLine("RefreshCurrentPreview");
            Slide.Slide currentSlide = this.SlideList.SelectedItem as Slide.Slide;
            if (currentSlide != null)
            {
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)this.DrawingCanvas.ActualWidth, (int)this.DrawingCanvas.ActualHeight, 100, 100, PixelFormats.Default);                
                rtb.Render(this.DrawingCanvas);

                currentSlide.Thumbnail = rtb;                
            }
        }
        
        private void HeaderBasicTool_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.radioTools.IsChecked = true;
        }

        private void HeaderColorTool_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.radioColors.IsChecked = true;
        }

        private void HeaderStrokeTool_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.radioStrokes.IsChecked = true;
        }
        

        private ImageSearchWindow _searchWindow = null;
        protected ImageSearchWindow SearchWindow
        {
            get
            {
                if (_searchWindow == null)
                {
                    _searchWindow = new ImageSearchWindow();
                }
                return _searchWindow;
            }
        }

        private void LoopingListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (ToolHeader item in e.AddedItems)
            {
                string selectedToolName = item.RelatedControlName;
                System.Diagnostics.Debug.WriteLine("LoopingListBox_SelectionChanged: " + selectedToolName);


                //if (selectedToolName.Equals("CutMode") == false && this._pathGenerator.IsCollecting == true)
                //{
                //    this.Cursor = Cursors.Arrow;
                //    this.llbTools.SelectedIndex = 1;
                //    this._pathGenerator.EndCollect();
                //    this.SearchWindow.Hide();
                //}
                if (this.radioTools != null && selectedToolName.Equals("llbTools") == true)
                {
                    this.radioTools.IsChecked = true;
                    this.SearchWindow.Hide();
                }
                else if (this.radioColors != null && selectedToolName.Equals("llbColors") == true)
                {
                    this.radioColors.IsChecked = true;
                    this.SearchWindow.Hide();
                }
                else if (this.radioStrokes != null && selectedToolName.Equals("llbStroke") == true)
                {
                    this.radioStrokes.IsChecked = true;
                    this.SearchWindow.Hide();
                }
                else if (this.radioStrokes != null && selectedToolName.Equals("ctl_Search") == true)
                {
                    //검색창 띄움
                    this.SearchWindow.Show();                    
                }
                else if (selectedToolName.Equals("CutMode") == true)
                {
                    //if (this._pathGenerator.IsCollecting == false)
                    {
                        this.Cursor = Cursors.Pen; //가위로 바꿔야 함
                        this.llbTools.SelectedIndex = 3;
                         
                        //this._pathGenerator.BeginCollect();
                    }
                    this.SearchWindow.Hide();
                }
                else if (selectedToolName.Equals("LinerMode") == true)
                {
                    this._recognier.IsRuler = true;
                    _liner.Visibility = Visibility.Visible;
                    _liner.Opacity = 1;
                    this.llbToolHeaders.SelectedIndex = 0;      // line ruler
                    this.SelectedIndex(0);                    
                }
                else
                {
                    this.radioTools.IsChecked = false;
                    this.radioColors.IsChecked = false;
                    this.radioStrokes.IsChecked = false;
                    this.SearchWindow.Hide();
                }               
            }
        }
        
        private void llbTools_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                BasicTool tool = e.AddedItems[0] as BasicTool;
                if (tool != null)
                {
                    this.DrawingCanvas.EditingMode = tool.Mode;
                }
            }
        }

      

#region Button Top for Tools 
        
        private void btnTop_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {            
            //툴 스크롤
            //this.llbToolHeaders.Offset += e.DeltaManipulation.Translation.Length;
            //System.Diagnostics.Debug.WriteLine(string.Format("btnTop_ManipulationDelta : Offset - {0}, Trans - {1}",this.llbToolHeaders.Offset, e.DeltaManipulation.Translation.Length));
        }

        private void btnTop_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("btnTop_ManipulationStarted");
            this.ToolPanel.Visibility = Visibility.Visible;
        }

#endregion

#region Button Bottom for Tools 
                
        private void btnBottom_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            //페이지 넘기기 인터렉션
            int currentIndex = this.SlideList.SelectedIndex;
            int totalSlides = this.SlideList.Items.Count;

            double trans = e.DeltaManipulation.Translation.X;

            System.Diagnostics.Debug.WriteLine(string.Format("btnBottom_ManipulationDelta : X - {0}", trans));

        }

        private void btnBottom_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {            
            System.Diagnostics.Debug.WriteLine("btnBottom_ManipulationStarted");
            this.SlideList.Visibility = Visibility.Visible;   
        }
        
#endregion

        private void DrawingCanvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.RefreshCurrentPreview();
        }


        private HitTestFilterBehavior FilterCallBack(DependencyObject e)
        {
            if (e is TouchableImage)
            {
                (e as TouchableImage).Focus();
                return HitTestFilterBehavior.Stop;
            }

            return HitTestFilterBehavior.Continue;
        }

        private HitTestResultBehavior ResultCallBack(HitTestResult e)
        {
            TouchableImage touchObj = e.VisualHit as TouchableImage;
            if (touchObj != null)
            {
                return HitTestResultBehavior.Stop;
            }

            return HitTestResultBehavior.Continue;
        }     

        private void BackupChildObj(SelectionChangedEventArgs e)
        {
            //이전거 백업 
            if (e.RemovedItems != null && e.RemovedItems.Count > 0)
            {
                Slide.Slide oldSlide = e.RemovedItems[0] as Slide.Slide;
                if (oldSlide != null)
                {
                    oldSlide.Children.Clear();
                    foreach (UIElement item in this.DrawingCanvas.Children)
                    {
                        oldSlide.Children.Add(cloneElement(item));
                    }        
                }
            }
        }

        private void RestoreChildObj(SelectionChangedEventArgs e)
        {
            //새거 로드
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                Slide.Slide newSlide = e.AddedItems[0] as Slide.Slide;
                if (newSlide != null)
                {
                    foreach (UIElement item in newSlide.Children)
                    {                        
                        this.DrawingCanvas.Children.Add(item);
                       
                        item.IsManipulationEnabled = true;
                    }
                }
            }           
        }

        private void SlideList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.BackupChildObj(e);
            this.DrawingCanvas.Children.Clear();
            this.RestoreChildObj(e);
        }


        /// clones a UIElement
        public static UIElement cloneElement(UIElement orig)
        {
            if (orig == null)
            {
                return (null);
            }

            var s = System.Windows.Markup.XamlWriter.Save(orig);
            var stringReader = new System.IO.StringReader(s);
            var xmlReader = System.Xml.XmlTextReader.Create(stringReader, new System.Xml.XmlReaderSettings());

            return (UIElement)System.Windows.Markup.XamlReader.Load(xmlReader);

        }

        #region 드래그드롭 기능 

        private string[] supportedFormats = new string[] { ".jpg", ".bmp", ".gif", ".png" };

        private void DrawingCanvas_DragOver(object sender, DragEventArgs e)
        {
            DoWithSupportedImage(e, (path, args) => 
            {
                string extension = System.IO.Path.GetExtension(path).ToLower();
                bool isSupportedFormat = supportedFormats.Contains(extension);
                if (isSupportedFormat == true)
                {
                    args.Effects = DragDropEffects.Copy | DragDropEffects.Move;
                }                
            });           
        }

        private Point lastPt;

        private void DrawingCanvas_Drop(object sender, DragEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DrawwingCanvas_Drop");

            DoWithSupportedImage(e, (path, args) =>
            {
                lastPt = e.GetPosition(this.DrawingCanvas);
                Uri uri = new Uri(path);
                BitmapImage image = new BitmapImage(uri);
                
                if (uri.IsFile == true)
                {
                    this.AddTouchableImage(lastPt, image); 
                }
                else
                {                    
                    image.DownloadCompleted += new EventHandler(image_DownloadCompleted);                    
                }                
            });
            
        }

        void AddTouchableImage(Point pt, BitmapImage image)
        {
            TouchableImage ti = new TouchableImage();
            ti.Source = image;
            ti.Width = image.PixelWidth > 500 ? (double)(image.PixelWidth >> 1) : image.PixelWidth;
            ti.Height = image.PixelHeight > 500 ? (double)(image.PixelHeight >> 1) : image.PixelHeight;
            this.DrawingCanvas.Children.Add(ti);

            this.RefreshCurrentPreview();
            InkCanvas.SetLeft(ti, pt.X - ((int)ti.Width >> 2));
            InkCanvas.SetTop(ti, pt.Y - ((int)ti.Height >> 2));

            ti.IsManipulationEnabled = true;
        }

        void image_DownloadCompleted(object sender, EventArgs e)
        {
            BitmapImage image = sender as BitmapImage;
            this.AddTouchableImage(lastPt, image);            
        }

        private void DoWithSupportedImage(DragEventArgs e, Action<string, DragEventArgs> doAction)
        {
            bool isFileExist = e.Data.GetDataPresent("FileNameW");
            if (isFileExist == true)
            {
                string[] path = e.Data.GetData("FileNameW") as string[];
                if (path != null && path.Length > 0)
                {
                    string file = path[0];
                    doAction(file, e);
                }
            }
            else
            {
                bool isDataExist = e.Data.GetDataPresent("GongSolutions.Wpf.DragDrop");
                if (isDataExist == true)
                {
                    XmlElement element = e.Data.GetData("GongSolutions.Wpf.DragDrop") as XmlElement;
                    if (element != null)
                    {
                        string file = element.SelectSingleNode("child::thumbnail").InnerText;
                        doAction(file, e);
                    }
                }
            }

        }

        #endregion

        #region Manipulation

        private void DrawingCanvas_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            e.TranslationBehavior = new System.Windows.Input.InertiaTranslationBehavior();
            e.TranslationBehavior.InitialVelocity = e.InitialVelocities.LinearVelocity;
            e.TranslationBehavior.DesiredDeceleration = 10.0 * 96.0 / (1000.0 * 1000.0);

            e.ExpansionBehavior = new System.Windows.Input.InertiaExpansionBehavior();
            e.ExpansionBehavior.InitialVelocity = e.InitialVelocities.ExpansionVelocity;
            e.ExpansionBehavior.DesiredDeceleration = 0.1 * 96 / (1000.0 * 1000.0);

            e.RotationBehavior = new InertiaRotationBehavior();
            e.RotationBehavior.InitialVelocity = e.InitialVelocities.AngularVelocity;
            e.RotationBehavior.DesiredDeceleration = 720 / (1000.0 * 1000.0);
        }

        private void DrawingCanvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            TouchableImage image = e.OriginalSource as TouchableImage;
            if (image != null)
            {
                Rect containingRect = new Rect(((FrameworkElement)e.ManipulationContainer).RenderSize);
                Rect shapeBounds = image.transformGroup.TransformBounds(new Rect(image.RenderSize));

                if (e.IsInertial && !containingRect.Contains(shapeBounds))
                {
                    e.Complete();
                }

                Point center = new Point(image.RenderSize.Width / 2.0, image.RenderSize.Height / 2.0);

                //rotation
                image.rotate.CenterX = center.X;
                image.rotate.CenterY = center.Y;
                image.rotate.Angle += e.DeltaManipulation.Rotation;

                //scale
                image.scale.CenterX = center.X;
                image.scale.CenterY = center.Y;
                image.scale.ScaleX *= e.DeltaManipulation.Scale.X;
                image.scale.ScaleY *= e.DeltaManipulation.Scale.Y;

                //trans
                image.traslation.X += e.DeltaManipulation.Translation.X;
                image.traslation.Y += e.DeltaManipulation.Translation.Y;  
            }
            
        }

        private void DrawingCanvas_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {            
            e.ManipulationContainer = this.DrawingCanvas;  
        }
        
        #endregion //Manipulation

        private void btnBottom_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            this.SlideList.Visibility = Visibility.Visible;            
        }

        private void SlideListHide_Completed(object sender, EventArgs e)
        {
            this.SlideList.Visibility = Visibility.Collapsed;
        }

        private void LineRulerHide_Completed(object sender, EventArgs e)
        {
            //this.ui_lineRuler.Opacity = 1;
            //this.ui_lineRuler.Visibility = Visibility.Collapsed;
            //this.llbToolHeaders.SelectedIndex = -1;      // line ruler
        }

        private void btnBottom_Click(object sender, RoutedEventArgs e)
        {
            this.SlideList.Visibility = Visibility.Visible;
        }


        private UIElement CloneDocument(UIElement fdToClone)
        {

            String savedDoc = XamlWriter.Save(fdToClone);

            StringReader srStringReader = new StringReader(savedDoc);

            XmlReader xrDoc = XmlReader.Create(srStringReader);

            return (UIElement)XamlReader.Load(xrDoc);

        }
        
        
        public void CloseWindows(object sender, MouseButtonEventArgs e) 
        {
            int slide_cnt = this.SlideList.Items.Count;
            int i;

            //현재 슬라이드의 Child 이미지들의 UIElement를 저장

            Slide.Slide s = this.SlideList.SelectedItem as Slide.Slide;
            s.Children.Clear();

            foreach (UIElement item in this.DrawingCanvas.Children)
            {
              //  Slide.Slide s = this.SlideList.SelectedItem as Slide.Slide;
                s.Children.Add(cloneElement(item));
            }

            //전체 슬라이드를 순회하면서 Stroke, Child Element, Thumbnail을 저장
            for (i = 0; i < slide_cnt; i++)
            {
                int j;

                // Strokes를 저장
                StrokeCollection _strokes;
                Slide.Slide _slide;
                _slide = (this.SlideList.Items[i] as Slide.Slide);
                _strokes = _slide.Strokes;
                File.WriteAllText(".\\Saved\\" + _slide.Title + "_Strokes.xaml", XamlWriter.Save(_strokes));
            
                //Thumbnail을 저장
                IFormatter formatter = new BinaryFormatter();
                Stream stream = File.Create(".\\Saved\\" + _slide.Title + "_th.jpg");
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.QualityLevel = 30;
                encoder.Frames.Add(BitmapFrame.Create(_slide.Thumbnail as BitmapSource));
                encoder.Save(stream);
                stream.Flush();
                stream.Close();

                                //ChildUIElement를 저장
                //System.Diagnostics.Debug.WriteLine("ChildCout :"+ _slide.Children.Count);
                for (j = 0; j < _slide.Children.Count; j++)
                {                    
                    UIElement _saved;
                    _saved = CloneDocument(_slide.Children[j] as UIElement);                    
                    File.WriteAllText(".\\Saved\\" + _slide.Title + "_UIElement"+j+".xaml", XamlWriter.Save(_saved));
                }
            }
            this.SearchWindow.KillMe();
            this.Close();
        }

        private void MinimizeWindows(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }

    public class UdpState
    {
        public UdpClient u;
        public IPEndPoint e;
    }

}
