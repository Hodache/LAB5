using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace LAB5.Objects
{
    class Darkness : BaseObject
    {
        public Action<Player> OnPlayerOverlap;
        public Action<Marker> OnMarkerOverlap;
        public Action<MyPoint> OnPointOverlap;
        public Action<Obstacle> OnObstacleOverlap;

        public Action<Player> OnPlayerRelease;
        public Action<Marker> OnMarkerRelease;
        public Action<MyPoint> OnPointRelease;
        public Action<Obstacle> OnObstacleRelease;

        public Darkness(float x, float y, float angle, Color color) : base(x, y, angle, color)
        {
        }

        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, 150, 450);
        }

        public override String GetName()
        {
            return "Мрак";
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(0, 0, 150, 450);
            return path;
        }

        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);

            if (obj is Player)
            {
                OnPlayerOverlap(obj as Player);
            }

            if (obj is Marker)
            {
                OnMarkerOverlap(obj as Marker);
            }

            if (obj is MyPoint)
            {
                OnPointOverlap(obj as MyPoint);
            }

            if (obj is Obstacle)
            {
                OnObstacleOverlap(obj as Obstacle);
            }
        }

        public bool Releases(BaseObject obj, Graphics g)
        {
            return !base.Overlaps(obj, g);
        }

        public void Release(BaseObject obj)
        {
            if (obj is Player)
            {
                OnPlayerRelease(obj as Player);
            }

            if (obj is Marker)
            {
                OnMarkerRelease(obj as Marker);
            }

            if (obj is MyPoint)
            {
                OnPointRelease(obj as MyPoint);
            }

            if (obj is Obstacle)
            {
                OnObstacleRelease(obj as Obstacle);
            }
        }
    }
}
