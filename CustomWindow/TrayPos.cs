using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyCustomWindowNamespace
{
    class TrayPos
    {
        public tpos pos;
        int pw, ph, waw, wah;
        public TrayPos()
        {
            pw = (int)SystemParameters.PrimaryScreenWidth;
            ph = (int)SystemParameters.PrimaryScreenHeight;
            waw = (int)SystemParameters.WorkArea.Width;
            wah = (int)SystemParameters.WorkArea.Height;

            if (SystemParameters.WorkArea.Top > 0)
            {
                pos = tpos.top;
            }
            else if (SystemParameters.WorkArea.Left > 0)
            {
                pos = tpos.left;
            }
            else if (pw > waw)
            {
                pos = tpos.right;
            }
            else pos = tpos.bottom;
        }

        private int top;
        private int left;
        private DependencyProperty prop;
        private int end = 0;

        public void getXY(int width, int height, out int _top, out int _left, out DependencyProperty _prop, out int _end)
        {
            switch (pos)
            {
                case tpos.top:
                    left = pw - width;
                    top = (int)SystemParameters.WorkArea.Top - height;
                    prop = Window.TopProperty;
                    end = (int)SystemParameters.WorkArea.Top;
                    break;
                case tpos.left:
                    left = (int)SystemParameters.WorkArea.Left - width;
                    top = ph - height;
                    prop = Window.LeftProperty;
                    end = (int)SystemParameters.WorkArea.Left;
                    break;
                case tpos.right:
                    left = waw;
                    top = ph - height;
                    prop = Window.LeftProperty;
                    end = waw - width;
                    break;
                case tpos.bottom:
                default:
                    left = pw - width;
                    top = wah;
                    prop = Window.TopProperty;
                    end = wah - height;
                    break;
            }

            _left = left;
            _top = top;
            _prop = prop;
            _end = end;
        }
    }

    enum tpos
    {
        top,
        left,
        bottom,
        right
    }
}
