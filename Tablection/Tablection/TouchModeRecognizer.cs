using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace TablectionSketch
{
    /// <summary>
    /// 현재 멀티터치 입력인지 아닌지 확인     
    /// </summary>
    public class TouchModeRecognizer
    {
        private int _prevDevID = -1;

        public bool IsMultiTouch
        {
            get;
            private set;
        }

        public void Recognize(TouchEventArgs e)
        {
              //멀티터치
            if ((_prevDevID > 0) && (_prevDevID != e.TouchDevice.Id))
            {
                //펜을 캔버스에 대면 자동적으로 지우기모드
                this.IsMultiTouch = true;

                System.Diagnostics.Debug.WriteLine(string.Format("Multitouch"));
            }
            //싱글터치
            else if ((_prevDevID > 0) && (_prevDevID == e.TouchDevice.Id))
            {
                //펜을 캔버스에 대면 자동적으로 지우기모드
                this.IsMultiTouch = false;

                System.Diagnostics.Debug.WriteLine(string.Format("Singletouch"));
            }            
            
            this._prevDevID = e.TouchDevice.Id;
        }
    }
}
