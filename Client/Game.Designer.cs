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
            this.GameField = new System.Windows.Forms.GroupBox();
            this.DeckBack = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gameStateTb = new System.Windows.Forms.TextBox();
            this.newGameToolStripMenuItem = new System.Windows.Forms.Button();
            this.DeckZone = new System.Windows.Forms.GroupBox();
            this.TrumpCard = new System.Windows.Forms.PictureBox();
            this.TrumpImage = new System.Windows.Forms.PictureBox();
            takeCardsBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DeckBack)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.DeckZone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrumpCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrumpImage)).BeginInit();
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
            // GameField
            // 
            this.GameField.Location = new System.Drawing.Point(205, 159);
            this.GameField.Name = "GameField";
            this.GameField.Size = new System.Drawing.Size(460, 287);
            this.GameField.TabIndex = 9;
            this.GameField.TabStop = false;
            this.GameField.Text = "Стол";
            // 
            // DeckBack
            // 
            this.DeckBack.Location = new System.Drawing.Point(59, 22);
            this.DeckBack.Name = "DeckBack";
            this.DeckBack.Size = new System.Drawing.Size(162, 127);
            this.DeckBack.TabIndex = 10;
            this.DeckBack.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DeckZone);
            this.groupBox1.Controls.Add(this.gameStateTb);
            this.groupBox1.Controls.Add(this.endRoundBtn);
            this.groupBox1.Controls.Add(takeCardsBtn);
            this.groupBox1.Location = new System.Drawing.Point(671, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 287);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Игра";
            // 
            // gameStateTb
            // 
            this.gameStateTb.BackColor = System.Drawing.SystemColors.Control;
            this.gameStateTb.Location = new System.Drawing.Point(6, 22);
            this.gameStateTb.Name = "gameStateTb";
            this.gameStateTb.Size = new System.Drawing.Size(221, 23);
            this.gameStateTb.TabIndex = 11;
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
            // DeckZone
            // 
            this.DeckZone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.DeckZone.Controls.Add(this.DeckBack);
            this.DeckZone.Controls.Add(this.TrumpCard);
            this.DeckZone.Controls.Add(this.TrumpImage);
            this.DeckZone.Location = new System.Drawing.Point(6, 62);
            this.DeckZone.Name = "DeckZone";
            this.DeckZone.Size = new System.Drawing.Size(221, 149);
            this.DeckZone.TabIndex = 12;
            this.DeckZone.TabStop = false;
            this.DeckZone.Text = "Колода";
            // 
            // TrumpCard
            // 
            this.TrumpCard.Location = new System.Drawing.Point(0, 22);
            this.TrumpCard.Name = "TrumpCard";
            this.TrumpCard.Size = new System.Drawing.Size(90, 127);
            this.TrumpCard.TabIndex = 13;
            this.TrumpCard.TabStop = false;
            // 
            // TrumpImage
            // 
            this.TrumpImage.Location = new System.Drawing.Point(94, 62);
            this.TrumpImage.Name = "TrumpImage";
            this.TrumpImage.Size = new System.Drawing.Size(40, 40);
            this.TrumpImage.TabIndex = 14;
            this.TrumpImage.TabStop = false;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 624);
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
            ((System.ComponentModel.ISupportInitialize)(this.DeckBack)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.DeckZone.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TrumpCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrumpImage)).EndInit();
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
        private System.Windows.Forms.GroupBox GameField;
        private System.Windows.Forms.PictureBox DeckBack;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox gameStateTb;
        private System.Windows.Forms.Button newGameToolStripMenuItem;
        private System.Windows.Forms.GroupBox DeckZone;
        private System.Windows.Forms.PictureBox TrumpCard;
        private System.Windows.Forms.PictureBox TrumpImage;
    }
}