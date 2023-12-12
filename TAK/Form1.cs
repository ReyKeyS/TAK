using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TAK
{
    public partial class Form1 : Form
    {
        bool earlyStep_p1;
        bool earlyStep_p2;
        Button[,] map;
        List<Stone>[,] board;

        bool pickedUp;
        int y_pivot;
        int x_pivot;
        string direction;
        List<Stone> picked;
        List<Button> btnPicked;

        int whosTurn;

        Color p1_color;
        int p1_stones;
        int p1_capstones;
        bool p1_stand;
        bool p1_caps;

        Color p2_color;
        int p2_stones;
        int p2_capstones;
        bool p2_stand;
        bool p2_caps;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize Map
            earlyStep_p1 = true;
            earlyStep_p2 = true;
            GenerateMap();

            // Initialize Variable
            picked = new List<Stone>();
            pickedUp = false;
            y_pivot = -1;
            x_pivot = -1;
            direction = "";
            whosTurn = 1;
            //// Player 1
                p1_color = Color.LightCoral;
                p1_stones = 30;
                p1_capstones = 1;
                p1_stand = false;
                p1_caps = false;
            //// Player 2
                p2_color = Color.DeepSkyBlue;
                p2_stones = 30;
                p2_capstones = 1;
                p2_stand = false;
                p2_caps = false;
        }

        public void GenerateMap()
        {
            map = new Button[6, 6];
            board = new List<Stone>[6, 6];
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
                    temp.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    temp.Click += Btn_Click;

                    map[y, x] = temp;
                    panelBoard.Controls.Add(temp);

                    // Board
                    board[y, x] = new List<Stone>();
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            string[] coord = ((Button)sender).Name.ToString().Split(',');
            int y = Convert.ToInt32(coord[0]);
            int x = Convert.ToInt32(coord[1]);

            ActionClicking(y, x);
        }

        public void ActionClicking(int y, int x)
        {
            if (pickedUp)
            {
                Move_Stone(y, x);
            }
            else
            {
                if (board[y, x].Count < 1)
                {
                    Add_Stone(y, x);
                }
                else
                {
                    if (board[y, x][board[y, x].Count - 1].Player == whosTurn)
                    {
                        btnPicked = new List<Button>();
                        // Get Stacked Stone
                        pickedUp = true;
                        for (int i = 0; i < board[y, x].Count; i++)
                        {
                            picked.Add(board[y, x][i]);

                            // Show Stacked Stone
                            btnPicked.Add(new Button());
                            btnPicked[btnPicked.Count - 1].Size = new Size(75, 25);
                            btnPicked[btnPicked.Count - 1].Location = (whosTurn == 1) ? new Point(54, 111 - (i * 15)) : new Point(831, 111 - (i * 15));
                            btnPicked[btnPicked.Count - 1].BackColor = (picked[picked.Count - 1].Player == 1) ? p1_color : p2_color;
                            this.Controls.Add(btnPicked[btnPicked.Count - 1]);
                        }

                        y_pivot = y;
                        x_pivot = x;

                        // Reset Map
                        map[y, x].Text = "";
                        map[y, x].BackColor = Color.White;
                        board[y, x].Clear();

                    }
                    else MessageBox.Show("Not Yours");
                }
            }
        }

        public void Add_Stone(int y, int x)
        {
            board[y, x].Add(new Stone(whosTurn, false, false));
            
            if (whosTurn == 1)
            {
                if (earlyStep_p1) {
                    board[y, x][board[y, x].Count - 1].Player = 2;
                    map[y, x].BackColor = p2_color;
                    earlyStep_p1 = false;
                    p2_stones--;
                    button_stone_p2.Text = p2_stones.ToString();
                }
                else
                {
                    map[y, x].BackColor = p1_color;
                    p1_stones--;
                    button_stone_p1.Text = p1_stones.ToString();
                }                
                whosTurn = 2;
            }
            else if (whosTurn == 2)
            {
                if (earlyStep_p2)
                {
                    board[y, x][board[y, x].Count - 1].Player = 1;
                    map[y, x].BackColor = p1_color;
                    earlyStep_p2 = false;
                    p1_stones--;
                    button_stone_p1.Text = p1_stones.ToString();
                }
                else
                {
                    map[y, x].BackColor = p2_color;
                    p2_stones--;
                    button_stone_p2.Text = p2_stones.ToString();
                }
                whosTurn = 1;
            }

            map[y, x].Text = board[y, x].Count.ToString();
            lblWhosTurn.Text = "Player " + whosTurn;
        }

        public void Move_Stone(int y, int x)
        {
            if (Valid_Move(y, x))
            {
                board[y, x].Add(picked[0]);

                map[y, x].BackColor = (picked[0].Player == 1) ? p1_color : p2_color;
                map[y, x].Text = board[y, x].Count.ToString();

                // Replace
                picked.RemoveAt(0);
                this.Controls.Remove(btnPicked[0]);
                btnPicked.RemoveAt(0);

                // Set pivot + Direction
                if (y == y_pivot - 1 && x == x_pivot) direction = "up";
                if (y == y_pivot + 1 && x == x_pivot) direction = "down";
                if (y == y_pivot && x == x_pivot - 1) direction = "left";
                if (y == y_pivot && x == x_pivot + 1) direction = "right";
                MessageBox.Show(direction);
                y_pivot = y; x_pivot = x;

                // Reset & Moved All
                if (btnPicked.Count == 0)
                {
                    pickedUp = false;
                    y_pivot = -1; x_pivot = -1;

                    if (direction != "")
                        whosTurn = (whosTurn == 1) ? 2 : 1;

                    direction = "";
                    lblWhosTurn.Text = "Player " + whosTurn;
                }
            } 
            else MessageBox.Show("Invalid Move");
        }

        public bool Valid_Move(int y, int x)
        {
            if (y == y_pivot && x == x_pivot) return true;
            
            if (direction == "")
            {
                if (((y == y_pivot + 1 || y == y_pivot - 1) && x == x_pivot) || ((x == x_pivot + 1 || x == x_pivot - 1) && y == y_pivot))
                    return true;
            }
            else
            {
                if (direction == "up" && (y == y_pivot - 1 && x == x_pivot)) return true;
                if (direction == "down" && (y == y_pivot + 1 && x == x_pivot)) return true;
                if (direction == "left" && (y == y_pivot && x == x_pivot - 1)) return true;
                if (direction == "right" && (y == y_pivot && x == x_pivot + 1)) return true;
            }

            return false;
        }
    }
}
