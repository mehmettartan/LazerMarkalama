using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazerMarka
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();

            // Arka plan ve PictureBox ayarları
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            PictureBox pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = Properties.Resources.LoadingGif, // Projeye eklenen GIF
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            this.Controls.Add(pictureBox);
        }
    }
}
