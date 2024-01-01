﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
                    temp.Location = new Point((x * 99)+6, (y * 99)+6);
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
                    if (board[y, x][board[y, x].Count - 1].Player == whosTurn && !earlyStep_p1 && !earlyStep_p2)
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
                            btnPicked[btnPicked.Count - 1].Location = (whosTurn == 1) ? new Point(54, 290 - (i * 15)) : new Point(831, 290 - (i * 15));
                            btnPicked[btnPicked.Count - 1].BackColor = (picked[picked.Count - 1].Player == 1) ? p1_color : p2_color;
                            this.Controls.Add(btnPicked[btnPicked.Count - 1]);
                        }

                        y_pivot = y;
                        x_pivot = x;

                        // Reset Map
                        map[y, x].Text = "";
                        map[y, x].BackColor = Color.White;
                        board[y, x].Clear();
                        // Reset btn Stand Stone
                        if (map[y, x].Size.Height != 90)
                        {
                            map[y, x].Size = new Size(90, 90);
                            map[y, x].Location = new Point(map[y, x].Location.X, map[y, x].Location.Y - 20);
                        }
                        // Reset btn Capstone
                        if (map[y, x].FlatStyle == FlatStyle.Flat) map[y, x].FlatStyle = FlatStyle.Standard;
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
                if (earlyStep_p1) {
                    board[y, x].Add(new Stone(whosTurn, false, false));
                    board[y, x][board[y, x].Count - 1].Player = 2;
                    earlyStep_p1 = false;

                    p2_stones--;
                    button_stone_p2.Text = p2_stones.ToString();

                    Update_Map(y, x);
                }
                else
                {
                    if (p1_stand)       board[y, x].Add(new Stone(whosTurn, true, false));
                    else if (p1_caps)   board[y, x].Add(new Stone(whosTurn, false, true));
                    else                board[y, x].Add(new Stone(whosTurn, false, false));

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
                    if (p2_stand)       board[y, x].Add(new Stone(whosTurn, true, false));
                    else if (p2_caps)   board[y, x].Add(new Stone(whosTurn, false, true));
                    else                board[y, x].Add(new Stone(whosTurn, false, false));

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



        private void button_stone_p1_Click(object sender, EventArgs e)
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

        private void button_caps_p1_Click(object sender, EventArgs e)
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

        private void button_stone_p2_Click(object sender, EventArgs e)
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

        private void button_caps_p2_Click(object sender, EventArgs e)
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
                CheckWin();

            bool win = false;
            // Rows & Columns
            if (p1_win) { MessageBox.Show("Player 1 Win"); win = true; }
            else if (p2_win) { MessageBox.Show("Player 2 Win"); win = true; }

            if (win)
            {
                timerAI.Stop();
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

        public void CheckWin()
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
                        RecursiveVertical(0, sX, -1, -1, whosTurn, stepped);
                    if (whosTurn == 2 && !p2_win)
                        RecursiveVertical(0, sX, -1, -1, whosTurn, stepped);
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
                        RecursiveHorizontal(sY, 0, -1, -1, whosTurn, stepped);
                    if (whosTurn == 2 && !p2_win)
                        RecursiveHorizontal(sY, 0, -1, -1, whosTurn, stepped);
                }   
            }
        }

        public void RecursiveVertical(int y, int x, int before_y, int before_x, int whosTurn, List<(int, int)> stepped)
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
                    if (validColor(y + nextY[i], x + nextX[i], before_y, before_x, whosTurn, stepped))
                    {
                        stepped.Add((y + nextY[i], x + nextX[i]));
                        RecursiveVertical(y + nextY[i], x + nextX[i], y, x, whosTurn, stepped);
                    }
                }
            }
        }

        public void RecursiveHorizontal(int y, int x, int before_y, int before_x, int whosTurn, List<(int, int)> stepped)
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
                    if (validColor(y + nextY[i], x + nextX[i], before_y, before_x, whosTurn, stepped))
                    {
                        stepped.Add((y + nextY[i], x + nextX[i]));
                        RecursiveHorizontal(y + nextY[i], x + nextX[i], y, x, whosTurn, stepped);
                    }
                }
            }
        }

        public bool validColor(int y, int x, int before_y, int before_x, int whosTurn, List<(int, int)> stepped)
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
            if (!earlyStep_p2 && whosTurn == 2)
            {
                // Minimax with Alpha Beta Pruning
                (int, int) bestMove = GetBestMoveForAI();
                
                // Action
                ActionClicking(bestMove.Item1, bestMove.Item2);                
            }
            else if (earlyStep_p2 && whosTurn == 2)
            {
                // Randoming the first Step
                Random rand = new Random();
                int y, x;
                bool randoming = true;
                do
                {
                    y = rand.Next(0, 6);
                    x = rand.Next(0, 6);
                    randoming = false;
                    if (board[y, x].Count > 0) randoming = true;
                } while (randoming);

                ActionClicking(y, x);
            }
        }

        private (int, int) GetBestMoveForAI()
        {
            int bestEval = int.MinValue;
            (int, int) bestMove = (-1, -1);

            foreach (var move in GetPossibleMoves(2))
            {
                MakeMove(move.Item1, move.Item2, 2);

                int eval = MinimaxWithAlphaBeta(5, false, int.MinValue, int.MaxValue); // Adjust the depth as needed
                if (eval > bestEval)
                {
                    bestEval = eval;
                    bestMove = move;
                }

                UndoMove(move.Item1, move.Item2);
            }

            return bestMove;
        }

        private List<(int, int)> GetPossibleMoves(int player)
        {
            List<(int, int)> moves = new List<(int, int)>();

            if (pickedUp)
            {
                moves.Add((y_pivot, x_pivot));

                if (direction == "")
                {
                    moves.Add((y_pivot + 1, x_pivot));
                    moves.Add((y_pivot - 1, x_pivot));
                    moves.Add((y_pivot, x_pivot + 1));
                    moves.Add((y_pivot, x_pivot - 1));
                }
                else
                {
                    if (direction == "up") moves.Add((y_pivot - 1, x_pivot));
                    if (direction == "down") moves.Add((y_pivot + 1, x_pivot));
                    if (direction == "left") moves.Add((y_pivot, x_pivot - 1));
                    if (direction == "right") moves.Add((y_pivot, x_pivot + 1));
                }
            }
            else
            {
                for (int y = 0; y < 6; y++)
                {
                    for (int x = 0; x < 6; x++)
                    {
                        if (board[y, x].Count < 1)
                        {
                            //|| board[y, x][board[y, x].Count - 1].Player == 2
                            moves.Add((y, x));
                        }
                    }
                }
            }

            return moves;
        }

        private int MinimaxWithAlphaBeta(int depth, bool maximizingPlayer, int alpha, int beta)
        {
            if (depth == 0)
            {
                // Evaluate the current state (you need to define your own evaluation function)
                return Evaluate();
            }

            if (maximizingPlayer) // Player 2 (AI)
            {
                int maxEval = int.MinValue;

                foreach (var move in GetPossibleMoves(2))
                {
                    MakeMove(move.Item1, move.Item2, 2);

                    int eval = MinimaxWithAlphaBeta(depth - 1, false, alpha, beta);
                    maxEval = Math.Max(maxEval, eval);

                    UndoMove(move.Item1, move.Item2);

                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                        break; // Beta pruning
                }

                return maxEval;
            }
            else // Player 1
            {
                int minEval = int.MaxValue;

                foreach (var move in GetPossibleMoves(1))
                {
                    MakeMove(move.Item1, move.Item2, 1);

                    int eval = MinimaxWithAlphaBeta(depth - 1, true, alpha, beta);
                    minEval = Math.Min(minEval, eval);

                    UndoMove(move.Item1, move.Item2);

                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                        break; // Alpha pruning
                }

                return minEval;
            }
        }

        public void MakeMove(int y, int x, int player)
        {
            board[y, x].Add(new Stone(player, false, false));
        }

        private void UndoMove(int y, int x)
        {
            board[y, x].RemoveAt(board[y, x].Count - 1);
        }

        private int Evaluate()
        {
            int score = 0;

            // Evaluate the difference in the number of stones
            int stone_p1 = 0;
            int stone_p2 = 0;
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 6; x++)
                {
                    if (board[y, x].Count > 0 && board[y, x][board[y, x].Count - 1].Player == 1)
                        stone_p1++;
                    if (board[y, x].Count > 0 && board[y, x][board[y, x].Count - 1].Player == 2)
                        stone_p2++;
                }
            }
            score += stone_p2 - stone_p1;

            // Evaluate control of the center of the board
            int centerControl = 0;
            for (int y = 2; y <= 3; y++)
            {
                for (int x = 2; x <= 3; x++)
                {
                    if (board[y, x].Count > 0 && board[y, x][board[y, x].Count - 1].Player == 1)
                        centerControl--;
                    if (board[y, x].Count > 0 && board[y, x][board[y, x].Count - 1].Player == 2)
                        centerControl++;
                }
            }
            score += centerControl;

            //if (!earlyStep_p2)
            //{
            //    CheckWin();
            //    if (p1_win) score -= 100;
            //    if (p2_win) score += 100;
            //    p1_win = false;
            //    p2_win = false;
            //}

            return score;
        }
    }
}