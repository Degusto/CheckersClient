namespace CheckersClient
{
    partial class MainForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.enterGameButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.offerDrawButton = new System.Windows.Forms.Button();
            this.gameIdTextBox = new System.Windows.Forms.TextBox();
            this.leaveGameButton = new System.Windows.Forms.Button();
            this.hostRadioButton = new System.Windows.Forms.RadioButton();
            this.guestRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.surrenderButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.currentPlayerLabel = new System.Windows.Forms.Label();
            this.hostPawnCountLabel = new System.Windows.Forms.Label();
            this.guestPawnCountLabel = new System.Windows.Forms.Label();
            this.gameTimeLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.remainingTimeLabel = new System.Windows.Forms.Label();
            this.gameBoardTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.endRoundButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(842, 451);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.endRoundButton);
            this.groupBox1.Controls.Add(this.gameBoardTableLayoutPanel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.tableLayoutPanel1.SetRowSpan(this.groupBox1, 2);
            this.groupBox1.Size = new System.Drawing.Size(586, 445);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Plansza";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Identyfikator gry:";
            // 
            // enterGameButton
            // 
            this.enterGameButton.Location = new System.Drawing.Point(20, 72);
            this.enterGameButton.Name = "enterGameButton";
            this.enterGameButton.Size = new System.Drawing.Size(215, 23);
            this.enterGameButton.TabIndex = 1;
            this.enterGameButton.Text = "Załóż nowy pokój/Dołącz do pokoju";
            this.enterGameButton.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.remainingTimeLabel);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.gameTimeLabel);
            this.groupBox3.Controls.Add(this.guestPawnCountLabel);
            this.groupBox3.Controls.Add(this.hostPawnCountLabel);
            this.groupBox3.Controls.Add(this.currentPlayerLabel);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.surrenderButton);
            this.groupBox3.Controls.Add(this.offerDrawButton);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(595, 153);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(244, 295);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Informacje o grze";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(475, 191);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Poddaj grę";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // offerDrawButton
            // 
            this.offerDrawButton.Location = new System.Drawing.Point(6, 266);
            this.offerDrawButton.Name = "offerDrawButton";
            this.offerDrawButton.Size = new System.Drawing.Size(100, 23);
            this.offerDrawButton.TabIndex = 3;
            this.offerDrawButton.Text = "Zaproponuj remis";
            this.offerDrawButton.UseVisualStyleBackColor = true;
            // 
            // gameIdTextBox
            // 
            this.gameIdTextBox.Location = new System.Drawing.Point(108, 46);
            this.gameIdTextBox.Name = "gameIdTextBox";
            this.gameIdTextBox.ReadOnly = true;
            this.gameIdTextBox.Size = new System.Drawing.Size(127, 20);
            this.gameIdTextBox.TabIndex = 3;
            this.gameIdTextBox.Text = "DUPA123";
            // 
            // leaveGameButton
            // 
            this.leaveGameButton.Location = new System.Drawing.Point(20, 101);
            this.leaveGameButton.Name = "leaveGameButton";
            this.leaveGameButton.Size = new System.Drawing.Size(215, 23);
            this.leaveGameButton.TabIndex = 4;
            this.leaveGameButton.Text = "Zamknij pokój/Opuść pokój";
            this.leaveGameButton.UseVisualStyleBackColor = true;
            // 
            // hostRadioButton
            // 
            this.hostRadioButton.AutoSize = true;
            this.hostRadioButton.Location = new System.Drawing.Point(103, 22);
            this.hostRadioButton.Name = "hostRadioButton";
            this.hostRadioButton.Size = new System.Drawing.Size(76, 17);
            this.hostRadioButton.TabIndex = 5;
            this.hostRadioButton.TabStop = true;
            this.hostRadioButton.Text = "Gospodarz";
            this.hostRadioButton.UseVisualStyleBackColor = true;
            // 
            // guestRadioButton
            // 
            this.guestRadioButton.AutoSize = true;
            this.guestRadioButton.Location = new System.Drawing.Point(185, 22);
            this.guestRadioButton.Name = "guestRadioButton";
            this.guestRadioButton.Size = new System.Drawing.Size(50, 17);
            this.guestRadioButton.TabIndex = 6;
            this.guestRadioButton.TabStop = true;
            this.guestRadioButton.Text = "Gość";
            this.guestRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.leaveGameButton);
            this.groupBox2.Controls.Add(this.gameIdTextBox);
            this.groupBox2.Controls.Add(this.enterGameButton);
            this.groupBox2.Controls.Add(this.guestRadioButton);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.hostRadioButton);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(595, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(244, 144);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rozgrywka";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Rodzaj klienta:";
            // 
            // surrenderButton
            // 
            this.surrenderButton.Location = new System.Drawing.Point(139, 266);
            this.surrenderButton.Name = "surrenderButton";
            this.surrenderButton.Size = new System.Drawing.Size(99, 23);
            this.surrenderButton.TabIndex = 4;
            this.surrenderButton.Text = "Poddaj grę";
            this.surrenderButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Biały - ilość pionków:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Czarny - ilość pionków:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Ruch:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Czas rozgrywki:";
            // 
            // currentPlayerLabel
            // 
            this.currentPlayerLabel.AutoSize = true;
            this.currentPlayerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.currentPlayerLabel.ForeColor = System.Drawing.Color.Black;
            this.currentPlayerLabel.Location = new System.Drawing.Point(63, 75);
            this.currentPlayerLabel.Name = "currentPlayerLabel";
            this.currentPlayerLabel.Size = new System.Drawing.Size(43, 13);
            this.currentPlayerLabel.TabIndex = 9;
            this.currentPlayerLabel.Text = "BIAŁY";
            // 
            // hostPawnCountLabel
            // 
            this.hostPawnCountLabel.AutoSize = true;
            this.hostPawnCountLabel.Location = new System.Drawing.Point(134, 28);
            this.hostPawnCountLabel.Name = "hostPawnCountLabel";
            this.hostPawnCountLabel.Size = new System.Drawing.Size(35, 13);
            this.hostPawnCountLabel.TabIndex = 10;
            this.hostPawnCountLabel.Text = "label7";
            // 
            // guestPawnCountLabel
            // 
            this.guestPawnCountLabel.AutoSize = true;
            this.guestPawnCountLabel.Location = new System.Drawing.Point(142, 52);
            this.guestPawnCountLabel.Name = "guestPawnCountLabel";
            this.guestPawnCountLabel.Size = new System.Drawing.Size(35, 13);
            this.guestPawnCountLabel.TabIndex = 11;
            this.guestPawnCountLabel.Text = "label8";
            // 
            // gameTimeLabel
            // 
            this.gameTimeLabel.AutoSize = true;
            this.gameTimeLabel.Location = new System.Drawing.Point(108, 97);
            this.gameTimeLabel.Name = "gameTimeLabel";
            this.gameTimeLabel.Size = new System.Drawing.Size(35, 13);
            this.gameTimeLabel.TabIndex = 12;
            this.gameTimeLabel.Text = "label9";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(24, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(211, 23);
            this.label10.TabIndex = 13;
            this.label10.Text = "CZAS NA RUCH";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // remainingTimeLabel
            // 
            this.remainingTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.remainingTimeLabel.Location = new System.Drawing.Point(24, 160);
            this.remainingTimeLabel.Name = "remainingTimeLabel";
            this.remainingTimeLabel.Size = new System.Drawing.Size(211, 23);
            this.remainingTimeLabel.TabIndex = 14;
            this.remainingTimeLabel.Text = "00:15";
            this.remainingTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gameBoardTableLayoutPanel
            // 
            this.gameBoardTableLayoutPanel.ColumnCount = 1;
            this.gameBoardTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gameBoardTableLayoutPanel.Location = new System.Drawing.Point(9, 19);
            this.gameBoardTableLayoutPanel.Name = "gameBoardTableLayoutPanel";
            this.gameBoardTableLayoutPanel.RowCount = 1;
            this.gameBoardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.gameBoardTableLayoutPanel.Size = new System.Drawing.Size(571, 388);
            this.gameBoardTableLayoutPanel.TabIndex = 0;
            // 
            // endRoundButton
            // 
            this.endRoundButton.Location = new System.Drawing.Point(492, 413);
            this.endRoundButton.Name = "endRoundButton";
            this.endRoundButton.Size = new System.Drawing.Size(88, 23);
            this.endRoundButton.TabIndex = 1;
            this.endRoundButton.Text = "Zakończ ruch";
            this.endRoundButton.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 451);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "mainForm";
            this.Text = "CheckersClient";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox gameIdTextBox;
        private System.Windows.Forms.Button enterGameButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button offerDrawButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button leaveGameButton;
        private System.Windows.Forms.RadioButton guestRadioButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton hostRadioButton;
        private System.Windows.Forms.Button surrenderButton;
        private System.Windows.Forms.Button endRoundButton;
        private System.Windows.Forms.TableLayoutPanel gameBoardTableLayoutPanel;
        private System.Windows.Forms.Label remainingTimeLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label gameTimeLabel;
        private System.Windows.Forms.Label guestPawnCountLabel;
        private System.Windows.Forms.Label hostPawnCountLabel;
        private System.Windows.Forms.Label currentPlayerLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}

