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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(420, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 65);
            this.label1.TabIndex = 0;
            this.label1.Text = "TAK";
            // 
            // panelBoard
            // 
            this.panelBoard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBoard.Location = new System.Drawing.Point(175, 150);
            this.panelBoard.Name = "panelBoard";
            this.panelBoard.Size = new System.Drawing.Size(600, 600);
            this.panelBoard.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(52, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Player 1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(827, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "Player 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(451, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Turn :";
            // 
            // lblWhosTurn
            // 
            this.lblWhosTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWhosTurn.Location = new System.Drawing.Point(395, 110);
            this.lblWhosTurn.Name = "lblWhosTurn";
            this.lblWhosTurn.Size = new System.Drawing.Size(164, 23);
            this.lblWhosTurn.TabIndex = 5;
            this.lblWhosTurn.Text = "Player 1";
            this.lblWhosTurn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_stone_p1
            // 
            this.button_stone_p1.BackColor = System.Drawing.Color.LightCoral;
            this.button_stone_p1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_stone_p1.Location = new System.Drawing.Point(54, 217);
            this.button_stone_p1.Name = "button_stone_p1";
            this.button_stone_p1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.button_stone_p1.Size = new System.Drawing.Size(75, 75);
            this.button_stone_p1.TabIndex = 6;
            this.button_stone_p1.Text = "30";
            this.button_stone_p1.UseVisualStyleBackColor = false;
            // 
            // button_stone_p2
            // 
            this.button_stone_p2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button_stone_p2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_stone_p2.Location = new System.Drawing.Point(831, 217);
            this.button_stone_p2.Name = "button_stone_p2";
            this.button_stone_p2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.button_stone_p2.Size = new System.Drawing.Size(75, 75);
            this.button_stone_p2.TabIndex = 7;
            this.button_stone_p2.Text = "30";
            this.button_stone_p2.UseVisualStyleBackColor = false;
            // 
            // button_caps_p1
            // 
            this.button_caps_p1.BackColor = System.Drawing.Color.LightCoral;
            this.button_caps_p1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_caps_p1.Location = new System.Drawing.Point(54, 333);
            this.button_caps_p1.Name = "button_caps_p1";
            this.button_caps_p1.Size = new System.Drawing.Size(75, 75);
            this.button_caps_p1.TabIndex = 8;
            this.button_caps_p1.Text = "O";
            this.button_caps_p1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_caps_p1.UseVisualStyleBackColor = false;
            // 
            // button_caps_p2
            // 
            this.button_caps_p2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button_caps_p2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_caps_p2.Location = new System.Drawing.Point(831, 333);
            this.button_caps_p2.Name = "button_caps_p2";
            this.button_caps_p2.Size = new System.Drawing.Size(75, 75);
            this.button_caps_p2.TabIndex = 9;
            this.button_caps_p2.Text = "O";
            this.button_caps_p2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_caps_p2.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 761);
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
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TAK";
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
    }
}

