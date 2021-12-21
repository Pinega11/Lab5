using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using obrabotka.Objects;


namespace obrabotka
{
    public partial class Form1 : Form
    {
       
        List<BaseObject> objects = new List<BaseObject>();
        Player player;
        Marker marker;
        Krug krug;
        Krug krug1;
        int ochko = 0;
        Random r = new Random();
        public Form1()
        {
            InitializeComponent();
            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            // добавляю реакцию на пересечение
            player.onOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };
            player.onMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
            player.onKrugOverlap += (m) =>
            {
                GenerateCircle(m);
              
                ochko++;
                Score.Text = $"Счёт: "+ochko;
            };
            marker  = new Marker(pbMain.Width / 2+50, pbMain.Height / 2+50, 0);
          
            krug = new Krug(0, 0,0);
            GenerateCircle(krug);
            krug1 = new Krug(0, 0, 0);
            GenerateCircle(krug1);
            objects.Add(krug);
            objects.Add(krug1);
            objects.Add(marker);
            objects.Add(player);

        }

        private void GenerateCircle(Krug сircle)
        {
            Random random = new Random();
            сircle.X = random.Next() % 780 + 40;
            сircle.Y = random.Next() % 380 + 40;

        }
        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            peredvig();
            // пересчитываем пересечения
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
                if (obj != krug && krug.Overlaps(obj, g))
                {
                    krug.Overlap(obj);
                    obj.Overlap(krug);
                    
                }
            }

            // рендерим объекты
            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }

        }
        private void peredvig()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;

                float lenght = MathF.Sqrt(dx * dx + dy * dy);
                dx /= lenght;
                dy /= lenght;

                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;

            }
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            // пересчет позиция игрока с помощью вектора скорости
            player.X += player.vX;
            player.Y += player.vY;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var obj in objects.ToList())
            {
                if (obj is Krug krug)
                {
                    krug.time--;

                    if (krug.time <= 0)
                    {
                        GenerateCircle(krug);
                        krug.time = 130 + r.Next() % 70;
                    }
                }

            }
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

        private void pbMain_Click(object sender, EventArgs e)
        {

        }
    }
}
