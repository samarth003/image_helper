﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace image_helper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();               
        }
        public string File;
        Bitmap bmp;
        string userWidth;
        string userlength;
        PictureBox picBox;
        public void ShowImage()
        {
            picBox = new PictureBox();
            picBox.Width = Convert.ToInt32(winFormHost.Width);
            picBox.Height = Convert.ToInt32(winFormHost.Height);
            winFormHost.Child = picBox;
            picBox.Paint += new System.Windows.Forms.PaintEventHandler(paintImage);              
        }

        void paintImage(object sender, System.Windows.Forms.PaintEventArgs e)
        {          
            bmp = new Bitmap(File);
            System.Drawing.Point ulPoint = new System.Drawing.Point(0, 0);
            e.Graphics.DrawImage(bmp, ulPoint);
        }
        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start(@"c:/");
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File = ofd.FileName;
                if (File.EndsWith(".png") || File.EndsWith(".jpeg") || File.EndsWith(".jpg"))
                {
                    //display
                    ShowImage(); 
                }
                else
                {
                    //do nothing
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //save the image to the local folder
            SaveFileDialog save = new SaveFileDialog();
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bmp.Save(save.FileName, System.Drawing.Imaging.ImageFormat.Jpeg); 
            }
        }

        private void userInWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            userWidth = userInWidth.Text;
        }

        private void userInLen_TextChanged(object sender, TextChangedEventArgs e)
        {
            userlength = userInLen.Text;
        }

        private void GenBox_Click(object sender, RoutedEventArgs e)
        {
            //create a box on the image
            int xWidth = Int32.Parse(userWidth);
            int yLen = Int32.Parse(userlength);
            Graphics g = picBox.CreateGraphics();
            g.DrawRectangle(Pens.Red, 0, 0, xWidth, yLen);
        }
    }
}
