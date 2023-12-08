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
    public partial class Form1 : Form
    {
        Button[,] map = new Button[6, 6];
        List<Button>[,] board = new List<Button>[6, 6];

        int player1_stones = 30;
        int player2_stones = 30;
        int player1_capstones = 1;
        int player2_capstones = 1;

        public Form1()
        {
            InitializeComponent();

            generateMap();
        }

        public void generateMap()
        {
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 6; x++)
                {
                    // Map
                    Button temp = new Button();
                    temp.Name = y + "," + x;
                    temp.Size = new Size(90, 90);
                    temp.Location = new Point((x * 99)+6, (y * 99)+6);
                    temp.BackColor = Color.White;
                    temp.Click += btn_Click;

                    map[y, x] = temp;
                    panelBoard.Controls.Add(temp);

                    // Board
                    board[y, x] = new List<Button>();
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            string[] coord = ((Button)sender).Name.ToString().Split(',');
            int y = Convert.ToInt32(coord[0]);
            int x = Convert.ToInt32(coord[1]);

            if (board[y, x].Count < 1)
            {
                add_stone(y, x);
            }
        }

        public void add_stone(int x, int y)
        {
            //board[y, x].Add();

            map[y, x].BackColor = Color.Cyan;
            map[y, x].Text = board[y, x].Count.ToString();
        }
    }
}
