using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace LAB5.Objects
{
    class BaseObject
    {   
        public float X;
        public float Y;
        public float Angle;
        public Color Color;

        public Action<BaseObject, BaseObject> OnOverlap;

        public BaseObject(float x, float y, float angle, Color color)
        {
            X = x;
            Y = y;
            Angle = angle;
            Color = color;
        }

        public Matrix GetTransform()
        {
            var matrix = new Matrix();
            matrix.Translate(X, Y);
            matrix.Rotate(Angle);

            return matrix;
        }

        public virtual void Render(Graphics g)
        {
        }

        public virtual String GetName()
        {
            return "Объект";
        }

        public virtual GraphicsPath GetGraphicsPath()
        {
            return new GraphicsPath();
        }

        public virtual bool Overlaps(BaseObject obj, Graphics g)
        {
            // Берем информацию о формах объектов
            var path1 = this.GetGraphicsPath();
            var path2 = obj.GetGraphicsPath();

            // Применяем к объектам матрицы трансформации
            path1.Transform(this.GetTransform());
            path2.Transform(obj.GetTransform());

            // Определяем пересечение
            var region = new Region(path1);
            region.Intersect(path2);
            return !region.IsEmpty(g);
        }

        public virtual void Overlap(BaseObject obj)
        {
            if (this.OnOverlap != null)
            {
                this.OnOverlap(this, obj);
            }
        }
    }
}
