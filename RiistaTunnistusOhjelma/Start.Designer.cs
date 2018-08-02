namespace RiistaTunnistusOhjelma {
	partial class RiistaTunnistusOhjelma {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.info = new System.Windows.Forms.GroupBox();
			this.infoBox = new System.Windows.Forms.TextBox();
			this.settings = new System.Windows.Forms.GroupBox();
			this.optionsPanel = new System.Windows.Forms.Panel();
			this.settingsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.classSelectLabel = new System.Windows.Forms.Label();
			this.settingsClasses = new System.Windows.Forms.CheckedListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.settingsTimeout = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.settingsQuestions = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.settingsChoices = new System.Windows.Forms.NumericUpDown();
			this.settingsFromSameClass = new System.Windows.Forms.CheckBox();
			this.settingsAllowDuplicates = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.settingsMultichoice = new System.Windows.Forms.RadioButton();
			this.settingsFreeInput = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.startGameButton = new System.Windows.Forms.Button();
			this.clearDataButton = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.info.SuspendLayout();
			this.settings.SuspendLayout();
			this.optionsPanel.SuspendLayout();
			this.settingsFlowPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.settingsTimeout)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingsQuestions)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.settingsChoices)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.info, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.settings, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// info
			// 
			this.info.Controls.Add(this.infoBox);
			this.info.Dock = System.Windows.Forms.DockStyle.Fill;
			this.info.Location = new System.Drawing.Point(3, 3);
			this.info.Name = "info";
			this.info.Size = new System.Drawing.Size(394, 294);
			this.info.TabIndex = 1;
			this.info.TabStop = false;
			this.info.Text = "Tietoja";
			// 
			// infoBox
			// 
			this.infoBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.infoBox.Location = new System.Drawing.Point(3, 16);
			this.infoBox.Multiline = true;
			this.infoBox.Name = "infoBox";
			this.infoBox.ReadOnly = true;
			this.infoBox.Size = new System.Drawing.Size(388, 275);
			this.infoBox.TabIndex = 0;
			this.infoBox.Text = "Ladataan tietoja...";
			// 
			// settings
			// 
			this.settings.Controls.Add(this.optionsPanel);
			this.settings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.settings.Location = new System.Drawing.Point(403, 3);
			this.settings.Name = "settings";
			this.settings.Size = new System.Drawing.Size(394, 294);
			this.settings.TabIndex = 2;
			this.settings.TabStop = false;
			this.settings.Text = "Asetukset";
			// 
			// optionsPanel
			// 
			this.optionsPanel.AutoScroll = true;
			this.optionsPanel.Controls.Add(this.settingsFlowPanel);
			this.optionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.optionsPanel.Location = new System.Drawing.Point(3, 16);
			this.optionsPanel.Name = "optionsPanel";
			this.optionsPanel.Size = new System.Drawing.Size(388, 275);
			this.optionsPanel.TabIndex = 0;
			// 
			// settingsFlowPanel
			// 
			this.settingsFlowPanel.AutoScroll = true;
			this.settingsFlowPanel.Controls.Add(this.classSelectLabel);
			this.settingsFlowPanel.Controls.Add(this.settingsClasses);
			this.settingsFlowPanel.Controls.Add(this.label1);
			this.settingsFlowPanel.Controls.Add(this.settingsTimeout);
			this.settingsFlowPanel.Controls.Add(this.label2);
			this.settingsFlowPanel.Controls.Add(this.settingsQuestions);
			this.settingsFlowPanel.Controls.Add(this.label3);
			this.settingsFlowPanel.Controls.Add(this.settingsChoices);
			this.settingsFlowPanel.Controls.Add(this.settingsFromSameClass);
			this.settingsFlowPanel.Controls.Add(this.settingsAllowDuplicates);
			this.settingsFlowPanel.Controls.Add(this.groupBox2);
			this.settingsFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.settingsFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.settingsFlowPanel.Location = new System.Drawing.Point(0, 0);
			this.settingsFlowPanel.Name = "settingsFlowPanel";
			this.settingsFlowPanel.Size = new System.Drawing.Size(388, 275);
			this.settingsFlowPanel.TabIndex = 0;
			this.settingsFlowPanel.WrapContents = false;
			// 
			// classSelectLabel
			// 
			this.classSelectLabel.AutoSize = true;
			this.classSelectLabel.Location = new System.Drawing.Point(3, 0);
			this.classSelectLabel.Name = "classSelectLabel";
			this.classSelectLabel.Size = new System.Drawing.Size(148, 13);
			this.classSelectLabel.TabIndex = 1;
			this.classSelectLabel.Text = "Valitse peliin arvottavat luokat";
			// 
			// settingsClasses
			// 
			this.settingsClasses.Dock = System.Windows.Forms.DockStyle.Fill;
			this.settingsClasses.FormattingEnabled = true;
			this.settingsClasses.Location = new System.Drawing.Point(3, 16);
			this.settingsClasses.Name = "settingsClasses";
			this.settingsClasses.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.settingsClasses.Size = new System.Drawing.Size(204, 96);
			this.settingsClasses.TabIndex = 0;
			this.settingsClasses.MouseUp += new System.Windows.Forms.MouseEventHandler(this.settingsClasses_MouseUp);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 115);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(204, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Aikaraja sekunneissa per kuva (0 ei rajaa)";
			// 
			// settingsTimeout
			// 
			this.settingsTimeout.Location = new System.Drawing.Point(3, 131);
			this.settingsTimeout.Name = "settingsTimeout";
			this.settingsTimeout.Size = new System.Drawing.Size(120, 20);
			this.settingsTimeout.TabIndex = 2;
			this.settingsTimeout.ValueChanged += new System.EventHandler(this.settingsTimeout_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 154);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Kuvia yhteensä";
			// 
			// settingsQuestions
			// 
			this.settingsQuestions.Location = new System.Drawing.Point(3, 170);
			this.settingsQuestions.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.settingsQuestions.Name = "settingsQuestions";
			this.settingsQuestions.Size = new System.Drawing.Size(120, 20);
			this.settingsQuestions.TabIndex = 5;
			this.settingsQuestions.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.settingsQuestions.ValueChanged += new System.EventHandler(this.settingsQuestions_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 193);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(110, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Arvottuja vaihtoehtoja";
			// 
			// settingsChoices
			// 
			this.settingsChoices.Location = new System.Drawing.Point(3, 209);
			this.settingsChoices.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.settingsChoices.Name = "settingsChoices";
			this.settingsChoices.Size = new System.Drawing.Size(120, 20);
			this.settingsChoices.TabIndex = 9;
			this.settingsChoices.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.settingsChoices.ValueChanged += new System.EventHandler(this.settingsChoices_ValueChanged);
			// 
			// settingsFromSameClass
			// 
			this.settingsFromSameClass.AutoSize = true;
			this.settingsFromSameClass.Location = new System.Drawing.Point(3, 235);
			this.settingsFromSameClass.Name = "settingsFromSameClass";
			this.settingsFromSameClass.Size = new System.Drawing.Size(168, 17);
			this.settingsFromSameClass.TabIndex = 7;
			this.settingsFromSameClass.Text = "Vaihtoehdot samasta luokasta";
			this.settingsFromSameClass.UseVisualStyleBackColor = false;
			this.settingsFromSameClass.CheckedChanged += new System.EventHandler(this.settingsFromSameClass_CheckedChanged);
			// 
			// settingsAllowDuplicates
			// 
			this.settingsAllowDuplicates.AutoSize = true;
			this.settingsAllowDuplicates.Location = new System.Drawing.Point(3, 258);
			this.settingsAllowDuplicates.Name = "settingsAllowDuplicates";
			this.settingsAllowDuplicates.Size = new System.Drawing.Size(195, 17);
			this.settingsAllowDuplicates.TabIndex = 10;
			this.settingsAllowDuplicates.Text = "Salli sama eläin useampaan kertaan";
			this.settingsAllowDuplicates.UseVisualStyleBackColor = true;
			this.settingsAllowDuplicates.CheckedChanged += new System.EventHandler(this.settingsAllowDuplicates_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.AutoSize = true;
			this.groupBox2.Controls.Add(this.flowLayoutPanel1);
			this.groupBox2.Location = new System.Drawing.Point(3, 281);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(101, 65);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Vastaustyyli";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Controls.Add(this.settingsMultichoice);
			this.flowLayoutPanel1.Controls.Add(this.settingsFreeInput);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(95, 46);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// settingsMultichoice
			// 
			this.settingsMultichoice.AutoSize = true;
			this.settingsMultichoice.Location = new System.Drawing.Point(3, 3);
			this.settingsMultichoice.Name = "settingsMultichoice";
			this.settingsMultichoice.Size = new System.Drawing.Size(79, 17);
			this.settingsMultichoice.TabIndex = 0;
			this.settingsMultichoice.TabStop = true;
			this.settingsMultichoice.Text = "Monivalinta";
			this.settingsMultichoice.UseVisualStyleBackColor = true;
			this.settingsMultichoice.CheckedChanged += new System.EventHandler(this.settingsMultichoice_CheckedChanged);
			// 
			// settingsFreeInput
			// 
			this.settingsFreeInput.AutoSize = true;
			this.settingsFreeInput.Location = new System.Drawing.Point(3, 26);
			this.settingsFreeInput.Name = "settingsFreeInput";
			this.settingsFreeInput.Size = new System.Drawing.Size(89, 17);
			this.settingsFreeInput.TabIndex = 1;
			this.settingsFreeInput.TabStop = true;
			this.settingsFreeInput.Text = "Vapaa kenttä";
			this.settingsFreeInput.UseVisualStyleBackColor = true;
			this.settingsFreeInput.CheckedChanged += new System.EventHandler(this.settingsFreeInput_CheckedChanged);
			// 
			// groupBox1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
			this.groupBox1.Controls.Add(this.tableLayoutPanel2);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 303);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(794, 144);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Toiminnot";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel2.Controls.Add(this.startGameButton, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.clearDataButton, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.button3, 2, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(788, 125);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// startGameButton
			// 
			this.startGameButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.startGameButton.Location = new System.Drawing.Point(3, 3);
			this.startGameButton.Name = "startGameButton";
			this.startGameButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.startGameButton.Size = new System.Drawing.Size(256, 119);
			this.startGameButton.TabIndex = 0;
			this.startGameButton.Text = "Aloita";
			this.startGameButton.UseVisualStyleBackColor = true;
			this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
			// 
			// clearDataButton
			// 
			this.clearDataButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.clearDataButton.Location = new System.Drawing.Point(265, 3);
			this.clearDataButton.Name = "clearDataButton";
			this.clearDataButton.Size = new System.Drawing.Size(256, 119);
			this.clearDataButton.TabIndex = 1;
			this.clearDataButton.Text = "Tyhjennä tiedot";
			this.clearDataButton.UseVisualStyleBackColor = true;
			this.clearDataButton.Click += new System.EventHandler(this.clearDataButton_Click);
			// 
			// button3
			// 
			this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button3.Location = new System.Drawing.Point(527, 3);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(258, 119);
			this.button3.TabIndex = 2;
			this.button3.Text = "🅱️";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// RiistaTunnistusOhjelma
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "RiistaTunnistusOhjelma";
			this.Text = "RiistaTunnistusOhjelma";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.info.ResumeLayout(false);
			this.info.PerformLayout();
			this.settings.ResumeLayout(false);
			this.optionsPanel.ResumeLayout(false);
			this.settingsFlowPanel.ResumeLayout(false);
			this.settingsFlowPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.settingsTimeout)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingsQuestions)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.settingsChoices)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox info;
		private System.Windows.Forms.GroupBox settings;
		private System.Windows.Forms.Panel optionsPanel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button startGameButton;
		private System.Windows.Forms.Button clearDataButton;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.FlowLayoutPanel settingsFlowPanel;
		private System.Windows.Forms.CheckedListBox settingsClasses;
		private System.Windows.Forms.Label classSelectLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown settingsTimeout;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown settingsQuestions;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.RadioButton settingsMultichoice;
		private System.Windows.Forms.RadioButton settingsFreeInput;
		private System.Windows.Forms.CheckBox settingsFromSameClass;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown settingsChoices;
		private System.Windows.Forms.TextBox infoBox;
		private System.Windows.Forms.CheckBox settingsAllowDuplicates;
	}
}

