using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace obrabotka.Objects
{

    class Krug : BaseObject
    {
        public int time = 0;
        public Action<Krug> onKrugOverlap;

        public Krug(float x, float y, float angle) : base(x, y, angle)
        {
            Random ran = new Random();
            time = 130 + ran.Next() % 70;
        }
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Aquamarine), -20, -20, 40, 40);

            g.DrawString($"{time}", new Font("Verdana", 8), new SolidBrush(Color.Red), 10, 10);

        }

        public override GraphicsPath GetGraphicsPath()
        {

            var path = base.GetGraphicsPath();
            path.AddEllipse(-10, -10, 20, 20);
            return path;

        }
        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            if (obj is Krug)
            {
                onKrugOverlap(obj as Krug);
            }
        }
    }
}
