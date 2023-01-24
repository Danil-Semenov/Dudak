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
            this.takeCardsBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.endRoundBtn = new System.Windows.Forms.Button();
            this.IamZone = new System.Windows.Forms.GroupBox();
            this.Gamer1Zone = new System.Windows.Forms.GroupBox();
            this.Gamer2Zone = new System.Windows.Forms.GroupBox();
            this.Gamer3Zone = new System.Windows.Forms.GroupBox();
            this.Gamer4Zone = new System.Windows.Forms.GroupBox();
            this.Gamer5Zone = new System.Windows.Forms.GroupBox();
            this.GameField = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DeckZone = new System.Windows.Forms.GroupBox();
            this.DeckBack = new System.Windows.Forms.PictureBox();
            this.TrumpCard = new System.Windows.Forms.PictureBox();
            this.TrumpImage = new System.Windows.Forms.PictureBox();
            this.gameStateTb = new System.Windows.Forms.TextBox();
            this.newGameToolStripMenuItem = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.DeckZone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeckBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrumpCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrumpImage)).BeginInit();
            this.SuspendLayout();
            // 
            // takeCardsBtn
            // 
            this.takeCardsBtn.Location = new System.Drawing.Point(9, 143);
            this.takeCardsBtn.Name = "takeCardsBtn";
            this.takeCardsBtn.Size = new System.Drawing.Size(64, 20);
            this.takeCardsBtn.TabIndex = 1;
            this.takeCardsBtn.Text = "Беру";
            this.takeCardsBtn.UseVisualStyleBackColor = true;
            this.takeCardsBtn.Click += new System.EventHandler(this.takeCardsBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 21);
            this.button1.TabIndex = 0;
            this.button1.Text = "Выйти из комнаты";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // endRoundBtn
            // 
            this.endRoundBtn.Location = new System.Drawing.Point(9, 117);
            this.endRoundBtn.Name = "endRoundBtn";
            this.endRoundBtn.Size = new System.Drawing.Size(64, 20);
            this.endRoundBtn.TabIndex = 2;
            this.endRoundBtn.Text = "Отбой";
            this.endRoundBtn.UseVisualStyleBackColor = true;
            this.endRoundBtn.Click += new System.EventHandler(this.endRoundBtn_Click);
            // 
            // IamZone
            // 
            this.IamZone.Location = new System.Drawing.Point(382, 493);
            this.IamZone.Name = "IamZone";
            this.IamZone.Size = new System.Drawing.Size(713, 177);
            this.IamZone.TabIndex = 3;
            this.IamZone.TabStop = false;
            this.IamZone.Text = "Мои карты";
            // 
            // Gamer1Zone
            // 
            this.Gamer1Zone.Location = new System.Drawing.Point(12, 363);
            this.Gamer1Zone.Name = "Gamer1Zone";
            this.Gamer1Zone.Size = new System.Drawing.Size(177, 307);
            this.Gamer1Zone.TabIndex = 4;
            this.Gamer1Zone.TabStop = false;
            this.Gamer1Zone.Text = "Игрок 1";
            // 
            // Gamer2Zone
            // 
            this.Gamer2Zone.BackColor = System.Drawing.Color.SaddleBrown;
            this.Gamer2Zone.Location = new System.Drawing.Point(10, 37);
            this.Gamer2Zone.Name = "Gamer2Zone";
            this.Gamer2Zone.Size = new System.Drawing.Size(177, 307);
            this.Gamer2Zone.TabIndex = 5;
            this.Gamer2Zone.TabStop = false;
            this.Gamer2Zone.Text = "Игрок2";
            // 
            // Gamer3Zone
            // 
            this.Gamer3Zone.Location = new System.Drawing.Point(382, 10);
            this.Gamer3Zone.Name = "Gamer3Zone";
            this.Gamer3Zone.Size = new System.Drawing.Size(713, 120);
            this.Gamer3Zone.TabIndex = 6;
            this.Gamer3Zone.TabStop = false;
            this.Gamer3Zone.Text = "Игрок 3";
            // 
            // Gamer4Zone
            // 
            this.Gamer4Zone.Location = new System.Drawing.Point(1251, 37);
            this.Gamer4Zone.Name = "Gamer4Zone";
            this.Gamer4Zone.Size = new System.Drawing.Size(175, 307);
            this.Gamer4Zone.TabIndex = 7;
            this.Gamer4Zone.TabStop = false;
            this.Gamer4Zone.Text = "Игрок 4";
            // 
            // Gamer5Zone
            // 
            this.Gamer5Zone.Location = new System.Drawing.Point(1249, 363);
            this.Gamer5Zone.Name = "Gamer5Zone";
            this.Gamer5Zone.Size = new System.Drawing.Size(177, 307);
            this.Gamer5Zone.TabIndex = 8;
            this.Gamer5Zone.TabStop = false;
            this.Gamer5Zone.Text = "Игрок 5";
            // 
            // GameField
            // 
            this.GameField.Location = new System.Drawing.Point(193, 136);
            this.GameField.Name = "GameField";
            this.GameField.Size = new System.Drawing.Size(527, 351);
            this.GameField.TabIndex = 9;
            this.GameField.TabStop = false;
            this.GameField.Text = "Стол";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DeckZone);
            this.groupBox1.Controls.Add(this.gameStateTb);
            this.groupBox1.Location = new System.Drawing.Point(726, 136);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(517, 351);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Игра";
            // 
            // DeckZone
            // 
            this.DeckZone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.DeckZone.Controls.Add(this.DeckBack);
            this.DeckZone.Controls.Add(this.TrumpCard);
            this.DeckZone.Controls.Add(this.endRoundBtn);
            this.DeckZone.Controls.Add(this.TrumpImage);
            this.DeckZone.Controls.Add(this.takeCardsBtn);
            this.DeckZone.Location = new System.Drawing.Point(6, 45);
            this.DeckZone.Name = "DeckZone";
            this.DeckZone.Size = new System.Drawing.Size(414, 300);
            this.DeckZone.TabIndex = 12;
            this.DeckZone.TabStop = false;
            this.DeckZone.Text = "Колода";
            // 
            // DeckBack
            // 
            this.DeckBack.Location = new System.Drawing.Point(139, 10);
            this.DeckBack.Name = "DeckBack";
            this.DeckBack.Size = new System.Drawing.Size(269, 284);
            this.DeckBack.TabIndex = 10;
            this.DeckBack.TabStop = false;
            // 
            // TrumpCard
            // 
            this.TrumpCard.Location = new System.Drawing.Point(79, 10);
            this.TrumpCard.Name = "TrumpCard";
            this.TrumpCard.Size = new System.Drawing.Size(94, 284);
            this.TrumpCard.TabIndex = 13;
            this.TrumpCard.TabStop = false;
            // 
            // TrumpImage
            // 
            this.TrumpImage.Location = new System.Drawing.Point(79, 10);
            this.TrumpImage.Name = "TrumpImage";
            this.TrumpImage.Size = new System.Drawing.Size(295, 284);
            this.TrumpImage.TabIndex = 14;
            this.TrumpImage.TabStop = false;
            // 
            // gameStateTb
            // 
            this.gameStateTb.BackColor = System.Drawing.SystemColors.Control;
            this.gameStateTb.Location = new System.Drawing.Point(6, 19);
            this.gameStateTb.Name = "gameStateTb";
            this.gameStateTb.Size = new System.Drawing.Size(505, 20);
            this.gameStateTb.TabIndex = 11;
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Location = new System.Drawing.Point(131, 11);
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(116, 20);
            this.newGameToolStripMenuItem.TabIndex = 12;
            this.newGameToolStripMenuItem.Text = "Начать игру";
            this.newGameToolStripMenuItem.UseVisualStyleBackColor = true;
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.button2_Click);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SaddleBrown;
            this.BackgroundImage = global::Client.Pictures.cardWallpaper;
            this.ClientSize = new System.Drawing.Size(1438, 706);
            this.Controls.Add(this.newGameToolStripMenuItem);
            this.Controls.Add(this.GameField);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.DeckZone.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DeckBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrumpCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrumpImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button endRoundBtn;
        private System.Windows.Forms.GroupBox IamZone;
        private System.Windows.Forms.GroupBox Gamer1Zone;
        private System.Windows.Forms.GroupBox Gamer2Zone;
        private System.Windows.Forms.GroupBox Gamer3Zone;
        private System.Windows.Forms.GroupBox Gamer4Zone;
        private System.Windows.Forms.GroupBox Gamer5Zone;
        private System.Windows.Forms.GroupBox GameField;
        private System.Windows.Forms.PictureBox DeckBack;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox gameStateTb;
        private System.Windows.Forms.Button newGameToolStripMenuItem;
        private System.Windows.Forms.GroupBox DeckZone;
        private System.Windows.Forms.PictureBox TrumpCard;
        private System.Windows.Forms.PictureBox TrumpImage;
        private System.Windows.Forms.Button takeCardsBtn;
    }
}