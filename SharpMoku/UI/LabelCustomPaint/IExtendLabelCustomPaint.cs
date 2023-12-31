using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpMoku.UI
{
    public interface IExtendLabelCustomPaint
    {
        void Paint(Graphics g, ExtendLabel pLabel);
    }
}
