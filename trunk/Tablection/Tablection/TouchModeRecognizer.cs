using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows;

namespace TablectionSketch
{

    public class TouchModeChangedEventArgs : EventArgs
    {
        public bool IsMultitouch { get; set; }
        //public int CurrentTouchPoints { get; set; }
    }

    public enum TouchMode
    {
        None,           // No touch input
        Single,         // 1point touch
        Multi           // 2point touch
    }       

    /// <summary>
    /// 현재 멀티터치 입력인지 아닌지 확인     
    /// </summary>
    public class TouchModeRecognizer
    {
        private TouchMode _PrevTouchMode;
        public int PenDevID { get; set; }           // Pen touch device ID
        public Point PenPoint { private set; get; }

        public TouchModeRecognizer(InkCanvas canvas)
        {
            _PrevTouchMode = TouchMode.None;
            PenDevID = -1;
            //canvas.PreviewTouchDown += new EventHandler<TouchEventArgs>(canvas_PreviewTouchDown);
            //canvas.PreviewTouchMove += new EventHandler<TouchEventArgs>(canvas_PreviewTouchMove);
            //canvas.PreviewTouchUp += new EventHandler<TouchEventArgs>(canvas_PreviewTouchUp);
        }

        //void canvas_PreviewTouchUp(object sender, TouchEventArgs e)
        //{
        //    this.Recognize(e);
        //}

        //void canvas_PreviewTouchMove(object sender, TouchEventArgs e)
        //{
        //    this.Recognize(e);
        //}

        //void canvas_PreviewTouchDown(object sender, TouchEventArgs e)
        //{
        //    this.Recognize(e);
        //}

        //private int _prevDevID = -1;

        public event EventHandler<TouchModeChangedEventArgs> ModeChanged;

        public bool IsMultiTouch
        {
            get;
            private set;
        }

        public bool IsEnableCollect
        {
            get;
            set;
        }

        public void Recognize(TouchEventArgs e, TouchStates touchState, bool isPen)
        {
            Debug.WriteLine("TOUCH mode : " + touchState);

            switch (_PrevTouchMode) 
            {
                case TouchMode.None:                    
                    if (touchState == TouchStates.TD)
                    {
                        MoveToNext(TouchMode.Single);

                        if (isPen)
                        {
                            PenDevID = e.TouchDevice.Id;
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Unexpected TOUCH mode change from None: " + touchState);
                    }
                    break;

                case TouchMode.Single:                  
                    if (touchState == TouchStates.TU)
                    {
                        MoveToNext(TouchMode.None);
                        PenDevID = -1;
                    }
                    else if (touchState == TouchStates.TM)
                    {
                        MoveToNext(TouchMode.Single);
                    }
                    else if (touchState == TouchStates.TD)
                    {
                        MoveToNext(TouchMode.Multi);
                        // pen started to input
                        if (isPen && PenDevID == -1)
                        {
                            PenDevID = e.TouchDevice.Id;
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Unexpected TOUCH mode change from Single: " + touchState);
                    }
                    break;

                case TouchMode.Multi:                   
                    if (touchState == TouchStates.TU)
                    {
                        MoveToNext(TouchMode.Single);

                        // 펜 입력이 원래 없거나 중단된 경우
                        if (!isPen)
                        {
                            PenDevID = -1;
                        }
                    }
                    else if (touchState == TouchStates.TM)
                    {
                        MoveToNext(TouchMode.Multi);
                    }
                    else
                    {
                        Debug.WriteLine("Unexpected TOUCH mode change from Multi: " + touchState);
                    }
                    break;

                default:
                    break;
            } 

            if (this.IsEnableCollect)
            {
                ////멀티터치
                //if ((_prevDevID > 0) && (_prevDevID != e.TouchDevice.Id))
                //{
                //    //펜을 캔버스에 대면 자동적으로 지우기모드
                //    this.IsMultiTouch = true;                    
                //    System.Diagnostics.Debug.WriteLine(string.Format("Multitouch"));
                //}
                ////싱글터치
                //else if ((_prevDevID > 0) && (_prevDevID == e.TouchDevice.Id))
                //{
                //    //펜을 캔버스에 대면 자동적으로 지우기모드
                //    this.IsMultiTouch = false;                    
                //    System.Diagnostics.Debug.WriteLine(string.Format("Singletouch"));
                //}

                //this._prevDevID = e.TouchDevice.Id;

                if (ModeChanged != null)
                {
                    ModeChanged(this, new TouchModeChangedEventArgs() { IsMultitouch = this.IsMultiTouch });
                }
            }
        }

        private void MoveToNext(TouchMode state)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("현재터치모드 : {0}", state));

            if (_PrevTouchMode != state)
            {
                _PrevTouchMode = state;

                if (state == TouchMode.Multi)
                {
                    this.IsMultiTouch = true;
                    System.Diagnostics.Debug.WriteLine(string.Format("Multitouch"));
                }
                else
                {
                    this.IsMultiTouch = false;
                    System.Diagnostics.Debug.WriteLine(string.Format("Singletouch"));
                }
            }
        }
    }
}
