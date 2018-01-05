namespace CheckersCommon.Views
{
    partial class MainView
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
            this.gameBoardTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.guestPawnCountLabel = new System.Windows.Forms.Label();
            this.hostPawnCountLabel = new System.Windows.Forms.Label();
            this.currentPlayerLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.surrenderButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.startGameButton = new System.Windows.Forms.Button();
            this.leaveGameButton = new System.Windows.Forms.Button();
            this.roomIdTextBox = new System.Windows.Forms.TextBox();
            this.enterGameButton = new System.Windows.Forms.Button();
            this.guestRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.hostRadioButton = new System.Windows.Forms.RadioButton();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(842, 451);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
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
            // gameBoardTableLayoutPanel
            // 
            this.gameBoardTableLayoutPanel.ColumnCount = 8;
            this.gameBoardTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameBoardTableLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.gameBoardTableLayoutPanel.Name = "gameBoardTableLayoutPanel";
            this.gameBoardTableLayoutPanel.RowCount = 8;
            this.gameBoardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.gameBoardTableLayoutPanel.Size = new System.Drawing.Size(580, 426);
            this.gameBoardTableLayoutPanel.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.guestPawnCountLabel);
            this.groupBox3.Controls.Add(this.hostPawnCountLabel);
            this.groupBox3.Controls.Add(this.currentPlayerLabel);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.surrenderButton);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(595, 203);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(244, 245);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Informacje o grze";
            // 
            // guestPawnCountLabel
            // 
            this.guestPawnCountLabel.AutoSize = true;
            this.guestPawnCountLabel.Location = new System.Drawing.Point(134, 52);
            this.guestPawnCountLabel.Name = "guestPawnCountLabel";
            this.guestPawnCountLabel.Size = new System.Drawing.Size(13, 13);
            this.guestPawnCountLabel.TabIndex = 11;
            this.guestPawnCountLabel.Text = "0";
            // 
            // hostPawnCountLabel
            // 
            this.hostPawnCountLabel.AutoSize = true;
            this.hostPawnCountLabel.Location = new System.Drawing.Point(134, 28);
            this.hostPawnCountLabel.Name = "hostPawnCountLabel";
            this.hostPawnCountLabel.Size = new System.Drawing.Size(13, 13);
            this.hostPawnCountLabel.TabIndex = 10;
            this.hostPawnCountLabel.Text = "0";
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Ruch:";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Biały - ilość pionków:";
            // 
            // surrenderButton
            // 
            this.surrenderButton.Location = new System.Drawing.Point(139, 216);
            this.surrenderButton.Name = "surrenderButton";
            this.surrenderButton.Size = new System.Drawing.Size(99, 23);
            this.surrenderButton.TabIndex = 4;
            this.surrenderButton.Text = "Poddaj grę";
            this.surrenderButton.UseVisualStyleBackColor = true;
            this.surrenderButton.Click += new System.EventHandler(this.OnSurrenderButtonClick);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.startGameButton);
            this.groupBox2.Controls.Add(this.leaveGameButton);
            this.groupBox2.Controls.Add(this.roomIdTextBox);
            this.groupBox2.Controls.Add(this.enterGameButton);
            this.groupBox2.Controls.Add(this.guestRadioButton);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.hostRadioButton);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(595, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(244, 194);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rozgrywka";
            // 
            // startGameButton
            // 
            this.startGameButton.Location = new System.Drawing.Point(20, 165);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(215, 23);
            this.startGameButton.TabIndex = 7;
            this.startGameButton.Text = "Nowa gra";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.OnStartGameButtonClick);
            // 
            // leaveGameButton
            // 
            this.leaveGameButton.Location = new System.Drawing.Point(20, 101);
            this.leaveGameButton.Name = "leaveGameButton";
            this.leaveGameButton.Size = new System.Drawing.Size(215, 23);
            this.leaveGameButton.TabIndex = 4;
            this.leaveGameButton.Text = "Zamknij pokój/Opuść pokój";
            this.leaveGameButton.UseVisualStyleBackColor = true;
            this.leaveGameButton.Click += new System.EventHandler(this.OnLeaveGameButtonClick);
            // 
            // roomIdTextBox
            // 
            this.roomIdTextBox.Location = new System.Drawing.Point(108, 46);
            this.roomIdTextBox.Name = "roomIdTextBox";
            this.roomIdTextBox.ReadOnly = true;
            this.roomIdTextBox.Size = new System.Drawing.Size(127, 20);
            this.roomIdTextBox.TabIndex = 3;
            // 
            // enterGameButton
            // 
            this.enterGameButton.Location = new System.Drawing.Point(20, 72);
            this.enterGameButton.Name = "enterGameButton";
            this.enterGameButton.Size = new System.Drawing.Size(215, 23);
            this.enterGameButton.TabIndex = 1;
            this.enterGameButton.Text = "Załóż nowy pokój/Dołącz do pokoju";
            this.enterGameButton.UseVisualStyleBackColor = true;
            this.enterGameButton.Click += new System.EventHandler(this.OnEnterGameButtonClick);
            // 
            // guestRadioButton
            // 
            this.guestRadioButton.AutoSize = true;
            this.guestRadioButton.Location = new System.Drawing.Point(185, 22);
            this.guestRadioButton.Name = "guestRadioButton";
            this.guestRadioButton.Size = new System.Drawing.Size(50, 17);
            this.guestRadioButton.TabIndex = 6;
            this.guestRadioButton.Text = "Gość";
            this.guestRadioButton.UseVisualStyleBackColor = true;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Rodzaj klienta:";
            // 
            // hostRadioButton
            // 
            this.hostRadioButton.AutoSize = true;
            this.hostRadioButton.Location = new System.Drawing.Point(103, 22);
            this.hostRadioButton.Name = "hostRadioButton";
            this.hostRadioButton.Size = new System.Drawing.Size(76, 17);
            this.hostRadioButton.TabIndex = 5;
            this.hostRadioButton.Text = "Gospodarz";
            this.hostRadioButton.UseVisualStyleBackColor = true;
            this.hostRadioButton.CheckedChanged += new System.EventHandler(this.OnHostRadioButtonCheckedChanged);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 451);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "MainView";
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
        private System.Windows.Forms.TextBox roomIdTextBox;
        private System.Windows.Forms.Button enterGameButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button leaveGameButton;
        private System.Windows.Forms.RadioButton guestRadioButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton hostRadioButton;
        private System.Windows.Forms.Button surrenderButton;
        private System.Windows.Forms.TableLayoutPanel gameBoardTableLayoutPanel;
        private System.Windows.Forms.Label guestPawnCountLabel;
        private System.Windows.Forms.Label hostPawnCountLabel;
        private System.Windows.Forms.Label currentPlayerLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button startGameButton;
    }
}

