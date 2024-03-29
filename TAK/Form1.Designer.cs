﻿namespace TAK
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.panelBoard = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblWhosTurn = new System.Windows.Forms.Label();
            this.button_stone_p1 = new System.Windows.Forms.Button();
            this.button_stone_p2 = new System.Windows.Forms.Button();
            this.button_caps_p1 = new System.Windows.Forms.Button();
            this.button_caps_p2 = new System.Windows.Forms.Button();
            this.p1_status = new System.Windows.Forms.Label();
            this.p2_status = new System.Windows.Forms.Label();
            this.timerAI = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(560, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 81);
            this.label1.TabIndex = 0;
            this.label1.Text = "TAK";
            // 
            // panelBoard
            // 
            this.panelBoard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBoard.Location = new System.Drawing.Point(233, 185);
            this.panelBoard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelBoard.Name = "panelBoard";
            this.panelBoard.Size = new System.Drawing.Size(799, 738);
            this.panelBoard.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(55, 400);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 30);
            this.label2.TabIndex = 2;
            this.label2.Text = "Player 1";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1080, 400);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 30);
            this.label3.TabIndex = 3;
            this.label3.Text = "Player 2";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(601, 91);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "Turn :";
            // 
            // lblWhosTurn
            // 
            this.lblWhosTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWhosTurn.Location = new System.Drawing.Point(527, 135);
            this.lblWhosTurn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWhosTurn.Name = "lblWhosTurn";
            this.lblWhosTurn.Size = new System.Drawing.Size(219, 28);
            this.lblWhosTurn.TabIndex = 5;
            this.lblWhosTurn.Text = "Player 1";
            this.lblWhosTurn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_stone_p1
            // 
            this.button_stone_p1.BackColor = System.Drawing.Color.LightCoral;
            this.button_stone_p1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_stone_p1.Location = new System.Drawing.Point(72, 482);
            this.button_stone_p1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_stone_p1.Name = "button_stone_p1";
            this.button_stone_p1.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.button_stone_p1.Size = new System.Drawing.Size(100, 92);
            this.button_stone_p1.TabIndex = 6;
            this.button_stone_p1.Text = "30";
            this.button_stone_p1.UseVisualStyleBackColor = false;
            this.button_stone_p1.Click += new System.EventHandler(this.Button_stone_p1_Click);
            // 
            // button_stone_p2
            // 
            this.button_stone_p2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button_stone_p2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_stone_p2.Location = new System.Drawing.Point(1095, 482);
            this.button_stone_p2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_stone_p2.Name = "button_stone_p2";
            this.button_stone_p2.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.button_stone_p2.Size = new System.Drawing.Size(100, 92);
            this.button_stone_p2.TabIndex = 7;
            this.button_stone_p2.Text = "30";
            this.button_stone_p2.UseVisualStyleBackColor = false;
            this.button_stone_p2.Click += new System.EventHandler(this.Button_stone_p2_Click);
            // 
            // button_caps_p1
            // 
            this.button_caps_p1.BackColor = System.Drawing.Color.LightCoral;
            this.button_caps_p1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_caps_p1.FlatAppearance.BorderSize = 6;
            this.button_caps_p1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_caps_p1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_caps_p1.Location = new System.Drawing.Point(72, 625);
            this.button_caps_p1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_caps_p1.Name = "button_caps_p1";
            this.button_caps_p1.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.button_caps_p1.Size = new System.Drawing.Size(100, 92);
            this.button_caps_p1.TabIndex = 8;
            this.button_caps_p1.Text = "O";
            this.button_caps_p1.UseVisualStyleBackColor = false;
            this.button_caps_p1.Click += new System.EventHandler(this.Button_caps_p1_Click);
            // 
            // button_caps_p2
            // 
            this.button_caps_p2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button_caps_p2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button_caps_p2.FlatAppearance.BorderSize = 6;
            this.button_caps_p2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_caps_p2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_caps_p2.Location = new System.Drawing.Point(1095, 625);
            this.button_caps_p2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_caps_p2.Name = "button_caps_p2";
            this.button_caps_p2.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.button_caps_p2.Size = new System.Drawing.Size(100, 92);
            this.button_caps_p2.TabIndex = 9;
            this.button_caps_p2.Text = "O";
            this.button_caps_p2.UseVisualStyleBackColor = false;
            this.button_caps_p2.Click += new System.EventHandler(this.Button_caps_p2_Click);
            // 
            // p1_status
            // 
            this.p1_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p1_status.Location = new System.Drawing.Point(21, 745);
            this.p1_status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.p1_status.Name = "p1_status";
            this.p1_status.Size = new System.Drawing.Size(200, 74);
            this.p1_status.TabIndex = 10;
            this.p1_status.Text = "Capstones";
            this.p1_status.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // p2_status
            // 
            this.p2_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.p2_status.Location = new System.Drawing.Point(1045, 745);
            this.p2_status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.p2_status.Name = "p2_status";
            this.p2_status.Size = new System.Drawing.Size(200, 74);
            this.p2_status.TabIndex = 11;
            this.p2_status.Text = "Capstones";
            this.p2_status.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timerAI
            // 
            this.timerAI.Tick += new System.EventHandler(this.TimerAI_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 937);
            this.Controls.Add(this.p2_status);
            this.Controls.Add(this.p1_status);
            this.Controls.Add(this.button_caps_p2);
            this.Controls.Add(this.button_caps_p1);
            this.Controls.Add(this.button_stone_p2);
            this.Controls.Add(this.button_stone_p1);
            this.Controls.Add(this.lblWhosTurn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelBoard);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TAK";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelBoard;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblWhosTurn;
        private System.Windows.Forms.Button button_stone_p1;
        private System.Windows.Forms.Button button_stone_p2;
        private System.Windows.Forms.Button button_caps_p1;
        private System.Windows.Forms.Button button_caps_p2;
        private System.Windows.Forms.Label p1_status;
        private System.Windows.Forms.Label p2_status;
        private System.Windows.Forms.Timer timerAI;
    }
}

