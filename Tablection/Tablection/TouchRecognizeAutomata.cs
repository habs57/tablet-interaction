using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;

using TablectionSketch.Tool;
using TablectionSketch.Controls;

namespace TablectionSketch
{
    public class TouchRecognizeAutomata
    {
        //Cut 모드 
        // 1) 한손가락으로 터치 
        // 2) 사진을 터치하면 3)으로, 아니면 1)로 돌아감 
        // 3) 2번째 손가락이 들어온것을 인식  
        // 4) 1번째 손가락의 입력은 무시, 2번째 손가락의 입력을 이벤트로 전달
        // 5) 1번째 또는 두번째 손가락중 하나라도 떼면 입력 취소.

        public enum TouchStates
        {
            None,
            TD,
            TM,
            TU
        }

        public enum Mode
        {
            None,           // 아무 입력도 없음 
            Pen,            // 펜입력 모드
            Manipulation,   // 이미지 확대/축소/이동 모드 
            Cut             // 이미지 잘라내기 모드 
        }

        private TouchModeRecognizer _modeRecognizer;

        private Mode _PrevMode = Mode.None;        

        private int _TouchCount = 0;
        private bool _IsPressed = false;        //펜 들어오면 이용
        private bool _IsOverImage = false;

        public event Action<Mode> ModeChanged;

        private InkCanvas _Canvas;
        
        public TouchRecognizeAutomata(InkCanvas canvas)
        {
            _Canvas = canvas;

            _modeRecognizer = new TouchModeRecognizer(canvas);
            _modeRecognizer.IsEnableCollect = true;

            canvas.PreviewTouchDown += new EventHandler<TouchEventArgs>(canvas_PreviewTouchDown);
            canvas.PreviewTouchMove += new EventHandler<TouchEventArgs>(canvas_PreviewTouchMove);
            canvas.PreviewTouchUp += new EventHandler<TouchEventArgs>(canvas_PreviewTouchUp);
        }

        void canvas_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            _TouchCount = (_modeRecognizer.IsMultiTouch == true ? 2 : 1);
            this.Run(e, TouchStates.TU);
        }

        void canvas_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            _TouchCount = (_modeRecognizer.IsMultiTouch == true ? 2 : 1);
            this.Run(e, TouchStates.TM);    
        }

        void canvas_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            _TouchCount = (_modeRecognizer.IsMultiTouch == true ? 2 : 1);

            this._IsOverImage = false;
            VisualTreeHelper.HitTest(_Canvas, new HitTestFilterCallback(FilterCallBack), 
                                              new HitTestResultCallback(ResultCallBack), 
                                              new PointHitTestParameters(e.GetTouchPoint(_Canvas).Position));                

            this.Run(e, TouchStates.TD);
        }


        private HitTestFilterBehavior FilterCallBack(DependencyObject e)
        {
            if (e is TouchableImage)
            {
                //(e as TouchableImage).Focus();
                this._IsOverImage = true;
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


        public void Run(TouchEventArgs input, TouchStates touchState)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("이전모드 : {0}, 터치포인트수 : {1}, 이미지 위에 있는가? : {2}", _PrevMode, _TouchCount, _IsOverImage));

            switch (_PrevMode)
            {
                case Mode.None:
                    if (touchState == TouchStates.TU)
                    {
                        this.MoveToNext(Mode.None);
                        return;
                    }
                    if ((_TouchCount == 1) && (touchState == TouchStates.TD))
                    {
                        if (_IsOverImage == true)
                        {
                            this.MoveToNext(Mode.Cut);
                            return;
                        }
                        else
                        {
                            this.MoveToNext(Mode.Pen);
                            return;
                        }                        
                    }
                    if ((_TouchCount > 1) && (touchState == TouchStates.TD))
                    {
                        if (_IsOverImage == true)
                        {
                            this.MoveToNext(Mode.Manipulation);
                            return;
                        }                             
                    }                    
                    break;
                case Mode.Pen:
                    if (touchState == TouchStates.TU)
                    {
                        this.MoveToNext(Mode.None);
                        return;
                    }
                    if (touchState == TouchStates.TM)
                    {
                        this.MoveToNext(Mode.Pen);
                        return;
                    }
                    if ((_TouchCount > 1) && (touchState == TouchStates.TD))
                    {
                        if (_IsOverImage == true)
                        {
                            this.MoveToNext(Mode.Manipulation);
                            return;
                        }                    
                    }
                    if ((_TouchCount == 1) && (touchState == TouchStates.TD))
                    {
                        if (_IsOverImage == true)
                        {
                            this.MoveToNext(Mode.Cut);
                            return;
                        }                        
                    }
                    break;
                case Mode.Manipulation:
                    if (touchState == TouchStates.TU)
                    {
                        this.MoveToNext(Mode.None);
                        return;
                    }
                    if (touchState == TouchStates.TM)
                    {
                        this.MoveToNext(Mode.Manipulation);
                        return;
                    }
                    if ((_TouchCount == 1) && (touchState == TouchStates.TD))
                    {
                        if (_IsOverImage == true)
                        {
                            this.MoveToNext(Mode.Cut);
                            return;
                        }
                        else
                        {
                            this.MoveToNext(Mode.Pen);
                            return;
                        }
                    }
                    break;
                case Mode.Cut:
                    if (touchState == TouchStates.TU)
                    {
                        this.MoveToNext(Mode.None);
                        return;
                    }
                    if ((_TouchCount == 1) && (touchState == TouchStates.TD))
                    {
                        this.MoveToNext(Mode.Pen);
                        return;
                    }
                    if ((_TouchCount > 1) && (touchState == TouchStates.TD))
                    {
                        if (_IsOverImage == true)
                        {
                            this.MoveToNext(Mode.Manipulation);
                            return;
                        }
                    }
                    if ((touchState == TouchStates.TM) || (touchState == TouchStates.TD))
                    {
                        this.MoveToNext(Mode.Cut);
                        return;
                    }
                    break;
                default:
                    break;
            }
        }

        private void MoveToNext(Mode state)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("현재모드 : {0}", state));

            _PrevMode = state;
            this.ModeChanged(state);
        }

    }
}
