using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Editor.xaml
    /// </summary>
    public partial class Editor : Window
    {
        public ShowTitles ShowTitles { get; set; }

        string imageFile;

        public static Graphics g;

        PictureBox pictureBox1;


        int focusedAreaIndex;
        List<Area> areas;
        string areaName;

        System.Drawing.Pen pen;
        System.Drawing.Pen selected;
        System.Drawing.Brush textBrush;

        bool isFocused;
        bool isResized;
        bool isEditingText;
        int deltaX, deltaY;

        Font font;

        public Editor(String imageFile)
        {
            Configuration oConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            oConfig.AppSettings.Settings.Add(new KeyValueConfigurationElement("EnableMultiMonitorDisplayClipping", "false"));
            oConfig.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");
            InitializeComponent();
            ShowTitles = new ShowTitles();

            this.imageFile = imageFile;

            AreasList.ItemsSource = ShowTitles;

            fonts.ItemsSource = Fonts.SystemFontFamilies;
            fonts.SelectedIndex = 0;
            colours.SelectedIndex = 7;
            colours.ItemsSource = typeof(Colors).GetProperties();

            font = new Font(fonts.Text, 10, System.Drawing.FontStyle.Bold);

            areas = new List<Area>();
            areas.Add(new Area(100, 100, 400, 200, areaName + Convert.ToString(areas.Count + 1)));
            areas[0].font = new Font(fonts.Text, 10, System.Drawing.FontStyle.Bold); 
            focusedAreaIndex = 0;
            ShowTitles.Add(areas[0].name);

            pen = new System.Drawing.Pen(System.Drawing.Color.CadetBlue, 3.0f);
            selected = new System.Drawing.Pen(System.Drawing.Color.Red, 3.0f);
            textBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

            isFocused = false;
            isResized = false;
            isEditingText = false;
        }

        private void fonts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (areas != null && areas.Count != 0 && pictureBox1 != null) {
                areas[focusedAreaIndex].font = new Font(fonts.Text, int.Parse(fontSize.Text), System.Drawing.FontStyle.Bold);
                pictureBox1.Invalidate();
            }
        }

        private void colours_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(areas != null && areas.Count > 0 && pictureBox1 != null) {
                areas[focusedAreaIndex].textBrush = new SolidBrush(System.Drawing.Color.FromName(colours.Text.Split(" ")[1]));
                pictureBox1.Invalidate();
            }
        }

        private void rightAligment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void centreAligment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void leftAligment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveProject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveArea_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddArea_Click(object sender, RoutedEventArgs e)
        {
            areas.Add(new Area(100, 100, 400, 200, areaName + Convert.ToString(areas.Count + 1)));
            areas[areas.Count-1].font = new Font(fonts.Text, int.Parse(fontSize.Text), System.Drawing.FontStyle.Bold);
            ShowTitles.Add(areas[areas.Count-1].name);
            pictureBox1.Invalidate();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            pictureBox1 = new PictureBox();
            //pictureBox1.Location = new System.Drawing.Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;

            pictureBox1.Image = System.Drawing.Image.FromFile(imageFile);
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);

            System.Windows.Forms.Panel panel1 = new System.Windows.Forms.Panel();
            panel1.AutoScroll = true;
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new System.Drawing.Point(12, 27);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(854, 522);
            panel1.TabIndex = 3;

           
            host.Child = panel1;
            viewer.Children.Add(host);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            foreach (Area a in areas) {
                a.Draw(g);
            }
        }

        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                //textEditor.Enabled = false;
                //nameEditor.Enabled = false;
                //areasList.SelectedIndex = -1;
                foreach (Area a in areas) {
                    a.isSelected = false;
                }
                for (int i = 0; i < areas.Count; i++) {
                    if (areas[i].rectangle.Contains(e.Location)) {
                        focusedAreaIndex = i;
                        areas[i].isSelected = true;
                        AreasList.SelectedIndex = i;

                        deltaX = e.X - areas[i].rectangle.X;
                        deltaY = e.Y - areas[i].rectangle.Y;

                        isFocused = true;


                        //pictureBox1.Invalidate();
                        break;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right) {
                foreach (Area a in areas) {
                    a.isSelected = false;
                }
                for (int i = 0; i < areas.Count; i++) {
                    if (areas[i].rectangle.Contains(e.Location)) {
                        focusedAreaIndex = i;
                        areas[i].isSelected = true;
                        isResized = true;
                        break;
                    }
                }
            }            
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            isFocused = false;
            isResized = false;
            //areas[focusedAreaIndex].isMoved = false;
            //pictureBox1.Invalidate();
        }

        private void fontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            int n1 = 0;
            if (fontSize.Text.Equals("") || fontSize.Equals("0") || !int.TryParse(fontSize.Text, out n1)) {
                fontSize.Text = "10";
            }
            else if (areas != null && areas.Count != 0 && pictureBox1 != null) {
                areas[focusedAreaIndex].font = new Font(fonts.Text, int.Parse(fontSize.Text), System.Drawing.FontStyle.Bold);
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isFocused) {
                //areas[focusedAreaIndex].isMoved = true;
                System.Drawing.Rectangle r = areas[focusedAreaIndex].rectangle;
                r.X = e.X - deltaX;
                r.Y = e.Y - deltaY;
                areas[focusedAreaIndex].rectangle = r;
                pictureBox1.Invalidate();
            }
            else if (isResized) {
                System.Drawing.Rectangle r = areas[focusedAreaIndex].rectangle;
                r.Width = e.X - r.X;
                r.Height = e.Y - r.Y;
                areas[focusedAreaIndex].rectangle = r;
                pictureBox1.Invalidate();
            }
        }
    }
}
