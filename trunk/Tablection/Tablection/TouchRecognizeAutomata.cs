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
using System.Diagnostics;
using System.Windows.Shapes;

namespace TablectionSketch
{
	public enum TouchStates
	{
		TD,     // Touch Down
		TM,     // Touch Move
		TU      // Touch Up
	}

	public class TouchRecognizeAutomata
	{
		//Cut 모드 
		// 1) 한손가락으로 터치 
		// 2) 사진을 터치하면 3)으로, 아니면 1)로 돌아감 
		// 3) 2번째 손가락이 들어온것을 인식  
		// 4) 1번째 손가락의 입력은 무시, 2번째 손가락의 입력을 이벤트로 전달
		// 5) 1번째 또는 두번째 손가락중 하나라도 떼면 입력 취소.


		public enum InputMode
		{
			None,           // 아무 입력 없음 
			Pen,            // 펜 입력 모드
			Erase,          // 지우기 모드
			SelMovImg,      // 이미지 선택 및 이동
			TransImg,       // 이미지 확대/축소/회전 
			Cut,            // 이미지 잘라내기 모드 
			Ruler,          // 자 보이기 및 컨트롤
			Line            // 직선 그리기 모드
		}

        // Declare the delegate (if using non-generic pattern).
        public delegate void EventHandler(object sender, EventArgs e);

        // Declare the event.
        public event EventHandler OnLineStarted;
        public event EventHandler OnLineMove;
        public event EventHandler OnLineEnded;

        // Wrap the event in a protected virtual method
        // to enable derived classes to raise the event.
        protected virtual void RaiseOnLineStarted()
        {
            // Raise the event by using the () operator.
            if (OnLineStarted != null)
                OnLineStarted(this, new EventArgs());
        }
        // Wrap the event in a protected virtual method
        // to enable derived classes to raise the event.
        protected virtual void RaiseOnLineMove()
        {
            // Raise the event by using the () operator.
            if (OnLineMove != null)
                OnLineMove(this, new EventArgs());
        }
        // Wrap the event in a protected virtual method
        // to enable derived classes to raise the event.
        protected virtual void RaiseOnLineEnded()
        {
            // Raise the event by using the () operator.
            if (OnLineEnded != null)
                OnLineEnded(this, new EventArgs());
        }


		private TouchModeRecognizer _modeRecognizer;
        public int PenDevID
        {
            get
            {
                if (_modeRecognizer != null)
                {
                    return _modeRecognizer.PenDevID;
                }
                else
                {
                    return 0;
                }
                
            }
        }

		private InputMode _PrevMode = InputMode.None;
     

		private int _TouchCount = 0;        
		public bool IsPen { get; set; }
		public bool IsRuler { get; set; }
		private bool _IsOverImage = false;
		private bool _IsOverLiner = false;
        /// <summary>
        /// Line모드일 때 펜 시작점
        /// </summary>
		public TouchPoint PenStartPoint { get; set; }
        /// <summary>
        /// Line모드일 때 펜 종료점
        /// <para><c>OnLineMove</c>일 때 계속적으로 갱신된다.</para>
        /// </summary>
		public TouchPoint PenEndPoint { get; set; }

		public event Action<InputMode> ModeChanged;

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
			_modeRecognizer.Recognize(e, TouchStates.TU, this.IsPen);
			_TouchCount = (_modeRecognizer.IsMultiTouch == true ? 2 : 1);
			this._IsOverImage = false;
			
			this.Run(e, TouchStates.TU);
		}

		void canvas_PreviewTouchMove(object sender, TouchEventArgs e)
		{
			_modeRecognizer.Recognize(e, TouchStates.TM, this.IsPen);
			_TouchCount = (_modeRecognizer.IsMultiTouch == true ? 2 : 1);
			this._IsOverImage = false;
			VisualTreeHelper.HitTest(_Canvas, new HitTestFilterCallback(FilterCallBack),
											new HitTestResultCallback(ResultCallBack),
											new PointHitTestParameters(e.GetTouchPoint(_Canvas).Position));
		   
			this.Run(e, TouchStates.TM);    
		}

		void canvas_PreviewTouchDown(object sender, TouchEventArgs e)
		{
			_modeRecognizer.Recognize(e, TouchStates.TD, this.IsPen);
			_TouchCount = (_modeRecognizer.IsMultiTouch == true ? 2 : 1);
			this._IsOverImage = false;
			VisualTreeHelper.HitTest(_Canvas, new HitTestFilterCallback(FilterCallBack), 
											  new HitTestResultCallback(ResultCallBack), 
											  new PointHitTestParameters(e.GetTouchPoint(_Canvas).Position));
		   
			this.Run(e, TouchStates.TD);
		}


		private HitTestFilterBehavior FilterCallBack(DependencyObject e)
		{
			if (e is Liner)
			{
				this._IsOverLiner = true;
			}
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
				case InputMode.None:
					if (touchState == TouchStates.TU)
					{
						this.MoveToNext(InputMode.None);
					}
					else if ((_TouchCount == 1) && this.IsPen && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.Pen);
					}
					else if ((_TouchCount == 1) && !this.IsPen && !_IsOverImage && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.Erase);
					}
					else if ((_TouchCount == 1) && !this.IsPen && _IsOverImage && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.SelMovImg);
					}
					else if ((_TouchCount == 2) && !this.IsPen && _IsOverImage && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.TransImg);   
					}
					else if ((_TouchCount == 2) && !this.IsPen && (!_IsOverImage || _IsOverLiner) && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.Ruler);
					}
					else if ((_TouchCount == 2) && this.IsPen && _IsOverLiner && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.Line);
					}
					else
					{
						Debug.WriteLine("Unexpected status change input");
					}
					break;

				case InputMode.Pen:
					if (touchState == TouchStates.TU)
					{
						this.MoveToNext(InputMode.None);
					}
					else if ((_TouchCount == 1) && this.IsPen && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.Pen);
					}
					else if ((_TouchCount == 1) && !this.IsPen && _IsOverImage && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.SelMovImg);
					}
					else
					{
						Debug.WriteLine("Unexpected status change input");
					}
					break;

				case InputMode.Erase:
					if (touchState == TouchStates.TU)
					{
						this.MoveToNext(InputMode.None);
					}
					else if ((_TouchCount == 1) && !this.IsPen && !_IsOverImage && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.Erase);
					}
					else if ((_TouchCount == 1) && !this.IsPen && _IsOverImage && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.SelMovImg);
					}
					else if ((_TouchCount == 2) && !this.IsPen && _IsOverImage && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.TransImg);
					}
					else if ((_TouchCount == 2) && !this.IsPen && (!_IsOverImage || _IsOverLiner) && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.Ruler);
					}
					else if ((_TouchCount == 2) && this.IsPen && _IsOverLiner && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.Line);
					}
					else
					{
						Debug.WriteLine("Unexpected status change input");
					}
					break;

				case InputMode.SelMovImg:
					if (touchState == TouchStates.TU)
					{
						this.MoveToNext(InputMode.None);
					}
					else if ((_TouchCount == 1) && this.IsPen && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.Pen);
					}
					else if ((_TouchCount == 1) && !this.IsPen && _IsOverImage && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.SelMovImg);
					}
					else if ((_TouchCount == 2) && !this.IsPen && _IsOverImage && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.TransImg);   
					}
					else if ((_TouchCount == 2) && this.IsPen && _IsOverImage && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.Cut);
					}
					else
					{
						Debug.WriteLine("Unexpected status change input");
					}
					break;

				case InputMode.TransImg:
					if (touchState == TouchStates.TU)
					{
						this.MoveToNext(InputMode.None);
					}
					else if ((_TouchCount == 1) && !this.IsPen && _IsOverImage && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.SelMovImg);
					}
					else if ((_TouchCount == 2) && !this.IsPen && _IsOverImage && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.TransImg);
					}
					else
					{
						Debug.WriteLine("Unexpected status change input");
					}
					break;

				case InputMode.Cut:
					if (touchState == TouchStates.TU)
					{
						this.MoveToNext(InputMode.None);
					}
					else if ((_TouchCount == 2) && this.IsPen && _IsOverImage && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.Cut);
					}
					else
					{
						Debug.WriteLine("Unexpected status change input");
					}
					break;
							
				case InputMode.Ruler:
					if (touchState == TouchStates.TU && !IsRuler)
					{
						this.MoveToNext(InputMode.None);
					}
					else if ((_TouchCount == 2) && !this.IsPen && (!_IsOverImage || _IsOverLiner) && touchState != TouchStates.TU)
					{
						this.MoveToNext(InputMode.Ruler);
					}
					else if (this.IsPen && touchState != TouchStates.TU)
					{
                        // 직선 그리기 시작
						this.MoveToNext(InputMode.Line);
						PenStartPoint = input.TouchDevice.GetTouchPoint(_Canvas);
						PenEndPoint = PenStartPoint;
                        RaiseOnLineStarted();
					}
					else
					{
						Debug.WriteLine("Unexpected status change input");
					}
					break;

				case InputMode.Line:
					if (touchState == TouchStates.TU)
					{
                        RaiseOnLineEnded();

						this.MoveToNext(InputMode.Ruler);
                        this.PenStartPoint = null;
                        this.PenEndPoint = null;    
					}
                    else if (this.IsPen && touchState != TouchStates.TU && input.TouchDevice.Id == _modeRecognizer.PenDevID)
					{
                        // 직선 움직이기
						this.MoveToNext(InputMode.Line);
                        PenEndPoint = input.TouchDevice.GetTouchPoint(_Canvas);
                        Debug.WriteLine("End:({0},{1})", PenEndPoint.Position.X, PenEndPoint.Position.Y);
                        RaiseOnLineMove();
					}
					else
					{
						Debug.WriteLine("Unexpected status change input");
					}
					break;
				default:
					break;
			}
		}

		private void MoveToNext(InputMode state)
		{
			System.Diagnostics.Debug.WriteLine(string.Format("현재모드 : {0}", state));

			if (_PrevMode != state)
			{
				_PrevMode = state;
				this.ModeChanged(state);                
			}
		}
	}
}
