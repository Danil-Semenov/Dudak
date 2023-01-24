namespace Client
{
    partial class Game
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
            System.Windows.Forms.Button takeCardsBtn;
            this.button1 = new System.Windows.Forms.Button();
            this.endRoundBtn = new System.Windows.Forms.Button();
            this.IamZone = new System.Windows.Forms.GroupBox();
            this.Gamer1Zone = new System.Windows.Forms.GroupBox();
            this.Gamer2Zone = new System.Windows.Forms.GroupBox();
            this.Gamer3Zone = new System.Windows.Forms.GroupBox();
            this.Gamer4Zone = new System.Windows.Forms.GroupBox();
            this.Gamer5Zone = new System.Windows.Forms.GroupBox();
            this.test = new System.Windows.Forms.GroupBox();
            this.DeckBack = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.newGameToolStripMenuItem = new System.Windows.Forms.Button();
            takeCardsBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DeckBack)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // takeCardsBtn
            // 
            takeCardsBtn.Location = new System.Drawing.Point(84, 217);
            takeCardsBtn.Name = "takeCardsBtn";
            takeCardsBtn.Size = new System.Drawing.Size(75, 23);
            takeCardsBtn.TabIndex = 1;
            takeCardsBtn.Text = "Беру";
            takeCardsBtn.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 24);
            this.button1.TabIndex = 0;
            this.button1.Text = "Выйти из комнаты";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // endRoundBtn
            // 
            this.endRoundBtn.Location = new System.Drawing.Point(84, 246);
            this.endRoundBtn.Name = "endRoundBtn";
            this.endRoundBtn.Size = new System.Drawing.Size(75, 23);
            this.endRoundBtn.TabIndex = 2;
            this.endRoundBtn.Text = "Отбой";
            this.endRoundBtn.UseVisualStyleBackColor = true;
            this.endRoundBtn.Click += new System.EventHandler(this.endRoundBtn_Click);
            // 
            // IamZone
            // 
            this.IamZone.Location = new System.Drawing.Point(308, 512);
            this.IamZone.Name = "IamZone";
            this.IamZone.Size = new System.Drawing.Size(445, 100);
            this.IamZone.TabIndex = 3;
            this.IamZone.TabStop = false;
            this.IamZone.Text = "Мои карты";
            // 
            // Gamer1Zone
            // 
            this.Gamer1Zone.Location = new System.Drawing.Point(12, 414);
            this.Gamer1Zone.Name = "Gamer1Zone";
            this.Gamer1Zone.Size = new System.Drawing.Size(108, 198);
            this.Gamer1Zone.TabIndex = 4;
            this.Gamer1Zone.TabStop = false;
            this.Gamer1Zone.Text = "Игрок 1";
            // 
            // Gamer2Zone
            // 
            this.Gamer2Zone.Location = new System.Drawing.Point(12, 79);
            this.Gamer2Zone.Name = "Gamer2Zone";
            this.Gamer2Zone.Size = new System.Drawing.Size(108, 198);
            this.Gamer2Zone.TabIndex = 5;
            this.Gamer2Zone.TabStop = false;
            this.Gamer2Zone.Text = "Игрок2";
            // 
            // Gamer3Zone
            // 
            this.Gamer3Zone.Location = new System.Drawing.Point(308, 42);
            this.Gamer3Zone.Name = "Gamer3Zone";
            this.Gamer3Zone.Size = new System.Drawing.Size(445, 100);
            this.Gamer3Zone.TabIndex = 6;
            this.Gamer3Zone.TabStop = false;
            this.Gamer3Zone.Text = "Игрок 3";
            // 
            // Gamer4Zone
            // 
            this.Gamer4Zone.Location = new System.Drawing.Point(921, 79);
            this.Gamer4Zone.Name = "Gamer4Zone";
            this.Gamer4Zone.Size = new System.Drawing.Size(114, 198);
            this.Gamer4Zone.TabIndex = 7;
            this.Gamer4Zone.TabStop = false;
            this.Gamer4Zone.Text = "Игрок 4";
            // 
            // Gamer5Zone
            // 
            this.Gamer5Zone.Location = new System.Drawing.Point(921, 414);
            this.Gamer5Zone.Name = "Gamer5Zone";
            this.Gamer5Zone.Size = new System.Drawing.Size(108, 198);
            this.Gamer5Zone.TabIndex = 8;
            this.Gamer5Zone.TabStop = false;
            this.Gamer5Zone.Text = "Игрок 5";
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(205, 159);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(460, 287);
            this.test.TabIndex = 9;
            this.test.TabStop = false;
            this.test.Text = "Стол";
            // 
            // DeckBack
            // 
            this.DeckBack.Location = new System.Drawing.Point(6, 22);
            this.DeckBack.Name = "DeckBack";
            this.DeckBack.Size = new System.Drawing.Size(221, 96);
            this.DeckBack.TabIndex = 10;
            this.DeckBack.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.DeckBack);
            this.groupBox1.Controls.Add(this.endRoundBtn);
            this.groupBox1.Controls.Add(takeCardsBtn);
            this.groupBox1.Location = new System.Drawing.Point(671, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 287);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Игра";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 136);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(221, 23);
            this.textBox1.TabIndex = 11;
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Location = new System.Drawing.Point(153, 13);
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(135, 23);
            this.newGameToolStripMenuItem.TabIndex = 12;
            this.newGameToolStripMenuItem.Text = "Начать игру";
            this.newGameToolStripMenuItem.UseVisualStyleBackColor = true;
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.button2_Click);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 624);
            this.Controls.Add(this.newGameToolStripMenuItem);
            this.Controls.Add(this.test);
            this.Controls.Add(this.Gamer5Zone);
            this.Controls.Add(this.Gamer4Zone);
            this.Controls.Add(this.Gamer3Zone);
            this.Controls.Add(this.Gamer2Zone);
            this.Controls.Add(this.Gamer1Zone);
            this.Controls.Add(this.IamZone);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Game";
            this.Text = "Game";
            ((System.ComponentModel.ISupportInitialize)(this.DeckBack)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button takeCardsBtn;
        private System.Windows.Forms.Button endRoundBtn;
        private System.Windows.Forms.GroupBox IamZone;
        private System.Windows.Forms.GroupBox Gamer1Zone;
        private System.Windows.Forms.GroupBox Gamer2Zone;
        private System.Windows.Forms.GroupBox Gamer3Zone;
        private System.Windows.Forms.GroupBox Gamer4Zone;
        private System.Windows.Forms.GroupBox Gamer5Zone;
        private System.Windows.Forms.GroupBox test;
        private System.Windows.Forms.PictureBox DeckBack;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button newGameToolStripMenuItem;
    }
}