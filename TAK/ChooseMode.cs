using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TAK
{
    public partial class ChooseMode : Form
    {
        bool pilih_lawan;
        public ChooseMode()
        {
            InitializeComponent();
        }

        private void ChooseMode_Load(object sender, EventArgs e)
        {
            
        }

        private void Player_MouseClick(object sender, MouseEventArgs e)
        {
            pilih_lawan = false;
            Form1 formutama= new Form1(pilih_lawan);
            formutama.Show();
            this.Hide();
        }

        private void AI_MouseClick(object sender, MouseEventArgs e)
        {
            pilih_lawan = true;
            Form1 formutama = new Form1(pilih_lawan);
            formutama.Show();
            this.Hide();
        }
    }
}
