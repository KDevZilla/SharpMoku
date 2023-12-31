using SharpMoku.UI;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SharpMoku.UI
{
    /*
     Credit Code for rounding
     https://stackoverflow.com/questions/42627293/label-with-smooth-rounded-corners
     */

    public class ExtendLabel : Label
    {
        public GomokuCellAttribute CellAttribute = new GomokuCellAttribute();
        public SharpMoku.UI.ThemeSpace.Theme theme;
        [Browsable(true)]
        public Color _BackColor
        {
            get { return __BackColor; }
            set
            {
                __BackColor = value;
                this.Invalidate();
                // this.BackColor = value;
            }
        }
        private Color __BackColor;

      
        public ExtendLabel()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Transparent;

        }
        public void MakeRound()
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path = _getRoundRectangle(this.ClientRectangle);
            this.Region = new Region(path);
        }
     
       
        private IExtendLabelCustomPaint _CustomPaint = null;
        public IExtendLabelCustomPaint CustomPaint
        {
            get { return _CustomPaint; }
            set { _CustomPaint = value; }
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (CornerRadius > 0)
            {
                using (var graphicsPath = _getRoundRectangle(this.ClientRectangle))
                {

                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var brush = new SolidBrush(_BackColor))
                        e.Graphics.FillPath(brush, graphicsPath);

                    TextRenderer.DrawText(e.Graphics, Text, this.Font, this.ClientRectangle, this.ForeColor);
                }
            }
            if (_CustomPaint != null)
            {
                _CustomPaint.Paint(e.Graphics, this);
            }


        }

        public int CornerRadius { get; set; } = 7;


        public GraphicsPath _getRoundRectangle(Rectangle rectangle)
        {
            int diminisher = 1;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rectangle.X, rectangle.Y, CornerRadius, CornerRadius, 180, 90);
            path.AddArc(rectangle.X + rectangle.Width - CornerRadius - diminisher, rectangle.Y, CornerRadius, CornerRadius, 270, 90);
            path.AddArc(rectangle.X + rectangle.Width - CornerRadius - diminisher, rectangle.Y + rectangle.Height - CornerRadius - diminisher, CornerRadius, CornerRadius, 0, 90);
            path.AddArc(rectangle.X, rectangle.Y + rectangle.Height - CornerRadius - diminisher, CornerRadius, CornerRadius, 90, 90);
            path.CloseAllFigures();
            return path;
        }
    }
}
