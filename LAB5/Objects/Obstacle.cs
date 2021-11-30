using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace LAB5.Objects
{
    class Obstacle : BaseObject
    {
        public int Time;

        public Obstacle(float x, float y, float angle) : base(x, y, angle)
        {
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Red), -15, -15, 30, 30);
        }

        public override String GetName()
        {
            return "Препятствие";
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }
    }
}
