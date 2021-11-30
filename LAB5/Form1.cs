using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LAB5.Objects;

namespace LAB5
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new List<BaseObject>();
        Player player;
        Marker marker;
        List<MyPoint> points = new List<MyPoint>();
        List<Obstacle> obstacles = new List<Obstacle>();
        int score = 0;

        public Form1()
        {
            InitializeComponent();

            // Создание зеленых точек
            for (int i = 0; i < 8; i++)
            {
                points.Add(new MyPoint(0, 0, 0));
                GeneratePoint(points[i]);

                objects.Add(points[i]);
            }

            // Создание препятствий
            for (int i = 0; i < 6; i++)
            {
                obstacles.Add(new Obstacle(0, 0, 0));
                GenerateObstacle(obstacles[i]);

                objects.Add(obstacles[i]);
            }

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            objects.Add(player);

            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересек {obj.GetName()}\n" + txtLog.Text;
            };

            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(marker);
                marker = null;
            };

            player.OnPointOverlap += (p) =>
            {
                GeneratePoint(p);
                score++;
            };

            player.OnObstacleOverlap += (o) =>
            {
                GenerateObstacle(o);
                score--;
            };
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            UpdatePlayer();

            // Уменьшаем зеленые круги
            foreach(var pt in points.ToList())
            {
                pt.Size -= 0.4f;
                pt.X += 0.2f;
                pt.Y += 0.2f;
                if (pt.Size <= 0)
                {
                    GeneratePoint(pt);
                }
            }

            // Уменьшаем время существования препятствий
            foreach (var obst in obstacles.ToList())
            {
                obst.Time -= 10;
                if (obst.Time <= 0)
                {
                    GenerateObstacle(obst);
                }
            }

            // Проверка пересечений
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }

            txtScore.Text = $"Очки: {score}";

            // Отрисовка объектов
            foreach (var obj in objects.ToList())
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }

        }

        private void UpdatePlayer()
        {
            // Расчитываем вектор между игроком и маркером
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;

                // Находим длину вектора ускорения и нормализуем координаты
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                // Угол поворота
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;


            }
            // Тормозящий момент
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            // Пересчитываем координаты игрока
            player.X += player.vX;
            player.Y += player.vY;
        }

        private void GeneratePoint(MyPoint p)
        {
            Random rnd = new Random();

            p.X = (float)rnd.NextDouble() * pbMain.Width;
            p.Y = (float)rnd.NextDouble() * pbMain.Width;

            p.Size = (float)rnd.NextDouble() * 30 + 30;
        }

        private void GenerateObstacle(Obstacle o)
        {
            Random rnd = new Random();

            o.X = (float)rnd.NextDouble() * pbMain.Width;
            o.Y = (float)rnd.NextDouble() * pbMain.Width;

            o.Time = rnd.Next(500, 1000);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Запрашиваем обновление pbMain
            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}
