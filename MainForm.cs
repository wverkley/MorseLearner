using System;
using System.Drawing;
using System.Drawing.Text;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

namespace Wxv.MorseLearner
{
	public enum MainFormState 
	{ 
		Message_Transmit, 
		Message_TransmitBuffer, 
		Message_Play, 
		Message_EndPlay, 
		
		Receive_BeginSession,
		Receive_Paused, 
		Receive_PlayMorse, 
		Receive_WaitInput, 
		Receive_ShowCorrectResult, 
		Receive_ShowIncorrectResult,
		Receive_EndSession,

		Transmit_BeginSession,
		Transmit_Paused, 
		Transmit_PlayMorse, 
		Transmit_WaitInput, 
		Transmit_ShowCorrectResult, 
		Transmit_ShowIncorrectResult,
		Transmit_EndSession,

		ReceiveWord_BeginSession,
		ReceiveWord_Paused, 
		ReceiveWord_PlayMorse, 
		ReceiveWord_WaitInput, 
		ReceiveWord_ShowCorrectResult, 
		ReceiveWord_ShowIncorrectResult,
		ReceiveWord_EndSession,

		Options_Show
}

	
	public class MainForm : System.Windows.Forms.Form
	{
		private FlickerFreePanel flashPanel;
		private Wxv.MorseLearner.FlickerFreePanel learnStatePanel;
		private Wxv.MorseLearner.FlickerFreePanel morsePanel;
		private System.Windows.Forms.Label morseLabel;
		private Wxv.MorseLearner.FlickerFreePanel resultPanel;
		private System.Windows.Forms.Label flashLabel;
		private System.Windows.Forms.Label resultLabel;
		private Wxv.MorseLearner.FlickerFreePanel messagePanel;
		private System.Windows.Forms.Label messageLabel;
		private Wxv.MorseLearner.FlickerFreePanel sessionPanel;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.MainMenu mainMenu;
		private Wxv.MorseLearner.FlickerFreePanel modePanel;
		private System.Windows.Forms.Label modeLabel;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.PictureBox modeImage;
		private System.Windows.Forms.MenuItem fileMenuItem;
		private System.Windows.Forms.MenuItem exitMenuItem;
		private System.Windows.Forms.MenuItem editMenuItem;
		private System.Windows.Forms.MenuItem modeMenuItem;
		private System.Windows.Forms.MenuItem helpMenuItem;
		private System.Windows.Forms.MenuItem copyMenuItem;
		private System.Windows.Forms.MenuItem pasteMenuItem;
		private System.Windows.Forms.MenuItem messageMenuItem;
		private System.Windows.Forms.MenuItem receiveCharactersMenuItem;
		private System.Windows.Forms.MenuItem sendCharactersMenuItem;
		private System.Windows.Forms.MenuItem receiveWordsMenuItem;
		private System.Windows.Forms.MenuItem aboutMenuItem;
		private System.Windows.Forms.MenuItem contentsMenuItem;
		private System.Windows.Forms.MenuItem optionsMenuItem;
		private System.Windows.Forms.ContextMenu learnStateContextMenu;
		private System.Windows.Forms.MenuItem removeAllContextMenuItem;
		private System.Windows.Forms.MenuItem intermediateContextMenuItem;
		private System.Windows.Forms.MenuItem advancedContextMenuItem;
		private System.Windows.Forms.MenuItem addAllContextMenuItem;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem addAlphabetContextMenuItem;
		private System.Windows.Forms.MenuItem removeAlphabetContextMenuItem;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem addNumericContextMenuItem;
		private System.Windows.Forms.MenuItem removeNumericContextMenuItem;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem addAllMenuItem;
		private System.Windows.Forms.MenuItem removeAllMenuItem;
		private System.Windows.Forms.MenuItem addAlphabetMenuItem;
		private System.Windows.Forms.MenuItem removeAlphabetMenuItem;
		private System.Windows.Forms.MenuItem addNumericMenuItem;
		private System.Windows.Forms.MenuItem removeNumericMenuItem;
		private System.Windows.Forms.MenuItem beginnerMenuItem;
		private System.Windows.Forms.MenuItem intermediateMenuItem;
		private System.Windows.Forms.MenuItem advancedMenuItem;
		private System.Windows.Forms.MenuItem charactersMenuItem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem addPunctuationContextMenuItem;
		private System.Windows.Forms.MenuItem removePunctuationContextMenuItem;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem addPunctuationMenuItem;
		private System.Windows.Forms.MenuItem removePunctuationMenuItem;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem beginnerContextMenuItem;
		private System.ComponentModel.IContainer components;

		public MainForm()
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
				if (components != null) 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.fileMenuItem = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
            this.editMenuItem = new System.Windows.Forms.MenuItem();
            this.copyMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteMenuItem = new System.Windows.Forms.MenuItem();
            this.modeMenuItem = new System.Windows.Forms.MenuItem();
            this.messageMenuItem = new System.Windows.Forms.MenuItem();
            this.receiveCharactersMenuItem = new System.Windows.Forms.MenuItem();
            this.sendCharactersMenuItem = new System.Windows.Forms.MenuItem();
            this.receiveWordsMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.optionsMenuItem = new System.Windows.Forms.MenuItem();
            this.charactersMenuItem = new System.Windows.Forms.MenuItem();
            this.addAllMenuItem = new System.Windows.Forms.MenuItem();
            this.removeAllMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.addAlphabetMenuItem = new System.Windows.Forms.MenuItem();
            this.removeAlphabetMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.addNumericMenuItem = new System.Windows.Forms.MenuItem();
            this.removeNumericMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.addPunctuationMenuItem = new System.Windows.Forms.MenuItem();
            this.removePunctuationMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.beginnerMenuItem = new System.Windows.Forms.MenuItem();
            this.intermediateMenuItem = new System.Windows.Forms.MenuItem();
            this.advancedMenuItem = new System.Windows.Forms.MenuItem();
            this.helpMenuItem = new System.Windows.Forms.MenuItem();
            this.contentsMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.aboutMenuItem = new System.Windows.Forms.MenuItem();
            this.learnStateContextMenu = new System.Windows.Forms.ContextMenu();
            this.addAllContextMenuItem = new System.Windows.Forms.MenuItem();
            this.removeAllContextMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.addAlphabetContextMenuItem = new System.Windows.Forms.MenuItem();
            this.removeAlphabetContextMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.addNumericContextMenuItem = new System.Windows.Forms.MenuItem();
            this.removeNumericContextMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.addPunctuationContextMenuItem = new System.Windows.Forms.MenuItem();
            this.removePunctuationContextMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.beginnerContextMenuItem = new System.Windows.Forms.MenuItem();
            this.intermediateContextMenuItem = new System.Windows.Forms.MenuItem();
            this.advancedContextMenuItem = new System.Windows.Forms.MenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.modePanel = new Wxv.MorseLearner.FlickerFreePanel();
            this.modeLabel = new System.Windows.Forms.Label();
            this.modeImage = new System.Windows.Forms.PictureBox();
            this.sessionPanel = new Wxv.MorseLearner.FlickerFreePanel();
            this.messagePanel = new Wxv.MorseLearner.FlickerFreePanel();
            this.messageLabel = new System.Windows.Forms.Label();
            this.resultPanel = new Wxv.MorseLearner.FlickerFreePanel();
            this.resultLabel = new System.Windows.Forms.Label();
            this.morsePanel = new Wxv.MorseLearner.FlickerFreePanel();
            this.morseLabel = new System.Windows.Forms.Label();
            this.learnStatePanel = new Wxv.MorseLearner.FlickerFreePanel();
            this.flashPanel = new Wxv.MorseLearner.FlickerFreePanel();
            this.flashLabel = new System.Windows.Forms.Label();
            this.modePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modeImage)).BeginInit();
            this.messagePanel.SuspendLayout();
            this.resultPanel.SuspendLayout();
            this.morsePanel.SuspendLayout();
            this.flashPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            this.imageList.Images.SetKeyName(4, "Icon32.png");
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileMenuItem,
            this.editMenuItem,
            this.modeMenuItem,
            this.helpMenuItem});
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.Index = 0;
            this.fileMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.exitMenuItem});
            this.fileMenuItem.Text = "&File";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Index = 0;
            this.exitMenuItem.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // editMenuItem
            // 
            this.editMenuItem.Index = 1;
            this.editMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.copyMenuItem,
            this.pasteMenuItem});
            this.editMenuItem.Text = "&Edit";
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Index = 0;
            this.copyMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.copyMenuItem.Text = "&Copy";
            this.copyMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
            // 
            // pasteMenuItem
            // 
            this.pasteMenuItem.Index = 1;
            this.pasteMenuItem.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
            this.pasteMenuItem.Text = "&Paste";
            this.pasteMenuItem.Click += new System.EventHandler(this.pasteMenuItem_Click);
            // 
            // modeMenuItem
            // 
            this.modeMenuItem.Index = 2;
            this.modeMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.messageMenuItem,
            this.receiveCharactersMenuItem,
            this.sendCharactersMenuItem,
            this.receiveWordsMenuItem,
            this.menuItem15,
            this.optionsMenuItem,
            this.charactersMenuItem});
            this.modeMenuItem.Text = "&Mode";
            // 
            // messageMenuItem
            // 
            this.messageMenuItem.Index = 0;
            this.messageMenuItem.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.messageMenuItem.Text = "&Message";
            this.messageMenuItem.Click += new System.EventHandler(this.messageMenuItem_Click);
            // 
            // receiveCharactersMenuItem
            // 
            this.receiveCharactersMenuItem.Index = 1;
            this.receiveCharactersMenuItem.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.receiveCharactersMenuItem.Text = "&Receive Characters";
            this.receiveCharactersMenuItem.Click += new System.EventHandler(this.receiveCharactersMenuItem_Click);
            // 
            // sendCharactersMenuItem
            // 
            this.sendCharactersMenuItem.Index = 2;
            this.sendCharactersMenuItem.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.sendCharactersMenuItem.Text = "&Send Characters";
            this.sendCharactersMenuItem.Click += new System.EventHandler(this.sendCharactersMenuItem_Click);
            // 
            // receiveWordsMenuItem
            // 
            this.receiveWordsMenuItem.Index = 3;
            this.receiveWordsMenuItem.Shortcut = System.Windows.Forms.Shortcut.F4;
            this.receiveWordsMenuItem.Text = "Receive &Words";
            this.receiveWordsMenuItem.Click += new System.EventHandler(this.receiveWordsMenuItem_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 4;
            this.menuItem15.Text = "-";
            // 
            // optionsMenuItem
            // 
            this.optionsMenuItem.Index = 5;
            this.optionsMenuItem.Shortcut = System.Windows.Forms.Shortcut.F10;
            this.optionsMenuItem.Text = "&Options...";
            this.optionsMenuItem.Click += new System.EventHandler(this.optionsMenuItem_Click);
            // 
            // charactersMenuItem
            // 
            this.charactersMenuItem.Index = 6;
            this.charactersMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.addAllMenuItem,
            this.removeAllMenuItem,
            this.menuItem7,
            this.addAlphabetMenuItem,
            this.removeAlphabetMenuItem,
            this.menuItem10,
            this.addNumericMenuItem,
            this.removeNumericMenuItem,
            this.menuItem13,
            this.addPunctuationMenuItem,
            this.removePunctuationMenuItem,
            this.menuItem12,
            this.beginnerMenuItem,
            this.intermediateMenuItem,
            this.advancedMenuItem});
            this.charactersMenuItem.Text = "Learning &Characters";
            // 
            // addAllMenuItem
            // 
            this.addAllMenuItem.Index = 0;
            this.addAllMenuItem.Text = "Add A&ll";
            this.addAllMenuItem.Click += new System.EventHandler(this.addAllContextMenuItem_Click);
            // 
            // removeAllMenuItem
            // 
            this.removeAllMenuItem.Index = 1;
            this.removeAllMenuItem.Text = "&Remove All";
            this.removeAllMenuItem.Click += new System.EventHandler(this.removeAllContextMenuItem_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 2;
            this.menuItem7.Text = "-";
            // 
            // addAlphabetMenuItem
            // 
            this.addAlphabetMenuItem.Index = 3;
            this.addAlphabetMenuItem.Text = "Add Alphabet";
            this.addAlphabetMenuItem.Click += new System.EventHandler(this.addAlphabetContextMenuItem_Click);
            // 
            // removeAlphabetMenuItem
            // 
            this.removeAlphabetMenuItem.Index = 4;
            this.removeAlphabetMenuItem.Text = "Remove Alphabet";
            this.removeAlphabetMenuItem.Click += new System.EventHandler(this.removeAlphabetContextMenuItem_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 5;
            this.menuItem10.Text = "-";
            // 
            // addNumericMenuItem
            // 
            this.addNumericMenuItem.Index = 6;
            this.addNumericMenuItem.Text = "Add Numeric";
            this.addNumericMenuItem.Click += new System.EventHandler(this.addNumericContextMenuItem_Click);
            // 
            // removeNumericMenuItem
            // 
            this.removeNumericMenuItem.Index = 7;
            this.removeNumericMenuItem.Text = "Remove Numeric";
            this.removeNumericMenuItem.Click += new System.EventHandler(this.removeNumericContextMenuItem_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 8;
            this.menuItem13.Text = "-";
            // 
            // addPunctuationMenuItem
            // 
            this.addPunctuationMenuItem.Index = 9;
            this.addPunctuationMenuItem.Text = "Add Punctuation";
            this.addPunctuationMenuItem.Click += new System.EventHandler(this.addPunctuationContextMenuItem_Click);
            // 
            // removePunctuationMenuItem
            // 
            this.removePunctuationMenuItem.Index = 10;
            this.removePunctuationMenuItem.Text = "Remove Punctuation";
            this.removePunctuationMenuItem.Click += new System.EventHandler(this.removePunctuationContextMenuItem_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 11;
            this.menuItem12.Text = "-";
            // 
            // beginnerMenuItem
            // 
            this.beginnerMenuItem.Index = 12;
            this.beginnerMenuItem.Text = "&Beginner Level";
            this.beginnerMenuItem.Click += new System.EventHandler(this.beginnerContextMenuItem_Click);
            // 
            // intermediateMenuItem
            // 
            this.intermediateMenuItem.Index = 13;
            this.intermediateMenuItem.Text = "&Intermediate Level";
            this.intermediateMenuItem.Click += new System.EventHandler(this.intermediateContextMenuItem_Click);
            // 
            // advancedMenuItem
            // 
            this.advancedMenuItem.Index = 14;
            this.advancedMenuItem.Text = "&Advanced Level";
            this.advancedMenuItem.Click += new System.EventHandler(this.advancedContextMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.Index = 3;
            this.helpMenuItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.contentsMenuItem,
            this.menuItem14,
            this.aboutMenuItem});
            this.helpMenuItem.Text = "&Help";
            // 
            // contentsMenuItem
            // 
            this.contentsMenuItem.Index = 0;
            this.contentsMenuItem.Text = "&Contents...";
            this.contentsMenuItem.Click += new System.EventHandler(this.contentsMenuItem_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 1;
            this.menuItem14.Text = "-";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Index = 2;
            this.aboutMenuItem.Text = "&About...";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // learnStateContextMenu
            // 
            this.learnStateContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.addAllContextMenuItem,
            this.removeAllContextMenuItem,
            this.menuItem2,
            this.addAlphabetContextMenuItem,
            this.removeAlphabetContextMenuItem,
            this.menuItem1,
            this.addNumericContextMenuItem,
            this.removeNumericContextMenuItem,
            this.menuItem11,
            this.addPunctuationContextMenuItem,
            this.removePunctuationContextMenuItem,
            this.menuItem5,
            this.beginnerContextMenuItem,
            this.intermediateContextMenuItem,
            this.advancedContextMenuItem});
            // 
            // addAllContextMenuItem
            // 
            this.addAllContextMenuItem.Index = 0;
            this.addAllContextMenuItem.Text = "Add A&ll";
            this.addAllContextMenuItem.Click += new System.EventHandler(this.addAllContextMenuItem_Click);
            // 
            // removeAllContextMenuItem
            // 
            this.removeAllContextMenuItem.Index = 1;
            this.removeAllContextMenuItem.Text = "&Remove All";
            this.removeAllContextMenuItem.Click += new System.EventHandler(this.removeAllContextMenuItem_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 2;
            this.menuItem2.Text = "-";
            // 
            // addAlphabetContextMenuItem
            // 
            this.addAlphabetContextMenuItem.Index = 3;
            this.addAlphabetContextMenuItem.Text = "Add Alphabet";
            this.addAlphabetContextMenuItem.Click += new System.EventHandler(this.addAlphabetContextMenuItem_Click);
            // 
            // removeAlphabetContextMenuItem
            // 
            this.removeAlphabetContextMenuItem.Index = 4;
            this.removeAlphabetContextMenuItem.Text = "Remove Alphabet";
            this.removeAlphabetContextMenuItem.Click += new System.EventHandler(this.removeAlphabetContextMenuItem_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 5;
            this.menuItem1.Text = "-";
            // 
            // addNumericContextMenuItem
            // 
            this.addNumericContextMenuItem.Index = 6;
            this.addNumericContextMenuItem.Text = "Add Numeric";
            this.addNumericContextMenuItem.Click += new System.EventHandler(this.addNumericContextMenuItem_Click);
            // 
            // removeNumericContextMenuItem
            // 
            this.removeNumericContextMenuItem.Index = 7;
            this.removeNumericContextMenuItem.Text = "Remove Numeric";
            this.removeNumericContextMenuItem.Click += new System.EventHandler(this.removeNumericContextMenuItem_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 8;
            this.menuItem11.Text = "-";
            // 
            // addPunctuationContextMenuItem
            // 
            this.addPunctuationContextMenuItem.Index = 9;
            this.addPunctuationContextMenuItem.Text = "Add Punctuation";
            this.addPunctuationContextMenuItem.Click += new System.EventHandler(this.addPunctuationContextMenuItem_Click);
            // 
            // removePunctuationContextMenuItem
            // 
            this.removePunctuationContextMenuItem.Index = 10;
            this.removePunctuationContextMenuItem.Text = "Remove Punctuation";
            this.removePunctuationContextMenuItem.Click += new System.EventHandler(this.removePunctuationContextMenuItem_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 11;
            this.menuItem5.Text = "-";
            // 
            // beginnerContextMenuItem
            // 
            this.beginnerContextMenuItem.Index = 12;
            this.beginnerContextMenuItem.Text = "&Beginner Level";
            this.beginnerContextMenuItem.Click += new System.EventHandler(this.beginnerContextMenuItem_Click);
            // 
            // intermediateContextMenuItem
            // 
            this.intermediateContextMenuItem.Index = 13;
            this.intermediateContextMenuItem.Text = "&Intermediate Level";
            this.intermediateContextMenuItem.Click += new System.EventHandler(this.intermediateContextMenuItem_Click);
            // 
            // advancedContextMenuItem
            // 
            this.advancedContextMenuItem.Index = 14;
            this.advancedContextMenuItem.Text = "&Advanced Level";
            this.advancedContextMenuItem.Click += new System.EventHandler(this.advancedContextMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(4, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 20);
            this.label1.TabIndex = 15;
            this.label1.Text = "Char:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(67, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Morse:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(155, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Status:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(67, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Message:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(4, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "Session:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Location = new System.Drawing.Point(5, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(157, 20);
            this.label6.TabIndex = 20;
            this.label6.Text = "Learning Characters:";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = -1;
            this.menuItem6.Text = "";
            // 
            // modePanel
            // 
            this.modePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.modePanel.Controls.Add(this.modeLabel);
            this.modePanel.Controls.Add(this.modeImage);
            this.modePanel.Location = new System.Drawing.Point(8, 4);
            this.modePanel.Name = "modePanel";
            this.modePanel.Padding = new System.Windows.Forms.Padding(1);
            this.modePanel.Size = new System.Drawing.Size(772, 44);
            this.modePanel.TabIndex = 14;
            // 
            // modeLabel
            // 
            this.modeLabel.BackColor = System.Drawing.Color.White;
            this.modeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modeLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modeLabel.Location = new System.Drawing.Point(56, 1);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(715, 42);
            this.modeLabel.TabIndex = 17;
            this.modeLabel.Text = "Message Mode";
            this.modeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // modeImage
            // 
            this.modeImage.BackColor = System.Drawing.Color.White;
            this.modeImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.modeImage.Location = new System.Drawing.Point(1, 1);
            this.modeImage.Name = "modeImage";
            this.modeImage.Size = new System.Drawing.Size(55, 42);
            this.modeImage.TabIndex = 16;
            this.modeImage.TabStop = false;
            // 
            // sessionPanel
            // 
            this.sessionPanel.BackColor = System.Drawing.SystemColors.Control;
            this.sessionPanel.Location = new System.Drawing.Point(8, 141);
            this.sessionPanel.Name = "sessionPanel";
            this.sessionPanel.Size = new System.Drawing.Size(771, 19);
            this.sessionPanel.TabIndex = 13;
            this.sessionPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.sessionPanel_Paint);
            // 
            // messagePanel
            // 
            this.messagePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.messagePanel.Controls.Add(this.messageLabel);
            this.messagePanel.Location = new System.Drawing.Point(71, 105);
            this.messagePanel.Name = "messagePanel";
            this.messagePanel.Padding = new System.Windows.Forms.Padding(1);
            this.messagePanel.Size = new System.Drawing.Size(330, 19);
            this.messagePanel.TabIndex = 12;
            // 
            // messageLabel
            // 
            this.messageLabel.BackColor = System.Drawing.Color.White;
            this.messageLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.Location = new System.Drawing.Point(1, 1);
            this.messageLabel.Margin = new System.Windows.Forms.Padding(0);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.messageLabel.Size = new System.Drawing.Size(328, 17);
            this.messageLabel.TabIndex = 1;
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // resultPanel
            // 
            this.resultPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.resultPanel.Controls.Add(this.resultLabel);
            this.resultPanel.Location = new System.Drawing.Point(159, 68);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Padding = new System.Windows.Forms.Padding(1);
            this.resultPanel.Size = new System.Drawing.Size(242, 19);
            this.resultPanel.TabIndex = 7;
            // 
            // resultLabel
            // 
            this.resultLabel.BackColor = System.Drawing.Color.White;
            this.resultLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultLabel.Location = new System.Drawing.Point(1, 1);
            this.resultLabel.Margin = new System.Windows.Forms.Padding(0);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.resultLabel.Size = new System.Drawing.Size(240, 17);
            this.resultLabel.TabIndex = 1;
            this.resultLabel.Text = "playing";
            this.resultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // morsePanel
            // 
            this.morsePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.morsePanel.Controls.Add(this.morseLabel);
            this.morsePanel.Location = new System.Drawing.Point(71, 68);
            this.morsePanel.Name = "morsePanel";
            this.morsePanel.Padding = new System.Windows.Forms.Padding(1);
            this.morsePanel.Size = new System.Drawing.Size(80, 19);
            this.morsePanel.TabIndex = 6;
            // 
            // morseLabel
            // 
            this.morseLabel.BackColor = System.Drawing.Color.White;
            this.morseLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.morseLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.morseLabel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.morseLabel.Location = new System.Drawing.Point(1, 1);
            this.morseLabel.Margin = new System.Windows.Forms.Padding(0);
            this.morseLabel.Name = "morseLabel";
            this.morseLabel.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.morseLabel.Size = new System.Drawing.Size(78, 17);
            this.morseLabel.TabIndex = 7;
            this.morseLabel.Text = "-----";
            this.morseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // learnStatePanel
            // 
            this.learnStatePanel.Location = new System.Drawing.Point(8, 178);
            this.learnStatePanel.Name = "learnStatePanel";
            this.learnStatePanel.Size = new System.Drawing.Size(772, 70);
            this.learnStatePanel.TabIndex = 5;
            this.learnStatePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.learnStatePanel_Paint);
            this.learnStatePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.learnStatePanel_MouseDown);
            // 
            // flashPanel
            // 
            this.flashPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.flashPanel.Controls.Add(this.flashLabel);
            this.flashPanel.Location = new System.Drawing.Point(8, 68);
            this.flashPanel.Name = "flashPanel";
            this.flashPanel.Padding = new System.Windows.Forms.Padding(1);
            this.flashPanel.Size = new System.Drawing.Size(56, 56);
            this.flashPanel.TabIndex = 4;
            // 
            // flashLabel
            // 
            this.flashLabel.BackColor = System.Drawing.Color.White;
            this.flashLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flashLabel.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flashLabel.Location = new System.Drawing.Point(1, 1);
            this.flashLabel.Name = "flashLabel";
            this.flashLabel.Size = new System.Drawing.Size(54, 54);
            this.flashLabel.TabIndex = 0;
            this.flashLabel.Text = "Q";
            this.flashLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(788, 255);
            this.Controls.Add(this.modePanel);
            this.Controls.Add(this.sessionPanel);
            this.Controls.Add(this.messagePanel);
            this.Controls.Add(this.resultPanel);
            this.Controls.Add(this.morsePanel);
            this.Controls.Add(this.learnStatePanel);
            this.Controls.Add(this.flashPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Morse Learner";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.modePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.modeImage)).EndInit();
            this.messagePanel.ResumeLayout(false);
            this.resultPanel.ResumeLayout(false);
            this.morsePanel.ResumeLayout(false);
            this.flashPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			using (RunOnce runOnce = new RunOnce ("693ef36f-a62b-4996-8e7e-144eb3d18ed1"))
				if (runOnce.IsSingleInstance)
					Application.Run (new MainForm());
				else
					runOnce.RaiseOtherProcess();
		}

		private Color borderColor = Color.FromArgb (127, 157, 185);
		private Pen borderPen;

		private MorseLearnerConfig config;
		private LearnState learnState;
		private Vocabulary vocabulary;

		private Sleeper sleeper;
		private Sleeper morseDocSleeper;
		private WavePlayer wavePlayer;
		private MorsePlayer morsePlayer;
		private MorseKey morseKey;
		private KeyActivityHook keyHook = new KeyActivityHook ();
		private MorseDocument morseDoc;
		private MorseDocumentStateRecorder morseDocStateRecorder;
		private MorseDocumentParser morseDocParser;

		private Hashtable sounds = new Hashtable();
		private WaveBuffer wordStartSound;
		private WaveBuffer wordFinishSound;

		private PrivateFontCollection privateFontCollection;
		private FontFamily fontFamily;

		private Font learnStateFont;
		private Font resultFont;
		private Font flashFont;
		private Font labelFont;


		private MainFormState state;

		private string selectedWord = null;
		private string wordInput = string.Empty;
		private string transmitBuffer = string.Empty;

		private int selectedWordIndex = -1;
		private int sessionCount = 0;
		private int sessionCorrectCount = 0;
		private bool sessionCompleted = false;
		private LearnCharacter selectedCharacter;
		private bool showMorse;

		private DateTime lastControlC = DateTime.MinValue;


		private void LoadPakData()
		{
            using (PakFile pakFile = new PakFile(Wxv.MorseLearner.Properties.Resources.Data))
			{
				foreach (MorseConstant c in MorseConstant.Values)
					if (c.Filename != null)
						using (Stream stream = pakFile.LoadFromPak (c.Filename))
						{
							WaveBuffer buffer = new WaveBuffer (stream);
							sounds.Add (c.Character, buffer);
						}

				using (Stream stream = pakFile.LoadFromPak (MorseLearnerConfig.WordStartSoundFilename))
					wordStartSound = new WaveBuffer (stream);

				using (Stream stream = pakFile.LoadFromPak (MorseLearnerConfig.WordFinishSoundFilename))
					wordFinishSound = new WaveBuffer (stream);

				using (Stream stream = pakFile.LoadFromPak (MorseLearnerConfig.VocabularyFilename))
					vocabulary = new Vocabulary (stream);

                //if (!File.Exists (MorseLearnerConfig.DataFilename))
                //    pakFile.SaveToFile (MorseLearnerConfig.DataFilename);
			}
		}


		private void LoadGDIObjects()
		{
			borderPen = new Pen (borderColor);
		}


		private void LoadFonts()
		{
			privateFontCollection = new PrivateFontCollection();
			privateFontCollection.AddFontFile (MorseLearnerConfig.FontFilename);
			fontFamily = privateFontCollection.Families[0];

			learnStateFont = new Font (fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
			resultFont = new Font (fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
			flashFont = new Font (fontFamily, 32, FontStyle.Regular, GraphicsUnit.Pixel);
			labelFont = new Font (fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);

			resultLabel.Font = resultFont;
			messageLabel.Font = resultFont;
			label1.Font = labelFont;
			label2.Font = labelFont;
			label3.Font = labelFont;
			label4.Font = labelFont;
			label5.Font = labelFont;
			label6.Font = labelFont;
			//flashLabel.Font = flashFont;
		}

		
		private void SetConfigSettings()
		{
			morsePlayer.Wpm = config.Wpm;
			morsePlayer.OverallWpm = config.OverallWpm;
			morsePlayer.Frequency = config.Frequency;

			morseKey.Frequency = config.Frequency;
			morseKey.UseKeyboard = config.UseKeyboard;
			morseKey.Key = config.Key;
			morseKey.UseMouse = config.UseMouse;
			morseKey.MouseButton = config.MouseButton;
			morseKey.UseComDevice = config.UseComDevice;
			morseKey.ComDevice = config.ComDevice;
			morseKey.ComDevicePin = config.ComDevicePin;
		}


		private void MainForm_Load(object sender, System.EventArgs e)
		{
			config = MorseLearnerConfig.GetInstance();
			learnState = LearnState.GetInstance();

			sleeper = new Sleeper (this);
			morseDocSleeper = new Sleeper (this);
			wavePlayer = new WavePlayer (this);
			
			morseDoc = new MorseDocument();
			morseDocStateRecorder = new MorseDocumentStateRecorder (morseDoc);
			morseDocParser = new MorseDocumentParser (morseDoc);

			morsePlayer = new MorsePlayer (this);
			
			morseKey = new MorseKey (this);
			morseKey.OnKeyDown = new WaitCallback (MainForm_MorseKeyDown);
			morseKey.OnKeyUp = new WaitCallback (MainForm_MorseKeyUp);

			keyHook.OnKeyDown += new KeyEventHandler (MainForm_HookKeyDown);

			LoadPakData();
			LoadGDIObjects();
			LoadFonts();
			SetConfigSettings();

			morseKey.Open();
			keyHook.Open();
			
			StartMessage_Play ("welcome");
		}


		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			sleeper.Close (false);
			morseDocSleeper.Close (false);
			wavePlayer.Close (false);
			morsePlayer.Stop();

			sleeper.Close ();
			morseDocSleeper.Close();
			wavePlayer.Close ();
			morsePlayer.Dispose();
			
			morseKey.Close();
			keyHook.Close();

			learnState.SaveToFile();
		}


		private void ResetEventGenerators()
		{
			sleeper.Close(false);
			morseDocSleeper.Close (false);
			wavePlayer.Close(false);
			morsePlayer.Stop();

			morseKey.Stop();
		}


		private void SelectCharacter (bool error)
		{
			if (learnState.Count == 0)
			{
				learnState.AddCharacter();
				learnStatePanel.Refresh();
			}

			if (error)
			{
				selectedWord = null;
				selectedWordIndex = -1;

				showMorse = true;
			}
			else
			{
				if ((selectedWord != null) && (selectedWordIndex >= selectedWord.Length))
				{
					selectedWord = null;
					selectedWordIndex = -1;
				}

				if ((selectedWord == null) && 
					(new Random ().NextDouble() < (config.WordChance * learnState.Count / learnState.LearnOrder.Length)))
				{
					selectedWord = vocabulary.PickRandomWord (learnState.Characters, config.SessionLength - sessionCount);
					selectedWordIndex = 0;
				}

				if (selectedWord != null)
					selectedCharacter = learnState.GetByCharacter (selectedWord [selectedWordIndex].ToString());
				else
					selectedCharacter = learnState.SelectRandomCharacter();
				showMorse = selectedCharacter.Error > 0.5;
			}
		}


		private void SelectWord ()
		{
			int wordSize = learnState.RandomWordSize();
			
			selectedWord = vocabulary.PickRandomWord (learnState.AllCharacters, wordSize);
			if (selectedWord == null)
			{
				selectedWord = "";
				Random random = new Random();
				for (int i = 0; i < wordSize; i++)
				{
					int r = random.Next (MorseConstant.DefaultLearnOrder.Length);
					selectedWord += MorseConstant.DefaultLearnOrder.Substring (r, 1);
				}
			}

			wordInput = "";
			selectedWordIndex = 0;
		}


		private void SetState (MainFormState state)
		{
			this.state = state;

			charactersMenuItem.Enabled = 
			 ((state == MainFormState.Message_Transmit)
				|| (state == MainFormState.Message_TransmitBuffer)
				|| (state == MainFormState.Message_Play)
				|| (state == MainFormState.Message_EndPlay)
				|| (state == MainFormState.Receive_EndSession)
				|| (state == MainFormState.Transmit_EndSession)
				|| (state == MainFormState.ReceiveWord_EndSession));
			
			copyMenuItem.Enabled = 
				(state == MainFormState.Message_Transmit)
				|| (state == MainFormState.Message_TransmitBuffer)
				|| (state == MainFormState.Message_Play)
				|| (state == MainFormState.Message_EndPlay);
		}


		private void StartMessage_Transmit ()
		{
			StartMessage_Transmit (true);
		}


		private void StartMessage_Transmit (bool clearMessage)
		{
			SetState (MainFormState.Message_Transmit);

			modeLabel.Text = "Message Mode";
			modeImage.Image = imageList.Images [0];

			ResetEventGenerators();

			resultLabel.ForeColor = Color.Black;
			resultLabel.BackColor = Color.White;
			resultLabel.Text = "transmitting";

			if (clearMessage)
			{
				messageLabel.Text = "";
				morseDoc.Clear();
				morseDocParser.Clear();
			}
			morseDocStateRecorder.Clear();
			morseDocStateRecorder.AddState (false);

			selectedCharacter = null;
			selectedWord = null;

			morseLabel.Text = "";
			flashLabel.Text = "";

			sessionCount = 0;
			sessionCorrectCount = 0;
			sessionCompleted = false;
			sessionPanel.Refresh();

			morseKey.Start();
		}


		private void StartMessage_Play (string message)
		{
			SetState (MainFormState.Message_Play);

			modeLabel.Text = "Message Mode";
			modeImage.Image = imageList.Images [0];

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Blue;
			resultLabel.Text = "playing";

			messageLabel.Text = "";
			flashLabel.Text = "";
			morseLabel.Text = "";

			ResetEventGenerators();

			if (MorseMail.IsMorseMail (message))
			{
				try
				{
					morseDoc.AsMorseMail = message;
				}
				catch (Exception)
				{
					return;
				}

				morsePlayer.OnAction = new MorsePlayerActionDelegate (StartMessage_PlayText_Action);
				morsePlayer.Play (morseDoc, MorsePlayer.AllActions);
			}
			else
			{
				morseDoc.Wpm = config.Wpm;
				morseDoc.OverallWpm = config.OverallWpm;
				morseDoc.Text = message;

				morsePlayer.OnAction = new MorsePlayerActionDelegate (StartMessage_PlayText_Action);
				morsePlayer.Play (message, MorsePlayer.AllActions);
			}
		}


		private void StartMessage_PlayText_Action (object sender, MorsePlayerActionArgs args)
		{
			flashLabel.BackColor = Color.White;

			if (args.Action == MorsePlayerActionEnum.Dit)
			{
				if (showMorse && config.ShowMorse)
					morseLabel.Text += ".";
				flashLabel.Text = "";
				flashLabel.BackColor = Color.Green;
			}
			else if (args.Action == MorsePlayerActionEnum.Dah)
			{
                if (showMorse && config.ShowMorse)
					morseLabel.Text += "-";
				flashLabel.Text = "";
				flashLabel.BackColor = Color.Green;
			}
			else if (args.Action == MorsePlayerActionEnum.Character)
			{
				if (args.Character != null)
				{
					flashLabel.Text = args.Character.ToUpper();
					AddMessageChar (args.Character.ToUpper());
				}
				else
				{
					flashLabel.Text = "";
					AddMessageChar ("?");
				}
			}
			else if (args.Action == MorsePlayerActionEnum.InterChar)
			{
				morseLabel.Text = "";
			}
			else if (args.Action == MorsePlayerActionEnum.InterWord)
			{
				morseLabel.Text = "";
				AddMessageChar (" ");
			}
			else if (args.Action == MorsePlayerActionEnum.Completed)
			{
				morseLabel.Text = "";
				StartMessage_EndPlay();
			}
		}

		private void StartMessage_EndPlay()
		{
			SetState (MainFormState.Message_EndPlay);

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Blue;
			resultLabel.Text = "end of message";

			sleeper.Sleep (new WaitCallback (StartMessage_EndPlay_Callback), config.MessageEndDelay);
		}

		private void StartMessage_EndPlay_Callback(object state0)
		{
			StartMessage_Transmit (false);
		}
		

		private void StartMessage_TransmitBuffer (string transmitBuffer)
		{
			SetState (MainFormState.Message_TransmitBuffer);

			modeLabel.Text = "Message Mode";
			modeImage.Image = imageList.Images [0];

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Blue;
			resultLabel.Text = "transmitting buffer";

			flashLabel.Text = "";
			morseLabel.Text = "";

			ResetEventGenerators();

			this.transmitBuffer = transmitBuffer;
			StartMessage_TransmitBuffer_Process();
		}


		private void StartMessage_TransmitBuffer_Process()
		{
			if (transmitBuffer == string.Empty)
			{
				StartMessage_Transmit(false);
				return;
			}

//			morseDoc.Wpm = config.Wpm;
//			morseDoc.OverallWpm = config.OverallWpm;
//			if (morseDoc.Count == 0)
//				morseDoc.StartTime = DateTime.Now;
//
//			morseDoc.AddText (transmitBuffer);
//			morseDoc.AddInterChar();

			MorseDocument playDoc = new MorseDocument();
			playDoc.Wpm = config.Wpm;
			playDoc.OverallWpm = config.OverallWpm;
			playDoc.AddText (transmitBuffer);
			playDoc.AddInterChar();

			transmitBuffer = "";

			morsePlayer.OnAction = new MorsePlayerActionDelegate (StartMessage_TransmitBuffer_Action);
			morsePlayer.Play (playDoc, MorsePlayer.AllActions);
		}


		private void StartMessage_TransmitBuffer_Action (object sender, MorsePlayerActionArgs args)
		{
			flashLabel.BackColor = Color.White;

			if (args.Action == MorsePlayerActionEnum.Dit)
			{
                if (showMorse && config.ShowMorse)
					morseLabel.Text += ".";
				flashLabel.Text = "";
				flashLabel.BackColor = Color.Green;
			}
			else if (args.Action == MorsePlayerActionEnum.Dah)
			{
                if (showMorse && config.ShowMorse)
					morseLabel.Text += "-";
				flashLabel.Text = "";
				flashLabel.BackColor = Color.Green;
			}
			else if (args.Action == MorsePlayerActionEnum.Character)
			{
				if (args.Character != null)
					flashLabel.Text = args.Character.ToUpper();
				else
					flashLabel.Text = "";
			}
			else if (args.Action == MorsePlayerActionEnum.InterChar)
			{
				morseLabel.Text = "";
			}
			else if (args.Action == MorsePlayerActionEnum.InterWord)
			{
				morseLabel.Text = "";
			}
			else if (args.Action == MorsePlayerActionEnum.Completed)
			{
				morseLabel.Text = "";
				StartMessage_TransmitBuffer_Process();
			}
		}
		
		
		private void StartReceive_BeginSession ()
		{
			SetState (MainFormState.Receive_BeginSession);

			modeLabel.Text = "Receive Characters";
			modeImage.Image = imageList.Images [1];

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Green;
			resultLabel.Text = "session starting";

			ResetEventGenerators();

			messageLabel.Text = "";
			morseLabel.Text = "";
			flashLabel.Text = "";
			
			sessionCount = 0;
			sessionCorrectCount = 0;
			sessionCompleted = false;
			sessionPanel.Refresh();

			SelectCharacter (false);

			sleeper.Sleep (new WaitCallback (StartReceive_BeginSession_Callback), config.SessionStartDelay);
		}


		private void StartReceive_BeginSession_Callback(object state)
		{
			StartReceive_PlayMorse();
		}


		private void StartReceive_Paused ()
		{
			SetState (MainFormState.Receive_Paused);

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Silver;
			resultLabel.Text = "paused";

			ResetEventGenerators();

			morseLabel.Text = "";
			flashLabel.Text = "";
		}


		private void StartReceive_PlayMorse ()
		{
			SetState (MainFormState.Receive_PlayMorse);

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Blue;
			resultLabel.Text = "playing";

			ResetEventGenerators();

			if (selectedWord == null)
				messageLabel.Text = "";
			
			morseLabel.Text = "";
			flashLabel.Text = "";
			
			if ((selectedWord != null) && (selectedWordIndex == 0))
				wavePlayer.Play (new WaitCallback (StartReceive_PlayMorse_PlayWordStart), wordStartSound);
			else 
			{
				morsePlayer.OnAction = new MorsePlayerActionDelegate (StartReceive_PlayMorse_Action);
				morsePlayer.Play (selectedCharacter.Character, MorsePlayer.AllActions);
			}
		}

		private void StartReceive_PlayMorse_PlayWordStart (object state)
		{
			morsePlayer.OnAction = new MorsePlayerActionDelegate (StartReceive_PlayMorse_Action);
			morsePlayer.Play (selectedCharacter.Character, MorsePlayer.AllActions);
		}


		private void StartReceive_PlayMorse_Action (object sender, MorsePlayerActionArgs args)
		{
			flashLabel.BackColor = Color.White;

			if (args.Action == MorsePlayerActionEnum.Dit)
			{
                if (showMorse && config.ShowMorse)
					morseLabel.Text += ".";
				flashLabel.BackColor = Color.Green;
			}
			else if (args.Action == MorsePlayerActionEnum.Dah)
			{
                if (showMorse && config.ShowMorse)
					morseLabel.Text += "-";
				flashLabel.BackColor = Color.Green;
			}
			else if (args.Action == MorsePlayerActionEnum.Completed)
			{
				StartReceive_WaitInput();
			}
		}

		private void StartReceive_WaitInput()
		{
			SetState (MainFormState.Receive_WaitInput);

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.LightSkyBlue;
			resultLabel.Text = "waiting";

			ResetEventGenerators();

			sleeper.Sleep (new WaitCallback (StartReceive_WaitInputCallback), config.WaitInputDelay);
		}
		
		private void StartReceive_WaitInputCallback (object state0)
		{
			StartReceive_ShowIncorrectResult();
		}

		private void StartReceive_ShowCorrectResult()
		{
			SetState (MainFormState.Receive_ShowCorrectResult);

			sessionCount++;
			sessionCorrectCount++;
			sessionCompleted = (sessionCount >= config.SessionLength);
			sessionPanel.Refresh();
			
            if (config.ShowMorse)
			    morseLabel.Text = selectedCharacter.Morse;

			flashLabel.Text = MorseConstant.ConvertToDisplay (selectedCharacter.Character).ToUpper();

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Green;
			resultLabel.Text = "correct";

			if (selectedWord != null)
			{
				selectedWordIndex++;
				string s = "";
				for (int i = 0; i < selectedWordIndex; i++)
					s += "" 
						// + (i > 0 ? " " : "") 
						+ selectedWord [i].ToString().ToUpper();
				messageLabel.Text = s;
			}

			learnState.Grade (selectedCharacter, true, true);
			learnStatePanel.Refresh();

			ResetEventGenerators();

			if ((selectedWord != null) && (selectedWordIndex >= selectedWord.Length))
			{
				wavePlayer.Play (new WaitCallback (StartReceive_ShowCorrectResult_PlayFinishWord), wordFinishSound);
			}
			else
			{
				sleeper.Sleep (
					new WaitCallback (StartReceive_ShowCorrectResult_Callback), 
					config.ShowCorrectResultDelay);
			}

			SelectCharacter (false);
		}

		private void StartReceive_ShowCorrectResult_PlayFinishWord(object state)
		{
			sleeper.Sleep (
				new WaitCallback (StartReceive_ShowCorrectResult_Callback), 
				config.ShowCorrectWordResultDelay);
		}

		private void StartReceive_ShowCorrectResult_Callback (object state0)
		{
			if (sessionCompleted)
				StartReceive_EndSession();
			else
				StartReceive_PlayMorse();
		}

		private void StartReceive_ShowIncorrectResult(string incorrectCharacter)
		{
			SetState (MainFormState.Receive_ShowIncorrectResult);

			sessionCount++;
			sessionCompleted = (sessionCount >= config.SessionLength);
			sessionPanel.Refresh();

            if (config.ShowMorse) 
			    morseLabel.Text = selectedCharacter.Morse;

			flashLabel.Text = MorseConstant.ConvertToDisplay (selectedCharacter.Character).ToUpper();

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Maroon;
			resultLabel.Text = "incorrect";

			if (selectedWord != null)
			{
				messageLabel.Text = selectedWord;
				selectedWord = null;
			}

			if (incorrectCharacter != null)
			{
				LearnCharacter lc = learnState.GetByCharacter (incorrectCharacter);
				if (lc != null)
					learnState.Grade (lc, false, false);
			}
			learnState.Grade (selectedCharacter, false, false);
			learnStatePanel.Refresh();

			ResetEventGenerators();

			WaveBuffer buffer = (WaveBuffer) sounds [selectedCharacter.Character.ToLower()];
			if (buffer != null)
				wavePlayer.Play (new WaitCallback (StartReceive_ShowIncorrectResult_PlayingLetterCallback), buffer);
			else
				sleeper.Sleep (new WaitCallback (StartReceive_ShowIncorrectResult_Callback), config.ShowIncorrectResultDelay);

			SelectCharacter (true);
		}


		private void StartReceive_ShowIncorrectResult()
		{
			StartReceive_ShowIncorrectResult(null);
		}
			
		private void StartReceive_ShowIncorrectResult_PlayingLetterCallback(object state0)
		{
			sleeper.Sleep (new WaitCallback (StartReceive_ShowIncorrectResult_Callback), config.ShowIncorrectResultDelay);
		}
			
		private void StartReceive_ShowIncorrectResult_Callback (object state0)
		{
			if (sessionCompleted)
				StartReceive_EndSession();
			else
				StartReceive_PlayMorse ();
		}


		private void StartReceive_EndSession()
		{
			SetState (MainFormState.Receive_EndSession);

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Blue;
			resultLabel.Text = "session completed";

			ResetEventGenerators();
		}

		
		private void StartTransmit_BeginSession ()
		{
			SetState (MainFormState.Transmit_BeginSession);

			modeLabel.Text = "Transmit Characters";
			modeImage.Image = imageList.Images [2];

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Green;
			resultLabel.Text = "session starting";

			ResetEventGenerators();

			messageLabel.Text = "";
			morseLabel.Text = "";
			flashLabel.Text = "";
			
			sessionCount = 0;
			sessionCorrectCount = 0;
			sessionCompleted = false;
			sessionPanel.Refresh();

			SelectCharacter (false);

			sleeper.Sleep (new WaitCallback (StartTransmit_BeginSession_Callback), config.SessionStartDelay);
		}


		private void StartTransmit_BeginSession_Callback(object state)
		{
			StartTransmit_PlayMorse();
		}


		private void StartTransmit_Paused ()
		{
			SetState (MainFormState.Transmit_Paused);

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Silver;
			resultLabel.Text = "paused";

			ResetEventGenerators();

			morseLabel.Text = "";
			flashLabel.Text = "";
		}


		private void StartTransmit_PlayMorse ()
		{
			SetState (MainFormState.Transmit_PlayMorse);

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Blue;
			resultLabel.Text = "playing";

			ResetEventGenerators();

			if (selectedWord == null)
				messageLabel.Text = "";
			
			morseLabel.Text = "";
			flashLabel.Text = MorseConstant.ConvertToDisplay (selectedCharacter.Character).ToUpper();
			
			if ((selectedWord != null) && (selectedWordIndex == 0))
			{
				flashLabel.BackColor = Color.SkyBlue;
				wavePlayer.Play (new WaitCallback (StartTransmit_PlayMorse_PlayWordStart), wordStartSound);
			}
			else if (!showMorse)
			{
				flashLabel.BackColor = Color.SkyBlue;
				sleeper.Sleep (new WaitCallback (StartTransmit_Callback), config.StartTransmitDelay);
			}
			else
			{
				morsePlayer.OnAction = new MorsePlayerActionDelegate (StartTransmit_PlayMorse_Action);
				morsePlayer.Play (selectedCharacter.Character, MorsePlayer.AllActions);
			}
		}

		private void StartTransmit_PlayMorse_PlayWordStart (object state)
		{
			if (!showMorse)
			{
				sleeper.Sleep (new WaitCallback (StartTransmit_Callback), config.StartTransmitDelay);
			}
			else
			{
				morsePlayer.OnAction = new MorsePlayerActionDelegate (StartTransmit_PlayMorse_Action);
				morsePlayer.Play (selectedCharacter.Character, MorsePlayer.AllActions);
			}
		}

		private void StartTransmit_Callback (object state)
		{
			StartTransmit_WaitInput();
		}

		private void StartTransmit_PlayMorse_Action (object sender, MorsePlayerActionArgs args)
		{
			flashLabel.BackColor = Color.White;

			if (args.Action == MorsePlayerActionEnum.Dit)
			{
                if (showMorse && config.ShowMorse)
					morseLabel.Text += ".";
				flashLabel.BackColor = Color.Green;
			}
			else if (args.Action == MorsePlayerActionEnum.Dah)
			{
                if (showMorse && config.ShowMorse)
					morseLabel.Text += "-";
				flashLabel.BackColor = Color.Green;
			}
			else if (args.Action == MorsePlayerActionEnum.Completed)
			{
				sleeper.Sleep (new WaitCallback (StartTransmit_Callback), config.StartTransmitDelay);
			}
		}


		private void StartTransmit_WaitInput()
		{
			SetState (MainFormState.Transmit_WaitInput);

			flashLabel.BackColor = Color.White;

			morseLabel.Text = "";

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.LightSkyBlue;
			resultLabel.Text = "waiting";

			ResetEventGenerators();

			morseKey.Start();
			morseDoc.Clear();
			morseDocParser.Clear();

			sleeper.Sleep (new WaitCallback (StartTransmit_WaitInputCallback), config.WaitInputDelay);
		}
		
		private void StartTransmit_WaitInputCallback (object state0)
		{
			morseDocStateRecorder.AddState (false);
			if (MorseParse (false))
				return;

			StartTransmit_ShowIncorrectResult();
		}

		private void StartTransmit_ShowCorrectResult()
		{
			SetState (MainFormState.Transmit_ShowCorrectResult);

			sessionCount++;
			sessionCorrectCount++;
			sessionCompleted = (sessionCount >= config.SessionLength);
			sessionPanel.Refresh();
			
			flashLabel.Text = MorseConstant.ConvertToDisplay (selectedCharacter.Character).ToUpper();
			flashLabel.BackColor = Color.White;

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Green;
			resultLabel.Text = "correct";

			if (selectedWord != null)
			{
				selectedWordIndex++;
				string s = "";
				for (int i = 0; i < selectedWordIndex; i++)
					s += (i > 0 ? " " : "") + selectedWord [i].ToString().ToUpper();
				messageLabel.Text = s;
			}

			learnState.Grade (selectedCharacter, true, true);
			learnStatePanel.Refresh();

			ResetEventGenerators();

			if ((selectedWord != null) && (selectedWordIndex >= selectedWord.Length))
			{
				wavePlayer.Play (new WaitCallback (StartTransmit_ShowCorrectResult_PlayFinishWord), wordFinishSound);
			}
			else
			{
				sleeper.Sleep (
					new WaitCallback (StartTransmit_ShowCorrectResult_Callback), 
					config.ShowCorrectResultDelay);
			}

			SelectCharacter (false);
		}

		private void StartTransmit_ShowCorrectResult_PlayFinishWord(object state)
		{
			sleeper.Sleep (
				new WaitCallback (StartTransmit_ShowCorrectResult_Callback), 
				config.ShowCorrectWordResultDelay);
		}

		private void StartTransmit_ShowCorrectResult_Callback (object state0)
		{
			if (sessionCompleted)
				StartTransmit_EndSession();
			else
				StartTransmit_PlayMorse();
		}

		private void StartTransmit_ShowIncorrectResult(string incorrectCharacter)
		{
			SetState (MainFormState.Transmit_ShowIncorrectResult);

			sessionCount++;
			sessionCompleted = (sessionCount >= config.SessionLength);
			sessionPanel.Refresh();

			flashLabel.Text = MorseConstant.ConvertToDisplay (selectedCharacter.Character).ToUpper();
			flashLabel.BackColor = Color.White;

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Maroon;
			resultLabel.Text = "incorrect";

			if (selectedWord != null)
			{
				messageLabel.Text = selectedWord;
				selectedWord = null;
			}

			learnState.Grade (selectedCharacter, false, false);
			learnStatePanel.Refresh();

			ResetEventGenerators();

			WaveBuffer buffer = (WaveBuffer) sounds [selectedCharacter.Character.ToLower()];
			if (buffer != null)
				wavePlayer.Play (new WaitCallback (StartTransmit_ShowIncorrectResult_PlayingLetterCallback), buffer);
			else
				sleeper.Sleep (new WaitCallback (StartTransmit_ShowIncorrectResult_Callback), config.ShowIncorrectResultDelay);

			SelectCharacter (true);
		}


		private void StartTransmit_ShowIncorrectResult()
		{
			StartTransmit_ShowIncorrectResult(null);
		}
			
		private void StartTransmit_ShowIncorrectResult_PlayingLetterCallback(object state0)
		{
			sleeper.Sleep (new WaitCallback (StartTransmit_ShowIncorrectResult_Callback), config.ShowIncorrectResultDelay);
		}
			
		private void StartTransmit_ShowIncorrectResult_Callback (object state0)
		{
			if (sessionCompleted)
				StartTransmit_EndSession();
			else
				StartTransmit_PlayMorse ();
		}


		private void StartTransmit_EndSession()
		{
			SetState (MainFormState.Transmit_EndSession);

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Blue;
			resultLabel.Text = "session completed";

			ResetEventGenerators();
		}

		private void StartReceiveWord_BeginSession ()
		{
			SetState (MainFormState.ReceiveWord_BeginSession);

			modeLabel.Text = "Receive Words";
			modeImage.Image = imageList.Images [1];

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Green;
			resultLabel.Text = "session starting";

			ResetEventGenerators();

			messageLabel.Text = "";
			morseLabel.Text = "";
			flashLabel.Text = "";
			
			sessionCount = 0;
			sessionCorrectCount = 0;
			sessionCompleted = false;
			sessionPanel.Refresh();

			SelectWord ();

			sleeper.Sleep (new WaitCallback (StartReceiveWord_BeginSession_Callback), config.SessionStartDelay);
		}


		private void StartReceiveWord_BeginSession_Callback(object state)
		{
			StartReceiveWord_PlayMorse();
		}


		private void StartReceiveWord_Paused ()
		{
			SetState (MainFormState.ReceiveWord_Paused);

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Silver;
			resultLabel.Text = "paused";

			ResetEventGenerators();

			wordInput = "";
			morseLabel.Text = "";
			flashLabel.Text = "";
		}


		private void StartReceiveWord_PlayMorse ()
		{
			SetState (MainFormState.ReceiveWord_PlayMorse);

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Blue;
			resultLabel.Text = "playing";

			ResetEventGenerators();

			morseLabel.Text = "";
			flashLabel.Text = "";
			messageLabel.Text = "";
			
			wavePlayer.Play (new WaitCallback (StartReceiveWord_PlayMorse_PlayWordStart), wordStartSound);
		}

		private void StartReceiveWord_PlayMorse_PlayWordStart (object state)
		{
			morsePlayer.OnAction = new MorsePlayerActionDelegate (StartReceiveWord_PlayMorse_Action);
			morsePlayer.Play (selectedWord, MorsePlayer.AllActions);
		}


		private void StartReceiveWord_PlayMorse_Action (object sender, MorsePlayerActionArgs args)
		{
			flashLabel.BackColor = Color.White;

			if (args.Action == MorsePlayerActionEnum.Dit)
			{
                if (showMorse && config.ShowMorse)
					morseLabel.Text += ".";
				flashLabel.BackColor = Color.Green;
			}
			else if (args.Action == MorsePlayerActionEnum.Dah)
			{
                if (showMorse && config.ShowMorse)
					morseLabel.Text += "-";
				flashLabel.BackColor = Color.Green;
			}
			else if (args.Action == MorsePlayerActionEnum.Completed)
			{
				StartReceiveWord_WaitInput();
			}
		}

		private void StartReceiveWord_WaitInput()
		{
			SetState (MainFormState.ReceiveWord_WaitInput);

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.LightSkyBlue;
			resultLabel.Text = "waiting";

			ResetEventGenerators();

			sleeper.Sleep (new WaitCallback (StartReceiveWord_WaitInputCallback), config.WaitInputDelay);
		}
		
		private void StartReceiveWord_WaitInputCallback (object state0)
		{
			StartReceiveWord_ShowIncorrectResult();
		}

		private void StartReceiveWord_ShowCorrectResult()
		{
			SetState (MainFormState.ReceiveWord_ShowCorrectResult);

			sessionCorrectCount += Math.Min (selectedWord.Length, config.SessionLength - sessionCount);;
			sessionCount += Math.Min (selectedWord.Length, config.SessionLength - sessionCount);;
			sessionCompleted = (sessionCount >= config.SessionLength);
			sessionPanel.Refresh();
			
			morseLabel.Text = "";
			flashLabel.Text = "";

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Green;
			resultLabel.Text = "correct";

			foreach (char c in selectedWord.ToCharArray())
			{
				LearnCharacter lc = learnState.GetByCharacter (c.ToString());
				if (lc != null)
					learnState.Grade (lc, true, false);
			}
			learnState.GradeWord (true);
			learnStatePanel.Refresh();

			ResetEventGenerators();

			wavePlayer.Play (new WaitCallback (StartReceiveWord_ShowCorrectResult_PlayFinishWord), wordFinishSound);

			SelectWord ();
		}

		private void StartReceiveWord_ShowCorrectResult_PlayFinishWord(object state)
		{
			sleeper.Sleep (
				new WaitCallback (StartReceiveWord_ShowCorrectResult_Callback), 
				config.ShowCorrectWordResultDelay);
		}

		private void StartReceiveWord_ShowCorrectResult_Callback (object state0)
		{
			if (sessionCompleted)
				StartReceiveWord_EndSession();
			else
				StartReceiveWord_PlayMorse();
		}


		private void StartReceiveWord_ShowIncorrectResult (string incorrectCharacter)
		{
			sessionCorrectCount += Math.Min (wordInput.Length, config.SessionLength - sessionCount);;
			sessionCount += Math.Min (selectedWord.Length, config.SessionLength - sessionCount);;

			sessionCompleted = (sessionCount >= config.SessionLength);
			sessionPanel.Refresh();

			SetState (MainFormState.ReceiveWord_ShowIncorrectResult);

			string correctCharacter = selectedWord.Substring (wordInput.Length, 1);
			MorseConstant mc = MorseConstant.GetByCharacter (correctCharacter);
			if (mc != null)
				morseLabel.Text = mc.Morse;
			flashLabel.Text = MorseConstant.ConvertToDisplay (correctCharacter).ToUpper();

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Maroon;
			resultLabel.Text = "incorrect";

			messageLabel.Text = selectedWord;

			LearnCharacter lc;
			if (incorrectCharacter != null)
			{
				lc = learnState.GetByCharacter (incorrectCharacter);
				if (lc != null)
					learnState.Grade (lc, false, false);
			}
			lc = learnState.GetByCharacter (correctCharacter);
			if (lc != null)
				learnState.Grade (lc, false, false);
			learnState.GradeWord (false);
			learnStatePanel.Refresh();

			ResetEventGenerators();

			WaveBuffer buffer = (WaveBuffer) sounds [correctCharacter.ToLower()];
			if (buffer != null)
				wavePlayer.Play (new WaitCallback (StartReceiveWord_ShowIncorrectResult_PlayingLetterCallback), buffer);
			else
				sleeper.Sleep (new WaitCallback (StartReceiveWord_ShowIncorrectResult_Callback), config.ShowIncorrectResultDelay);

			SelectWord();
		}


		private void StartReceiveWord_ShowIncorrectResult ()
		{
			StartReceiveWord_ShowIncorrectResult (null);
		}


		private void StartReceiveWord_ShowIncorrectResult_PlayingLetterCallback(object state0)
		{
			sleeper.Sleep (new WaitCallback (StartReceiveWord_ShowIncorrectResult_Callback), config.ShowIncorrectResultDelay);
		}
		
	
		private void StartReceiveWord_ShowIncorrectResult_Callback (object state0)
		{
			if (sessionCompleted)
				StartReceiveWord_EndSession();
			else
				StartReceiveWord_PlayMorse ();
		}


		private void StartReceiveWord_EndSession()
		{
			SetState (MainFormState.ReceiveWord_EndSession);

			resultLabel.ForeColor = Color.White;
			resultLabel.BackColor = Color.Blue;
			resultLabel.Text = "session completed";

			ResetEventGenerators();
		}


		private void StartOptions_Show()
		{
			try
			{
				SetState (MainFormState.Options_Show);

				morseKey.Close();

				modeLabel.Text = "Options";
				modeImage.Image = imageList.Images [3];

				ResetEventGenerators();

				OptionsForm optionsForm = new OptionsForm();
				optionsForm.Config = config;
				optionsForm.MorsePlayer = morsePlayer;
				if (optionsForm.ShowDialog() == DialogResult.OK)
				{
					SetConfigSettings();
					config.SaveToFile();
				}
			}
			finally
			{
				morseKey.Open();
			}

			StartMessage_Transmit();
		}


		private const int LearnStateCharacterWidth = 16;
		
		private void learnStatePanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawRectangle (borderPen, 0, 0, learnStatePanel.ClientSize.Width - 1, learnStatePanel.ClientSize.Height - 1);

			for (int i = 0; i < learnState.LearnOrder.Length; i++)
			{
				string c = learnState.LearnOrder [i].ToString();

				LearnCharacter lc = learnState.GetByCharacter (c);

				double error; 
				Brush brush;
				if (lc != null)
				{
					error = lc.Error;
					if (lc.Error > 0.5)
						brush = Brushes.Blue;
					else
						brush = Brushes.Green;
				}
				else
				{
					error = 1.0;
					brush = Brushes.Silver;
				}
				
				e.Graphics.DrawString (
					c, 
					learnStateFont, 
					Brushes.Black, 
					new Point (i * LearnStateCharacterWidth, 54));

				int height = (int) (50 * error);
				e.Graphics.FillRectangle (brush, i * LearnStateCharacterWidth + 5, 52 - height, 10, height);
			}
		}

		private void learnStatePanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ((e.Button != MouseButtons.Left) && (e.Button != MouseButtons.Right))
				return;

			if ((state != MainFormState.Message_Transmit)
				&& (state != MainFormState.Message_TransmitBuffer)
				&& (state != MainFormState.Message_Play)
				&& (state != MainFormState.Message_EndPlay)
				&& (state != MainFormState.Receive_EndSession)
				&& (state != MainFormState.Transmit_EndSession)
				&& (state != MainFormState.ReceiveWord_EndSession))
				return;

			if (e.Button == MouseButtons.Left) 
			{
				int index = (e.X - 1) / LearnStateCharacterWidth;
				if ((index < 0) || (index >= learnState.LearnOrder.Length))
					return;

				if ((Control.ModifierKeys & Keys.Shift) != 0)
				{
					for (int i = 0; i < learnState.LearnOrder.Length; i++)
					{
						string character = learnState.LearnOrder[i].ToString();
						LearnCharacter lc = learnState.GetByCharacter (character);

						if ((i <= index) && (lc == null))
							learnState.AddCharacter (character);
						else if ((i > index) && (lc != null))
							learnState.RemoveCharacter (lc);
					}
				}
				else if ((Control.ModifierKeys & Keys.Shift) == 0)
				{
					string character = learnState.LearnOrder[index].ToString();
					LearnCharacter lc = learnState.GetByCharacter (character);
					if (lc == null)
						learnState.AddCharacter (character);
					else 
						learnState.RemoveCharacter (lc);
				}

				learnStatePanel.Refresh();
			}
			else if (e.Button == MouseButtons.Right) 
			{
				if ((state == MainFormState.Message_Transmit)
				|| (state == MainFormState.Message_TransmitBuffer)
				|| (state == MainFormState.Message_Play)
				|| (state == MainFormState.Message_EndPlay)
				|| (state == MainFormState.Receive_EndSession)
				|| (state == MainFormState.Transmit_EndSession)
				|| (state == MainFormState.ReceiveWord_EndSession))
					learnStateContextMenu.Show (learnStatePanel, new Point (e.X, e.Y));
			}
		}

		private void sessionPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			int clientWidth = sessionPanel.ClientSize.Width - 3;
			int clientHeight = sessionPanel.ClientSize.Height - 2;

			int correctWidth = (int) Math.Round (sessionCorrectCount * clientWidth / (double) config.SessionLength);
			int errorWidth = (int) Math.Round ((sessionCount - sessionCorrectCount) * clientWidth / (double) config.SessionLength);
			int unfinishedWidth = clientWidth - correctWidth - errorWidth;

			int pos = 1;
			
			e.Graphics.DrawRectangle (borderPen, 0, 0, sessionPanel.ClientSize.Width - 1, sessionPanel.ClientSize.Height - 1);

			if (correctWidth > 0)
			{
				e.Graphics.FillRectangle (Brushes.Green, pos, 1, pos + correctWidth, clientHeight);
				pos += correctWidth;
			}
			if (errorWidth > 0)
			{
				e.Graphics.FillRectangle (Brushes.Maroon, pos, 1, pos + errorWidth, clientHeight);
				pos += errorWidth;
			}
			if (unfinishedWidth > 0)
				e.Graphics.FillRectangle (Brushes.DarkGray, pos, 1, pos + unfinishedWidth, clientHeight);
			
			string s = 
				sessionCorrectCount.ToString().PadLeft (3)
				+ " / " +
				sessionCount.ToString().PadLeft (3) + "   ";

			int currectCorrectPercent = 0;
			if (sessionCount == 0)
				s += "    ";
			else
			{
				currectCorrectPercent = ((int) Math.Floor (((double) sessionCorrectCount) / ((double) sessionCount) * 100));
				s += currectCorrectPercent.ToString().PadLeft (3) + "%";
			}

			SizeF size = e.Graphics.MeasureString (s, learnStateFont);

			e.Graphics.DrawString (
				s, 
				learnStateFont, 
				Brushes.White, 
				new Point (
				  clientWidth / 2 - (int) size.Width / 2,
                  clientHeight / 2 - (int)size.Height / 2 + 2));
		}

		private void MainForm_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			string c = e.KeyChar.ToString().ToLower();

			if (((state == MainFormState.Message_Transmit) || (state == MainFormState.Message_TransmitBuffer))
				&& ((Control.ModifierKeys == Keys.None) || (Control.ModifierKeys == Keys.Shift))
				&& ((MorseConstant.GetByCharacter (c) != null) || (c == " ")))
			{
				CheckAutoClear();
				AddMessageChar (c);

				morseDoc.Wpm = config.Wpm;
				morseDoc.OverallWpm = config.OverallWpm;
				if (morseDoc.Count == 0)
					morseDoc.StartTime = DateTime.Now;
				morseDoc.AddText (c);
				morseDoc.AddInterChar();
				
				if (state == MainFormState.Message_Transmit)
					StartMessage_TransmitBuffer (c);
				else
					transmitBuffer += c;
				e.Handled = true;
			}
			else if ((state == MainFormState.Receive_WaitInput) 
			 && (Control.ModifierKeys == Keys.None))
			{
				if (learnState.LearnOrder.IndexOf (c) != -1)
				{
					if (c == selectedCharacter.Character)
						StartReceive_ShowCorrectResult();
					else
						StartReceive_ShowIncorrectResult(c);

					e.Handled = true;
				}
			}
			else if (((state == MainFormState.ReceiveWord_PlayMorse) 
				|| (state == MainFormState.ReceiveWord_WaitInput)) 
			 && (Control.ModifierKeys == Keys.None))
			{
				if (learnState.LearnOrder.IndexOf (c) != -1)
				{
					if ((wordInput + c).ToLower() != selectedWord.Substring (0, wordInput.Length + 1).ToLower())
						StartReceiveWord_ShowIncorrectResult(c);
					else
					{
						wordInput += c;
						messageLabel.Text = wordInput.ToUpper();
						if (wordInput.Length >= selectedWord.Length)
							StartReceiveWord_ShowCorrectResult();
					}

					e.Handled = true;
				}
			}

		}

		private void ClipboardPaste()
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject == null)
				return;

			object data = dataObject.GetData (DataFormats.Text);
			if (data == null)
				return;

			string text = data.ToString();
			if ((text == null) || (text.Trim().Length == 0))
				return;

			StartMessage_Play (text);
		}

		private void MainForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (((e.Modifiers & Keys.Control) == Keys.Control) && (e.KeyCode == Keys.R) 
			 && ((state == MainFormState.Message_Transmit) 
			 || (state == MainFormState.Message_TransmitBuffer)
				|| (state == MainFormState.Message_Play)
				|| (state == MainFormState.Message_EndPlay)))
			{
				Clipboard.SetDataObject (morseDoc.AsParseReport);
				e.Handled = true;
			}
			else if (((e.Modifiers & Keys.Control) == Keys.Control) && (e.KeyCode == Keys.X) 
				&& ((state == MainFormState.Message_Transmit) 
				|| (state == MainFormState.Message_TransmitBuffer)
				|| (state == MainFormState.Message_Play)
				|| (state == MainFormState.Message_EndPlay)))
			{
				Clipboard.SetDataObject (morseDoc.Text);
				e.Handled = true;
			}
			else if ((e.Modifiers == Keys.None) && (e.KeyCode == Keys.Escape))
			{
				StartMessage_Transmit();
				e.Handled = true;
			}
			else if ((e.Modifiers == Keys.None) && (e.KeyCode == Keys.Space) && (!sessionCompleted)
				&& ((state == MainFormState.Receive_BeginSession)
					|| (state == MainFormState.Receive_PlayMorse)
					|| (state == MainFormState.Receive_WaitInput)
					|| (state == MainFormState.Receive_ShowCorrectResult)
					|| (state == MainFormState.Receive_ShowIncorrectResult)))
			{
				StartReceive_Paused();
				e.Handled = true;
			}
			else if ((e.Modifiers == Keys.None) && (e.KeyCode == Keys.Space) 
				&& (state == MainFormState.Receive_Paused))
			{
				StartReceive_PlayMorse();
				e.Handled = true;
			}
			else if ((e.Modifiers == Keys.None) && (e.KeyCode == Keys.Space) && (!sessionCompleted)
				&& ((state == MainFormState.Transmit_BeginSession)
				|| (state == MainFormState.Transmit_PlayMorse)
				|| (state == MainFormState.Transmit_WaitInput)
				|| (state == MainFormState.Transmit_ShowCorrectResult)
				|| (state == MainFormState.Transmit_ShowIncorrectResult)))
			{
				StartTransmit_Paused();
				e.Handled = true;
			}
			else if ((e.Modifiers == Keys.None) && (e.KeyCode == Keys.Space) 
				&& (state == MainFormState.Transmit_Paused))
			{
				StartTransmit_PlayMorse();
				e.Handled = true;
			}
			else if ((e.Modifiers == Keys.None) && (e.KeyCode == Keys.Space) && (!sessionCompleted)
				&& ((state == MainFormState.ReceiveWord_BeginSession)
				|| (state == MainFormState.ReceiveWord_PlayMorse)
				|| (state == MainFormState.ReceiveWord_WaitInput)
				|| (state == MainFormState.ReceiveWord_ShowCorrectResult)
				|| (state == MainFormState.ReceiveWord_ShowIncorrectResult)))
			{
				StartReceiveWord_Paused();
				e.Handled = true;
			}
			else if ((e.Modifiers == Keys.None) && (e.KeyCode == Keys.Space) 
				&& (state == MainFormState.ReceiveWord_Paused))
			{
				StartReceiveWord_PlayMorse();
				e.Handled = true;
			}
		}

		
		private void MainForm_MorseKeyDown(object sender)
		{
			if ((state != MainFormState.Message_Transmit) 
				&& (state != MainFormState.Transmit_WaitInput))
				return;

			if ((state == MainFormState.Message_Transmit) || (state == MainFormState.Message_TransmitBuffer))
				CheckAutoClear();

			flashLabel.BackColor = Color.Green;

			morseDocStateRecorder.AddState (true);
			morseDocSleeper.Close (false);
			MorseParse (true);
		}

		private void MainForm_MorseKeyUp(object sender)
		{
			if ((state != MainFormState.Message_Transmit) 
				&& (state != MainFormState.Transmit_WaitInput))
				return;
				
			morseDocSleeper.Close (false);
			morseDocStateRecorder.AddState (false);
			flashLabel.BackColor = Color.White;

			MorseParse (true);
			morseDocSleeper.Sleep (new WaitCallback (MorseParse_Callback), MorseConstant.InterWordLength (config.Wpm));
		}


		private void MorseParse_Callback (object state0)
		{
			morseDocStateRecorder.AddState (false);
			MorseParse (true);
		}


		private void CheckAutoClear()
		{
			if (!morseDoc.IsEmpty && ((DateTime.Now - morseDoc.EndTime).TotalMilliseconds > config.AutoClearDelay))
			{
				morseDoc.Clear();
				morseDocParser.Clear();
				messageLabel.Text = "";
			}
		}

		
		private void AddMessageChar (string character)
		{
			string text = messageLabel.Text;
			text += character;

			using (Graphics g = Graphics.FromHwnd (this.Handle))
			{
				SizeF size = g.MeasureString (text, learnStateFont);
				while (size.Width >= messageLabel.Width - 10)
				{
					text = text.Substring (1, text.Length - 1);
					size = g.MeasureString (text, learnStateFont);
				}
			}

			messageLabel.Text = text;
		}
		
		// function to parse the morse
		// returns true if the state has changed as a result
		private bool MorseParse (bool live)
		{
			MorseDocumentParser.ParseResult parseResult = morseDocParser.Parse (config.Wpm, live);
			while ((parseResult != null) && parseResult.Complete)
			{
				if (state == MainFormState.Message_Transmit) 
				{
                    if (config.ShowMorse)
					    morseLabel.Text = parseResult.Morse;

					if (parseResult.Character != null)
					{
						AddMessageChar (parseResult.Character);
						if (parseResult.Character.Trim().Length > 0)
						{
							flashLabel.Text = MorseConstant.ConvertToDisplay (parseResult.Character).ToUpper();
						}

						if (config.SendKeys && !this.Focused)
						{
							if (parseResult.Character.Trim().Length > 0)
								System.Windows.Forms.SendKeys.Send (MorseConstant.ConvertToDisplay (parseResult.Character));
							else
								System.Windows.Forms.SendKeys.Send (" ");
						}
					}
					else
						AddMessageChar ("?");
				}
				else if (state == MainFormState.Transmit_WaitInput)
				{
                    if (config.ShowMorse)
					    morseLabel.Text = parseResult.Morse;

					if (parseResult.IsWhitespace)
					{
					// ignore
					}
					else if (parseResult.Character != null)
					{
						if (parseResult.Character == selectedCharacter.Character)
						{
							StartTransmit_ShowCorrectResult();
							return true;
						}
						else
						{
							StartTransmit_ShowIncorrectResult(selectedCharacter.Character);
							return true;
						}
					}
					else
						StartTransmit_ShowIncorrectResult ("?");
				}

				parseResult = morseDocParser.Parse (config.Wpm, live);
			}
			
			if (parseResult != null) 
			{
                if (config.ShowMorse)
				    morseLabel.Text = parseResult.Morse;
			}

			return false;
		}


		private void MainForm_HookKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (!this.Focused && ((e.KeyData & Keys.C) == Keys.C) && (e.Modifiers == Keys.Control))
			{
				if ((DateTime.Now - lastControlC).TotalMilliseconds < MorseLearnerConfig.ControlCInterval)
				{
					ClipboardPaste();
					e.Handled = true;
				}
				lastControlC = DateTime.Now;
			}

		}


		private void exitMenuItem_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void messageMenuItem_Click(object sender, System.EventArgs e)
		{
			StartMessage_Transmit();
		}

		private void receiveCharactersMenuItem_Click(object sender, System.EventArgs e)
		{
			StartReceive_BeginSession();
		}

		private void sendCharactersMenuItem_Click(object sender, System.EventArgs e)
		{
			StartTransmit_BeginSession();
		}

		private void receiveWordsMenuItem_Click(object sender, System.EventArgs e)
		{
			StartReceiveWord_BeginSession();
		}

		private void optionsMenuItem_Click(object sender, System.EventArgs e)
		{
			StartOptions_Show();
		}

		private void copyMenuItem_Click(object sender, System.EventArgs e)
		{
			if ((state == MainFormState.Message_Transmit)
			 || (state == MainFormState.Message_TransmitBuffer)
			 || (state == MainFormState.Message_Play)
			 || (state == MainFormState.Message_EndPlay))
			{
				Clipboard.SetDataObject (morseDoc.AsMorseMail);
			}
		
		}

		private void pasteMenuItem_Click(object sender, System.EventArgs e)
		{
			ClipboardPaste();
		}

		private void addAllContextMenuItem_Click(object sender, System.EventArgs e)
		{
			learnState.AddAll();
			learnStatePanel.Refresh();
		}

		private void removeAllContextMenuItem_Click(object sender, System.EventArgs e)
		{
            learnState.RemoveAll();		
			learnStatePanel.Refresh();
		}

		private void addAlphabetContextMenuItem_Click(object sender, System.EventArgs e)
		{
			learnState.AddAlphabetic();
			learnStatePanel.Refresh();
		}

		private void removeAlphabetContextMenuItem_Click(object sender, System.EventArgs e)
		{
			learnState.RemoveAlphabetic();
			learnStatePanel.Refresh();
		}

		private void addNumericContextMenuItem_Click(object sender, System.EventArgs e)
		{
			learnState.AddNumeric();
			learnStatePanel.Refresh();
		}

		private void removeNumericContextMenuItem_Click(object sender, System.EventArgs e)
		{
			learnState.RemoveNumeric();
			learnStatePanel.Refresh();
		}

		private void addPunctuationContextMenuItem_Click(object sender, System.EventArgs e)
		{
			learnState.AddPunctuation();
			learnStatePanel.Refresh();
		}

		private void removePunctuationContextMenuItem_Click(object sender, System.EventArgs e)
		{
			learnState.RemovePunctuation();
			learnStatePanel.Refresh();
		}

		private void beginnerContextMenuItem_Click(object sender, System.EventArgs e)
		{
			learnState.SetBeginner();		
			learnStatePanel.Refresh();
		}

		private void intermediateContextMenuItem_Click(object sender, System.EventArgs e)
		{
			learnState.SetIntermediate();		
			learnStatePanel.Refresh();
		}

		private void advancedContextMenuItem_Click(object sender, System.EventArgs e)
		{
			learnState.SetAdvanced();		
			learnStatePanel.Refresh();
		}

		private void contentsMenuItem_Click(object sender, System.EventArgs e)
		{
			StartMessage_Transmit();
			System.Diagnostics.Process.Start("Help.mht");
		}

		private void aboutMenuItem_Click(object sender, System.EventArgs e)
		{
			StartMessage_Transmit();
			AboutForm aboutForm = new AboutForm();
			aboutForm.ShowDialog();
		}

	}
}
