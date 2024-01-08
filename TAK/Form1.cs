using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
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
        bool p1_win;
        bool p2_win;

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

        bool pilih_lawan;
        public Form1(bool pilih_lawan)
        {
            InitializeComponent();
            this.pilih_lawan = pilih_lawan;
            Initializing();
        }

        public void Initializing()
        {
            p1_win = false;
            p2_win = false;
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
            button_stone_p1.Size = new Size(85, 85);
            button_stone_p1.Location = new Point(button_stone_p1.Location.X - 5, button_stone_p1.Location.Y - 5);
            button_stone_p1.Font = new Font("Microsoft Sans Serif", 26F);
            button_stone_p2.Font = new Font("Microsoft Sans Serif", 21F);
            button_stone_p2.Enabled = false;
            button_caps_p2.Enabled = false;
            //// Player 1
            p1_color = Color.LightCoral;
            p1_stones = 30;
            p1_capstones = 1;
            p1_stand = false;
            p1_caps = false;
            p1_status.Text = "Stone";
            //// Player 2
            p2_color = Color.DeepSkyBlue;
            p2_stones = 30;
            p2_capstones = 1;
            p2_stand = false;
            p2_caps = false;
            p2_status.Text = "Stone";

            if (pilih_lawan)
                timerAI.Start();
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
                    temp.Location = new Point((x * 99) + 6, (y * 99) + 6);
                    temp.BackColor = Color.White;
                    temp.Font = new Font("Microsoft Sans Serif", 20.25F);
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
                    if (board[y, x][board[y, x].Count - 1].Player == whosTurn && !earlyStep_p1 && !earlyStep_p2 && !board[y, x][board[y, x].Count - 1].Stand)
                    {
                        btnPicked = new List<Button>();
                        // Get Stacked Stone
                        pickedUp = true;
                        int awalIdx = 0;
                        if (board[y, x].Count > 6) awalIdx = board[y, x].Count - 6;
                        for (int i = awalIdx; i < board[y, x].Count; i++)
                        {
                            picked.Add(board[y, x][i]);

                            // Show Stacked Stone
                            btnPicked.Add(new Button());
                            btnPicked[btnPicked.Count - 1].Size = new Size(75, 25);
                            btnPicked[btnPicked.Count - 1].Location = (whosTurn == 1) ? new Point(54, 290 - (i * 15)) : new Point(831, 290 - (i * 15));
                            btnPicked[btnPicked.Count - 1].BackColor = (picked[picked.Count - 1].Player == 1) ? p1_color : p2_color;
                            this.Controls.Add(btnPicked[btnPicked.Count - 1]);
                        }

                        y_pivot = y;
                        x_pivot = x;

                        // Reset Map
                        if (awalIdx == 0)
                        {
                            map[y, x].Text = "";
                            map[y, x].BackColor = Color.White;
                            board[y, x].Clear();
                        }
                        else
                        {
                            map[y, x].Text = awalIdx.ToString();
                            if (board[y, x][awalIdx].Player == 1) map[y, x].BackColor = p1_color;
                            else map[y, x].BackColor = p2_color;
                            board[y, x].RemoveRange(awalIdx, 6);
                        }
                        // Reset btn Stand Stone
                        if (map[y, x].Size.Height != 90)
                        {
                            map[y, x].Size = new Size(90, 90);
                            map[y, x].Location = new Point(map[y, x].Location.X, map[y, x].Location.Y - 20);
                        }
                        // Reset btn Capstone
                        if (map[y, x].FlatStyle == FlatStyle.Flat) map[y, x].FlatStyle = FlatStyle.Standard;
                    }
                    else if (board[y, x][board[y, x].Count - 1].Stand)
                    {
                        MessageBox.Show("Stand can't be picked up!");
                    }
                    else MessageBox.Show("Not Yours");
                }
            }
            CheckWinning();
        }

        public void Add_Stone(int y, int x)
        {
            if (whosTurn == 1)
            {
                if (earlyStep_p1)
                {
                    board[y, x].Add(new Stone(whosTurn, false, false));
                    board[y, x][board[y, x].Count - 1].Player = 2;
                    earlyStep_p1 = false;

                    p2_stones--;
                    button_stone_p2.Text = p2_stones.ToString();

                    Update_Map(y, x);
                }
                else
                {
                    if (p1_stand) board[y, x].Add(new Stone(whosTurn, true, false));
                    else if (p1_caps) board[y, x].Add(new Stone(whosTurn, false, true));
                    else board[y, x].Add(new Stone(whosTurn, false, false));

                    if (!p1_caps)
                    {
                        p1_stones--;
                        button_stone_p1.Text = p1_stones.ToString();
                    }
                    else
                    {
                        p1_capstones--;
                        button_caps_p1.Visible = false;
                    }

                    Update_Map(y, x);
                }

                Change_Turn();
            }
            else if (whosTurn == 2)
            {
                if (earlyStep_p2)
                {
                    board[y, x].Add(new Stone(whosTurn, false, false));
                    board[y, x][board[y, x].Count - 1].Player = 1;
                    earlyStep_p2 = false;

                    p1_stones--;
                    button_stone_p1.Text = p1_stones.ToString();

                    Update_Map(y, x);
                }
                else
                {
                    if (p2_stand) board[y, x].Add(new Stone(whosTurn, true, false));
                    else if (p2_caps) board[y, x].Add(new Stone(whosTurn, false, true));
                    else board[y, x].Add(new Stone(whosTurn, false, false));

                    if (!p2_caps)
                    {
                        p2_stones--;
                        button_stone_p2.Text = p1_stones.ToString();
                    }
                    else
                    {
                        p2_capstones--;
                        button_caps_p2.Visible = false;
                    }

                    Update_Map(y, x);
                }

                Change_Turn();
            }

            lblWhosTurn.Text = "Player " + whosTurn;
        }

        public void Move_Stone(int y, int x)
        {
            if (Valid_Move(y, x))
            {
                bool gas = true;
                if (board[y, x].Count > 0)
                {
                    if (board[y, x][board[y, x].Count - 1].Stand)
                    {
                        gas = false;
                        if (picked[0].Caps)
                        {
                            gas = true;
                            board[y, x][board[y, x].Count - 1].Stand = false;
                        }
                    }
                    if (board[y, x][board[y, x].Count - 1].Caps) gas = false;
                }

                if (gas)
                {
                    board[y, x].Add(picked[0]);

                    Update_Map(y, x);

                    // Replace
                    picked.RemoveAt(0);
                    this.Controls.Remove(btnPicked[0]);
                    btnPicked.RemoveAt(0);

                    // Set pivot + Direction
                    if (y == y_pivot - 1 && x == x_pivot) direction = "up";
                    if (y == y_pivot + 1 && x == x_pivot) direction = "down";
                    if (y == y_pivot && x == x_pivot - 1) direction = "left";
                    if (y == y_pivot && x == x_pivot + 1) direction = "right";
                    y_pivot = y; x_pivot = x;

                    // Reset & Moved All
                    if (btnPicked.Count == 0)
                    {
                        pickedUp = false;
                        y_pivot = -1; x_pivot = -1;

                        if (direction != "") Change_Turn();

                        direction = "";
                        lblWhosTurn.Text = "Player " + whosTurn;
                    }
                }
                else MessageBox.Show("Blocked");
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

        public void Update_Map(int y, int x)
        {
            Stone current = board[y, x][board[y, x].Count - 1];
            Color color = (current.Player == 1) ? p1_color : p2_color;

            // BackColor
            map[y, x].BackColor = color;
            map[y, x].Text = board[y, x].Count.ToString();

            // Reset Button
            Size curr = map[y, x].Size;
            if (curr.Height != 90)
            {
                map[y, x].Size = new Size(90, 90);
                map[y, x].Location = new Point(map[y, x].Location.X, map[y, x].Location.Y - 20);
            }
            if (map[y, x].FlatStyle == FlatStyle.Flat) map[y, x].FlatStyle = FlatStyle.Standard;

            // Set Button by the type
            if (current.Stand)
            {
                map[y, x].Size = new Size(90, 50);
                map[y, x].Location = new Point(map[y, x].Location.X, map[y, x].Location.Y + 20);
            }

            if (current.Caps)
            {
                map[y, x].FlatStyle = FlatStyle.Flat;
                map[y, x].FlatAppearance.BorderColor = Color.Black;
                map[y, x].FlatAppearance.BorderSize = 6;
            }
        }

        public void Change_Turn()
        {
            if (whosTurn == 1)
            {
                whosTurn = 2;
                // Tampilan Button
                button_stone_p1.Font = new Font("Microsoft Sans Serif", 21F);
                button_stone_p1.Size = new Size(75, 75);
                button_stone_p1.Location = new Point(button_stone_p1.Location.X + 5, button_stone_p1.Location.Y + 5);
                button_stone_p1.Enabled = false;
                button_caps_p1.Enabled = false;
                button_stone_p2.Font = new Font("Microsoft Sans Serif", 26F);
                button_stone_p2.Size = new Size(85, 85);
                button_stone_p2.Location = new Point(button_stone_p2.Location.X - 5, button_stone_p2.Location.Y - 5);
                button_stone_p2.Enabled = true;
                button_caps_p2.Enabled = true;
                if (p1_stand)
                {
                    p1_stand = false;
                    button_stone_p1.Size = new Size(75, 75);
                    button_stone_p1.Location = new Point(button_stone_p1.Location.X, button_stone_p1.Location.Y - 15);
                    button_caps_p1.Enabled = true;
                    p1_status.Text = "Stone";
                }
                if (p1_caps)
                {
                    p1_caps = false;
                    button_caps_p1.Size = new Size(75, 75);
                    button_caps_p1.Location = new Point(button_caps_p1.Location.X + 5, button_caps_p1.Location.Y + 5);
                    button_stone_p1.Size = new Size(75, 75);
                    button_stone_p1.Location = new Point(button_stone_p1.Location.X - 5, button_stone_p1.Location.Y - 5);
                    p1_status.Text = "Stone";
                }
            }
            else if (whosTurn == 2)
            {
                whosTurn = 1;
                // Tampilan Button
                button_stone_p1.Font = new Font("Microsoft Sans Serif", 26F);
                button_stone_p1.Size = new Size(85, 85);
                button_stone_p1.Location = new Point(button_stone_p1.Location.X - 5, button_stone_p1.Location.Y - 5);
                button_stone_p1.Enabled = true;
                button_caps_p1.Enabled = true;
                button_stone_p2.Font = new Font("Microsoft Sans Serif", 21F);
                button_stone_p2.Size = new Size(75, 75);
                button_stone_p2.Location = new Point(button_stone_p2.Location.X + 5, button_stone_p2.Location.Y + 5);
                button_stone_p2.Enabled = false;
                button_caps_p2.Enabled = false;
                if (p2_stand)
                {
                    p2_stand = false;
                    button_stone_p2.Size = new Size(75, 75);
                    button_stone_p2.Location = new Point(button_stone_p2.Location.X, button_stone_p2.Location.Y - 15);
                    button_caps_p2.Enabled = true;
                    p2_status.Text = "Stone";
                }
                if (p2_caps)
                {
                    p2_caps = false;
                    button_caps_p2.Size = new Size(75, 75);
                    button_caps_p2.Location = new Point(button_caps_p2.Location.X + 5, button_caps_p2.Location.Y + 5);
                    button_stone_p2.Size = new Size(75, 75);
                    button_stone_p2.Location = new Point(button_stone_p2.Location.X - 5, button_stone_p2.Location.Y - 5);
                    p2_status.Text = "Stone";
                }
            }
        }



        private void Button_stone_p1_Click(object sender, EventArgs e)
        {
            if (!earlyStep_p1)
            {
                if (p1_stand)
                {
                    p1_stand = false;
                    p1_status.Text = "Stone";
                    button_stone_p1.Size = new Size(85, 85);
                    button_stone_p1.Location = new Point(button_stone_p1.Location.X, button_stone_p1.Location.Y - 15);
                    button_caps_p1.Enabled = true;
                }
                else
                {
                    p1_stand = true;
                    p1_status.Text = "Stand Stone";
                    button_stone_p1.Size = new Size(85, 55);
                    button_stone_p1.Location = new Point(button_stone_p1.Location.X, button_stone_p1.Location.Y + 15);
                    button_caps_p1.Enabled = false;
                }
            }
        }

        private void Button_caps_p1_Click(object sender, EventArgs e)
        {
            if (!earlyStep_p1)
            {
                if (p1_caps)
                {
                    p1_caps = false;
                    p1_status.Text = "Stone";
                    button_caps_p1.Size = new Size(75, 75);
                    button_caps_p1.Location = new Point(button_caps_p1.Location.X + 5, button_caps_p1.Location.Y + 5);
                    button_stone_p1.Enabled = true;
                    button_stone_p1.Size = new Size(85, 85);
                    button_stone_p1.Location = new Point(button_stone_p1.Location.X - 5, button_stone_p1.Location.Y - 5);
                }
                else
                {
                    p1_caps = true;
                    p1_status.Text = "Capstone";
                    button_caps_p1.Size = new Size(85, 85);
                    button_caps_p1.Location = new Point(button_caps_p1.Location.X - 5, button_caps_p1.Location.Y - 5);
                    button_stone_p1.Enabled = false;
                    button_stone_p1.Size = new Size(75, 75);
                    button_stone_p1.Location = new Point(button_stone_p1.Location.X + 5, button_stone_p1.Location.Y + 5);
                }
            }
        }

        private void Button_stone_p2_Click(object sender, EventArgs e)
        {
            if (!earlyStep_p2)
            {
                if (p2_stand)
                {
                    p2_stand = false;
                    p2_status.Text = "Stone";
                    button_stone_p2.Size = new Size(85, 85);
                    button_stone_p2.Location = new Point(button_stone_p2.Location.X, button_stone_p2.Location.Y - 15);
                    button_caps_p2.Enabled = true;
                }
                else
                {
                    p2_stand = true;
                    p2_status.Text = "Stand Stone";
                    button_stone_p2.Size = new Size(85, 55);
                    button_stone_p2.Location = new Point(button_stone_p2.Location.X, button_stone_p2.Location.Y + 15);
                    button_caps_p2.Enabled = false;
                }
            }
        }

        private void Button_caps_p2_Click(object sender, EventArgs e)
        {
            if (!earlyStep_p2)
            {
                if (p2_caps)
                {
                    p2_caps = false;
                    p2_status.Text = "Stone";
                    button_caps_p2.Size = new Size(75, 75);
                    button_caps_p2.Location = new Point(button_caps_p2.Location.X + 5, button_caps_p2.Location.Y + 5);
                    button_stone_p2.Enabled = true;
                    button_stone_p2.Size = new Size(85, 85);
                    button_stone_p2.Location = new Point(button_stone_p2.Location.X - 5, button_stone_p2.Location.Y - 5);
                }
                else
                {
                    p2_caps = true;
                    p2_status.Text = "Capstone";
                    button_caps_p2.Size = new Size(85, 85);
                    button_caps_p2.Location = new Point(button_caps_p2.Location.X - 5, button_caps_p2.Location.Y - 5);
                    button_stone_p2.Enabled = false;
                    button_stone_p2.Size = new Size(75, 75);
                    button_stone_p2.Location = new Point(button_stone_p2.Location.X + 5, button_stone_p2.Location.Y + 5);
                }
            }
        }



        public bool CheckWinning()
        {
            if (!earlyStep_p1 && !earlyStep_p2)
                CheckWin(board);

            bool win = false;
            // Rows & Columns
            if (p1_win && p2_win) { timerAI.Stop(); MessageBox.Show("TIE"); win = true; }
            else if (p1_win) { timerAI.Stop(); MessageBox.Show("Player 1 Win "); win = true; }
            else if (p2_win) { timerAI.Stop(); MessageBox.Show("Player 2 Win"); win = true; }

            if (win)
            {
                DialogResult result = MessageBox.Show("Try Again?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Hide();
                    ChooseMode f = new ChooseMode();
                    f.ShowDialog();
                    this.Close();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                }
            }
            return win;
        }

        private void CheckWin(List<Stone>[,] board)
        {
            bool gas = true;
            for (int i = 0; i < 6 * 6; i++)
            {

                int y = i / 6;
                int x = i % 6;

                if (board[y, x].Count < 1)
                {
                    gas = false;
                }
            }
            if (gas)
            {
                int jumlahPlayer1 = 0;
                int jumlahPlayer2 = 0;
                for (int i = 0; i < 6 * 6; i++)
                {
                    int y = i / 6;
                    int x = i % 6;

                    if (board[y, x][board[y, x].Count - 1].Player == 1 && !board[y, x][board[y, x].Count - 1].Stand && !board[y, x][board[y, x].Count - 1].Caps)
                        jumlahPlayer1++;
                    if (board[y, x][board[y, x].Count - 1].Player == 2 && !board[y, x][board[y, x].Count - 1].Stand && !board[y, x][board[y, x].Count - 1].Caps)
                        jumlahPlayer2++;
                }
                if (jumlahPlayer1>jumlahPlayer2)
                    p1_win = true;
                else if (jumlahPlayer1 < jumlahPlayer2)
                    p2_win = true;
                
                else if (jumlahPlayer1 == jumlahPlayer2)
                {
                    p1_win = true;
                    p2_win = true;
                }
            }
            else
            {
                for (int whosTurn = 1; whosTurn <= 2; whosTurn++)
                {
                    // Atas
                    List<int> startStoneTop = new List<int>();
                    for (int x = 0; x < 6; x++)
                    {
                        if (board[0, x].Count > 0 && board[0, x][board[0, x].Count - 1].Player == whosTurn && !board[0, x][board[0, x].Count - 1].Stand)
                        {
                            startStoneTop.Add(x);
                        }
                    }
                    foreach (int sX in startStoneTop)
                    {
                        List<(int, int)> stepped = new List<(int, int)>();
                        stepped.Add((0, sX));
                        if (whosTurn == 1 && !p1_win)
                            RecursiveVertical(0, sX, -1, -1, whosTurn, stepped, board);
                        if (whosTurn == 2 && !p2_win)
                            RecursiveVertical(0, sX, -1, -1, whosTurn, stepped, board);
                    }

                    // Kiri
                    List<int> startStoneLeft = new List<int>();
                    for (int y = 0; y < 6; y++)
                    {
                        if (board[y, 0].Count > 0 && board[y, 0][board[y, 0].Count - 1].Player == whosTurn && !board[y, 0][board[y, 0].Count - 1].Stand)
                        {
                            startStoneLeft.Add(y);
                        }
                    }
                    foreach (int sY in startStoneLeft)
                    {
                        List<(int, int)> stepped = new List<(int, int)>();
                        stepped.Add((sY, 0));
                        if (whosTurn == 1 && !p1_win)
                            RecursiveHorizontal(sY, 0, -1, -1, whosTurn, stepped, board);
                        if (whosTurn == 2 && !p2_win)
                            RecursiveHorizontal(sY, 0, -1, -1, whosTurn, stepped, board);
                    }
                }
            }
            
        }

        private void RecursiveVertical(int y, int x, int before_y, int before_x, int whosTurn, List<(int, int)> stepped, List<Stone>[,] board)
        {
            if (y == 5)
            {
                if (whosTurn == 1) p1_win = true;
                else if (whosTurn == 2) p2_win = true;
            }
            else
            {
                // Bawah, Kiri, Kanan, Atas
                int[] nextY = { 1, 0, 0, -1 };
                int[] nextX = { 0, -1, 1, 0 };
                for (int i = 0; i < 4; i++)
                {
                    if (ValidColor(y + nextY[i], x + nextX[i], before_y, before_x, whosTurn, stepped, board))
                    {
                        stepped.Add((y + nextY[i], x + nextX[i]));
                        RecursiveVertical(y + nextY[i], x + nextX[i], y, x, whosTurn, stepped, board);
                    }
                }
            }
        }

        private void RecursiveHorizontal(int y, int x, int before_y, int before_x, int whosTurn, List<(int, int)> stepped, List<Stone>[,] board)
        {
            if (x == 5)
            {
                if (whosTurn == 1) p1_win = true;
                else if (whosTurn == 2) p2_win = true;
            }
            else
            {
                // Kanan, Bawah, Atas, Kiri
                int[] nextY = { 0, 1, -1, 0 };
                int[] nextX = { -1, 0, 0, 1 };
                for (int i = 0; i < 4; i++)
                {
                    if (ValidColor(y + nextY[i], x + nextX[i], before_y, before_x, whosTurn, stepped, board))
                    {
                        stepped.Add((y + nextY[i], x + nextX[i]));
                        RecursiveHorizontal(y + nextY[i], x + nextX[i], y, x, whosTurn, stepped, board);
                    }
                }
            }
        }

        private bool ValidColor(int y, int x, int before_y, int before_x, int whosTurn, List<(int, int)> stepped, List<Stone>[,] board)
        {
            if (y == before_y && x == before_x) return false;

            for (int i = 0; i < stepped.Count; i++)
            {
                if (y == stepped[i].Item1 && x == stepped[i].Item2) return false;
            }

            if (y >= 0 && y < 6 && x >= 0 && x < 6)
            {
                if (board[y, x].Count > 0 && board[y, x][board[y, x].Count - 1].Player == whosTurn && !board[y, x][board[y, x].Count - 1].Stand)
                    return true;
            }

            return false;
        }



        // ----------------------    AI    ----------------------
        private void TimerAI_Tick(object sender, EventArgs e)
        {
            if (whosTurn == 2)
            {
                //// Minimax with Alpha Beta Pruning
                
                // Clone
                List<Stone>[,] clonedBoard = new List<Stone>[6, 6];
                for (int x = 0; x < 6 * 6; x++)
                {
                    int nRow = x / 6;
                    int nCol = x % 6;
                    clonedBoard[nRow, nCol] = new List<Stone>(board[nRow, nCol]);
                }

                List<(int, int, string)> bestMove = GetBestMoveForAI(clonedBoard);

                // Action
                foreach (var move in bestMove)
                {
                    if (move.Item3 == "stand") p2_stand = true;
                    else if (move.Item3 == "caps") p2_caps = true;

                    timerAI.Stop();
                    MessageBox.Show("Result : " + move.Item1.ToString() + " - " + move.Item2.ToString() + "\nTipe : " + move.Item3.ToString());
                    timerAI.Start();

                    ActionClicking(move.Item1, move.Item2);

                    p1_caps = false;
                    p1_stand = false;
                    p2_caps = false;
                    p2_stand = false;
                }
            }
            //else if (earlyStep_p2 && whosTurn == 2)
            //{
            //    // Randoming the first Step
            //    Random rand = new Random();
            //    int y, x;
            //    bool randoming = true;
            //    do
            //    {
            //        y = rand.Next(1, 5);
            //        x = rand.Next(1, 5);
            //        randoming = false;
            //        if (board[y, x].Count > 0) randoming = true;
            //    } while (randoming);

            //    ActionClicking(y, x);
            //}
        }

        private List<(int, int, string)> GetBestMoveForAI(List<Stone>[,] clonedBoard)
        {
            int bestEval = int.MinValue;
            List<(int, int, string)> bestMove = new List<(int, int, string)> { (-1, -1, "flat") };

            foreach (var moves in GetPossibleMoves(clonedBoard, 2, p1_stones, p1_capstones, p2_stones, p2_capstones))
            {
                // Clone
                List<Stone>[,] newBoard = new List<Stone>[6, 6];
                for (int x = 0; x < 6 * 6; x++)
                {
                    int nRow = x / 6;
                    int nCol = x % 6;
                    newBoard[nRow, nCol] = new List<Stone>(clonedBoard[nRow, nCol]);
                }

                int p1_sisaStone = p1_stones;
                int p1_sisaCaps = p1_capstones;
                int p2_sisaStone = p2_stones;
                int p2_sisaCaps = p2_capstones;

                MakeMove(ref newBoard, moves, 2, ref p1_sisaStone, ref p1_sisaCaps, ref p2_sisaStone, ref p2_sisaCaps);

                int eval = MinimaxWithAlphaBeta(newBoard, 3, false, int.MinValue, int.MaxValue, p1_sisaStone, p1_sisaCaps, p2_sisaStone, p2_sisaCaps); // Adjust the depth as needed
                if (eval > bestEval)
                {
                    bestEval = eval;
                    bestMove = moves;

                    //timerAI.Stop();
                    //MessageBox.Show("Best Eval : " + bestMove[0].Item1.ToString() + " - " + bestMove[0].Item2.ToString() + "\nTipe : " + bestMove[0].Item3.ToString());
                    //timerAI.Start();
                }
            }

            return bestMove;
        }

        private List<List<(int, int, string)>> GetPossibleMoves(List<Stone>[,] newBoard, int player, int p1_sisaStone, int p1_sisaCaps, int p2_sisaStone, int p2_sisaCaps)
        {
            List<List<(int, int, string)>> moves = new List<List<(int, int, string)>>();

            for (int ij = 0; ij < 6 * 6; ij++)
            {
                int y = ij / 6;
                int x = ij % 6;

                if (newBoard[y, x].Count < 1)
                {
                    // Chance place Flat & Stand
                    if ((player == 1 && p1_sisaStone > 0) || (player == 2 && p2_sisaStone > 0))
                    {
                        List<(int, int, string)> inside = new List<(int, int, string)>
                        {
                            (y, x, "flat")
                        };
                        moves.Add(inside);

                        //inside = new List<(int, int, string)>
                        //{
                        //    (y, x, "stand")
                        //};
                        //moves.Add(inside);
                    }
                    // Chance place Capstone
                    if ((player == 1 && p1_sisaCaps > 0) || (player == 2 && p2_sisaCaps > 0))
                    {
                        List<(int, int, string)> inside = new List<(int, int, string)>
                        {
                            (y, x, "caps")
                        };
                        moves.Add(inside);
                    }
                }
                else if (newBoard[y, x].Count > 0 && newBoard[y, x][newBoard[y, x].Count - 1].Player == player && !newBoard[y, x][newBoard[y, x].Count - 1].Stand)
                {
                    // Pick Up
                    List<Stone> stacked = newBoard[y, x];
                    int height = newBoard[y, x].Count;
                    if (height > 6) height = 6;
                    List<List<int>> allPlaces = GetAllPlaces(height);

                    int top = y;
                    int bottom = (6 - 1) - y;
                    int left = x;
                    int right = (6 - 1) - x;

                    List<(int, int, string)> inside;
                    for (int i = 0; i < allPlaces.Count; i++)
                    {
                        // Top
                        inside = new List<(int, int, string)> { (y, x, "pick") };
                        height = newBoard[y, x].Count;
                        if (allPlaces[i].Count <= top)
                        {
                            for (int j = 1; j <= allPlaces[i].Count; j++)
                            {
                                bool gas = true;
                                for (int k = 0; k < allPlaces[i][j - 1]; k++)
                                {
                                    if (newBoard[y - j, x].Count > 0)
                                    {
                                        Stone topBoard = newBoard[y - j, x][newBoard[y - j, x].Count - 1];
                                        Stone curBoard = stacked[stacked.Count - height];
                                        if (!topBoard.Caps && (!topBoard.Stand || curBoard.Caps))
                                        {
                                            inside.Add((y - j, x, "pick"));
                                            height--;
                                        }
                                        else
                                            gas = false;
                                    }
                                    //else
                                    //{
                                    //    inside.Add((y - j, x, "pick"));
                                    //    height--;
                                    //}
                                }
                                if (gas) moves.Add(inside);
                            }
                        }

                        // Bottom
                        inside = new List<(int, int, string)> { (y, x, "pick") };
                        height = newBoard[y, x].Count;
                        if (allPlaces[i].Count <= bottom)
                        {
                            for (int j = 1; j <= allPlaces[i].Count; j++)
                            {
                                bool gas = true;
                                for (int k = 0; k < allPlaces[i][j - 1]; k++)
                                {
                                    if (newBoard[y + j, x].Count > 0)
                                    {
                                        Stone bottomBoard = newBoard[y + j, x][newBoard[y + j, x].Count - 1];
                                        Stone curBoard = stacked[stacked.Count - height];
                                        if (!bottomBoard.Caps && (!bottomBoard.Stand || curBoard.Caps))
                                        {
                                            inside.Add((y + j, x, "pick"));
                                            height--;
                                        }
                                        else
                                            gas = false;
                                    }
                                    //else
                                    //{
                                    //    inside.Add((y + j, x, "pick"));
                                    //    height--;
                                    //}
                                }
                                if (gas) moves.Add(inside);
                            }
                        }

                        // Left
                        inside = new List<(int, int, string)> { (y, x, "pick") };
                        height = newBoard[y, x].Count;
                        if (allPlaces[i].Count <= left)
                        {
                            for (int j = 1; j <= allPlaces[i].Count; j++)
                            {
                                bool gas = true;
                                for (int k = 0; k < allPlaces[i][j - 1]; k++)
                                {
                                    if (newBoard[y, x - 1].Count > 0)
                                    {
                                        Stone leftBoard = newBoard[y, x - 1][newBoard[y, x - 1].Count - 1];
                                        Stone curBoard = stacked[stacked.Count - height];
                                        if (!leftBoard.Caps && (!leftBoard.Stand || curBoard.Caps))
                                        {
                                            inside.Add((y, x - 1, "pick"));
                                            height--;
                                        }
                                        else
                                            gas = false;
                                    }
                                    //else
                                    //{
                                    //    inside.Add((y, x - 1, "pick"));
                                    //    height--;
                                    //}
                                }
                                if (gas) moves.Add(inside);
                            }
                        }

                        // Right
                        inside = new List<(int, int, string)> { (y, x, "pick") };
                        height = newBoard[y, x].Count;
                        if (allPlaces[i].Count <= right)
                        {
                            for (int j = 1; j <= allPlaces[i].Count; j++)
                            {
                                bool gas = true;
                                for (int k = 0; k < allPlaces[i][j - 1]; k++)
                                {
                                    if (newBoard[y, x + 1].Count > 0)
                                    {
                                        Stone rightBoard = newBoard[y, x + 1][newBoard[y, x + 1].Count - 1];
                                        Stone curBoard = stacked[stacked.Count - height];
                                        if (!rightBoard.Caps && (!rightBoard.Stand || curBoard.Caps))
                                        {
                                            inside.Add((y, x + 1, "pick"));
                                            height--;
                                        }
                                        else
                                            gas = false;
                                    }
                                    //else
                                    //{
                                    //    inside.Add((y, x + 1, "pick"));
                                    //    height--;
                                    //}
                                }
                                if (gas) moves.Add(inside);
                            }
                        }
                    }
                }
            }
            return moves;
        }

        private List<List<int>> GetAllPlaces(int height)
        {
            List<List<int>> places = new List<List<int>>();

            if (height <= 0)
                return places;

            if (height == 1)
            {
                List<int> temp1 = new List<int> { 1 };
                places.Add(temp1);
                return places;
            }

            for (int i = 1; i < height; i++)
            {
                int j = height - i;
                List<int> temp2 = new List<int>();
                List<List<int>> part = GetAllPlaces(j);

                foreach (var partition in part)
                {
                    temp2 = new List<int>(partition);
                    temp2.Insert(0, i);
                    places.Add(temp2);
                }
            }

            List<int> temp3 = new List<int> { height };
            places.Add(temp3);
            return places;
        }

        private int MinimaxWithAlphaBeta(List<Stone>[,] prevBoard, int depth, bool maximizingPlayer, int alpha, int beta, int p1_sisaStone, int p1_sisaCaps, int p2_sisaStone, int p2_sisaCaps)
        {
            CheckWin(prevBoard);
            if (p1_win || p2_win)
            {
                p1_win = false;
                p2_win = false;
                return Evaluate(prevBoard, p1_sisaCaps, p2_sisaCaps);
            }

            if (depth == 0)
            {
                return Evaluate(prevBoard, p1_sisaCaps, p2_sisaCaps);
            }

            if (maximizingPlayer) // Player 2 (AI)
            {
                int maxEval = int.MinValue;

                foreach (var moves in GetPossibleMoves(prevBoard, 2, p1_sisaStone, p1_sisaCaps, p2_sisaStone, p2_sisaCaps))
                {
                    // Clone
                    List<Stone>[,] newBoard = new List<Stone>[6, 6];
                    for (int x = 0; x < 6 * 6; x++)
                    {
                        int nRow = x / 6;
                        int nCol = x % 6;
                        newBoard[nRow, nCol] = new List<Stone>(prevBoard[nRow, nCol]);
                    }

                    int p1_sisaStone_rec = p1_sisaStone;
                    int p1_sisaCaps_rec = p1_sisaCaps;
                    int p2_sisaStone_rec = p2_sisaStone;
                    int p2_sisaCaps_rec = p2_sisaCaps;

                    MakeMove(ref newBoard, moves, 2, ref p1_sisaStone_rec, ref p1_sisaCaps_rec, ref p2_sisaStone_rec, ref p2_sisaCaps_rec);

                    int eval = MinimaxWithAlphaBeta(newBoard, depth - 1, false, alpha, beta, p1_sisaStone_rec, p1_sisaCaps_rec, p2_sisaStone_rec, p2_sisaCaps_rec);
                    maxEval = Math.Max(maxEval, eval);

                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                        break; // Beta pruning
                }

                return maxEval;
            }
            else // Player 1
            {
                int minEval = int.MaxValue;

                foreach (var moves in GetPossibleMoves(prevBoard, 1, p1_sisaStone, p1_sisaCaps, p2_sisaStone, p2_sisaCaps))
                {
                    // Clone
                    List<Stone>[,] newBoard = new List<Stone>[6, 6];
                    for (int x = 0; x < 6 * 6; x++)
                    {
                        int nRow = x / 6;
                        int nCol = x % 6;
                        newBoard[nRow, nCol] = new List<Stone>(prevBoard[nRow, nCol]);
                    }

                    int p1_sisaStone_rec = p1_sisaStone;
                    int p1_sisaCaps_rec = p1_sisaCaps;
                    int p2_sisaStone_rec = p2_sisaStone;
                    int p2_sisaCaps_rec = p2_sisaCaps;

                    MakeMove(ref newBoard, moves, 1, ref p1_sisaStone_rec, ref p1_sisaCaps_rec, ref p2_sisaStone_rec, ref p2_sisaCaps_rec);

                    int eval = MinimaxWithAlphaBeta(newBoard, depth - 1, true, alpha, beta, p1_sisaStone_rec, p1_sisaCaps_rec, p2_sisaStone_rec, p2_sisaCaps_rec);
                    minEval = Math.Min(minEval, eval);

                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                        break; // Alpha pruning
                }

                return minEval;
            }
        }

        private void MakeMove(ref List<Stone>[,] newBoard, List<(int, int, string)> moves, int player, ref int p1_sisaStone, ref int p1_sisaCaps, ref int p2_sisaStone, ref int p2_sisaCaps)
        {
            if (moves.Count == 1 )
            {
                (int, int, string) move = moves[0];
                int y = move.Item1;
                int x = move.Item2;
                string type = move.Item3;

                if (type == "flat")
                {
                    newBoard[y, x].Add(new Stone(player, false, false));
                    if (player == 1) p1_sisaStone--; else if (player == 2) p2_sisaStone--;
                }
                else if (type == "stand")
                {
                    newBoard[y, x].Add(new Stone(player, true, false));
                    if (player == 1) p1_sisaStone--; else if (player == 2) p2_sisaStone--;
                }
                else if (type == "caps")
                {
                    newBoard[y, x].Add(new Stone(player, false, true));
                    if (player == 1) p1_sisaCaps--; else if (player == 2) p2_sisaCaps--;
                }
            }
            else if (moves.Count > 1 && moves[0].Item3 == "pick")
            {
                (int, int, string) move = moves[0];
                int y = move.Item1;
                int x = move.Item2;

                int awalIdx = 0;
                if (newBoard[y, x].Count > 6) awalIdx = newBoard[y, x].Count - 6;
                picked.Clear();
                for (int i = awalIdx; i < newBoard[y, x].Count; i++)
                {
                    picked.Add(newBoard[y, x][i]);
                }
                if (awalIdx == 0)
                    newBoard[y, x].Clear();
                else
                    newBoard[y, x].RemoveRange(awalIdx, 6);        

                for (int i = 1; i < moves.Count; i++)
                {
                    (int, int, string) move2 = moves[i];
                    y = move2.Item1;
                    x = move2.Item2;

                    newBoard[y, x].Add(picked[0]);
                    picked.RemoveAt(0);
                }
            }
        }

        private int Evaluate(List<Stone>[,] newBoard, int p1_sisaCaps, int p2_sisaCaps)
        {
            int flatCountScore = 0;
            int wallScore = 0;
            int capstoneScore = p2_sisaCaps;
            int stackHeightScore = 0;
            int roadThicknessScore = 0;
            int centerControlScore = 0;
            int blockadeScore = 0;
            int boardStructure = 0;

            for (int ij = 0; ij < 6 * 6; ij++)
            {
                int y = ij / 6;
                int x = ij % 6;

                var currentCell = newBoard[y, x];

                if (currentCell.Count > 0 && currentCell[currentCell.Count - 1].Player == 2)
                {
                    var currentStone = currentCell[currentCell.Count - 1];

                    if (!currentStone.Stand)
                        flatCountScore++;
                    else
                        wallScore++;

                    stackHeightScore = currentCell.Count;

                    if (IsPlayerStone(newBoard, y, x, 2))
                    {
                        UpdateRoadThickness(newBoard, y, x, ref roadThicknessScore);

                        if (IsWithinCenter(y, x))
                            centerControlScore++;

                        if (ContributesToBlockade(newBoard, y, x, 2))
                            blockadeScore++;

                        if (ContributesToBoardStructure(newBoard, y, x, 2))
                            boardStructure++;
                    }
                }
            }

            int totalScore = 3 * flatCountScore + 2 * wallScore +
                             1 * capstoneScore +
                             2 * stackHeightScore + 2 * roadThicknessScore +
                             4 * centerControlScore + 3 * blockadeScore + 5 * boardStructure;

            CheckWin(newBoard);
            if (p2_win)
            {
                totalScore += 100000;
                p2_win = false;
            }
            if (p1_win)
            {
                totalScore -= 100000;
                p1_win = false;
            }

            return totalScore;
        }

        private bool IsPlayerStone(List<Stone>[,] newBoard, int y, int x, int player)
        {
            return newBoard[y, x].Count > 0 && newBoard[y, x][newBoard[y, x].Count - 1].Player == player;
        }

        private void UpdateRoadThickness(List<Stone>[,] newBoard, int y, int x, ref int roadThicknessScore)
        {
            int rowThickness = (x > 0 && IsPlayerStone(newBoard, y, x - 1, 2)) ? 1 : 0;
            rowThickness += (x < 5 && IsPlayerStone(newBoard, y, x + 1, 2)) ? 1 : 0;

            int colThickness = (y > 0 && IsPlayerStone(newBoard, y - 1, x, 2)) ? 1 : 0;
            colThickness += (y < 5 && IsPlayerStone(newBoard, y + 1, x, 2)) ? 1 : 0;

            roadThicknessScore = Math.Max(roadThicknessScore, Math.Max(rowThickness, colThickness));
        }

        private bool IsWithinCenter(int y, int x)
        {
            return Math.Abs(y - 2.5) + Math.Abs(x - 2.5) <= 2;
        }

        private bool ContributesToBlockade(List<Stone>[,] newBoard, int y, int x, int player)
        {
            bool surrounded = true;

            if (y > 0 && newBoard[y - 1, x].Count > 0 && newBoard[y - 1, x][newBoard[y - 1, x].Count - 1].Player != player)
                surrounded = false;
            if (y < 5 && newBoard[y + 1, x].Count > 0 && newBoard[y + 1, x][newBoard[y + 1, x].Count - 1].Player != player)
                surrounded = false;
            if (x > 0 && newBoard[y, x - 1].Count > 0 && newBoard[y, x - 1][newBoard[y, x - 1].Count - 1].Player != player)
                surrounded = false;
            if (x < 5 && newBoard[y, x + 1].Count > 0 && newBoard[y, x + 1][newBoard[y, x + 1].Count - 1].Player != player)
                surrounded = false;

            return surrounded;
        }

        private bool ContributesToBoardStructure(List<Stone>[,] newBoard, int y, int x, int player)
        {
            bool contributes = false;

            int horizontalStones = CountStonesInDirection(newBoard, y, x, player, 0, 1) + CountStonesInDirection(newBoard, y, x, player, 0, -1);
            int verticalStones = CountStonesInDirection(newBoard, y, x, player, 1, 0) + CountStonesInDirection(newBoard, y, x, player, -1, 0);

            if (horizontalStones >= 2 || verticalStones >= 2)
            {
                contributes = true;
            }

            return contributes;
        }

        private int CountStonesInDirection(List<Stone>[,] newBoard, int startY, int startX, int player, int deltaY, int deltaX)
        {
            int count = 0;
            int y = startY + deltaY;
            int x = startX + deltaX;

            while (y >= 0 && y < 6 && x >= 0 && x < 6 && newBoard[y, x].Count > 0 && newBoard[y, x][newBoard[y, x].Count - 1].Player == player)
            {
                count++;
                y += deltaY;
                x += deltaX;
            }

            return count;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                if (timerAI.Enabled)
                    timerAI.Stop();
                else
                    timerAI.Start();
            }
        }
    }
}