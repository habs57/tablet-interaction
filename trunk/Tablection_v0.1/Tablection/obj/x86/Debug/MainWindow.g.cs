﻿#pragma checksum "..\..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3DB35A4E539563DBCF9AE12EACE9EA1B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DrWPF.Windows.Controls;
using GongSolutions.Wpf.DragDrop;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TablectionSketch;
using TablectionSketch.Controls;
using TablectionSketch.Converter;
using TablectionSketch.Data;
using TablectionSketch.Slide;
using TablectionSketch.Tool;


namespace TablectionSketch {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 94 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grdMain;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnTop;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBottom;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TablectionSketch.Controls.MultiTouchInkCanvas DrawingCanvas;
        
        #line default
        #line hidden
        
        
        #line 194 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DrWPF.Windows.Controls.LoopingListBox SlideList;
        
        #line default
        #line hidden
        
        
        #line 221 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Animation.BeginStoryboard SlideListHideAnimation;
        
        #line default
        #line hidden
        
        
        #line 228 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Animation.BeginStoryboard SlideListHideAnimation2;
        
        #line default
        #line hidden
        
        
        #line 238 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ToolPanel;
        
        #line default
        #line hidden
        
        
        #line 258 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Animation.BeginStoryboard ToolPanelHideAnimation;
        
        #line default
        #line hidden
        
        
        #line 265 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Animation.BeginStoryboard ToolPanelHideAnimation2;
        
        #line default
        #line hidden
        
        
        #line 269 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TablectionSketch.Controls.SlideListBox llbToolHeaders;
        
        #line default
        #line hidden
        
        
        #line 281 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton radioTools;
        
        #line default
        #line hidden
        
        
        #line 284 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TablectionSketch.Controls.SlideListBox llbTools;
        
        #line default
        #line hidden
        
        
        #line 293 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton radioColors;
        
        #line default
        #line hidden
        
        
        #line 296 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TablectionSketch.Controls.SlideListBox llbColors;
        
        #line default
        #line hidden
        
        
        #line 305 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton radioStrokes;
        
        #line default
        #line hidden
        
        
        #line 308 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TablectionSketch.Controls.SlideListBox llbStroke;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TablectionSketch;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 74 "..\..\..\MainWindow.xaml"
            ((System.Windows.Media.Animation.Storyboard)(target)).Completed += new System.EventHandler(this.ToolPanelHideAnimaton_Completed);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 78 "..\..\..\MainWindow.xaml"
            ((System.Windows.Media.Animation.Storyboard)(target)).Completed += new System.EventHandler(this.SlideListHideAnimation_Completed);
            
            #line default
            #line hidden
            return;
            case 3:
            this.grdMain = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.btnTop = ((System.Windows.Controls.Button)(target));
            
            #line 105 "..\..\..\MainWindow.xaml"
            this.btnTop.ManipulationStarted += new System.EventHandler<System.Windows.Input.ManipulationStartedEventArgs>(this.btnTop_ManipulationStarted);
            
            #line default
            #line hidden
            
            #line 106 "..\..\..\MainWindow.xaml"
            this.btnTop.ManipulationDelta += new System.EventHandler<System.Windows.Input.ManipulationDeltaEventArgs>(this.btnTop_ManipulationDelta);
            
            #line default
            #line hidden
            
            #line 107 "..\..\..\MainWindow.xaml"
            this.btnTop.Click += new System.Windows.RoutedEventHandler(this.btnTop_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnBottom = ((System.Windows.Controls.Button)(target));
            
            #line 128 "..\..\..\MainWindow.xaml"
            this.btnBottom.ManipulationStarted += new System.EventHandler<System.Windows.Input.ManipulationStartedEventArgs>(this.btnBottom_ManipulationStarted);
            
            #line default
            #line hidden
            
            #line 129 "..\..\..\MainWindow.xaml"
            this.btnBottom.ManipulationDelta += new System.EventHandler<System.Windows.Input.ManipulationDeltaEventArgs>(this.btnBottom_ManipulationDelta);
            
            #line default
            #line hidden
            
            #line 131 "..\..\..\MainWindow.xaml"
            this.btnBottom.Click += new System.Windows.RoutedEventHandler(this.btnBottom_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.DrawingCanvas = ((TablectionSketch.Controls.MultiTouchInkCanvas)(target));
            return;
            case 7:
            this.SlideList = ((DrWPF.Windows.Controls.LoopingListBox)(target));
            return;
            case 8:
            this.SlideListHideAnimation = ((System.Windows.Media.Animation.BeginStoryboard)(target));
            return;
            case 9:
            this.SlideListHideAnimation2 = ((System.Windows.Media.Animation.BeginStoryboard)(target));
            return;
            case 10:
            this.ToolPanel = ((System.Windows.Controls.StackPanel)(target));
            
            #line 244 "..\..\..\MainWindow.xaml"
            this.ToolPanel.TouchEnter += new System.EventHandler<System.Windows.Input.TouchEventArgs>(this.ToolPanel_TouchEnter);
            
            #line default
            #line hidden
            
            #line 245 "..\..\..\MainWindow.xaml"
            this.ToolPanel.TouchLeave += new System.EventHandler<System.Windows.Input.TouchEventArgs>(this.ToolPanel_TouchLeave);
            
            #line default
            #line hidden
            
            #line 246 "..\..\..\MainWindow.xaml"
            this.ToolPanel.MouseEnter += new System.Windows.Input.MouseEventHandler(this.ToolPanel_MouseEnter);
            
            #line default
            #line hidden
            
            #line 247 "..\..\..\MainWindow.xaml"
            this.ToolPanel.MouseLeave += new System.Windows.Input.MouseEventHandler(this.ToolPanel_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ToolPanelHideAnimation = ((System.Windows.Media.Animation.BeginStoryboard)(target));
            return;
            case 12:
            this.ToolPanelHideAnimation2 = ((System.Windows.Media.Animation.BeginStoryboard)(target));
            return;
            case 13:
            this.llbToolHeaders = ((TablectionSketch.Controls.SlideListBox)(target));
            return;
            case 14:
            this.radioTools = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 15:
            this.llbTools = ((TablectionSketch.Controls.SlideListBox)(target));
            return;
            case 16:
            this.radioColors = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 17:
            this.llbColors = ((TablectionSketch.Controls.SlideListBox)(target));
            return;
            case 18:
            this.radioStrokes = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 19:
            this.llbStroke = ((TablectionSketch.Controls.SlideListBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
