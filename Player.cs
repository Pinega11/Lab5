using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;


namespace obrabotka.Objects
{
     class Player : BaseObject
    {
        public float vX, vY;
        public Player(float x,float y, float angle) : base(x,y,angle)
        {
        }
        public Action<Marker> onMarkerOverlap;
        public Action<Krug> onKrugOverlap;
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.DeepSkyBlue), -20, -20, 40, 40);
            g.DrawEllipse(new Pen(Color.Black,4), -20, -20, 40, 40);
            g.DrawLine(new Pen(Color.Black, 4), 0, 0, 30, 0);
        }
        public override GraphicsPath GetGraphicsPath()
        {

          var path = base.GetGraphicsPath();
            path.AddEllipse(-10, -10, 20, 20);
            return path;

        }
        public override void  Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            if(obj is Marker)
            {
                onMarkerOverlap(obj as Marker);
            }
            else if(obj is Krug krug)
            {
                onKrugOverlap.Invoke(krug);
            }
        }

    }
}
