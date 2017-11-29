namespace app
{
	partial class FormMain
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuTabRename = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuDeactiveOpacity = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDeactive20 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDeactive40 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDeactive60 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDeactive80 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDeactive100 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuStayOnTop = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFontChange = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFnBarPos = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFunctionBarTop = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFunctionBarBottom = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuScrollBarV = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuScrollBarH = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuExportText = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuVersion = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.tabPage7 = new System.Windows.Forms.TabPage();
			this.tabPage8 = new System.Windows.Forms.TabPage();
			this.tabPage9 = new System.Windows.Forms.TabPage();
			this.tabPage10 = new System.Windows.Forms.TabPage();
			this.tabPage11 = new System.Windows.Forms.TabPage();
			this.tabPage12 = new System.Windows.Forms.TabPage();
			this.txtTabRename = new System.Windows.Forms.TextBox();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage6);
			this.tabControl1.Controls.Add(this.tabPage7);
			this.tabControl1.Controls.Add(this.tabPage8);
			this.tabControl1.Controls.Add(this.tabPage9);
			this.tabControl1.Controls.Add(this.tabPage10);
			this.tabControl1.Controls.Add(this.tabPage11);
			this.tabControl1.Controls.Add(this.tabPage12);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
			this.tabControl1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.Padding = new System.Drawing.Point(1, 3);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(410, 19);
			this.tabControl1.TabIndex = 1;
			this.tabControl1.TabStop = false;
			this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
			this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
			this.tabControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDoubleClick);
			this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTabRename,
            this.toolStripSeparator2,
            this.mnuDeactiveOpacity,
            this.mnuStayOnTop,
            this.toolStripSeparator5,
            this.mnuFontChange,
            this.mnuFnBarPos,
            this.mnuScrollBarV,
            this.mnuScrollBarH,
            this.mnuExportText,
            this.toolStripSeparator4,
            this.mnuVersion,
            this.toolStripSeparator3,
            this.mnuExit});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(254, 248);
			// 
			// mnuTabRename
			// 
			this.mnuTabRename.Name = "mnuTabRename";
			this.mnuTabRename.Size = new System.Drawing.Size(253, 22);
			this.mnuTabRename.Text = "タブの名前を変更(&R)";
			this.mnuTabRename.Click += new System.EventHandler(this.mnuTabRename_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(250, 6);
			// 
			// mnuDeactiveOpacity
			// 
			this.mnuDeactiveOpacity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDeactive20,
            this.mnuDeactive40,
            this.mnuDeactive60,
            this.mnuDeactive80,
            this.mnuDeactive100});
			this.mnuDeactiveOpacity.Name = "mnuDeactiveOpacity";
			this.mnuDeactiveOpacity.Size = new System.Drawing.Size(253, 22);
			this.mnuDeactiveOpacity.Text = "非アクティブ時の不透明度";
			// 
			// mnuDeactive20
			// 
			this.mnuDeactive20.Name = "mnuDeactive20";
			this.mnuDeactive20.Size = new System.Drawing.Size(111, 22);
			this.mnuDeactive20.Text = "20%";
			this.mnuDeactive20.Click += new System.EventHandler(this.mnuDeactive20_Click);
			// 
			// mnuDeactive40
			// 
			this.mnuDeactive40.Name = "mnuDeactive40";
			this.mnuDeactive40.Size = new System.Drawing.Size(111, 22);
			this.mnuDeactive40.Text = "40%";
			this.mnuDeactive40.Click += new System.EventHandler(this.mnuDeactive40_Click);
			// 
			// mnuDeactive60
			// 
			this.mnuDeactive60.Name = "mnuDeactive60";
			this.mnuDeactive60.Size = new System.Drawing.Size(111, 22);
			this.mnuDeactive60.Text = "60%";
			this.mnuDeactive60.Click += new System.EventHandler(this.mnuDeactive60_Click);
			// 
			// mnuDeactive80
			// 
			this.mnuDeactive80.Name = "mnuDeactive80";
			this.mnuDeactive80.Size = new System.Drawing.Size(111, 22);
			this.mnuDeactive80.Text = "80%";
			this.mnuDeactive80.Click += new System.EventHandler(this.mnuDeactive80_Click);
			// 
			// mnuDeactive100
			// 
			this.mnuDeactive100.Checked = true;
			this.mnuDeactive100.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mnuDeactive100.Name = "mnuDeactive100";
			this.mnuDeactive100.Size = new System.Drawing.Size(111, 22);
			this.mnuDeactive100.Text = "100%";
			this.mnuDeactive100.Click += new System.EventHandler(this.mnuDeactive100_Click);
			// 
			// mnuStayOnTop
			// 
			this.mnuStayOnTop.CheckOnClick = true;
			this.mnuStayOnTop.Name = "mnuStayOnTop";
			this.mnuStayOnTop.Size = new System.Drawing.Size(253, 22);
			this.mnuStayOnTop.Text = "常に手前に表示(&T)";
			this.mnuStayOnTop.Click += new System.EventHandler(this.mnuStayOnTop_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(250, 6);
			// 
			// mnuFontChange
			// 
			this.mnuFontChange.Name = "mnuFontChange";
			this.mnuFontChange.Size = new System.Drawing.Size(253, 22);
			this.mnuFontChange.Text = "フォントの変更(&F)";
			this.mnuFontChange.Click += new System.EventHandler(this.mnuFontChange_Click);
			// 
			// mnuFnBarPos
			// 
			this.mnuFnBarPos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFunctionBarTop,
            this.mnuFunctionBarBottom});
			this.mnuFnBarPos.Name = "mnuFnBarPos";
			this.mnuFnBarPos.Size = new System.Drawing.Size(253, 22);
			this.mnuFnBarPos.Text = "ファンクションバーの位置";
			// 
			// mnuFunctionBarTop
			// 
			this.mnuFunctionBarTop.Checked = true;
			this.mnuFunctionBarTop.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mnuFunctionBarTop.Name = "mnuFunctionBarTop";
			this.mnuFunctionBarTop.Size = new System.Drawing.Size(108, 22);
			this.mnuFunctionBarTop.Text = "上(&U)";
			this.mnuFunctionBarTop.Click += new System.EventHandler(this.mnuFunctionBarTop_Click);
			// 
			// mnuFunctionBarBottom
			// 
			this.mnuFunctionBarBottom.Name = "mnuFunctionBarBottom";
			this.mnuFunctionBarBottom.Size = new System.Drawing.Size(108, 22);
			this.mnuFunctionBarBottom.Text = "下(&D)";
			this.mnuFunctionBarBottom.Click += new System.EventHandler(this.mnuFunctionBarBottom_Click);
			// 
			// mnuScrollBarV
			// 
			this.mnuScrollBarV.Name = "mnuScrollBarV";
			this.mnuScrollBarV.Size = new System.Drawing.Size(253, 22);
			this.mnuScrollBarV.Text = "縦スクロールバー(&V)";
			this.mnuScrollBarV.Click += new System.EventHandler(this.mnuScrollBarV_Click);
			// 
			// mnuScrollBarH
			// 
			this.mnuScrollBarH.Name = "mnuScrollBarH";
			this.mnuScrollBarH.Size = new System.Drawing.Size(253, 22);
			this.mnuScrollBarH.Text = "横スクロールバー(&H)";
			this.mnuScrollBarH.Click += new System.EventHandler(this.mnuScrollBarH_Click);
			// 
			// mnuExportText
			// 
			this.mnuExportText.Name = "mnuExportText";
			this.mnuExportText.Size = new System.Drawing.Size(253, 22);
			this.mnuExportText.Text = "メモをテキストファイルにエクスポート(&E)";
			this.mnuExportText.Click += new System.EventHandler(this.mnuExportText_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(250, 6);
			// 
			// mnuVersion
			// 
			this.mnuVersion.Name = "mnuVersion";
			this.mnuVersion.Size = new System.Drawing.Size(253, 22);
			this.mnuVersion.Text = "Version(&A)";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(250, 6);
			// 
			// mnuExit
			// 
			this.mnuExit.Name = "mnuExit";
			this.mnuExit.Size = new System.Drawing.Size(253, 22);
			this.mnuExit.Text = "終了(&X)";
			this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 23);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(402, 0);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "F1";
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 23);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(402, 0);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "F2";
			// 
			// tabPage3
			// 
			this.tabPage3.Location = new System.Drawing.Point(4, 23);
			this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(402, 0);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "F3";
			// 
			// tabPage4
			// 
			this.tabPage4.Location = new System.Drawing.Point(4, 23);
			this.tabPage4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(402, 0);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "F4";
			// 
			// tabPage5
			// 
			this.tabPage5.Location = new System.Drawing.Point(4, 23);
			this.tabPage5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new System.Drawing.Size(402, 0);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "F5";
			// 
			// tabPage6
			// 
			this.tabPage6.Location = new System.Drawing.Point(4, 23);
			this.tabPage6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Size = new System.Drawing.Size(402, 0);
			this.tabPage6.TabIndex = 5;
			this.tabPage6.Text = "F6";
			// 
			// tabPage7
			// 
			this.tabPage7.Location = new System.Drawing.Point(4, 23);
			this.tabPage7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage7.Name = "tabPage7";
			this.tabPage7.Size = new System.Drawing.Size(402, 0);
			this.tabPage7.TabIndex = 6;
			this.tabPage7.Text = "F7";
			// 
			// tabPage8
			// 
			this.tabPage8.Location = new System.Drawing.Point(4, 23);
			this.tabPage8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage8.Name = "tabPage8";
			this.tabPage8.Size = new System.Drawing.Size(402, 0);
			this.tabPage8.TabIndex = 7;
			this.tabPage8.Text = "F8";
			// 
			// tabPage9
			// 
			this.tabPage9.Location = new System.Drawing.Point(4, 23);
			this.tabPage9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage9.Name = "tabPage9";
			this.tabPage9.Size = new System.Drawing.Size(402, 0);
			this.tabPage9.TabIndex = 8;
			this.tabPage9.Text = "F9";
			// 
			// tabPage10
			// 
			this.tabPage10.Location = new System.Drawing.Point(4, 23);
			this.tabPage10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage10.Name = "tabPage10";
			this.tabPage10.Size = new System.Drawing.Size(402, 0);
			this.tabPage10.TabIndex = 9;
			this.tabPage10.Text = "F10";
			// 
			// tabPage11
			// 
			this.tabPage11.Location = new System.Drawing.Point(4, 23);
			this.tabPage11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage11.Name = "tabPage11";
			this.tabPage11.Size = new System.Drawing.Size(402, 0);
			this.tabPage11.TabIndex = 10;
			this.tabPage11.Text = "F11";
			// 
			// tabPage12
			// 
			this.tabPage12.Location = new System.Drawing.Point(4, 23);
			this.tabPage12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage12.Name = "tabPage12";
			this.tabPage12.Size = new System.Drawing.Size(402, 0);
			this.tabPage12.TabIndex = 11;
			this.tabPage12.Text = "F12";
			// 
			// txtTabRename
			// 
			this.txtTabRename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtTabRename.Location = new System.Drawing.Point(32, 24);
			this.txtTabRename.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtTabRename.Name = "txtTabRename";
			this.txtTabRename.Size = new System.Drawing.Size(55, 19);
			this.txtTabRename.TabIndex = 2;
			this.txtTabRename.Visible = false;
			this.txtTabRename.VisibleChanged += new System.EventHandler(this.txtTabRename_VisibleChanged);
			this.txtTabRename.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTabRename_KeyDown);
			this.txtTabRename.Leave += new System.EventHandler(this.txtTabRename_Leave);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "Memoru";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
			// 
			// textBox1
			// 
			this.textBox1.AcceptsTab = true;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Location = new System.Drawing.Point(0, 19);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(410, 176);
			this.textBox1.TabIndex = 8;
			this.textBox1.TabStop = false;
			this.textBox1.WordWrap = false;
			this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
			this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
			this.textBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseUp);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(410, 195);
			this.ControlBox = false;
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.txtTabRename);
			this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FormMain";
			this.ShowInTaskbar = false;
			this.Activated += new System.EventHandler(this.FormMain_Activated);
			this.Deactivate += new System.EventHandler(this.FormMain_Deactivate);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormMain_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseMove);
			this.tabControl1.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.TabPage tabPage7;
		private System.Windows.Forms.TabPage tabPage8;
		private System.Windows.Forms.TabPage tabPage9;
		private System.Windows.Forms.TabPage tabPage10;
		private System.Windows.Forms.TabPage tabPage11;
		private System.Windows.Forms.TabPage tabPage12;
		private System.Windows.Forms.TextBox txtTabRename;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mnuFnBarPos;
		private System.Windows.Forms.ToolStripMenuItem mnuFunctionBarTop;
		private System.Windows.Forms.ToolStripMenuItem mnuFunctionBarBottom;
		private System.Windows.Forms.ToolStripMenuItem mnuFontChange;
		private System.Windows.Forms.ToolStripMenuItem mnuScrollBarV;
		private System.Windows.Forms.ToolStripMenuItem mnuScrollBarH;
		private System.Windows.Forms.ToolStripMenuItem mnuStayOnTop;
		private System.Windows.Forms.ToolStripMenuItem mnuExit;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ToolStripMenuItem mnuVersion;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem mnuTabRename;
		private System.Windows.Forms.ToolStripMenuItem mnuDeactiveOpacity;
		private System.Windows.Forms.ToolStripMenuItem mnuDeactive20;
		private System.Windows.Forms.ToolStripMenuItem mnuDeactive40;
		private System.Windows.Forms.ToolStripMenuItem mnuDeactive60;
		private System.Windows.Forms.ToolStripMenuItem mnuDeactive80;
		private System.Windows.Forms.ToolStripMenuItem mnuDeactive100;
		private System.Windows.Forms.ToolStripMenuItem mnuExportText;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.TextBox textBox1;
	}
}

