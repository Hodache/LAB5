using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace LAB5.Objects
{
    class MyPoint : BaseObject
    {
        public float Size;

        public MyPoint(float x, float y, float angle, Color color) : base(x, y, angle, color)
        {
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color), -15, -15, Size, Size);
        }

        public override String GetName()
        {
            return "Поинт";
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, Size, Size);
            return path;
        }
    }
}
