using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioConv
{
    public partial class FormFfmpegCodecs : Form
    {
        public FormFfmpegCodecs()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(UtiliTunes.SearchImageFile(UtiliTunes.ImageRepo.Apple, "CLC - Devil"));
            pictureBoxAlbumArt.Image = UtiliTunes.SearchImageFile(UtiliTunes.ImageRepo.Apple, textBoxSearchQuery.Text);
        }
    }
}
