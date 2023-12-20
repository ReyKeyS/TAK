namespace TAK
{
    partial class ChooseMode
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
            this.Player = new System.Windows.Forms.Button();
            this.AI = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Player
            // 
            this.Player.Location = new System.Drawing.Point(141, 179);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(100, 100);
            this.Player.TabIndex = 0;
            this.Player.Text = "Player";
            this.Player.UseVisualStyleBackColor = true;
            this.Player.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Player_MouseClick);
            // 
            // AI
            // 
            this.AI.Location = new System.Drawing.Point(505, 179);
            this.AI.Name = "AI";
            this.AI.Size = new System.Drawing.Size(100, 100);
            this.AI.TabIndex = 1;
            this.AI.Text = "Bot (AI)";
            this.AI.UseVisualStyleBackColor = true;
            this.AI.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AI_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(299, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Choose enemy";
            // 
            // ChooseMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AI);
            this.Controls.Add(this.Player);
            this.Name = "ChooseMode";
            this.Text = "ChooseMode";
            this.Load += new System.EventHandler(this.ChooseMode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Player;
        private System.Windows.Forms.Button AI;
        private System.Windows.Forms.Label label1;
    }
}