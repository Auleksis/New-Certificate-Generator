using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WpfApp1
{
    [Serializable]
    public class Area
    {
        [NonSerialized] public static Pen pen = new Pen(Color.CadetBlue, 3.0f);
        [NonSerialized] public static Pen selected = new Pen(Color.Red, 3.0f);
        public SolidBrush textBrush = new SolidBrush(Color.Black);

        public Font font;

        public Rectangle rectangle;
        public string text;
        public string name;

        [NonSerialized] public bool isSelected;

        public Area(int x, int y, int width, int height, string name)
        {
            rectangle = new Rectangle(x, y, width, height);            
            text = "Двойной клик для редактирования";
            this.name = name;
            isSelected = false;
        }

        public Area()
        {
            rectangle = new Rectangle();
            text = "";
            this.name = "";
            isSelected = false;
        }

        public void Draw(Graphics g)
        {
            if (isSelected)
                g.DrawRectangle(selected, rectangle);              
            else
                g.DrawRectangle(pen, rectangle);
            g.DrawString(text, font, textBrush, rectangle);
        }     
    }
}
