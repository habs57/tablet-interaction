using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TablectionSketch
{
    public static class SlideRepository
    {
        public static event Action<Slide> SlideSelectionChanged;

        private static Slide _currentSlide = null;
        public static Slide CurrentSlide 
        {
            get
            {
                return _currentSlide;
            }

            set
            {
                _currentSlide = value;

                if (SlideSelectionChanged != null)
                {
                    SlideSelectionChanged(_currentSlide);
                }
            }
        }
    }
}
