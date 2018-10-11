using System;
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
using System.Drawing.Drawing2D;

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
        Boolean bShowImage;
        string userWidth;
        string userlength;
        string xPosition;
        string yPosition;
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
            if(bShowImage == true)
            {
                bmp = new Bitmap(File);
                picBox.Image = bmp;
                System.Drawing.Point ulPoint = new System.Drawing.Point(0, 0);
                e.Graphics.DrawImage(bmp, ulPoint);
                bShowImage = false;
            }
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
                    bShowImage = true;
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
                //bmp.Save(save.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                picBox.Image.Save(save.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void userInWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            int i;
            int wUserFeed;
            int wMax = 272;
            int wMin = 0;
            userWidth = userInWidth.Text;
            if (!string.IsNullOrEmpty(userInWidth.Text))
            {
                if(Int32.TryParse(userInWidth.Text, out i))
                {
                    wUserFeed = Int32.Parse(userWidth);
                    if(wUserFeed <= wMax && wUserFeed > wMin)
                        userWidth = userInWidth.Text;
                    else
                    {
                        System.Windows.MessageBox.Show("Out of bounds error!");
                        userInWidth.Text = userWidth;
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Enter numbers only!");
                    userInWidth.Text = userWidth;
                }
            }
            else
                userWidth = " ";
        }

        private void userInLen_TextChanged(object sender, TextChangedEventArgs e)
        {
            //userlength = userInLen.Text;
            int i;
            int lUserFeed;
            int lMax = 480;
            int lMin = 0;
            userlength = userInLen.Text;
            if (!string.IsNullOrEmpty(userInLen.Text))
            {
                if (Int32.TryParse(userInLen.Text, out i))
                {
                    lUserFeed = Int32.Parse(userlength);
                    if (lUserFeed <= lMax && lUserFeed > lMin)
                        userlength = userInLen.Text;
                    else
                    {
                        System.Windows.MessageBox.Show("Out of bounds error!");
                        userInLen.Text = userlength;
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Enter numbers only!");
                    userInLen.Text = userlength;
                }
            }
            else
                userlength = " ";
        }

        private void GenBox_Click(object sender, RoutedEventArgs e)
        {
            //create a box on the image
            int xWidth = Int32.Parse(userWidth);
            int yLen = Int32.Parse(userlength);
            int xPosCoord = Int32.Parse(xPosition);
            int yPosCoord = Int32.Parse(yPosition);

            using (var g = Graphics.FromImage(bmp))
            {
                g.DrawRectangle(Pens.Red, xPosCoord, yPosCoord, xWidth, yLen);
                g.DrawLine(Pens.Cyan, ((xWidth / 2) + xPosCoord), yPosCoord, ((xWidth / 2) + xPosCoord), (yLen + yPosCoord));
                g.DrawLine(Pens.Cyan, xPosCoord, ((yLen / 2) + yPosCoord), (xWidth + xPosCoord), ((yLen / 2) + yPosCoord));
            }
            picBox.Image = bmp;
        }

        private void xPosPoint_TextChanged(object sender, TextChangedEventArgs e)
        {
            xPosition = xPosPoint.Text;
        }

        private void yPosPoint_TextChanged(object sender, TextChangedEventArgs e)
        {
            yPosition = yPosPoint.Text;
        }
    }
}
