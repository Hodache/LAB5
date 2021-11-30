using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace LAB5.Objects
{
    class Marker : BaseObject
    {
        public Marker(float x, float y, float angle, Color color) : base(x, y, angle, color)
        {
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color), -3, -3, 6, 6);
            g.DrawEllipse(new Pen(Color, 2), -6, -6, 12, 12);
            g.DrawEllipse(new Pen(Color, 2), -10, -10, 20, 20);
        }

        public override String GetName()
        {
            return "Маркер";
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-3, -3, 6, 6);
            return path;
        }
    }
}
