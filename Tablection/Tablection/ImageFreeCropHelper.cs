using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TablectionSketch.Controls;

using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace TablectionSketch
{
    public class ImageCropEventArgs : EventArgs
    {

    }

    /// <summary>
    /// 손으로 선택한 이미지 알아냄 
    /// 펜 정보 입력시 선택한 이미지와 접촉하는지 여부 알아냄
    /// 접촉하면 선택한 이미지를 잘라냄 
    /// </summary>
    public class ImageFreeCropHelper
    {

        public event EventHandler<ImageCropEventArgs> ImageCropped;
             
        private TouchableImage _selectedImage = null;
        private InkCanvas _inkCanvas = null;

        private PathGenerator _pathGenerator = null;

        public ImageFreeCropHelper(InkCanvas canvas)
        {
            _inkCanvas = canvas;
            _pathGenerator = new PathGenerator(canvas);
            _pathGenerator.PathGenerated += new Action<PathGeometry>(_pathGenerator_PathGenerated);
        }

        void _pathGenerator_PathGenerated(PathGeometry obj)
        {
            this.BeginCrop(obj);
        }

        public void SetCropTarget(System.Windows.Input.TouchEventArgs e)
        {          
            this._selectedImage = null;                
           
            Visual visual = this._inkCanvas as Visual;
            if (visual != null)
            {
                //선택한 위치가 이미지랑 겹치는지 확인             
                //겹치면 가장 위 이미지에 대해서 정보를 클래스에 저장 
                VisualTreeHelper.HitTest(this._inkCanvas.Parent as Visual,
                    new HitTestFilterCallback(p =>
                    {
                        if (p is TouchableImage)
                        {
                            this._selectedImage = p as TouchableImage;
                            return HitTestFilterBehavior.Stop;
                        }

                        this._selectedImage = null;
                        return HitTestFilterBehavior.Continue;
                    }),
                    new HitTestResultCallback(q =>
                    {
                        return HitTestResultBehavior.Continue;
                    }),
                    new PointHitTestParameters(e.GetTouchPoint(null).Position));                             
            }           
        }

        public void StartCropArea(System.Windows.Input.TouchEventArgs e)
        {
            this._pathGenerator.BeginCollect();
            this.SetCropTarget(e);        
        }

        public void CollectCropArea(System.Windows.Input.TouchEventArgs e)
        {
            this._pathGenerator.Collect(e);          
        }

        public void EndCropArea(System.Windows.Input.TouchEventArgs e)
        {
            this._pathGenerator.EndCollect();
        }      

        private bool BeginCrop(PathGeometry path)
        {          
            TouchableImage image = this._selectedImage;
            if (image != null)
            {
                //이 이미지 위치정보 A+ 저장 
                double outerImage_x = InkCanvas.GetLeft(image);
                double outerImage_y = InkCanvas.GetTop(image);
                double outerImage_width = image.Width;
                double outerImage_height = image.Height;
                Rect outerImageRect = new Rect(0, 0, outerImage_width, outerImage_height);

                //입력받은 패스를 가지고 이미지의 좌표에 맞게 좌표수정 
                path.Transform = new TranslateTransform(-outerImage_x, -outerImage_y);

                //화면 영역에 맞게 combine 된 clipping path를 구함
                RectangleGeometry imageRectGeo = new RectangleGeometry(outerImageRect);
                CombinedGeometry combinedGeoIntersect = new CombinedGeometry(GeometryCombineMode.Intersect, imageRectGeo, path);

                //이미지에 적용, 패스로 크로핑    
                image.Clip = combinedGeoIntersect;
                              
                //새 이미지를 만든다.
                TouchableImage cloneImg = new TouchableImage();
                //cloneImg.RenderTransform = image.RenderTransform.Clone();
                cloneImg.Source = image.Source.Clone();
                cloneImg.Width = image.ActualWidth;
                cloneImg.Height = image.ActualHeight;
                
                //이미지의 영역과 클리핑 데이터로 새로운 클리핑 영역을 만듦                
                CombinedGeometry combinedGeoXor = new CombinedGeometry(GeometryCombineMode.Xor, imageRectGeo, path);
                cloneImg.Clip = combinedGeoXor;
                
                //새로 만들어진 이미지를 넣고 배치한다.
                this._inkCanvas.Children.Add(cloneImg);
                                
                //cloneImg.RenderTransform = new RotateTransform(); 

                InkCanvas.SetLeft(cloneImg, outerImage_x);
                InkCanvas.SetTop(cloneImg, outerImage_y);
                               

                //원본 이미지에 재적용 
                //클리핑된 이미지 B 완성 (선택한 영역만 남은 그림)
                //이 이미지 위치정보 B+ 저장

                //원본 이미지의 인덱스 위치에 
                //이미지 A 를 A+ 위치에 배치 
                //이미지 B 를 B+ 위치에 배치 

                //원본 이미지 캔버스에서 제거           

                if (this.ImageCropped != null)
                {
                    ImageCropped(this, null);
                }
            }         
            return true;
        }

    }
}
