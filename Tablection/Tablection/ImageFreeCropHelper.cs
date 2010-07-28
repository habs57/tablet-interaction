using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TablectionSketch.Controls;

using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace TablectionSketch
{
    /// <summary>
    /// 손으로 선택한 이미지 알아냄 
    /// 펜 정보 입력시 선택한 이미지와 접촉하는지 여부 알아냄
    /// 접촉하면 선택한 이미지를 잘라냄 
    /// </summary>
    public class ImageFreeCropHelper
    {
        private TouchableImage _selectedImage = null;
        private InkCanvas _inkCanvas = null;

        public ImageFreeCropHelper(InkCanvas canvas)
        {
            _inkCanvas = canvas;

            _inkCanvas.PreviewTouchDown += new EventHandler<System.Windows.Input.TouchEventArgs>(_inkCanvas_PreviewTouchDown);
        }

        void _inkCanvas_PreviewTouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            Visual visual = this._inkCanvas as Visual;
            if (visual != null)
            {
                //선택한 위치가 이미지랑 겹치는지 확인             
                //겹치면 가장 위 이미지에 대해서 정보를 클래스에 저장 
                VisualTreeHelper.HitTest(visual, 
                    new HitTestFilterCallback(p => 
                    {
                        if (p is TouchableImage)
                        {                            
                            return HitTestFilterBehavior.Stop;
                        }
                        return HitTestFilterBehavior.Continue;
                    }), 
                    new HitTestResultCallback(q => 
                    {
                        TouchableImage image = q.VisualHit as TouchableImage;
                        if (image != null)
                        {
                            this._selectedImage = image;
                            return HitTestResultBehavior.Stop;
                        }
                        return HitTestResultBehavior.Continue;
                    }), 
                    new PointHitTestParameters(e.GetTouchPoint(null).Position));                
            }           
        }
                

        public bool BeginCrop(PathGeometry path)
        {
            //입력받은 패스를 가지고 
            //이미지에 적용 

            //패스로 크로핑
            //크로핑된 이미지 A 저장 (선택한 영역이 빈 그림)
            //이 이미지 위치정보 A+ 저장 
            
            //크로핑된 이미지 복제
            //이 이미지로 클리핑 영역 제작 
            
            //원본 이미지에 재적용 
            //클리핑된 이미지 B 완성 (선택한 영역만 남은 그림)
            //이 이미지 위치정보 B+ 저장

            //원본 이미지의 인덱스 위치에 
            //이미지 A 를 A+ 위치에 배치 
            //이미지 B 를 B+ 위치에 배치 

            //원본 이미지 캔버스에서 제거
            
            return true;
        }

    }
}
