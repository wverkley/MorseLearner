using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Wxv.MorseLearner
{
	/// <summary>
	/// Summary description for OptionsForm.
	/// </summary>
	public class OptionsForm : System.Windows.Forms.Form
	{
		public const string TestWord = "QST";

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TrackBar frequencyTrackBar;
		private System.Windows.Forms.CheckBox useComCheckBox;
		private System.Windows.Forms.CheckBox useMouseCheckbox;
		private System.Windows.Forms.CheckBox useKeyboardCheckbox;
		private System.Windows.Forms.RadioButton advancedRadioButton;
		private System.Windows.Forms.RadioButton intermediateRadioButton;
		private System.Windows.Forms.RadioButton learnerRadioButton;
		private System.Windows.Forms.TrackBar overallWpmTrackBar;
		private System.Windows.Forms.TrackBar characterWpmTrackBar;
		private System.Windows.Forms.Label frequencyLabel;
		private System.Windows.Forms.Label overallWpmLabel;
		private System.Windows.Forms.Label characterWpmLabel;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.ComboBox comDevicePinComboBox;
		private System.Windows.Forms.ComboBox comDeviceComboBox;
		private System.Windows.Forms.ComboBox mouseComboBox;
		private System.Windows.Forms.ComboBox keyboardComboBox;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.CheckBox useSendKeys;
        private CheckBox showMorseCheckBox;
		private System.ComponentModel.IContainer components;

		public OptionsForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.frequencyTrackBar = new System.Windows.Forms.TrackBar();
            this.frequencyLabel = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.useSendKeys = new System.Windows.Forms.CheckBox();
            this.comDevicePinComboBox = new System.Windows.Forms.ComboBox();
            this.comDeviceComboBox = new System.Windows.Forms.ComboBox();
            this.mouseComboBox = new System.Windows.Forms.ComboBox();
            this.keyboardComboBox = new System.Windows.Forms.ComboBox();
            this.useComCheckBox = new System.Windows.Forms.CheckBox();
            this.useMouseCheckbox = new System.Windows.Forms.CheckBox();
            this.useKeyboardCheckbox = new System.Windows.Forms.CheckBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.advancedRadioButton = new System.Windows.Forms.RadioButton();
            this.intermediateRadioButton = new System.Windows.Forms.RadioButton();
            this.learnerRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.overallWpmLabel = new System.Windows.Forms.Label();
            this.characterWpmLabel = new System.Windows.Forms.Label();
            this.overallWpmTrackBar = new System.Windows.Forms.TrackBar();
            this.characterWpmTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.showMorseCheckBox = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.overallWpmTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.characterWpmTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(8, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(448, 442);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(440, 416);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.frequencyTrackBar);
            this.groupBox4.Controls.Add(this.frequencyLabel);
            this.groupBox4.Controls.Add(this.pictureBox2);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox4.Location = new System.Drawing.Point(8, 217);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(424, 64);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Sound";
            // 
            // frequencyTrackBar
            // 
            this.frequencyTrackBar.LargeChange = 100;
            this.frequencyTrackBar.Location = new System.Drawing.Point(128, 16);
            this.frequencyTrackBar.Maximum = 2500;
            this.frequencyTrackBar.Minimum = 500;
            this.frequencyTrackBar.Name = "frequencyTrackBar";
            this.frequencyTrackBar.Size = new System.Drawing.Size(240, 45);
            this.frequencyTrackBar.SmallChange = 25;
            this.frequencyTrackBar.TabIndex = 1;
            this.frequencyTrackBar.TickFrequency = 50;
            this.frequencyTrackBar.Value = 500;
            this.frequencyTrackBar.ValueChanged += new System.EventHandler(this.frequencyTrackBar_ValueChanged);
            // 
            // frequencyLabel
            // 
            this.frequencyLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.frequencyLabel.Location = new System.Drawing.Point(368, 24);
            this.frequencyLabel.Name = "frequencyLabel";
            this.frequencyLabel.Size = new System.Drawing.Size(48, 23);
            this.frequencyLabel.TabIndex = 2;
            this.frequencyLabel.Text = "925 Hz";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 20);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 32);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(64, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Frequency:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.useSendKeys);
            this.groupBox3.Controls.Add(this.comDevicePinComboBox);
            this.groupBox3.Controls.Add(this.comDeviceComboBox);
            this.groupBox3.Controls.Add(this.mouseComboBox);
            this.groupBox3.Controls.Add(this.keyboardComboBox);
            this.groupBox3.Controls.Add(this.useComCheckBox);
            this.groupBox3.Controls.Add(this.useMouseCheckbox);
            this.groupBox3.Controls.Add(this.useKeyboardCheckbox);
            this.groupBox3.Controls.Add(this.pictureBox3);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox3.Location = new System.Drawing.Point(8, 288);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(424, 120);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Input";
            // 
            // useSendKeys
            // 
            this.useSendKeys.ForeColor = System.Drawing.SystemColors.ControlText;
            this.useSendKeys.Location = new System.Drawing.Point(64, 88);
            this.useSendKeys.Name = "useSendKeys";
            this.useSendKeys.Size = new System.Drawing.Size(264, 24);
            this.useSendKeys.TabIndex = 7;
            this.useSendKeys.Text = "Send Input to External Application";
            // 
            // comDevicePinComboBox
            // 
            this.comDevicePinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comDevicePinComboBox.Location = new System.Drawing.Point(280, 64);
            this.comDevicePinComboBox.Name = "comDevicePinComboBox";
            this.comDevicePinComboBox.Size = new System.Drawing.Size(96, 21);
            this.comDevicePinComboBox.TabIndex = 6;
            // 
            // comDeviceComboBox
            // 
            this.comDeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comDeviceComboBox.Location = new System.Drawing.Point(176, 64);
            this.comDeviceComboBox.Name = "comDeviceComboBox";
            this.comDeviceComboBox.Size = new System.Drawing.Size(96, 21);
            this.comDeviceComboBox.TabIndex = 5;
            // 
            // mouseComboBox
            // 
            this.mouseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mouseComboBox.Location = new System.Drawing.Point(176, 40);
            this.mouseComboBox.Name = "mouseComboBox";
            this.mouseComboBox.Size = new System.Drawing.Size(168, 21);
            this.mouseComboBox.TabIndex = 3;
            // 
            // keyboardComboBox
            // 
            this.keyboardComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.keyboardComboBox.Location = new System.Drawing.Point(176, 16);
            this.keyboardComboBox.Name = "keyboardComboBox";
            this.keyboardComboBox.Size = new System.Drawing.Size(168, 21);
            this.keyboardComboBox.TabIndex = 1;
            // 
            // useComCheckBox
            // 
            this.useComCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.useComCheckBox.Location = new System.Drawing.Point(64, 64);
            this.useComCheckBox.Name = "useComCheckBox";
            this.useComCheckBox.Size = new System.Drawing.Size(104, 24);
            this.useComCheckBox.TabIndex = 4;
            this.useComCheckBox.Text = "COM Device";
            this.useComCheckBox.CheckedChanged += new System.EventHandler(this.useComCheckBox_CheckedChanged);
            // 
            // useMouseCheckbox
            // 
            this.useMouseCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.useMouseCheckbox.Location = new System.Drawing.Point(64, 40);
            this.useMouseCheckbox.Name = "useMouseCheckbox";
            this.useMouseCheckbox.Size = new System.Drawing.Size(104, 24);
            this.useMouseCheckbox.TabIndex = 2;
            this.useMouseCheckbox.Text = "Mouse";
            this.useMouseCheckbox.CheckedChanged += new System.EventHandler(this.useMouseCheckbox_CheckedChanged);
            // 
            // useKeyboardCheckbox
            // 
            this.useKeyboardCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.useKeyboardCheckbox.Location = new System.Drawing.Point(64, 16);
            this.useKeyboardCheckbox.Name = "useKeyboardCheckbox";
            this.useKeyboardCheckbox.Size = new System.Drawing.Size(104, 24);
            this.useKeyboardCheckbox.TabIndex = 0;
            this.useKeyboardCheckbox.Text = "Key Board";
            this.useKeyboardCheckbox.CheckedChanged += new System.EventHandler(this.useKeyboardCheckbox_CheckedChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(7, 20);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(48, 32);
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.showMorseCheckBox);
            this.groupBox2.Controls.Add(this.advancedRadioButton);
            this.groupBox2.Controls.Add(this.intermediateRadioButton);
            this.groupBox2.Controls.Add(this.learnerRadioButton);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox2.Location = new System.Drawing.Point(8, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(424, 74);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Difficulty";
            // 
            // advancedRadioButton
            // 
            this.advancedRadioButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.advancedRadioButton.Location = new System.Drawing.Point(264, 20);
            this.advancedRadioButton.Name = "advancedRadioButton";
            this.advancedRadioButton.Size = new System.Drawing.Size(88, 16);
            this.advancedRadioButton.TabIndex = 2;
            this.advancedRadioButton.Text = "Advanced";
            // 
            // intermediateRadioButton
            // 
            this.intermediateRadioButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.intermediateRadioButton.Location = new System.Drawing.Point(152, 20);
            this.intermediateRadioButton.Name = "intermediateRadioButton";
            this.intermediateRadioButton.Size = new System.Drawing.Size(88, 16);
            this.intermediateRadioButton.TabIndex = 1;
            this.intermediateRadioButton.Text = "Intermediate";
            // 
            // learnerRadioButton
            // 
            this.learnerRadioButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.learnerRadioButton.Location = new System.Drawing.Point(64, 20);
            this.learnerRadioButton.Name = "learnerRadioButton";
            this.learnerRadioButton.Size = new System.Drawing.Size(88, 16);
            this.learnerRadioButton.TabIndex = 0;
            this.learnerRadioButton.Text = "Learner";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.overallWpmLabel);
            this.groupBox1.Controls.Add(this.characterWpmLabel);
            this.groupBox1.Controls.Add(this.overallWpmTrackBar);
            this.groupBox1.Controls.Add(this.characterWpmTrackBar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(424, 120);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Morse Speed";
            // 
            // overallWpmLabel
            // 
            this.overallWpmLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.overallWpmLabel.Location = new System.Drawing.Point(368, 76);
            this.overallWpmLabel.Name = "overallWpmLabel";
            this.overallWpmLabel.Size = new System.Drawing.Size(48, 23);
            this.overallWpmLabel.TabIndex = 5;
            this.overallWpmLabel.Text = "5 wpm";
            // 
            // characterWpmLabel
            // 
            this.characterWpmLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.characterWpmLabel.Location = new System.Drawing.Point(368, 24);
            this.characterWpmLabel.Name = "characterWpmLabel";
            this.characterWpmLabel.Size = new System.Drawing.Size(48, 23);
            this.characterWpmLabel.TabIndex = 2;
            this.characterWpmLabel.Text = "5 wpm";
            // 
            // overallWpmTrackBar
            // 
            this.overallWpmTrackBar.Location = new System.Drawing.Point(128, 68);
            this.overallWpmTrackBar.Maximum = 40;
            this.overallWpmTrackBar.Minimum = 5;
            this.overallWpmTrackBar.Name = "overallWpmTrackBar";
            this.overallWpmTrackBar.Size = new System.Drawing.Size(240, 45);
            this.overallWpmTrackBar.TabIndex = 4;
            this.overallWpmTrackBar.Value = 5;
            this.overallWpmTrackBar.ValueChanged += new System.EventHandler(this.overallWpmTrackBar_ValueChanged);
            // 
            // characterWpmTrackBar
            // 
            this.characterWpmTrackBar.Location = new System.Drawing.Point(128, 16);
            this.characterWpmTrackBar.Maximum = 40;
            this.characterWpmTrackBar.Minimum = 5;
            this.characterWpmTrackBar.Name = "characterWpmTrackBar";
            this.characterWpmTrackBar.Size = new System.Drawing.Size(240, 45);
            this.characterWpmTrackBar.TabIndex = 1;
            this.characterWpmTrackBar.Value = 5;
            this.characterWpmTrackBar.ValueChanged += new System.EventHandler(this.characterWpmTrackBar_ValueChanged);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(64, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Character:";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(64, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Overall:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(56, 56);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // okButton
            // 
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.Location = new System.Drawing.Point(300, 456);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "&OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelButton.Location = new System.Drawing.Point(380, 456);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // timer
            // 
            this.timer.Interval = 50;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // showMorseCheckBox
            // 
            this.showMorseCheckBox.AutoSize = true;
            this.showMorseCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.showMorseCheckBox.Location = new System.Drawing.Point(64, 48);
            this.showMorseCheckBox.Name = "showMorseCheckBox";
            this.showMorseCheckBox.Size = new System.Drawing.Size(127, 17);
            this.showMorseCheckBox.TabIndex = 3;
            this.showMorseCheckBox.Text = "Show Morse Symbols";
            this.showMorseCheckBox.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(466, 486);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OptionsForm_Closing);
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.frequencyTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.overallWpmTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.characterWpmTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private bool playSound;
		private DateTime playSoundStart;
		private int oldFrequency;

		private MorseLearnerConfig config;
		public MorseLearnerConfig Config
		{
			get { return config; }
			set { config = value; }
		}

		private MorsePlayer morsePlayer;
		public MorsePlayer MorsePlayer
		{
			get { return morsePlayer; }
			set { morsePlayer = value; }
		}

		private void WriteConfigToUI()
		{
			characterWpmTrackBar.Minimum = MorseLearnerConfig.MinWpm;
			characterWpmTrackBar.Maximum = MorseLearnerConfig.MaxWpm;
			characterWpmTrackBar.TickFrequency = MorseLearnerConfig.StepWpm;
			characterWpmTrackBar.SmallChange = MorseLearnerConfig.StepWpm;
			characterWpmTrackBar.LargeChange = MorseLearnerConfig.StepWpm * 5;
			characterWpmTrackBar.Value = config.Wpm;

			overallWpmTrackBar.Minimum = MorseLearnerConfig.MinWpm;
			overallWpmTrackBar.Maximum = MorseLearnerConfig.MaxWpm;
			overallWpmTrackBar.TickFrequency = MorseLearnerConfig.StepWpm;
			overallWpmTrackBar.SmallChange = MorseLearnerConfig.StepWpm;
			overallWpmTrackBar.LargeChange = MorseLearnerConfig.StepWpm * 5;
			overallWpmTrackBar.Value = config.OverallWpm;

			if (config.Difficulty == MorseLearnerConfig.DifficultyEnum.Learner)
				learnerRadioButton.Checked = true;
			else if (config.Difficulty == MorseLearnerConfig.DifficultyEnum.Intermediate)
				intermediateRadioButton.Checked = true;
			else if (config.Difficulty == MorseLearnerConfig.DifficultyEnum.Advanced)
				advancedRadioButton.Checked = true;

            showMorseCheckBox.Checked = config.ShowMorse;

			frequencyTrackBar.Minimum = MorseLearnerConfig.MinFrequency;
			frequencyTrackBar.Maximum = MorseLearnerConfig.MaxFrequency;
			frequencyTrackBar.TickFrequency = MorseLearnerConfig.StepFrequency * 2;
			frequencyTrackBar.SmallChange = MorseLearnerConfig.StepFrequency;
			frequencyTrackBar.LargeChange = MorseLearnerConfig.StepFrequency * 5;
			frequencyTrackBar.Value = config.Frequency;

			useKeyboardCheckbox.Checked = config.UseKeyboard;
			
			keyboardComboBox.Items.Clear();
			keyboardComboBox.Items.AddRange (MorseLearnerConfig.InputKeyNames);
			keyboardComboBox.SelectedIndex =
				Array.IndexOf (MorseLearnerConfig.InputKeys, config.Key);
			keyboardComboBox.Enabled = useKeyboardCheckbox.Checked;

			useMouseCheckbox.Checked = config.UseMouse;

			mouseComboBox.Items.Clear();
			mouseComboBox.Items.AddRange (MorseLearnerConfig.InputMouseNames);
			mouseComboBox.SelectedIndex =
				Array.IndexOf (MorseLearnerConfig.InputMouseButtons, config.MouseButton);
			mouseComboBox.Enabled = useMouseCheckbox.Checked;

			useComCheckBox.Checked = config.UseComDevice;

			comDeviceComboBox.Items.Clear();
			comDeviceComboBox.Items.AddRange (Enum.GetNames (typeof (ComDeviceEnum)));
			comDeviceComboBox.SelectedIndex =
				Array.IndexOf (Enum.GetValues (typeof (ComDeviceEnum)), config.ComDevice);
			comDeviceComboBox.Enabled = useComCheckBox.Checked;

			comDevicePinComboBox.Items.Clear();
			comDevicePinComboBox.Items.AddRange (Enum.GetNames (typeof (ComDevicePinEnum)));
			comDevicePinComboBox.SelectedIndex =
				Array.IndexOf (Enum.GetValues (typeof (ComDevicePinEnum)), config.ComDevicePin);
			comDevicePinComboBox.Enabled = useComCheckBox.Checked;

			useSendKeys.Checked = config.SendKeys;
		}

		private void ReadConfigFromUI()
		{
			config.Wpm = characterWpmTrackBar.Value;
			config.OverallWpm = overallWpmTrackBar.Value;
			if (learnerRadioButton.Checked)
				config.Difficulty = MorseLearnerConfig.DifficultyEnum.Learner;
			else if (intermediateRadioButton.Checked)
				config.Difficulty = MorseLearnerConfig.DifficultyEnum.Intermediate;
			else if (advancedRadioButton.Checked)
				config.Difficulty = MorseLearnerConfig.DifficultyEnum.Advanced;
            config.ShowMorse = showMorseCheckBox.Checked;
            config.Frequency = frequencyTrackBar.Value;
			config.UseKeyboard = useKeyboardCheckbox.Checked;
			config.Key = MorseLearnerConfig.InputKeys [keyboardComboBox.SelectedIndex];
			config.UseMouse = useMouseCheckbox.Checked;
			config.MouseButton = MorseLearnerConfig.InputMouseButtons [mouseComboBox.SelectedIndex];
			config.UseComDevice = useComCheckBox.Checked;
			config.ComDevice = (ComDeviceEnum) Enum.GetValues (typeof (ComDeviceEnum)).GetValue (comDeviceComboBox.SelectedIndex);
			config.ComDevicePin = (ComDevicePinEnum) Enum.GetValues (typeof (ComDevicePinEnum)).GetValue (comDevicePinComboBox.SelectedIndex);
			config.SendKeys = useSendKeys.Checked;
		}

		private void OptionsForm_Load(object sender, System.EventArgs e)
		{
			WriteConfigToUI();

			oldFrequency = frequencyTrackBar.Value;
			playSound = false;
			timer.Enabled = true;
		}

		private void OptionsForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			timer.Enabled = false;
		}

		private void okButton_Click(object sender, System.EventArgs e)
		{
			ReadConfigFromUI();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void characterWpmTrackBar_ValueChanged(object sender, System.EventArgs e)
		{
			characterWpmLabel.Text = characterWpmTrackBar.Value.ToString() + " Wpm";
            if (overallWpmTrackBar.Value > characterWpmTrackBar.Value)
				overallWpmTrackBar.Value = characterWpmTrackBar.Value;
			
			playSoundStart = DateTime.Now;
			playSound = true;
		}

		private void overallWpmTrackBar_ValueChanged(object sender, System.EventArgs e)
		{
			overallWpmLabel.Text = overallWpmTrackBar.Value.ToString() + " Wpm";
			if (characterWpmTrackBar.Value < overallWpmTrackBar.Value)
				characterWpmTrackBar.Value = overallWpmTrackBar.Value;

			playSoundStart = DateTime.Now;
			playSound = true;
		}

		private void frequencyTrackBar_ValueChanged(object sender, System.EventArgs e)
		{
			frequencyTrackBar.Value -= frequencyTrackBar.Value % MorseLearnerConfig.StepFrequency;
			frequencyLabel.Text = frequencyTrackBar.Value.ToString() + " Hz";

			playSoundStart = DateTime.Now;
			playSound |= (oldFrequency != frequencyTrackBar.Value);
			oldFrequency = frequencyTrackBar.Value;
		}

		private void timer_Tick(object sender, System.EventArgs e)
		{
			if (playSound && ((DateTime.Now - playSoundStart).TotalMilliseconds > 500))
			{
				if (morsePlayer != null) 
				{
					morsePlayer.Stop();
					morsePlayer.Wpm = characterWpmTrackBar.Value;
					morsePlayer.OverallWpm = overallWpmTrackBar.Value;
					morsePlayer.Frequency = frequencyTrackBar.Value;
					morsePlayer.Play (TestWord);
				}

				playSound = false;
			}
		
		}

		private void useKeyboardCheckbox_CheckedChanged(object sender, System.EventArgs e)
		{
			keyboardComboBox.Enabled = useKeyboardCheckbox.Checked;
		}

		private void useMouseCheckbox_CheckedChanged(object sender, System.EventArgs e)
		{
			mouseComboBox.Enabled = useMouseCheckbox.Checked;
		}

		private void useComCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			comDeviceComboBox.Enabled = useComCheckBox.Checked;
			comDevicePinComboBox.Enabled = useComCheckBox.Checked;
		}

	}

}
