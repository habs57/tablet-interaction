using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TablectionSketch.Tool
{
    public class ToolHeader : ToolBase
    {
        private string _relatedControlName = string.Empty;
        public string RelatedControlName
        {
            get { return _relatedControlName; }
            set 
            { 
                _relatedControlName = value;
                RaisePropertyChanged("RelatedControlName");
            }
        }
        
    }
}
